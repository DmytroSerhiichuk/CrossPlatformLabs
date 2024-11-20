using Lab_13_Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab_13_Server.Controllers
{
	[Route("api/lablib")]
	[ApiController]
	[Authorize]
	public class LabLibController : ControllerBase
	{
		[HttpPost("lab-1")]
		public IActionResult Lab1([FromBody] Lab1ViewModel model)
		{
			string response;

			try
			{
				var lab = new LabLib.Lab_1(model.P1, model.P2);
				var res = lab.Do();
				response = $"Результат: {res}";
			}
			catch (Exception ex)
			{
				response = ex.Message;
			}

			return Ok(response);
		}

		[HttpPost("lab-2")]
		public IActionResult Lab2([FromBody] Lab2ViewModel model)
		{
			string response;

			try
			{
				var lab = new LabLib.Lab_2(model.N, false);
				var res = lab.Do();
				response = $"Результат: {res}";
			}
			catch (Exception ex)
			{
				response = ex.Message;
			}

			return Ok(response);
		}

		[HttpPost("lab-3")]
		public IActionResult Lab3([FromBody] Lab3ViewModel model)
		{
			string response;

			try
			{
				var lab = new LabLib.Lab_3(model.N, model.X1, model.Y1, model.X2, model.Y2);
				var res = lab.Do();
				response = $"Результат: {res}";
			}
			catch (Exception ex)
			{
				response = ex.Message;
			}

			return Ok(response);
		}
	}
}
