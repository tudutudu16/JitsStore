using JitsStore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JitsStore.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Admin()
		{
			return View();
		}

		private readonly JITS_STORE jITSSTORE;

		public AdminController(JITS_STORE jITSSTORE)
		{
			this.jITSSTORE = jITSSTORE;
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(UserRegister user)
		{
			var data = jITSSTORE.Users.Where(item => item.UserName.Equals(user.UserName) && item.Password.Equals(user.Password)).ToList();
			if (data.Count > 0)
			{
				user.UserName = data[0].UserName;
				user.Password = data[0].Password;
				return await Task.Run(() => RedirectToAction("Admin"));
			}
			else
			{
				ViewBag.error = "Login failed";
				return RedirectToAction("Login");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Register(UserRegister user)
		{
			var check = jITSSTORE.Users.FirstOrDefault(item => item.UserName.Equals(user.UserName));
			if (check == null)
			{
				var userAdd = new User()
				{
					Id = Guid.NewGuid(),
					UserName = user.UserName,
					Email = user.Email,
					Password = user.Password
				};

				await jITSSTORE.Users.AddAsync(userAdd);
				await jITSSTORE.SaveChangesAsync();
				return RedirectToAction("Login");
			}
			else
			{
				ViewBag.error = "RegisterFail";
				return RedirectToAction("Login");
			}
		}
		public IActionResult Logout()
		{
			return RedirectToAction("Login");
		}
	}
}
