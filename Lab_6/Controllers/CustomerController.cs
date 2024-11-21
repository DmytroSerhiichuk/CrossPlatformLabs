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
	[Route("api/customer")]
	public class CustomerController : Controller
	{
		private readonly BookingDbContext _dbContext;

		public CustomerController(BookingDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public IActionResult GetCustomers()
		{
			var request = _dbContext.Customers
				.Select(c => new CustomerDTO
				{
					Id = c.Id,
					BookingCount = c.Bookings.Count(),
					Name = c.Name,
					Details = c.Details,
					Gender = c.Gender,
					Email = c.Email,
					Phone = c.Phone,
					AddressLine1 = c.AddressLine1,
					AddressLine2 = c.AddressLine2,
					AddressLine3 = c.AddressLine3,
					Town = c.Town,
					County = c.County,
					Country = c.Country,
				});

			return Ok(request);
		}

		[HttpGet("{id}")]
		public IActionResult GetCustomers(int id)
		{
			var customers = _dbContext.Customers
				.Include(c => c.Bookings)
				.Select(c => new CustomerDetailedDTO
				{
					Id = c.Id,
					Bookings = c.Bookings.Select(b => new BookingDTO
					{
						Id = b.Id,
						DateFrom = TimeConverter.ConvertTime(b.DateFrom),
						DateTo = TimeConverter.ConvertTime(b.DateTo),
						IsConfirmationLetterSent = b.IsConfirmationLetterSent,
						IsPaymentReceived = b.IsPaymentReceived,
						BookingStatusCode = b.BookingStatusCode,
						VehicleRegNumber = b.VehicleRegNumber,
						CustomerId = b.CustomerId
					}),
					Name = c.Name,
					Details = c.Details,
					Gender = c.Gender,
					Email = c.Email,
					Phone = c.Phone,
					AddressLine1 = c.AddressLine1,
					AddressLine2 = c.AddressLine2,
					AddressLine3 = c.AddressLine3,
					Town = c.Town,
					County = c.County,
					Country = c.Country,
				})
				.FirstOrDefault(c => c.Id == id);

			return Ok(customers);
		}
	}
}
