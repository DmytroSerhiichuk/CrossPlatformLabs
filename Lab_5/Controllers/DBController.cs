using Lab_5.HttpClients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// Only for connection test
namespace Lab_5.Controllers
{
	[ApiController]
	[Route("db")]
	public class DBController : Controller
	{
		private readonly Lab6HttpClient _lab6HttpClient;
		private readonly AuthConfig _authConfig;

		public DBController(Lab6HttpClient lab6HttpClient, IOptions<AuthConfig> options)
        {
			_lab6HttpClient = lab6HttpClient;
			_authConfig = options.Value;
		}

		[HttpGet("")]
        public async Task<IActionResult> Index()
		{
			var token = Request.Cookies[_authConfig.CookieName];

			if (String.IsNullOrEmpty(token))
			{
				return RedirectToAction("Login", "Account");
			}

			try
			{
				var res = await _lab6HttpClient.GetTest(token);

				return Ok(res.GetProperty("name"));
			}
			catch
			{
				return RedirectToAction("Login", "Account");
			}
		}
	}
}
