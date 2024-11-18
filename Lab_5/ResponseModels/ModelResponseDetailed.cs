namespace Lab_5.ResponseModels
{
	public class ModelResponseDetailed
	{
		public string Code { get; set; }
		public decimal DailyHireRate { get; set; }
		public string Name { get; set; }
		public IEnumerable<VehicleResponse> Vehicles { get; set; }
	}
}
