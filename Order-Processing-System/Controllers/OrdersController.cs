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

        public IActionResult Index()
        {
            Console.WriteLine("Program started.");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromForm] Order _order)
        {
            Order order = new Order
            {//placeholder values
                OrderId = _order.OrderId,
                UserId = _order.UserId,
                ProductId = _order.ProductId,
                Status = "Processed",
                CreatedAt = DateTime.Now
            };

            string json = JsonSerializer.Serialize(order);

            await _queueStorageService.SendMessageAsync(json);
            return Ok("Order successfully created");
        }
    }
}
