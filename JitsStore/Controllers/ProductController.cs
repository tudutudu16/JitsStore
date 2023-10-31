using Microsoft.AspNetCore.Mvc;

namespace JitsStore.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
