using Lab_5;
using Lab_5.HttpClients;
using Lab_5.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AuthConfig>(builder.Configuration.GetSection("Auth0"));
var appConfig = builder.Configuration.GetSection("Auth0").Get<AuthConfig>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient<Auth0HttpClient>();
builder.Services.AddHttpClient<Lab6HttpClient>();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
        options.Cookie.Name = appConfig.CookieName;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseCookiePolicy();

app.UseMiddleware<AuthMiddleware>();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
