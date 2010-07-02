using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(s))
                return parameters;

            string[] array = s.Split('&');
            foreach (string parameter in array)
            {
                string[] pair = parameter.Split('=');
                parameters.Add(pair[0], pair[1]);
            }
            return parameters;
        }

        /// <summary>
        /// Parse a URL query and fragment parameters into a key-value bundle.
        /// </summary>
        /// <param name="url">The URL to parse</param>
        /// <returns>Returns a dictionary of keys and values.</returns>
        public static IDictionary<string, string> ParseUrl(string url)
        {
            // hack to prevent MalformedURLException
            url = url.Replace("fbconnect", "http");
            try
            {
                Uri u = new Uri(url);
                IDictionary<string, string> b = DecodeUrl(u.Query); // need to test this method.
                return b;
            }
            catch (Exception ex)
            {   // todo: need to catch the invalid url exception.
                return new Dictionary<string, string>();
            }
        }

        /// <summary>
        /// Connect to an HTTP url and return the response as a string.
        /// </summary>
        /// <param name="url">The resource to open: must be a welformed URL</param>
        /// <param name="method">The HTTP method to use ("GET", "POST", etc.)</param>
        /// <param name="parameters">The query parameter for the URL (e.g. access_token=foo)</param>
        /// <param name="userAgent">Sets the user agent when opening the url.</param>
        /// <returns>The URL contents as a string.</returns>
        /// <remarks>
        /// Note that the HTTP method override is used on non-GET requests.
        /// (i.e. requests are made as "POST" with method specified in the body).
        /// </remarks>
        public static string OpenUrl(string url, string method, IDictionary<string, string> parameters)
        {   // this is the default method signature (same method arguments) as in facebook android sdk.
            return OpenUrl(url, method, parameters, "FacebookSharp", false);
        }

        /// <summary>
        /// Connect to an HTTP url and return the response as a string.
        /// </summary>
        /// <param name="url">The resource to open: must be a welformed URL</param>
        /// <param name="method">The HTTP method to use ("GET", "POST", etc.)</param>
        /// <param name="parameters">The query parameter for the URL (e.g. access_token=foo)</param>
        /// <param name="userAgent">Sets the user agent when opening the url.</param>
        /// <param name="compressHttp">If true adds Accept-Encoding as "gzip,deflate".</param>
        /// <returns>The URL contents as a string.</returns>
        /// <remarks>
        /// Note that the HTTP method override is used on non-GET requests.
        /// (i.e. requests are made as "POST" with method specified in the body).
        /// </remarks>
        public static string OpenUrl(string url, string method, IDictionary<string, string> parameters, string userAgent, bool compressHttp)
        {
            if (method.Equals("GET", StringComparison.OrdinalIgnoreCase))
                url = url + "?" + EncodeUrl(parameters);
            // might be should log this method.

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            if (!string.IsNullOrEmpty(userAgent))
                request.UserAgent = userAgent;

            if (compressHttp)
            {
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            if (!method.Equals("GET", StringComparison.OrdinalIgnoreCase))
            {
                // use method override
                parameters.Add("method", method);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                byte[] data = Encoding.UTF8.GetBytes(EncodeUrl(parameters));
                request.ContentLength = data.Length;

                request.GetRequestStream().Write(data, 0, data.Length);
            }

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (WebException ex)
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
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
        public static JToken ParseJson(string response)
        {
            return ParseJson(response, true); // this is the default behavior in facebook android sdk.
        }

        /// <summary>
        /// Parse a server response into a JSON object.
        /// </summary>
        /// <param name="response">String representation of the response.</param>
        /// <param name="throwException">If set to true, then it converts to FacebookException and throws error, else returns JToken always.</param>
        /// <returns>Returns the response as a JSON object.</returns>
        /// <remarks>
        /// This is a basic implementation using Newtonsoft.JSON.
        /// 
        /// The parsed JSON is checked for a variety of error fields and
        /// a <see cref="FacebookException"/> is thrown if an error condition is set,
        /// populated with the error message and error type or code if available. 
        /// </remarks>
        public static JToken ParseJson(string response, bool throwException)
        {
            JToken json;
            using (StringReader reader = new StringReader(response))
            {
                using (JsonTextReader jsonTextReader = new JsonTextReader(reader))
                {
                    json = JToken.ReadFrom(jsonTextReader);
                }
            }

            if (!throwException) // i think sometimes, it shouldn't throw error,
                return json;     // rather user should have more control over the behavior.

            // todo: need to create a Facebook Exception Parser and throw more specific exceptions.
            // edge case: when sending a POST request to /[post_id]/likes
            // the return value is 'true' or 'false'.
            // just throw normal FacebookException.
            if (response.Equals("false", StringComparison.OrdinalIgnoreCase))
                throw new FacebookException("request failed.");
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
                response = "{value:true}";

            JToken error = json["error"];
            if (error != null)
                throw new FacebookException(error.Value<string>("message"), error.Value<string>("type"), 0);

            JToken errorCode = json["error_code"];
            JToken errorMsg = json["error_msg"];

            if (errorCode != null && errorMsg != null)
                throw new FacebookException(errorMsg.Value<string>(), "", int.Parse(errorCode.Value<string>()));
            if (errorCode != null)
                throw new FacebookException("request faild", "", int.Parse(errorCode.Value<string>()));
            if (errorMsg != null)
                throw new FacebookException(errorMsg.Value<string>());

            JToken errorReason = json["error_reason"];
            if (errorReason != null)
                throw new FacebookException(errorReason.Value<string>());

            return json;
        }
    }
}