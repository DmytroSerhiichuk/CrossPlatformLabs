using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab_6.Models
{
	public class VehicleCategory
	{
		[Key]
		[Column(TypeName = "char(5)")]
		public string Code { get; set; }
		[Column(TypeName = "char(10)")]
		public string Description { get; set; }

		public ICollection<Vehicle> Vehicles { get; set; }
	}
}
