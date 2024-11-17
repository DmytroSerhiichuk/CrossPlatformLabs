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

		[HttpGet("Vehicles")]
        public async Task<IActionResult> GetVehicles()
		{
			var token = GetToken();

			var res = await _lab6HttpClient.GetVehicles(token);

			return Ok(res);

			//try
			//{
			//	var res = await _lab6HttpClient.GetVehicles(token);

			//	return Ok(res);
			//}
			//catch
			//{
			//	return RedirectToAction("Login", "Account");
			//}
		}

		private string GetToken()
		{
			return Request.Cookies[_authConfig.CookieName] ?? "";
		}
	}
}
