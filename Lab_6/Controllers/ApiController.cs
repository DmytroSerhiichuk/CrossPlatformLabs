using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab_6.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api")]
	public class ApiController : Controller
	{
		// test action
		[HttpGet("test")]
		public IActionResult Index()
		{
			return Ok(new {id = "1", name = "Dima", age = 20});
		}
	}
}
