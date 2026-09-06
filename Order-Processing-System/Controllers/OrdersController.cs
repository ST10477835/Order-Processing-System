using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Order_Processing_System.Models;
using Order_Processing_System.Services;
using System.Text.Json;

namespace Order_Processing_System.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly QueueStorageService _queueStorageService = new QueueStorageService();
        private readonly TableStorageService _tableStorageService = new TableStorageService();

        [HttpGet]
        public IActionResult Index()
        {
            Console.WriteLine("Program started.");
            ViewBag.Products = _tableStorageService.GetProducts();
            return View(_tableStorageService.GetOrders());
        }
        [HttpGet("CreateOrder")]
        public IActionResult CreateOrder()
        {
            ViewBag.Products = _tableStorageService.GetProducts();
            return View();
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromForm] Order _order)
        {
            Order order = new Order
            {//placeholder values
                OrderId = _tableStorageService.CountOrders() + 1,
                CustomerName = _order.CustomerName,
                Email = _order.Email,
                ProductId = _order.ProductId,
                Quanitity = _order.Quanitity,
                CreatedAt = DateTime.Now
            };


            await _queueStorageService.SendMessageAsync(
                new OrderMessage{
                    Operation="Create Order",
                    Order = order
                });
            return RedirectToAction("Index");
        }
        [HttpGet("DeleteOrder")]
        public IActionResult DeleteOrder(int OrderId)
        {
            var order = _tableStorageService.GetOrder(OrderId);
            Console.WriteLine($"Inside GET Delete Order. The Order Id is {OrderId}");
            return View(order);
        }
        [HttpPost("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromForm] Order Order)
        {
            Console.WriteLine("Delete Order insides");
            await _queueStorageService.SendMessageAsync(
                new OrderMessage
                {
                    Operation="Delete Order",
                    OrderId = Order.OrderId
                });
            Console.WriteLine($"Delete Message sent order {Order.OrderId}.");
            return RedirectToAction("Index");
        }
        [HttpGet("UpdateOrder")]
        public IActionResult UpdateOrder(int OrderId)
        {
            var order = _tableStorageService.GetOrder(OrderId);
            ViewBag.Products = _tableStorageService.GetProducts();
            return View(order);
        }
        [HttpPost("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromForm] Order Order)
        {
            await _queueStorageService.SendMessageAsync(
                new OrderMessage
                {
                    Operation = "Update Order",
                    Order = Order
                });
            return RedirectToAction("Index");
        }
    }
}
