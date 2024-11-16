using Lab_5.HttpClients;
using Lab_5.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lab_5.Controllers
{
	public class AccountController : Controller
    {
        private readonly Auth0HttpClient _auth0Service;

		private readonly AuthConfig _authConfig;

		public AccountController(Auth0HttpClient auth0Service, IOptions<AuthConfig> options)
        {
            _auth0Service = auth0Service;

			_authConfig = options.Value;

		}

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var token = await _auth0Service.AuthenticateUserAsync(model.Email, model.Password);

                StoreToken(token);

                return RedirectToAction("Index", "Profile");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Profile");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _auth0Service.CreateUserAsync(model);

                var token = await _auth0Service.AuthenticateUserAsync(model.Email, model.Password);

                StoreToken(token);

                return RedirectToAction("Index", "Profile");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
                return View(model);
            }
        }

        private void StoreToken(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1)
            };

            Response.Cookies.Append(_authConfig.CookieName, token, cookieOptions);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete(_authConfig.CookieName);
            return RedirectToAction("Index", "Home");
        }
    }
}
