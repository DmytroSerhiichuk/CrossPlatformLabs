using Lab_5.HttpClients;
using Lab_5.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lab_5.Controllers
{
	[Authorize]
	[Route("Db")]
	public class DbController : Controller
	{
		private readonly Lab6HttpClient _lab6HttpClient;
		private readonly AuthConfig _authConfig;

		public DbController(Lab6HttpClient lab6HttpClient, IOptions<AuthConfig> options)
        {
			_lab6HttpClient = lab6HttpClient;
			_authConfig = options.Value;
		}

		[HttpGet("Bookings")]
        public async Task<IActionResult> GetBookings()
		{
			var token = GetToken();

			try
			{
				var res = await _lab6HttpClient.GetData<List<BookingDTO>>(token, "bookings");

				return View("Bookings", res);
			}
			catch
			{
				return RedirectToAction("Login", "Account");
			}
		}

		[HttpGet("Booking/{id}")]
		public async Task<IActionResult> GetBooking(int id)
		{
			var token = GetToken();

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

        [HttpGet("Models")]
        public async Task<IActionResult> GetModels()
        {
            var token = GetToken();

            try
            {
                var res = await _lab6HttpClient.GetData<List<ModelDTO>>(token, "models");

                return View("Models", res);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
        }
		[HttpGet("Model/{code}")]
		public async Task<IActionResult> GetModel(string code)
		{
			var token = GetToken();

			try
			{
				var res = await _lab6HttpClient.GetData<ModelDetailedDTO>(token, $"model/{code}");

                return View("Model", res);
			}
			catch
			{
				return RedirectToAction("Login", "Account");
			}
		}

		[HttpGet("BookingStatuses")]
		public async Task<IActionResult> GetBookingStatuses()
		{
			var token = GetToken();

			try
			{
				var res = await _lab6HttpClient.GetData<List<BookingStatusDTO>>(token, "booking-statuses");

                return View("BookingStatuses", res);
			}
			catch
			{
				return RedirectToAction("Login", "Account");
			}
		}
		[HttpGet("BookingStatus/{code}")]
		public async Task<IActionResult> GetBookingStatus(string code)
		{
			var token = GetToken();

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

		private string GetToken()
		{
			return Request.Cookies[_authConfig.CookieName] ?? "";
		}
	}
}
