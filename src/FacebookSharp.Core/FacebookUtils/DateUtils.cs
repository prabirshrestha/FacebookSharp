namespace FacebookSharp
{
    using System.Diagnostics.CodeAnalysis;
    using System;

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
        }
    }
}