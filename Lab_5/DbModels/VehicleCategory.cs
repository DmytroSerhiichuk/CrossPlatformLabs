namespace Lab_5.Models
{
	public class VehicleCategory
	{
		public string Code { get; set; }
		public string Description { get; set; }

		public ICollection<Vehicle> Vehicles { get; set; }
	}
}
