using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab_6.Models
{
	public class BookingStatus
	{
		[Key]
		[Column(TypeName = "char(3)")]
		public string Code { get; set; }

		[Column(TypeName = "char(10)")]
		public string Description { get; set; }

		public ICollection<Booking>? Bookings { get; set; }
	}
}
