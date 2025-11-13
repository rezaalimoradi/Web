using System;
using System.Globalization;

public static class DateTimeUtils
{
    private static readonly PersianCalendar _persianCalendar = new PersianCalendar();

    public static string ToPersianDate(DateTime gregorianDate)
    {
        int year = _persianCalendar.GetYear(gregorianDate);
        int month = _persianCalendar.GetMonth(gregorianDate);
        int day = _persianCalendar.GetDayOfMonth(gregorianDate);

        return $"{year:0000}/{month:00}/{day:00}";
    }
}
