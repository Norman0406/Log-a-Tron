namespace Helpers;

public static class DateTimeExtensions
{
    public static DateTime ToDate(this DateTime dateTime)
    {
        return dateTime.Date;
    }

    public static TimeSpan ToTime(this DateTime dateTime)
    {
        return dateTime.TimeOfDay;
    }

    public static DateTime FromDateAndTime(DateTime date, TimeSpan time)
    {
        return date + time;
    }
}
