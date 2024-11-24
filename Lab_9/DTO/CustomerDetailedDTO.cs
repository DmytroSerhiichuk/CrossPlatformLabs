namespace Lab_9.DTO
{
	public class CustomerDetailedDTO
	{
		public int Id { get; set; }
		public IEnumerable<BookingDTO> Bookings { get; set; }
		public string Name { get; set; }
		public string Details { get; set; }
		public string Gender { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string AddressLine3 { get; set; }
		public string Town { get; set; }
		public string County { get; set; }
		public string Country { get; set; }
	}
}
