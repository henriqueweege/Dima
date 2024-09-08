namespace Dima.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetStartDay(this DateTime dateTime, int? year = null, int? month = null)
            => new(year ?? dateTime.Year, month ?? dateTime.Month, 1);

        public static DateTime GetEndDay(this DateTime dateTime, int? year = null, int? month = null)
            => new DateTime(year ?? dateTime.Year, month ?? dateTime.Month, 1).AddMonths(1).AddDays(-1);
    }
}
