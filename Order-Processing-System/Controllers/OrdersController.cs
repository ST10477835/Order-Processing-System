using Microsoft.AspNetCore.Mvc;

namespace Order_Processing_System.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
