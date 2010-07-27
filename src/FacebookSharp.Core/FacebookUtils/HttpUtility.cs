namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;
    using System.Text;

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

        #region FacebookSharp Helper methods for paramaters encode/decode

        /*
         * NOTE: EncodeDictionaryUrl,DecodeDictionaryUrl and ParseUrlQueryString methods are marked for deletion 
         * most probably won't require it coz RestSharp does it internally.
         * 
         */

        [Obsolete("This method is marked for deletion in future release.")]
        public static string EncodeDictionaryUrl(IDictionary<string, string> parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return "";
            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (KeyValuePair<string, string> pair in parameters)
            {
                if (first)
                    first = false;
                else
                    sb.Append("&");
                sb.AppendFormat("{0}={1}", UrlEncode(pair.Key), UrlEncode(pair.Value));
            }
            return sb.ToString();
        }

        [Obsolete("This method is marked for deletion in future release.")]
        public static IDictionary<string, string> DecodeDictionaryUrl(string s)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(s))
                return parameters;

            string[] array = s.Split('&');
            foreach (string parameter in array)
            {
                string[] pair = parameter.Split('=');
                if (pair[0].StartsWith("#") || pair[0].StartsWith("?"))
                    pair[0] = pair[0].Substring(1, pair[0].Length - 1);
                parameters.Add(UrlDecode(pair[0]), UrlDecode(pair[1]));
            }
            return parameters;
        }

        /// <summary>
        /// Parse a URL query and fragment parameters into a key-value bundle.
        /// </summary>
        /// <param name="url">The URL to parse</param>
        /// <returns>Returns a dictionary of keys and values.</returns>
        [Obsolete("This method is marked for deletion in future release.")]
        public static IDictionary<string, string> ParseUrlQueryString(string url)
        {
            // hack to prevent MalformedURLException
            url = url.Replace("fbconnect", "http");
            try
            {
                Uri u = new Uri(url);
                IDictionary<string, string> b = DecodeDictionaryUrl(u.Query); // need to test this method.
                return b;
            }
            catch (Exception ex)
            {   // todo: need to catch the invalid url exception.
                return new Dictionary<string, string>();
            }
        }

        #endregion

    }
}