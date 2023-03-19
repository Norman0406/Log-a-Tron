using System.Globalization;

namespace Logatron.QrzDotCom
{
    internal static class Util
    {
        /**
         * So, QRZ.com apparently uses two different time formats. In the session fields "SubExp" and "Time", a
         * string in the form of "Wed Mar 15 19:25:07 2023" is returned.
         * In the callsign date fields, a string in the form of "2023-01-03 21:30:19" is returned.
         */

        public static DateTime? ParseDateTimeSession(string? dateTimeString) => dateTimeString != null
            ? DateTime.ParseExact(dateTimeString, "ddd MMM dd HH:mm:ss yyyy", CultureInfo.InvariantCulture)
            : null;

        public static DateTime? ParseDateTimeCallsign(string? dateTimeString) => dateTimeString != null
            ? DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture)
            : null;
    }
}
