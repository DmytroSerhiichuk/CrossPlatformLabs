using Lab_5.DTO;
using Lab_5.HttpClients;
using Lab_5.RequestDTO;
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
		[HttpGet("v1.0/{id}")]
		public async Task<IActionResult> GetBookingV1(int id)
		{
			var token = Request.Cookies[_authConfig.CookieName] ?? "";

			try
			{
				var res = await _lab6HttpClient.GetData<BookingDetailedDTO>(token, $"v1.0/booking/{id}");

				return View("BookingV1", res);
			}
			catch
			{
				return RedirectToAction("Login", "Account");
			}
		}
        [HttpGet("v2.0/{id}")]
        public async Task<IActionResult> GetBookingV2(int id)
        {
            var token = Request.Cookies[_authConfig.CookieName] ?? "";

            try
            {
                var res = await _lab6HttpClient.GetData<BookingDetailedDTO>(token, $"v2.0/booking/{id}");

                return View("BookingV2", res);
            }
            catch
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet("Create")]
		public IActionResult Create()
		{
			return View("Create");
		}
		[HttpPost("Create")]
		public async Task<IActionResult> Create(BookingRequestDTO bookingStatus)
		{
			if (!ModelState.IsValid)
			{
				return View("Create", bookingStatus);
			}

			var token = Request.Cookies[_authConfig.CookieName] ?? "";

			try
			{
				await _lab6HttpClient.PostData(token, "booking", bookingStatus);

				ViewData["alertMessage"] = "Success";
				return View("Create");
			}
			catch (HttpRequestException ex)
			{
				if (ex.StatusCode == System.Net.HttpStatusCode.Forbidden || ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
				{
					return RedirectToAction("Login", "Account");
				}
				else
				{
					ModelState.AddModelError(String.Empty, ex.Message);
					return View("Create", bookingStatus);
				}
			}
			catch (Exception ex)
			{
				ModelState.AddModelError(String.Empty, ex.Message);
				return View("Create", bookingStatus);
			}
		}
	}
}
