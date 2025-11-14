namespace CMS.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Rounds the given DateTime to the nearest 5 minutes.
        /// </summary>
        /// <param name="dateTime">The DateTime to round.</param>
        /// <returns>A new DateTime rounded to the nearest 5 minutes.</returns>
        public static DateTime RoundToFiveMinutes(this DateTime dateTime)
        {
            int minutes = (int)(dateTime.Minute / 5) * 5;
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, minutes, 0);
        }

        /// <summary>
        /// Rounds the given nullable DateTime to the nearest 5 minutes.
        /// If the DateTime is null, it returns null.
        /// </summary>
        /// <param name="dateTime">The nullable DateTime to round.</param>
        /// <returns>A new DateTime rounded to the nearest 5 minutes or null if the input is null.</returns>
        public static DateTime? RoundToFiveMinutes(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }

            return dateTime.Value.RoundToFiveMinutes();
        }
    }
}
