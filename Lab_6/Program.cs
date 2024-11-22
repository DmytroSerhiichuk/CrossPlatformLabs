using Lab_6;
using Lab_6.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Auth0"));
var appSettings = builder.Configuration.GetSection("Auth0").Get<AppSettings>();

// Entity
var dbProvider = args.FirstOrDefault(arg => arg.StartsWith("--DbType="))?.Split('=')[1] ?? "InMemory";
Console.WriteLine($"\n\n\nARGUMENTS:{String.Join(' ', args)}\n\n\n");
var connectionString = builder.Configuration.GetConnectionString(dbProvider);

builder.Services.AddDbContext<BookingDbContext>(options =>
{
    switch (dbProvider)
    {
        case "PostgreSQL":
            options.UseNpgsql(connectionString);
            break;
        case "SQLServer":
            options.UseSqlServer(connectionString);
            break;
        case "SQLite":
            options.UseSqlite("Data Source=Lab_6.db");
            break;
        default:
            options.UseInMemoryDatabase("Lab_6");
            break;
    }
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Authentication
var skipAuth = args.FirstOrDefault(arg => arg.StartsWith("--SkipAuth="))?.Split('=')[1] ?? "false";

if (skipAuth == "false")
{
    builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = $"https://{appSettings.Domain}";
        options.Audience = appSettings.Audience;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero
        };
    });
}
else
{
    builder.Services.AddAuthentication("FakeAuth")
        .AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>("FakeAuth", options => { });
}

builder.Services.AddAuthorization();

// Api
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Migration
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BookingDbContext>();

    if (dbProvider == "PostgreSQL" || dbProvider == "SQLServer" || dbProvider == "SQLite")
    {
        dbContext.Database.Migrate();
        dbContext.SaveChanges();
    }
    dbContext.Database.EnsureCreated();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
