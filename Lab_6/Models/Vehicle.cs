using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab_6.Models
{
	public class Vehicle
	{
		[Key]
		[Column(TypeName = "char(10)")]
		public string RegNumber { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public int CurrentMileage { get; set; }

		[Column(TypeName = "decimal(8, 2)")]
		public decimal DailyHireRate { get; set; }

		[Column(TypeName = "date")]
		public DateTime DateMotDue { get; set; }

		[Column(TypeName = "char(10)")]
		public string ManufacturerCode { get; set; }
		public Manufacturer Manufacturer { get; set; }

		[Column(TypeName = "char(10)")]
		public string ModelCode { get; set; }
		public Model Model { get; set; }

		[Column(TypeName = "char(5)")]
		public string VehicleCategoryCode { get; set; }
		public VehicleCategory VehicleCategory { get; set; }

	}
}
