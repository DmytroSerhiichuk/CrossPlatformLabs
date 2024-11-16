using Microsoft.AspNetCore.Mvc;

namespace Lab_6.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
