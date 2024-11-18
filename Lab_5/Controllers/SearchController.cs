using Lab_5.HttpClients;
using Lab_5.Models;
using Lab_5.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Lab_5.Controllers
{
	[Authorize]
	public class SearchController : Controller
	{
        private readonly Lab6HttpClient _lab6HttpClient;
        private readonly AuthConfig _authConfig;

        public SearchController(Lab6HttpClient lab6HttpClient, IOptions<AuthConfig> options)
        {
            _lab6HttpClient = lab6HttpClient;
            _authConfig = options.Value;
        }

        [HttpGet]
		public IActionResult Index()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Index(SearchViewModel model)
        {
			if (!ModelState.IsValid)
			{
                return View(model);
            }

            try
            {
                var token = Request.Cookies[_authConfig.CookieName] ?? "";

                var query = model.ParseToQuery();

                var res = await _lab6HttpClient.GetData<List<BookingDTO>>(token, $"search?{query}");

                return View("SearchResult", res);
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Account");
                }
                throw;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
                return View(model);
            }
        }
    }
}
