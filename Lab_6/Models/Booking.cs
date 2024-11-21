using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab_6.Models
{
	public class Booking
	{
		[Key]
		public int Id { get; set; }
		
		[Column(TypeName = "date")]
		public DateTime DateFrom { get; set; }
		
		[Column(TypeName = "date")]
		public DateTime DateTo { get; set; }
		
		[Column(TypeName = "char(1)")]
		public string IsConfirmationLetterSent { get; set; }
		
		[Column(TypeName = "char(1)")]
		public string IsPaymentReceived { get; set; }

		[Column(TypeName = "char(3)")]
		public string BookingStatusCode { get; set; }
		public BookingStatus? BookingStatus { get; set; }

		[Column(TypeName = "char(10)")]
		public string VehicleRegNumber { get; set; }
		public Vehicle? Vehicle { get; set; }

		public int CustomerId { get; set; }
		public Customer? Customer { get; set; }
	}
}
