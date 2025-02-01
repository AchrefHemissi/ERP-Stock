using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Services.ServicesContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;
using StockManagement.Repositories;

namespace backend.Controllers;
[ApiController]

[Route("[controller]")]
public class TestController : Controller
{
    private readonly  AppDbContext _db;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderProductsRepository _orderProductsRepository;
    private readonly IGetOrderService _getOrderService;
    private readonly IGetProductService _getProductService;
    private readonly ILocationService _locationService;
    private readonly IStockMovementService _stockMovementService;
    private readonly IProductWithBlocksService _productWithBlocksService;
    
    public TestController(AppDbContext db , IOrderRepository orderRepository, 
        IOrderProductsRepository orderProductsRepository , 
        IGetOrderService getOrderService, IGetProductService getProductService
        ,ILocationService locationService , IStockMovementService stockMovementService
        ,IProductWithBlocksService productWithBlocksService)
    {
        _db = db;
        _orderRepository = orderRepository;
        _orderProductsRepository = orderProductsRepository;
        _getOrderService = getOrderService;
        _getProductService = getProductService;
        _locationService = locationService;
        _stockMovementService = stockMovementService;
        _productWithBlocksService = productWithBlocksService;
        
    }
    
    [Route("get all Order")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var buyOrders = await _orderRepository.GetAllSellOrdersAsync();
        return Json(buyOrders);
    }
    [Route("get all order with include")]
    [HttpGet]
    public async Task<IActionResult> Index2()
    {
        var order = await _orderProductsRepository.GetOrdersByProductIdAsync(8);
        var Orders = await _orderRepository.GetAllAsync(include => include.Include(e => e.OrderProducts).ThenInclude(e => e.Product));
        return Json(Orders , new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false
            
        });
    }
    [Route("getall sells order")]
    [HttpGet]
    public async Task<IActionResult> Index3()
    {
        var order = await _getOrderService.GetAllSellOrders();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false
            
        });
    }
    
    [Route("getall buy order")]
    [HttpGet]
    public async Task<IActionResult> Index7()
    {
        var order = await _getOrderService.GetAllBuyOrders();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false
            
        });
    }
    
    [Route("detailof an order")]
    [HttpGet]
    public async Task<IActionResult> Index4(int id)
    {
        var order = await _getOrderService.GetOrderDetail(id);
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false
            
        });
    }
    [Route("getAllProduct")]
    [HttpGet]
    public async Task<IActionResult> Index5()
    {
        var order = await _getProductService.GetAllProductsAsync();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("get All Clothing Product")]
    [HttpGet]
    public async Task<IActionResult> Index6()
    {
        var order = await _getProductService.GetAllClothingProduct();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
          
            
        });
    }
    
    [Route("get all location")]
    [HttpGet]
    public async Task<IActionResult> Index8()
    {
        var order = await _locationService.GetAllLocations();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
            
            
        });
    }
    
    [Route("get free location")]
    [HttpGet]
    public async Task<IActionResult> Index9()
    {
        var order = await _locationService.GetFreeLocations();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("get all stock movement")]
    [HttpGet]
    public async Task<IActionResult> Index10()
    {
        var order = await _stockMovementService.GetAllStockMovements();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    [Route("get all product with blocks")]
    [HttpGet]
    public async Task<IActionResult> Index11()
    {
        var order = await _productWithBlocksService.GetAllProductWithBlocks();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false,
           
            
        });
    }
    
    
    
    
    
    
}