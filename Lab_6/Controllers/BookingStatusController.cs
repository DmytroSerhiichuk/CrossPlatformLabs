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
	[Route("api/booking-status")]
	public class BookingStatusController : Controller
	{
		private readonly BookingDbContext _dbContext;

		public BookingStatusController(BookingDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public IActionResult GetBookingStatuses()
		{
			var models = _dbContext.BookingStatuses
				.Select(bs => new BookingStatusDTO
				{
					Code = bs.Code.Trim(),
					Description = bs.Description,
					BookingCount = bs.Bookings.Count()
				});

			return Ok(models);
		}
		[HttpGet("{code}")]
		public IActionResult GetBookingStatus(string code)
		{
			var models = _dbContext.BookingStatuses
				.Include(bs => bs.Bookings)
				.Select(bs => new BookingStatusDetailedDTO
				{
					Code = bs.Code,
					Description = bs.Description,
					Bookings = bs.Bookings.Select(b => new BookingDTO
					{
						Id = b.Id,
						DateFrom = TimeConverter.ConvertTime(b.DateFrom),
						DateTo = TimeConverter.ConvertTime(b.DateTo),
						IsConfirmationLetterSent = b.IsConfirmationLetterSent,
						IsPaymentReceived = b.IsPaymentReceived,
						BookingStatusCode = b.BookingStatusCode,
						VehicleRegNumber = b.VehicleRegNumber,
						CustomerId = b.CustomerId
					})
				})
				.FirstOrDefault(bs => bs.Code == code);

			return Ok(models);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] BookingStatus bookingStatus)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				_dbContext.BookingStatuses.Add(bookingStatus);
                await _dbContext.SaveChangesAsync();

				return Created($"api/booking-status/{bookingStatus.Code}", bookingStatus);
			}
			catch (Exception ex)
			{
				return Problem
				(
					detail: ex.Message,
					statusCode: 500
				);
			}
		}
	}
}
