namespace Lab_5.Models
{
	public class Booking
	{
		public int Id { get; set; }
		
		public DateTime DateFrom { get; set; }
		
		public DateTime DateTo { get; set; }
		
		public string IsConfirmationLetterSent { get; set; }
		
		public string IsPaymentReceived { get; set; }

		public string BookingStatusCode { get; set; }
		public BookingStatus BookingStatus { get; set; }

		public string VehicleRegNumber { get; set; }
		public Vehicle Vehicle { get; set; }

		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
	}
}
