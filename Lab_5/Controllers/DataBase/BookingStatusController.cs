using Lab_5.DTO;
using Lab_5.HttpClients;
using Lab_5.RequestDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

		[HttpGet("Create")]
		public IActionResult Create()
		{
			return View("Create");
		}
		[HttpPost("Create")]
		public async Task<IActionResult> Create(BookingStatusRequestDTO bookingStatus)
		{
			if (!ModelState.IsValid)
			{
				return View("Create", bookingStatus);
			}

			var token = Request.Cookies[_authConfig.CookieName] ?? "";

			try
			{
				await _lab6HttpClient.PostData(token, "booking-status", bookingStatus);

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
