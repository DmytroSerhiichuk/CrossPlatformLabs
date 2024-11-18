namespace Lab_5.DTO
{
	public class VehicleDTO
	{
		public string RegNumber { get; set; }
		public int BookingCount { get; set; }
		public int CurrentMileage { get; set; }
		public decimal DailyHireRate { get; set; }
		public DateTime DateMotDue { get; set; }
		public string ManufacturerCode { get; set; }
		public string ModelCode { get; set; }
		public string VehicleCategoryCode { get; set; }
	}
}
