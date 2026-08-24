using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
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
            return View(_tableStorageService.GetOrders());
        }
        [HttpGet("CreateOrder")]
        public IActionResult CreateOrder()
        {
            return View();
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromForm] Order _order)
        {
            Order order = new Order
            {//placeholder values
                OrderId = _tableStorageService.Count() + 1,
                CustomerName = _order.CustomerName,
                Email = _order.Email,
                Product = _order.Product,
                Quanitity = _order.Quanitity,
                Price = _order.Price,
                CreatedAt = DateTime.Now
            };

            string json = JsonSerializer.Serialize(order);

            await _queueStorageService.SendMessageAsync(json);
            return RedirectToAction("Index");
        }

    }
}
