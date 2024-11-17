namespace Lab_5.Models
{
	public class Vehicle
	{
		public string RegNumber { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public int CurrentMileage { get; set; }

		public decimal DailyHireRate { get; set; }

		public DateTime DateMotDue { get; set; }

		public string ManufacturerCode { get; set; }
		public Manufacturer Manufacturer { get; set; }

		public string ModelCode { get; set; }
		public Model Model { get; set; }

		public string VehicleCategoryCode { get; set; }
		public VehicleCategory VehicleCategory { get; set; }

	}
}
