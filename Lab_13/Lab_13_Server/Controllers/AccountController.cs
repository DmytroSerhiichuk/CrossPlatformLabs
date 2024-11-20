using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Lab_13_Server.HttpClients;
using Lab_13_Server.Models;

namespace Lab_13_Server.Controllers
{
	[ApiController]
	[Route("api/account")]
	public class AccountController : Controller
	{
		private readonly Auth0HttpClient _auth0Service;
		private readonly AuthConfig _authConfig;

		public AccountController(Auth0HttpClient auth0Service, IOptions<AuthConfig> options)
		{
			_auth0Service = auth0Service;
			_authConfig = options.Value;
		}

		[Authorize]
		[HttpGet("check")]
		public IActionResult Check()
		{
			return Ok();
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new
				{
					message = "Validation failed",
					errors = ModelState.Values.SelectMany(v => v.Errors)
				});
			}

			try
			{
				var token = await _auth0Service.AuthenticateUserAsync(model.Email, model.Password);
				StoreToken(token);

				return Ok();
			}
			catch (HttpRequestException ex)
			{
				return Problem
				(
					detail: ex.Message,
					statusCode: (int)ex.StatusCode
				);
			}
			catch (Exception ex)
			{
				return Problem
				(
					detail: ex.Message,
					statusCode: 500,
					title: "Something went wrong"
				);
			}
		}

		[HttpPost("signup")]
		public async Task<IActionResult> SignUp([FromBody] SignUpViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(new
				{
					message = "Validation failed",
					errors = ModelState.Values.SelectMany(v => v.Errors)
				});
			}

			try
			{
				await _auth0Service.CreateUserAsync(model);
				var token = await _auth0Service.AuthenticateUserAsync(model.Email, model.Password);
				StoreToken(token);

				return Ok();
			}
			catch (HttpRequestException ex)
			{
				return Problem
				(
					detail: ex.Message,
					statusCode: (int)ex.StatusCode
				);
			}
			catch (Exception ex)
			{
				return Problem
				(
					detail: ex.Message,
					statusCode: 500,
					title: "Something went wrong"
				);
			}
		}

		private void StoreToken(string token)
		{
			var cookieOptions = new CookieOptions
			{
				HttpOnly = true,
				//Secure = true,
				SameSite = SameSiteMode.Strict,
				Expires = DateTime.UtcNow.AddHours(1)
			};

			Response.Cookies.Append(_authConfig.CookieName, token, cookieOptions);
		}


		[Authorize]
		[HttpGet("profile")]
		public IActionResult GetProfile()
		{
			var user = HttpContext.User;

			var responseUser = new
			{
				UserName = user.FindFirst("User Name")?.Value ?? "",
				FullName = user.FindFirst("Full Name")?.Value ?? "",
				Email = user.FindFirst("Email")?.Value ?? "",
				Phone = user.FindFirst("Phone")?.Value ?? "",
				Picture = user.FindFirst("Picture")?.Value ?? ""
			};

			return Ok(responseUser);
		}

		[Authorize]
		[HttpPost("logout")]
		public async Task<IActionResult> Logout()
		{
			try
			{
				await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
				Response.Cookies.Delete(_authConfig.CookieName);
				return Ok();
			}
			catch (Exception ex)
			{
				return Problem
				(
					detail: ex.Message,
					statusCode: 500,
					title: "Something went wrong"
				);
			}
		}
	}
}
