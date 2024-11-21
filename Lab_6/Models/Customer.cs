using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab_6.Models
{
	public class Customer
	{
		[Key]
        public int Id{ get; set; }

        public ICollection<Booking>? Bookings { get; set; }


        [Column(TypeName = "varchar(255)")]
		public string Name { get; set; }

		[Column(TypeName = "varchar(2000)")]
		public string? Details { get; set; }

		[Column(TypeName = "char(1)")]
		public string Gender{ get; set; }

		[Column(TypeName = "varchar(255)")]
		public string Email { get; set; }

		[Column(TypeName = "varchar(50)")]
		public string Phone { get; set; }

		[Column(TypeName = "varchar(255)")]
		public string AddressLine1 { get; set; }

		[Column(TypeName = "varchar(255)")]
		public string? AddressLine2 { get; set; }

		[Column(TypeName = "varchar(255)")]
		public string? AddressLine3 { get; set; }

		[Column(TypeName = "varchar(50)")]
		public string Town { get; set; }

		[Column(TypeName = "varchar(30)")]
		public string County { get; set; }

		[Column(TypeName = "varchar(30)")]
		public string Country { get; set; }
	}
}
