using Lab_6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Lab_6.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api")]
	public class ApiController : Controller
	{
		private readonly BookingDbContext _dbContext;

        public ApiController(BookingDbContext dbContext)
        {
			_dbContext = dbContext;
		}

        [HttpGet("vehicles")]
		public IActionResult GetVehicles()
		{
			var vehicles = _dbContext.Vehicles
				.Include(v => v.Bookings)
				.Include(v => v.Manufacturer)
				.Include(v => v.Model)
				.Include(v => v.VehicleCategory)
				.ToList();

			var response = JsonConvert.SerializeObject(vehicles.Select(v => new
			{
				v.RegNumber,
				v.CurrentMileage,
				v.DailyHireRate,
				v.DateMotDue,
				Manufacturer = new 
				{
					v.Manufacturer.Code,
					v.Manufacturer.Name,
					v.Manufacturer.Details
				},
				Model = new
				{
					v.Model.Code,
					v.Model.Name,
					v.Model.DailyHireRate
				},
				VehicleCategory = new
				{
					v.VehicleCategory.Code,
					v.VehicleCategory.Description
				},
				Bookings = v.Bookings.Select(b => new { b.Id }).ToList()
			}));

			return Ok(response);
		}
	}
}
