using Lab_6.DTO;
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
				.Select(b => new BookingDTO
				{
					Id = b.Id,
					DateFrom = ConvertTime(b.DateFrom),
					DateTo = ConvertTime(b.DateTo),
					IsConfirmationLetterSent = b.IsConfirmationLetterSent,
					IsPaymentReceived = b.IsPaymentReceived,
					BookingStatusCode = b.BookingStatusCode,
					VehicleRegNumber = b.VehicleRegNumber,
					CustomerId = b.CustomerId
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
				.Select(b => new BookingDetailedDTO
				{
					Id = b.Id,
					DateFrom = ConvertTime(b.DateFrom),
					DateTo = ConvertTime(b.DateTo),
					IsConfirmationLetterSent = b.IsConfirmationLetterSent,
					IsPaymentReceived = b.IsPaymentReceived,
					BookingStatus = new BookingStatusDTO
					{
						Code = b.BookingStatus.Code,
						Description = b.BookingStatus.Description,
						BookingCount = b.BookingStatus.Bookings.Count()
					},
					Vehicle = new VehicleDTO
					{
						RegNumber = b.Vehicle.RegNumber,
						BookingCount = b.Vehicle.Bookings.Count(),
						CurrentMileage = b.Vehicle.CurrentMileage,
						DailyHireRate = b.Vehicle.DailyHireRate,
						DateMotDue = ConvertTime(b.Vehicle.DateMotDue),
						ManufacturerCode = b.Vehicle.ManufacturerCode,
						ModelCode = b.Vehicle.ModelCode,
						VehicleCategoryCode = b.Vehicle.VehicleCategoryCode
					},
					Customer = new CustomerDTO
					{
						Id = b.Customer.Id,
						BookingCount = b.Customer.Bookings.Count(),
						Name = b.Customer.Name,
						Details = b.Customer.Details,
						Gender = b.Customer.Gender,
						Email = b.Customer.Email,
						Phone = b.Customer.Phone,
						AddressLine1 = b.Customer.AddressLine1,
						AddressLine2 = b.Customer.AddressLine2,
						AddressLine3 = b.Customer.AddressLine3,
						Town = b.Customer.Town,
						County = b.Customer.County,
						Country = b.Customer.Country,
					}
				})
				.FirstOrDefault(b => b.Id == id);

			return Ok(booking);
		}

		[HttpGet("models")]
		public IActionResult GetModels()
		{
			var models = _dbContext.Models
				.Select(m => new ModelDTO
				{
					Code = m.Code.Trim(),
					Name = m.Name,
					DailyHireRate = m.DailyHireRate,
					VehicleCount = m.Vehicles.Count()
				});

			return Ok(models);
		}
		[HttpGet("model/{code}")]
		public IActionResult GetModel(string code)
		{
			var models = _dbContext.Models
				.Include(m => m.Vehicles)
				.Select(m => new ModelDetailedDTO
				{
					Code = m.Code,
					Name = m.Name,
					DailyHireRate = m.DailyHireRate,
					Vehicles = m.Vehicles.Select(v => new VehicleDTO
					{
						RegNumber = v.RegNumber,
						BookingCount = v.Bookings.Count,
						CurrentMileage = v.CurrentMileage,
						DailyHireRate = v.DailyHireRate,
						DateMotDue = ConvertTime(v.DateMotDue),
						ManufacturerCode = v.ManufacturerCode,
						ModelCode = v.ModelCode,
						VehicleCategoryCode = v.VehicleCategoryCode
					})
				})
				.FirstOrDefault(m => m.Code == code);

			return Ok(models);
		}

		[HttpGet("booking-statuses")]
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
		[HttpGet("booking-status/{code}")]
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
						DateFrom = ConvertTime(b.DateFrom),
						DateTo = ConvertTime(b.DateTo),
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

		[HttpGet("search")]
		public IActionResult GetSearch([FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo, [FromQuery] string? items, [FromQuery] string? nameStartsWith, [FromQuery] string? nameEndsWith)
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
					DateFrom = ConvertTime(b.DateFrom),
					DateTo = ConvertTime(b.DateTo),
					IsConfirmationLetterSent = b.IsConfirmationLetterSent,
					IsPaymentReceived = b.IsPaymentReceived,
					BookingStatusCode = b.BookingStatusCode,
					VehicleRegNumber = b.VehicleRegNumber,
					CustomerId = b.CustomerId
				}).OrderBy(b => b.Id).ToList();

			return Ok(response);
		}

		public static DateTime ConvertTime(DateTime time)
		{
			return TimeZoneInfo.ConvertTimeFromUtc(time, TimeZoneInfo.FindSystemTimeZoneById("Europe/Kiev"));
		}
	}
}
