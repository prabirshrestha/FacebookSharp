namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;
    using RestSharp.Extensions;

    public static partial class FacebookUtils
    {
        #region Url Encode/Decode Methods

        public static string UrlDecode(string str)
        {
            return str.UrlDecode();
        }

        public static string UrlEncode(string str)
        {
            return str.UrlEncode();
        }

        #endregion

        #region Html Encode/Decode Methods

        public static string HtmlDecode(string s)
        {
            return s.HtmlDecode();
        }

        public static string HtmlEncode(string s)
        {
            return s.HtmlEncode();
        }

        #endregion

        #region FacebookSharp Helper methods for parsing QueryString

        /// <summary>
        /// Parse a URL query and fragment parameters into a key-value bundle.
        /// </summary>
        /// <param name="query">
        /// The URL query to parse.
        /// </param>
        /// <returns>
        /// Returns a dictionary of keys and values for the querystring.
        /// </returns>
        public static IDictionary<string, List<string>> ParseUrlQueryString(string query)
        {
            var result = new Dictionary<string, List<string>>();

            if (string.IsNullOrEmpty(query))
                return result;

            string decoded = HtmlDecode(query);
            int decodedLength = decoded.Length;
            int namePos = 0;
            bool first = true;

            while (namePos <= decodedLength)
            {
                int valuePos = -1, valueEnd = -1;
                for (int q = namePos; q < decodedLength; q++)
                {
                    if (valuePos == -1 && decoded[q] == '=')
                    {
                        valuePos = q + 1;
                    }
                    else if (decoded[q] == '&')
                    {
                        valueEnd = q;
                        break;
                    }
                }

                if (first)
                {
                    first = false;
                    if (decoded[namePos] == '?')
                        namePos++;
                }

                string name, value;
                if (valuePos == -1)
                {
                    name = null;
                    valuePos = namePos;
                }
                else
                {
                    name = UrlDecode(decoded.Substring(namePos, valuePos - namePos - 1));
                }
                if (valueEnd < 0)
                {
                    namePos = -1;
                    valueEnd = decoded.Length;
                }
                else
                {
                    namePos = valueEnd + 1;
                }
                value = UrlDecode(decoded.Substring(valuePos, valueEnd - valuePos));

                if (name != null)
                {
                    if (result.ContainsKey(name))
                        result["name"].Add(value);
                    else
                        result.Add(name, new List<string> { value });
                }

                if (namePos == -1)
                    break;
            }

            return result;
        }

        /// <summary>
        /// Parse a URL query and fragment parameters into a key-value bundle.
        /// </summary>
        /// <param name="uri">
        /// The uri to parse
        /// </param>
        /// <returns>
        /// Returns a dictionary of keys and values for the querystring.
        /// </returns>
        public static IDictionary<string, List<string>> ParseUrlQueryString(Uri uri)
        {
            return ParseUrlQueryString(uri.Query);
        }

        #endregion

    }
}