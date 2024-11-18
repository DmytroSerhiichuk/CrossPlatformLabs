namespace Lab_5.ResponseModels
{
	public class BookingStatusResponseDetailed
	{
		public string Code { get; set; }
		public string Description { get; set; }
		public IEnumerable<BookingResponse> Bookings { get; set; }
	}
}
