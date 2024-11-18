namespace Lab_5.ResponseModels
{
	public class BookingResponseDetailed
	{
		public int Id { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		public string IsConfirmationLetterSent { get; set; }
		public string IsPaymentReceived { get; set; }
		public BookingStatusResponse BookingStatus { get; set; }
		public VehicleResponse Vehicle { get; set; }
		public CustomerResponse Customer { get; set; }
	}
}
