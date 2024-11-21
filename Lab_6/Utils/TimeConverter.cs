namespace Lab_6.Utils
{
	public static class TimeConverter
	{
		public static DateTime ConvertTime(DateTime time)
		{
			return TimeZoneInfo.ConvertTimeFromUtc(time, TimeZoneInfo.FindSystemTimeZoneById("Europe/Kiev"));
		}
	}
}
