namespace Lab_9.DTO
{
	public class BookingDTO
	{
		public int Id { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		public string IsConfirmationLetterSent { get; set; }
		public string IsPaymentReceived { get; set; }
		public string BookingStatusCode { get; set; }
		public string VehicleRegNumber { get; set; }
		public int CustomerId { get; set; }
	}
}
