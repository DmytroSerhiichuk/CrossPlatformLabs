using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab_6.Models
{
	public class Manufacturer
	{
		[Key]
		[Column(TypeName = "char(10)")]
		public string Code { get; set; }

		[Column(TypeName = "varchar(50)")]
		public string Name { get; set; }

		[Column(TypeName = "varchar(2000)")]
		public string Details { get; set; }

		public ICollection<Vehicle> Vehicles { get; set; }
	}
}
