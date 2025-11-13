using System.Globalization;

namespace Shared.Utilities
{
    public static class NumberUtility
    {
        public static string FormatWithSeparators(long number, string separator = ",")
        {
            var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            culture.NumberFormat.NumberGroupSeparator = separator;
            return number.ToString("N0", culture);
        }

        public static string FormatWithSeparators(decimal number, string separator = ",")
        {
            var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
            culture.NumberFormat.NumberGroupSeparator = separator;
            return number.ToString("N0", culture);
        }
    }
}
