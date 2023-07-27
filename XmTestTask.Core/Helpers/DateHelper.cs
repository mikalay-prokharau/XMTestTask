namespace XmTestTask.Core.Helpers
{
    public static class DateHelper
    {
        public static long ConvertIntDateToUnix(int date)
        {
            var dateAsDateTime = DateTime.ParseExact(date.ToString(), "yyyyMMddHH",
                                       System.Globalization.CultureInfo.InvariantCulture);
            dateAsDateTime = DateTime.SpecifyKind(dateAsDateTime, DateTimeKind.Utc);
            return ((DateTimeOffset)dateAsDateTime).ToUnixTimeSeconds();
        }

        public static long ConvertIntDateToUnixInMilliseconds(int date)
        {
            return ConvertIntDateToUnix(date) * 1000;
        }

        public static int ConvertUnixDateToInt(long date)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(date);

            return int.Parse(dateTimeOffset.ToString("yyyyMMddHH",
                                       System.Globalization.CultureInfo.InvariantCulture));
        }

        public static bool IsUnixDateHasHourAccuracy(string? date)
        {
            if (!long.TryParse(date?.ToString(), out long longDate))
                return false;

            DateTimeOffset startDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(longDate);
            return (startDateTimeOffset.UtcDateTime.Ticks % TimeSpan.TicksPerHour) == 0;
        }
    }
}
