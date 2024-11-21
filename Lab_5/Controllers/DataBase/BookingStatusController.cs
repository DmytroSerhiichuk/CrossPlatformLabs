using Lab_5.DTO;
using Lab_5.HttpClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lab_5.Controllers.DataBase
{
	[Authorize]
	[Route("DataBase/BookingStatus")]
	public class BookingStatusController : Controller
	{
		private readonly Lab6HttpClient _lab6HttpClient;
		private readonly AuthConfig _authConfig;

		public BookingStatusController(Lab6HttpClient lab6HttpClient, IOptions<AuthConfig> options)
		{
			_lab6HttpClient = lab6HttpClient;
			_authConfig = options.Value;
		}

		[HttpGet]
		public async Task<IActionResult> GetBookingStatuses()
		{
			var token = Request.Cookies[_authConfig.CookieName] ?? "";

			try
			{
				var res = await _lab6HttpClient.GetData<List<BookingStatusDTO>>(token, "booking-status");

				return View("BookingStatuses", res);
			}
			catch
			{
				return RedirectToAction("Login", "Account");
			}
		}
		[HttpGet("{code}")]
		public async Task<IActionResult> GetBookingStatus(string code)
		{
			var token = Request.Cookies[_authConfig.CookieName] ?? "";

			try
			{
				var res = await _lab6HttpClient.GetData<BookingStatusDetailedDTO>(token, $"booking-status/{code}");

				return View("BookingStatus", res);
			}
			catch
			{
				return RedirectToAction("Login", "Account");
			}
		}
	}
}
