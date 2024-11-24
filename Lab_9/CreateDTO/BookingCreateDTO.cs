namespace Lab_9.CreateDTO
{
	public class BookingCreateDTO
	{
		public int Id { get; set; }
		public DateTime DateFrom { get; set; } = DateTime.Now;
		public DateTime DateTo { get; set; } = DateTime.Now;
		public string IsConfirmationLetterSent { get; set; }
		public string IsPaymentReceived { get; set; }
		public string BookingStatusCode { get; set; }
		public string VehicleRegNumber { get; set; }
		public int CustomerId { get; set; }
	}
}
