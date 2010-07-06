namespace FacebookSharp
{
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
        }
    }
}