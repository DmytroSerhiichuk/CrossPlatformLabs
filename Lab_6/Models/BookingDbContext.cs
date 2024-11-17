using Microsoft.EntityFrameworkCore;

namespace Lab_6.Models
{
	public class BookingDbContext : DbContext
	{
		public BookingDbContext(DbContextOptions<BookingDbContext> options) : base(options) { }

		public DbSet<Booking> Bookings { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<VehicleCategory> VehicleCategories { get; set; }
		public DbSet<Model> Models { get; set; }
		public DbSet<Manufacturer> Manufacturers { get; set; }
		public DbSet<BookingStatus> BookingStatuses { get; set; }
		public DbSet<Customer> Customers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			InitForeignKeys(modelBuilder);
			GenerateData(modelBuilder);
		}

		private void InitForeignKeys(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<BookingStatus>()
				.HasMany(bs => bs.Bookings)
				.WithOne(b => b.BookingStatus)
				.HasForeignKey(b => b.BookingStatusCode);

			modelBuilder.Entity<Customer>()
				.HasMany(c => c.Bookings)
				.WithOne(b => b.Customer)
				.HasForeignKey(b => b.CustomerId);

			modelBuilder.Entity<Manufacturer>()
				.HasMany(m => m.Vehicles)
				.WithOne(v => v.Manufacturer)
				.HasForeignKey(v => v.ManufacturerCode);

			modelBuilder.Entity<Model>()
				.HasMany(m => m.Vehicles)
				.WithOne(v => v.Model)
				.HasForeignKey(v => v.ModelCode);

			modelBuilder.Entity<VehicleCategory>()
				.HasMany(vc => vc.Vehicles)
				.WithOne(v => v.VehicleCategory)
				.HasForeignKey(v => v.VehicleCategoryCode);

			modelBuilder.Entity<Vehicle>()
				.HasMany(v => v.Bookings)
				.WithOne(b => b.Vehicle)
				.HasForeignKey(b => b.VehicleRegNumber);
		}

		private void GenerateData(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<BookingStatus>().HasData(
				new BookingStatus { Code = "NEW", Description = "New" },
				new BookingStatus { Code = "CON", Description = "Confirmed" },
				new BookingStatus { Code = "CAN", Description = "Cancelled" },
				new BookingStatus { Code = "PEN", Description = "Pending" },
				new BookingStatus { Code = "COM", Description = "Completed" },
				new BookingStatus { Code = "WAI", Description = "Waiting" },
				new BookingStatus { Code = "HOL", Description = "On hold" },
				new BookingStatus { Code = "PAI", Description = "Paid" },
				new BookingStatus { Code = "EXP", Description = "Expired" },
				new BookingStatus { Code = "NOS", Description = "No-show" }
			);

			modelBuilder.Entity<Customer>().HasData(
				new Customer { Id = 1, Name = "John Doe", Gender = "M", Email = "john.doe@example.com", Phone = "123-456-7890", AddressLine1 = "123 Elm St", AddressLine2 = "Apt 4B", AddressLine3 = "", Town = "Springfield", County = "Illinois", Country = "USA", Details = "Regular customer", },
				new Customer { Id = 2, Name = "Jane Smith", Gender = "F", Email = "jane.smith@example.com", Phone = "987-654-3210", AddressLine1 = "456 Oak St", AddressLine2 = "", AddressLine3 = "", Town = "Greenwich", County = "Connecticut", Country = "USA", Details = "VIP customer", },
				new Customer { Id = 3, Name = "Michael Johnson", Gender = "M", Email = "michael.johnson@example.com", Phone = "555-123-4567", AddressLine1 = "789 Pine St", AddressLine2 = "Suite 100", AddressLine3 = "", Town = "Chicago", County = "Illinois", Country = "USA", Details = "Occasional customer", },
				new Customer { Id = 4, Name = "Emily Brown", Gender = "F", Email = "emily.brown@example.com", Phone = "222-333-4444", AddressLine1 = "101 Maple Ave", AddressLine2 = "", AddressLine3 = "", Town = "Boston", County = "Massachusetts", Country = "USA", Details = "Loyal customer", },
				new Customer { Id = 5, Name = "William Davis", Gender = "M", Email = "william.davis@example.com", Phone = "444-555-6666", AddressLine1 = "202 Birch Rd", AddressLine2 = "Unit 5", AddressLine3 = "", Town = "Los Angeles", County = "California", Country = "USA", Details = "First-time customer", },
				new Customer { Id = 6, Name = "Sophia Miller", Gender = "F", Email = "sophia.miller@example.com", Phone = "555-987-6543", AddressLine1 = "303 Cedar Dr", AddressLine2 = "Floor 3", AddressLine3 = "", Town = "Miami", County = "Florida", Country = "USA", Details = "Frequent traveler", },
				new Customer { Id = 7, Name = "Daniel Wilson", Gender = "M", Email = "daniel.wilson@example.com", Phone = "888-777-6666", AddressLine1 = "404 Pinecrest Rd", AddressLine2 = "", AddressLine3 = "", Town = "Dallas", County = "Texas", Country = "USA", Details = "Preferred customer", },
				new Customer { Id = 8, Name = "Olivia Garcia", Gender = "F", Email = "olivia.garcia@example.com", Phone = "666-555-4444", AddressLine1 = "505 Birch St", AddressLine2 = "Apartment 2A", AddressLine3 = "", Town = "Houston", County = "Texas", Country = "USA", Details = "Customer with VIP status", },
				new Customer { Id = 9, Name = "Lucas Martinez", Gender = "M", Email = "lucas.martinez@example.com", Phone = "111-222-3333", AddressLine1 = "606 Willow Ln", AddressLine2 = "", AddressLine3 = "", Town = "Seattle", County = "Washington", Country = "USA", Details = "New customer", },
				new Customer { Id = 10, Name = "Ava Rodriguez", Gender = "F", Email = "ava.rodriguez@example.com", Phone = "777-888-9999", AddressLine1 = "707 Redwood Ave", AddressLine2 = "", AddressLine3 = "", Town = "San Francisco", County = "California", Country = "USA", Details = "Occasional customer", }
			);

			modelBuilder.Entity<Manufacturer>().HasData(
				new Manufacturer { Code = "FORD", Name = "Ford Motor Company", Details = "American multinational automaker", },
				new Manufacturer { Code = "TOYO", Name = "Toyota Motor Corporation", Details = "Japanese multinational automotive manufacturer", },
				new Manufacturer { Code = "HONDA", Name = "Honda Motor Co.", Details = "Japanese multinational conglomerate known for manufacturing automobiles, motorcycles, and power equipment", },
				new Manufacturer { Code = "BMW", Name = "Bayerische Motoren Werke", Details = "German multinational company which produces luxury vehicles and motorcycles", }
			);

			modelBuilder.Entity<Model>().HasData(
				new Model { Code = "FIESTA", Name = "Ford Fiesta", DailyHireRate = 50.00m },
				new Model { Code = "COROLLA", Name = "Toyota Corolla", DailyHireRate = 60.00m },
				new Model { Code = "CIVIC", Name = "Honda Civic", DailyHireRate = 65.00m },
				new Model { Code = "M3", Name = "BMW M3", DailyHireRate = 120.00m }
			);

			modelBuilder.Entity<VehicleCategory>().HasData(
				new VehicleCategory { Code = "ECON", Description = "Economy" },
				new VehicleCategory { Code = "LUX", Description = "Luxury" },
				new VehicleCategory { Code = "SUV", Description = "SUV" },
				new VehicleCategory { Code = "VAN", Description = "Van" }
			);

			modelBuilder.Entity<Vehicle>().HasData(
				new Vehicle { RegNumber = "ABC123", CurrentMileage = 15000, DailyHireRate = 50.00m, DateMotDue = DateTime.Parse("2025-05-01"), ManufacturerCode = "FORD", ModelCode = "FIESTA", VehicleCategoryCode = "ECON" },
				new Vehicle { RegNumber = "XYZ987", CurrentMileage = 25000, DailyHireRate = 60.00m, DateMotDue = DateTime.Parse("2025-06-01"), ManufacturerCode = "TOYO", ModelCode = "COROLLA", VehicleCategoryCode = "ECON" },
				new Vehicle { RegNumber = "LMN456", CurrentMileage = 30000, DailyHireRate = 65.00m, DateMotDue = DateTime.Parse("2025-07-01"), ManufacturerCode = "HONDA", ModelCode = "CIVIC", VehicleCategoryCode = "LUX" },
				new Vehicle { RegNumber = "PQR789", CurrentMileage = 12000, DailyHireRate = 120.00m, DateMotDue = DateTime.Parse("2025-08-01"), ManufacturerCode = "BMW", ModelCode = "M3", VehicleCategoryCode = "LUX" },
				new Vehicle { RegNumber = "DEF234", CurrentMileage = 18000, DailyHireRate = 80.00m, DateMotDue = DateTime.Parse("2025-09-01"), ManufacturerCode = "FORD", ModelCode = "FIESTA", VehicleCategoryCode = "SUV" },
				new Vehicle { RegNumber = "GHI567", CurrentMileage = 22000, DailyHireRate = 110.00m, DateMotDue = DateTime.Parse("2025-10-01"), ManufacturerCode = "TOYO", ModelCode = "COROLLA", VehicleCategoryCode = "VAN" }
			);

			modelBuilder.Entity<Booking>().HasData(
				new Booking { Id = 1, DateFrom = DateTime.Parse("2024-11-01"), DateTo = DateTime.Parse("2024-11-05"), IsConfirmationLetterSent = "Y", IsPaymentReceived = "Y", BookingStatusCode = "NEW", VehicleRegNumber = "ABC123", CustomerId = 1 },
				new Booking { Id = 2, DateFrom = DateTime.Parse("2024-11-10"), DateTo = DateTime.Parse("2024-11-15"), IsConfirmationLetterSent = "Y", IsPaymentReceived = "N", BookingStatusCode = "CON", VehicleRegNumber = "XYZ987", CustomerId = 2 },
				new Booking { Id = 3, DateFrom = DateTime.Parse("2024-11-20"), DateTo = DateTime.Parse("2024-11-25"), IsConfirmationLetterSent = "N", IsPaymentReceived = "Y", BookingStatusCode = "CAN", VehicleRegNumber = "LMN456", CustomerId = 3 },
				new Booking { Id = 4, DateFrom = DateTime.Parse("2024-12-01"), DateTo = DateTime.Parse("2024-12-03"), IsConfirmationLetterSent = "Y", IsPaymentReceived = "Y", BookingStatusCode = "PEN", VehicleRegNumber = "PQR789", CustomerId = 4 },
				new Booking { Id = 5, DateFrom = DateTime.Parse("2024-12-05"), DateTo = DateTime.Parse("2024-12-10"), IsConfirmationLetterSent = "N", IsPaymentReceived = "Y", BookingStatusCode = "WAI", VehicleRegNumber = "DEF234", CustomerId = 5 },
				new Booking { Id = 6, DateFrom = DateTime.Parse("2024-12-10"), DateTo = DateTime.Parse("2024-12-15"), IsConfirmationLetterSent = "Y", IsPaymentReceived = "N", BookingStatusCode = "CON", VehicleRegNumber = "GHI567", CustomerId = 6 },
				new Booking { Id = 7, DateFrom = DateTime.Parse("2024-12-12"), DateTo = DateTime.Parse("2024-12-18"), IsConfirmationLetterSent = "Y", IsPaymentReceived = "Y", BookingStatusCode = "COM", VehicleRegNumber = "ABC123", CustomerId = 7 },
				new Booking { Id = 8, DateFrom = DateTime.Parse("2024-12-15"), DateTo = DateTime.Parse("2024-12-20"), IsConfirmationLetterSent = "N", IsPaymentReceived = "N", BookingStatusCode = "EXP", VehicleRegNumber = "XYZ987", CustomerId = 8 },
				new Booking { Id = 9, DateFrom = DateTime.Parse("2024-12-18"), DateTo = DateTime.Parse("2024-12-22"), IsConfirmationLetterSent = "Y", IsPaymentReceived = "Y", BookingStatusCode = "NEW", VehicleRegNumber = "LMN456", CustomerId = 9 },
				new Booking { Id = 10, DateFrom = DateTime.Parse("2024-12-20"), DateTo = DateTime.Parse("2024-12-25"), IsConfirmationLetterSent = "N", IsPaymentReceived = "Y", BookingStatusCode = "CAN", VehicleRegNumber = "PQR789", CustomerId = 10 },
				new Booking { Id = 11, DateFrom = DateTime.Parse("2024-12-22"), DateTo = DateTime.Parse("2024-12-27"), IsConfirmationLetterSent = "Y", IsPaymentReceived = "N", BookingStatusCode = "PEN", VehicleRegNumber = "DEF234", CustomerId = 1 },
				new Booking { Id = 12, DateFrom = DateTime.Parse("2024-12-25"), DateTo = DateTime.Parse("2024-12-30"), IsConfirmationLetterSent = "Y", IsPaymentReceived = "Y", BookingStatusCode = "WAI", VehicleRegNumber = "GHI567", CustomerId = 2 },
				new Booking { Id = 13, DateFrom = DateTime.Parse("2025-01-01"), DateTo = DateTime.Parse("2025-01-07"), IsConfirmationLetterSent = "N", IsPaymentReceived = "Y", BookingStatusCode = "CON", VehicleRegNumber = "ABC123", CustomerId = 3 },
				new Booking { Id = 14, DateFrom = DateTime.Parse("2025-01-05"), DateTo = DateTime.Parse("2025-01-10"), IsConfirmationLetterSent = "Y", IsPaymentReceived = "N", BookingStatusCode = "COM", VehicleRegNumber = "XYZ987", CustomerId = 4 },
				new Booking { Id = 15, DateFrom = DateTime.Parse("2025-01-10"), DateTo = DateTime.Parse("2025-01-15"), IsConfirmationLetterSent = "Y", IsPaymentReceived = "Y", BookingStatusCode = "EXP", VehicleRegNumber = "LMN456", CustomerId = 5 },
				new Booking { Id = 16, DateFrom = DateTime.Parse("2025-01-12"), DateTo = DateTime.Parse("2025-01-17"), IsConfirmationLetterSent = "N", IsPaymentReceived = "Y", BookingStatusCode = "WAI", VehicleRegNumber = "PQR789", CustomerId = 6 },
				new Booking { Id = 17, DateFrom = DateTime.Parse("2025-01-15"), DateTo = DateTime.Parse("2025-01-20"), IsConfirmationLetterSent = "Y", IsPaymentReceived = "N", BookingStatusCode = "PEN", VehicleRegNumber = "DEF234", CustomerId = 7 },
				new Booking { Id = 18, DateFrom = DateTime.Parse("2025-01-18"), DateTo = DateTime.Parse("2025-01-22"), IsConfirmationLetterSent = "Y", IsPaymentReceived = "Y", BookingStatusCode = "NEW", VehicleRegNumber = "GHI567", CustomerId = 8 },
				new Booking { Id = 19, DateFrom = DateTime.Parse("2025-01-22"), DateTo = DateTime.Parse("2025-01-27"), IsConfirmationLetterSent = "N", IsPaymentReceived = "Y", BookingStatusCode = "CAN", VehicleRegNumber = "ABC123", CustomerId = 9 },
				new Booking { Id = 20, DateFrom = DateTime.Parse("2025-01-25"), DateTo = DateTime.Parse("2025-01-30"), IsConfirmationLetterSent = "Y", IsPaymentReceived = "N", BookingStatusCode = "CON", VehicleRegNumber = "XYZ987", CustomerId = 10 }
			);

		}
	}
}
