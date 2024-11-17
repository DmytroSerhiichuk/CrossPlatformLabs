namespace Lab_5.Models
{
	public class BookingStatus
	{
		public string Code { get; set; }

		public string Description { get; set; }

		public ICollection<Booking> Bookings { get; set; }
	}
}
