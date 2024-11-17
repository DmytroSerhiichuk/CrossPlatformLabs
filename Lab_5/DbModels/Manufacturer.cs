namespace Lab_5.Models
{
	public class Manufacturer
	{
		public string Code { get; set; }
		public string Name { get; set; }

		public string Details { get; set; }

		public ICollection<Vehicle> Vehicles { get; set; }
	}
}
