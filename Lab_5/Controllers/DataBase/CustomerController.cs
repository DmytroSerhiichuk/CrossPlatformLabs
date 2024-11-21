using Lab_5.DTO;
using Lab_5.HttpClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lab_5.Controllers.DataBase
{
	[Authorize]
	[Route("DataBase/Customer")]
	public class CustomerController : Controller
	{
		private readonly Lab6HttpClient _lab6HttpClient;
		private readonly AuthConfig _authConfig;

		public CustomerController(Lab6HttpClient lab6HttpClient, IOptions<AuthConfig> options)
		{
			_lab6HttpClient = lab6HttpClient;
			_authConfig = options.Value;
		}

		[HttpGet]
		public async Task<IActionResult> GetCustomers()
		{
			var token = Request.Cookies[_authConfig.CookieName] ?? "";

			try
			{
				var res = await _lab6HttpClient.GetData<List<CustomerDTO>>(token, "customer");

				return View("Customers", res);
			}
			catch
			{
				return RedirectToAction("Login", "Account");
			}
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCustomer(int id)
		{
			var token = Request.Cookies[_authConfig.CookieName] ?? "";

			try
			{
				var res = await _lab6HttpClient.GetData<CustomerDetailedDTO>(token, $"customer/{id}");

				return View("Customer", res);
			}
			catch
			{
				return RedirectToAction("Login", "Account");
			}
		}
	}
}
