
namespace FacebookSharp
{

#if !SILVERLIGHT
    using RestSharp.Contrib;
#endif
#if SILVERLIGHT
    using System.Windows.Browser;
#endif

    public static partial class FacebookUtils
    {
        #region Url Encode/Decode Methods

        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        #endregion

        #region Html Encode/Decode Methods

        public static string HtmlDecode(string s)
        {
            return HttpUtility.HtmlDecode(s);
        }

        public static string HtmlEncode(string s)
        {
            return HttpUtility.HtmlEncode(s);
        }

        #endregion

    }
}