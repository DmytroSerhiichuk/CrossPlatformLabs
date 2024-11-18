using Lab_5.HttpClients;
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
				var res = await _lab6HttpClient.GetBookings(token);

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
				var res = await _lab6HttpClient.GetBooking(token, id);

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
                var res = await _lab6HttpClient.GetModels(token);

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
				var res = await _lab6HttpClient.GetModel(token, code);

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
				var res = await _lab6HttpClient.GetBookingStatuses(token);

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
				var res = await _lab6HttpClient.GetBookingStatus(token, code);

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
