using Lab_13_Server;
using Lab_13_Server.HttpClients;
using Lab_13_Server.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<AuthConfig>(builder.Configuration.GetSection("Auth0"));
var appConfig = builder.Configuration.GetSection("Auth0").Get<AuthConfig>();

builder.Services.AddControllers();

builder.Services.AddHttpClient<Auth0HttpClient>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.Cookie.HttpOnly = true;
		options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
		options.Cookie.SameSite = SameSiteMode.Strict;

		options.LoginPath = "/account/login";

		options.Events.OnRedirectToLogin = context =>
		{
			context.Response.StatusCode = 401;
			return Task.CompletedTask;
		};
	});

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowOrigins",
		policy =>
		{
			policy.WithOrigins("http://localhost:3000", "http://localhost:3001")
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials();
		});
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("AllowOrigins");

app.UseStaticFiles();
app.UseCookiePolicy();

app.UseMiddleware<AuthMiddleware>();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
