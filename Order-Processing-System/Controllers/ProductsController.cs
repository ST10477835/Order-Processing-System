using Microsoft.AspNetCore.Mvc;
using Order_Processing_System.Models;
using Order_Processing_System.Services;

namespace Order_Processing_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly QueueStorageService _queueStorageService = new QueueStorageService();
        private readonly TableStorageService _tableStorageService = new TableStorageService();
        [HttpGet]
        public IActionResult Index()
        {
            return View(_tableStorageService.GetProducts());
        }
        [HttpGet("CreateProduct")]
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm] Product _product)
        {
            Product product = new Product
            {
                ProductId = _tableStorageService.CountProducts() + 1,
                Name = _product.Name,
                Price = _product.Price
            };
            await _queueStorageService.SendMessageAsync(
                new ProductMessage
                {
                    Operation = "Create Product",
                    Product = product
                });
            return RedirectToAction("Index");
        }
    }
}
