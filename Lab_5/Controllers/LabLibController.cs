using Lab_5.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab_5.Controllers
{
	[Authorize]
	public class LabLibController : Controller
	{
		[HttpGet]
		public IActionResult Lab1()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Lab1(Lab1ViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var lab = new LabLib.Lab_1(model.P1, model.P2);
				var res = lab.Do();
				ViewBag.Result = $"Результат: {res}";
			}
			catch (Exception ex)
			{
				ViewBag.Result = ex.Message;
			}

			return View();
		}

		[HttpGet]
		public IActionResult Lab2()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Lab2(Lab2ViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var lab = new LabLib.Lab_2(model.N, false);
				var res = lab.Do();
				ViewBag.Result = $"Результат: {res}";
			}
			catch (Exception ex)
			{
				ViewBag.Result = ex.Message;
			}

			return View();
		}

		[HttpGet]
		public IActionResult Lab3()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Lab3(Lab3ViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}

			try
			{
				var lab = new LabLib.Lab_3(model.N, model.X1, model.Y1, model.X2, model.Y2);
				var res = lab.Do();
				ViewBag.Result = $"Результат: {res}";
			}
			catch (Exception ex)
			{
				ViewBag.Result = ex.Message;
			}

			return View();
		}
	}
}
