using Lab_5.HttpClients;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Lab_5.Middleware
{
	public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Auth0HttpClient _auth0Service;
		private readonly AuthConfig _authConfig;

		public AuthMiddleware(RequestDelegate next, Auth0HttpClient auth0Service, IOptions<AuthConfig> options)
        {
            _next = next;
            _auth0Service = auth0Service;
			_authConfig = options.Value;
		}

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Cookies[_authConfig.CookieName];

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var user = await _auth0Service.GetUserInfoAsync(token);

                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.GetProperty("email").GetString()),
                        new Claim("User Name", user.GetProperty("nickname").GetString()),
                        new Claim("Full Name", user.GetProperty("name").GetString()),
                        new Claim("Email", user.GetProperty("email").GetString()),
                        new Claim("Phone", user.GetProperty("phone_number").GetString()),
                        new Claim("Picture", user.GetProperty("picture").GetString()),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    context.User = claimsPrincipal;
                }
                catch (Exception ex)
                {
                    await _next(context);
                }
                
            }

            await _next(context);
        }
    }
}
