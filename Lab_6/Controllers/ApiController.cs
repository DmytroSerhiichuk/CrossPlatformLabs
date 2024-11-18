using Lab_6.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("bookings")]
		public IActionResult GetBookings()
		{
			var request = _dbContext.Bookings
				.Select(b => new
				{
					b.Id,
					b.DateFrom,
					b.DateTo,
					b.IsConfirmationLetterSent,
					b.IsPaymentReceived,
					b.BookingStatusCode,
					b.VehicleRegNumber,
					b.CustomerId
				});

			return Ok(request);
		}

		[HttpGet("booking/{id}")]
		public IActionResult GetBooking(int id)
		{
			var booking = _dbContext.Bookings
				.Include(b => b.BookingStatus)
				.Include(b => b.Vehicle)
				.Include(b => b.Customer)
				.Select(b => new
				{
					b.Id,
					b.DateFrom,
					b.DateTo,
					b.IsConfirmationLetterSent,
					b.IsPaymentReceived,
					BookingStatus = new
					{
						b.BookingStatus.Code,
						b.BookingStatus.Description,
						BookingCount = b.BookingStatus.Bookings.Count()
					},
					Vehicle = new
					{
						b.Vehicle.RegNumber,
						BookingCount = b.Vehicle.Bookings.Count(),
						b.Vehicle.CurrentMileage,
						b.Vehicle.DailyHireRate,
						b.Vehicle.DateMotDue,
						b.Vehicle.ManufacturerCode,
						b.Vehicle.ModelCode,
						b.Vehicle.VehicleCategoryCode
					},
					Customer = new
					{
						b.Customer.Id,
						BookingCount = b.Customer.Bookings.Count(),
						b.Customer.Name,
						b.Customer.Details,
						b.Customer.Gender,
						b.Customer.Email,
						b.Customer.Phone,
						b.Customer.AddressLine1,
						b.Customer.AddressLine2,
						b.Customer.AddressLine3,
						b.Customer.Town,
						b.Customer.County,
						b.Customer.Country,
					}
				})
				.FirstOrDefault(b => b.Id == id);

			return Ok(booking);
		}

        [HttpGet("models")]
        public IActionResult GetModels()
        {
			var models = _dbContext.Models
				.Select(m => new
				{
					Code = m.Code.Trim(),
					m.Name,
					m.DailyHireRate,
					VehicleCount = m.Vehicles.Count()
				});

			return Ok(models);
        }
		[HttpGet("model/{code}")]
		public IActionResult GetModel(string code)
		{
			var models = _dbContext.Models
				.Include(m => m.Vehicles)
				.Select(m => new
				{
					m.Code,
					m.Name,
					m.DailyHireRate,
					Vehicles = m.Vehicles.Select(v => new
					{
						v.RegNumber,
						BookingCount = v.Bookings.Count(),
						v.CurrentMileage,
						v.DailyHireRate,
						v.DateMotDue,
						v.ManufacturerCode,
						v.ModelCode,
						v.VehicleCategoryCode
					})
				})
				.FirstOrDefault(m => m.Code == code);

			return Ok(models);
		}

		[HttpGet("booking-statuses")]
		public IActionResult GetBookingStatuses()
		{
			var models = _dbContext.BookingStatuses
				.Select(bs => new
				{
					Code = bs.Code.Trim(),
					bs.Description,
					BookingCount = bs.Bookings.Count()
				});

			return Ok(models);
		}
		[HttpGet("booking-status/{code}")]
		public IActionResult GetBookingStatus(string code)
		{
			var models = _dbContext.BookingStatuses
				.Include(bs => bs.Bookings)
				.Select(bs => new
				{
					bs.Code,
					bs.Description,
					Bookings = bs.Bookings.Select(b => new
					{
						b.Id,
						b.DateFrom,
						b.DateTo,
						b.IsConfirmationLetterSent,
						b.IsPaymentReceived,
						b.BookingStatusCode,
						b.VehicleRegNumber,
						b.CustomerId
					})
				})
				.FirstOrDefault(bs => bs.Code == code);

			return Ok(models);
		}
	}
}
