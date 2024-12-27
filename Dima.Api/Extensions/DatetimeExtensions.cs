namespace Dima.Api.Extensions
{
    public static class DatetimeExtensions
    {

        public static DateOnly ToDateOnly(this DateTime dateTime)
        {
            return new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
        }
    }
}
