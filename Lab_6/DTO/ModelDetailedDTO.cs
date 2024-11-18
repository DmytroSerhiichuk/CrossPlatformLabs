namespace Lab_6.DTO
{
	public class ModelDetailedDTO
	{
		public string Code { get; set; }
		public decimal DailyHireRate { get; set; }
		public string Name { get; set; }
		public IEnumerable<VehicleDTO> Vehicles { get; set; }
	}
}
