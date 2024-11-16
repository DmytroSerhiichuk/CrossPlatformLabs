using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab_6.Models
{
	public class Model
	{
		[Key]
		[Column(TypeName = "char(10)")]
		public string Code { get; set; }

		[Column(TypeName = "decimal(8, 2)")]
		public decimal DailyHireRate { get; set; }

		[Column(TypeName = "varchar(200)")]
		public string Name { get; set; }

		public ICollection<Vehicle> Vehicles { get; set; }
	}
}
