﻿using Lab_6.DTO;
using Lab_6.Models;
using Lab_6.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_6.Controllers
{
	[Authorize]
	[ApiController]
    [Route("api/booking")]
    [Route("api/v{version:apiVersion}/booking")]
	[ApiVersion("1.0")]
	[ApiVersion("2.0")]
	public class BookingController : Controller
	{
		private readonly BookingDbContext _dbContext;

		public BookingController(BookingDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		[MapToApiVersion("1.0")]
		public IActionResult GetBookings()
		{
			var request = _dbContext.Bookings
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
				});

			return Ok(request);
		}

		[HttpGet("{id}")]
		[MapToApiVersion("1.0")]
		public IActionResult GetBookingV1(int id)
		{
			var booking = _dbContext.Bookings
				.Include(b => b.BookingStatus)
				.Include(b => b.Vehicle)
				.Include(b => b.Customer)
				.Select(b => new BookingDetailedDTO
				{
					Id = b.Id,
					DateFrom = TimeConverter.ConvertTime(b.DateFrom),
					DateTo = TimeConverter.ConvertTime(b.DateTo),
					IsConfirmationLetterSent = b.IsConfirmationLetterSent,
					IsPaymentReceived = b.IsPaymentReceived,
					BookingStatus = new BookingStatusDTO
					{
						Code = b.BookingStatus.Code,
					},
					Vehicle = new VehicleDTO
					{
						RegNumber = b.Vehicle.RegNumber,
					},
					Customer = new CustomerDTO
					{
						Id = b.Customer.Id,
					}
				})
				.FirstOrDefault(b => b.Id == id);

			return Ok(booking);
		}

		[HttpGet("{id}")]
		[MapToApiVersion("2.0")]
		public IActionResult GetBookingV2(int id)
		{
			var booking = _dbContext.Bookings
				.Include(b => b.BookingStatus)
				.Include(b => b.Vehicle)
				.Include(b => b.Customer)
				.Select(b => new BookingDetailedDTO
				{
					Id = b.Id,
					DateFrom = TimeConverter.ConvertTime(b.DateFrom),
					DateTo = TimeConverter.ConvertTime(b.DateTo),
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
						DateMotDue = TimeConverter.ConvertTime(b.Vehicle.DateMotDue),
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

		[HttpPost]
		[MapToApiVersion("1.0")]
		public async Task<IActionResult> Create([FromBody] Booking booking)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				_dbContext.Bookings.Add(booking);
				await _dbContext.SaveChangesAsync();

                return Created($"api/booking/{booking.Id}", booking);
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
