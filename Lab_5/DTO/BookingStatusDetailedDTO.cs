﻿namespace Lab_5.DTO
{
	public class BookingStatusDetailedDTO
	{
		public string Code { get; set; }
		public string Description { get; set; }
		public IEnumerable<BookingDTO> Bookings { get; set; }
	}
}