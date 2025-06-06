﻿
using StockManagement.Models;

namespace backend.Services.ServicesContract;

public interface IGetOrderService
{
    public Task<IEnumerable<Order>> GetAllSellOrders();
    
    public Task<IEnumerable<Order>> GetAllBuyOrders();
    public Task<Order> GetOrderDetail(int id);
    public Task CancelOrder(int id);

}