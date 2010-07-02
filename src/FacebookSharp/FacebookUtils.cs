using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Text;

namespace FacebookSharp
{
    public static class FacebookUtils
    {
        public static string EncodeUrl(IDictionary<string, string> parameters)
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
                sb.AppendFormat("{0}={1}", pair.Key, pair.Value);
            }
            return sb.ToString();
        }

        public static IDictionary<string, string> DecodeUrl(string s)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Parse a URL query and fragment parameters into a key-value bundle.
        /// </summary>
        /// <param name="url">The URL to parse</param>
        /// <returns>Returns a dictionary of keys and values.</returns>
        public static IDictionary<string, string> ParseUrl(string url)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Connect to an HTTP url and return the response as a string.
        /// </summary>
        /// <param name="url">The resource to open: must be a welformed URL</param>
        /// <param name="method">The HTTP method to use ("GET", "POST", etc.)</param>
        /// <param name="parameters">The query parameter for the URL (e.g. access_token=foo)</param>
        /// <returns>The URL contents as a string.</returns>
        /// <remarks>
        /// Note that the HTTP method override is used on non-GET requests.
        /// (i.e. requests are made as "POST" with method specified in the body).
        /// </remarks>
        public static string OpenUrl(string url, string method, IDictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Parse a server response into a JSON object.
        /// </summary>
        /// <param name="response">String representation of the response.</param>
        /// <returns>Returns the response as a JSON object.</returns>
        /// <remarks>
        /// This is a basic implementation using Newtonsoft.JSON.
        /// 
        /// The parsed JSON is checked for a variety of error fields and
        /// a <see cref="FacebookException"/> is thrown if an error condition is set,
        /// populated with the error message and error type or code if available. 
        /// </remarks>
        public static JObject ParseJson(string response)
        {
            throw new NotImplementedException();
        }
    }
}