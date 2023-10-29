using Microsoft.AspNetCore.Mvc;

namespace JitsStore.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Admin()
		{
			return View();
		}

        public IActionResult Table()
        {
            return View();
        }
    }
}
