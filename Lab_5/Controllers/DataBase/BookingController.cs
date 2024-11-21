using Lab_5.DTO;
using Lab_5.HttpClients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lab_5.Controllers.DataBase
{
	[Authorize]
	[Route("DataBase/Booking")]
	public class BookingController : Controller
	{
		private readonly Lab6HttpClient _lab6HttpClient;
		private readonly AuthConfig _authConfig;

		public BookingController(Lab6HttpClient lab6HttpClient, IOptions<AuthConfig> options)
		{
			_lab6HttpClient = lab6HttpClient;
			_authConfig = options.Value;
		}

		[HttpGet]
		public async Task<IActionResult> GetBookings()
		{
			var token = Request.Cookies[_authConfig.CookieName] ?? "";

			try
			{
				var res = await _lab6HttpClient.GetData<List<BookingDTO>>(token, "booking");

				return View("Bookings", res);
			}
			catch
			{
				return RedirectToAction("Login", "Account");
			}
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetBooking(int id)
		{
			var token = Request.Cookies[_authConfig.CookieName] ?? "";

			try
			{
				var res = await _lab6HttpClient.GetData<BookingDetailedDTO>(token, $"booking/{id}");

				return View("Booking", res);
			}
			catch
			{
				return RedirectToAction("Login", "Account");
			}
		}
	}
}
