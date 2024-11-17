namespace Lab_5.Models
{
	public class Model
	{
		public string Code { get; set; }

		public decimal DailyHireRate { get; set; }

		public string Name { get; set; }

		public ICollection<Vehicle> Vehicles { get; set; }
	}
}
