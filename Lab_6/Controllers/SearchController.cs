using Lab_6.DTO;
using Lab_6.Models;
using Lab_6.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_6.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/search")]
	public class SearchController : Controller
	{
		private readonly BookingDbContext _dbContext;

		public SearchController(BookingDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public IActionResult Index([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo, [FromQuery] string? items, [FromQuery] string? nameStartsWith, [FromQuery] string? nameEndsWith)
		{
			var bookings = _dbContext.Bookings
				.Include(b => b.BookingStatus)
				.Include(b => b.Vehicle)
				.Include(b => b.Customer)
				.ToList();

			if (dateFrom != null) bookings = bookings.Where(b => b.DateFrom >= dateFrom).ToList();
			if (dateTo != null) bookings = bookings.Where(b => b.DateTo <= dateTo).ToList();

			if (!String.IsNullOrEmpty(items))
			{
				var vehicleRegNumbers = items.Trim().ToLower().Split(',', StringSplitOptions.RemoveEmptyEntries).Select(i => i.Trim()).ToList();

				bookings = bookings.Where(b => vehicleRegNumbers.Contains(b.VehicleRegNumber.Trim().ToLower())).ToList();
			}

			if (!String.IsNullOrEmpty(nameStartsWith))
			{
				var lNameStartsWith = nameStartsWith.ToLower();
				bookings = bookings.Where(b => b.Customer.Name.ToLower().StartsWith(lNameStartsWith)).ToList();
			}
			if (!String.IsNullOrEmpty(nameEndsWith))
			{
				var lNameEndsWith = nameEndsWith.ToLower();
				bookings = bookings.Where(b => b.Customer.Name.ToLower().EndsWith(lNameEndsWith)).ToList();
			}

			var response = bookings
				.Select(b => new BookingDTO
				{
					Id = b.Id,
					DateFrom = TimeConverter.ConvertTime(b.DateFrom),
					DateTo = TimeConverter.ConvertTime(b.DateTo),
					IsConfirmationLetterSent = b.IsConfirmationLetterSent,
					IsPaymentReceived = b.IsPaymentReceived,
					BookingStatusCode = b.BookingStatusCode,
					VehicleRegNumber = b.VehicleRegNumber,
					CustomerId = b.CustomerId
				}).OrderBy(b => b.Id).ToList();

			return Ok(response);
		}
	}
}
