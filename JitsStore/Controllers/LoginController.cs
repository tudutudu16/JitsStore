﻿using Microsoft.AspNetCore.Mvc;

namespace JitsStore.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
