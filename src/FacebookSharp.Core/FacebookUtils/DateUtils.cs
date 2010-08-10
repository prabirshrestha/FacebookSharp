namespace FacebookSharp
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public static partial class FacebookUtils
    {
        public static class Date
        {
            [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore",
                Justification = "Reviewed. Suppression is OK here.")]
            public const string FACEBOOK_LONG_DATE_FORMAT = "yyyy-MM-dd'T'kk:mm:ssZ";

            [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore",
                Justification = "Reviewed. Suppression is OK here.")]
            public const string FACEBOOK_SHORT_DATE_FORMAT = "MMY/dd/yyyy";

            /// <summary>
            /// Converts to specified <see cref="DateTime"/> to ISO-8601 format (yyyy-MM-ddTHH:mm:ssZ).
            /// </summary>
            /// <param name="dateTime">
            /// The date time.
            /// </param>
            /// <returns>
            /// Returns the string representation of date time in ISO-8601 format (yyyy-MM-ddTHH:mm:ssZ).
            /// </returns>
            public static string ToIso8601FormattedDateTime(DateTime dateTime)
            {
                return dateTime.ToString("o");
            }

            /// <summary>
            /// Converts ISO-8601 format (yyyy-MM-ddTHH:mm:ssZ) date time to <see cref="DateTime"/>.
            /// </summary>
            /// <param name="iso8601DateTime">
            /// The iso 8601 formatted date time.
            /// </param>
            /// <returns>
            /// Returns the <see cref="DateTime"/> equivalent to the ISO-8601 formatted date time. 
            /// </returns>
            public static DateTime FromIso8601FormattedDateTime(string iso8601DateTime)
            {
                return DateTime.ParseExact(iso8601DateTime, "o", System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}