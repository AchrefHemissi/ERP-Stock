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
    
    public TestController(AppDbContext db , IOrderRepository orderRepository, IOrderProductsRepository orderProductsRepository , IGetOrderService getOrderService)
    {
        _db = db;
        _orderRepository = orderRepository;
        _orderProductsRepository = orderProductsRepository;
        _getOrderService = getOrderService;
    }
    
    [Route("Index")]
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var buyOrders = await _orderRepository.GetAllSellOrdersAsync();
        return Json(buyOrders);
    }
    [Route("Index2")]
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
    [Route("Index3")]
    [HttpGet]
    public async Task<IActionResult> Index3()
    {
        var order = await _getOrderService.GetAllSellOrders();
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false
            
        });
    }
    
    [Route("Index4")]
    [HttpGet]
    public async Task<IActionResult> Index4()
    {
        var order = await _getOrderService.GetOrderDetail(2);
        return Json(order ,new JsonSerializerOptions{
            ReferenceHandler =  ReferenceHandler.IgnoreCycles,
            WriteIndented = false
            
        });
    }
    
}