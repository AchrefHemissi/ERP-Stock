﻿using backend.Services.ServicesContract;
using StockManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using StockManagement.Models;

namespace backend.Services.ConcreteServices;

public class ConfirmOrderService : IConfirmOrderService
{

    private readonly IOrderRepository _orderRepository;
    private readonly IProductBlockRepository _productBlockRepository;
    private readonly IStockMovementRepository _stockMovementRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IProductItemRepository _productItemRepository;
    private readonly IStockMovementItemsRepository _stockMovementItemRepository;
    private readonly IProductRepository _productRepository;
    
    public ConfirmOrderService(IOrderRepository orderRepository, IProductBlockRepository productBlockRepository, IStockMovementRepository stockMovementRepository, ILocationRepository locationRepository, IProductItemRepository productItemRepository, IStockMovementItemsRepository stockMovementItemRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _productBlockRepository = productBlockRepository;
        _stockMovementRepository = stockMovementRepository;
        _locationRepository = locationRepository;
        _productItemRepository = productItemRepository;
        _stockMovementItemRepository = stockMovementItemRepository;
        _productRepository = productRepository;
        
    }
    
    
    public async Task ConfirmBuyOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync("OrderId", orderId,
            q => q
                    .Include(o => o.OrderProducts)
                        .ThenInclude(op => op.Product)
                    .Include(o => o.StockMovements)
                        .ThenInclude(s => s.DestinationProductBlock)
                    .Include(o => o.StockMovements)
                        .ThenInclude(s => s.StockMovementItems)
                            .ThenInclude(si => si.ProductItem));
         
        
        if (order == null)
        {
            throw new ArgumentException($"Order with ID {orderId} not found");
        }
        
        if (order.Status != OrderStatus.Processing)
        {
            throw new ArgumentException($"Order with ID {orderId} is already confirmed");
        }
        
        var transaction = await _stockMovementRepository.BeginTransactionAsync();
        try{
            
            order.Status = OrderStatus.Received;
            await _orderRepository.UpdateAsync(order);
            
            
            foreach (var stockMovement in order.StockMovements)
                {
                    foreach (var stockMovementItem in stockMovement.StockMovementItems)
                    {
                        if (stockMovementItem.ProductItem.Status == ProductItemStatus.PreccessBuy)
                        {
                            stockMovementItem.ProductItem.Status = ProductItemStatus.InStock;
                            await _productItemRepository.UpdateAsync(stockMovementItem.ProductItem);
                        }
                        else
                        {
                            throw new ArgumentException($"Product Item with ID {stockMovementItem.ProductItem.ProductItemId} is not in PreccessBuy status");
                        }
                    }
                    stockMovement.DestinationProductBlock.Status = ProductBlockStatus.InStock;
                    
                }

            foreach (var orderProduct in order.OrderProducts)
            {   
                var product = orderProduct.Product;
                product.StockQuantity += orderProduct.Quantity;
                await _productRepository.UpdateAsync(product);
                
            }
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception(e.Message);
        }
    }
    
    
    
    
    
    
    public async Task ConfirmSaleOrderAsync(int orderId)
    {
        var order = await _orderRepository.GetByIdAsync("OrderId", orderId,
            q => q
                    .Include(o => o.OrderProducts)
                        .ThenInclude(op => op.Product)
                    .Include(o => o.StockMovements)
                        .ThenInclude(s => s.SourceProductBlock)
                    .Include(o => o.StockMovements)
                        .ThenInclude(s => s.StockMovementItems)
                            .ThenInclude(si => si.ProductItem));
         
        
        if (order == null)
        {
            throw new ArgumentException($"Order with ID {orderId} not found");
        }
        
        if (order.Status != OrderStatus.Processing)
        {
            throw new ArgumentException($"Order with ID {orderId} is already confirmed");
        }
        var transaction = await _stockMovementRepository.BeginTransactionAsync();
        try
        {
            order.Status = OrderStatus.Delivered;
            await _orderRepository.UpdateAsync(order);
            
            
            foreach (var stockMovement in order.StockMovements)
                {
                    foreach (var stockMovementItem in stockMovement.StockMovementItems)
                    {
                        if (stockMovementItem.ProductItem.Status == ProductItemStatus.PreccessSale)
                        {
                            stockMovementItem.ProductItem.Status = ProductItemStatus.Sold;
                            await _productItemRepository.UpdateAsync(stockMovementItem.ProductItem);
                        }
                        else
                        {
                            throw new ArgumentException($"Product Item with ID {stockMovementItem.ProductItem.ProductItemId} is not in PreccessSale status");
                        }
                    }
                }

            foreach (var orderProduct in order.OrderProducts)
            {   
                var product = orderProduct.Product;
                product.StockQuantity -= orderProduct.Quantity;
                product.SalesQuantity += orderProduct.Quantity;
                await _productRepository.UpdateAsync(product);
                
            }
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception(e.Message);
        }
        
    }
    
}