namespace Lab_6.DTO
{
	public class BookingDetailedDTO
	{
		public int Id { get; set; }
		public DateTime DateFrom { get; set; }
		public DateTime DateTo { get; set; }
		public string IsConfirmationLetterSent { get; set; }
		public string IsPaymentReceived { get; set; }
		public BookingStatusDTO BookingStatus { get; set; }
		public VehicleDTO Vehicle { get; set; }
		public CustomerDTO Customer { get; set; }
	}
}
