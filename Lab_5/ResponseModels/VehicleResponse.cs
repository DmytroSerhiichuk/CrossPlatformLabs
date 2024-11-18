namespace Lab_5.ResponseModels
{
	public class VehicleResponse
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
