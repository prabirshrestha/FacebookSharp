namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static partial class FacebookUtils
    {

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
            return OpenUrl(url, method, parameters, "FacebookSharp", true);
        }

#if !SILVERLIGHT
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
                url = url + "?" + EncodeDictionaryUrl(parameters);
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

                byte[] data = Encoding.UTF8.GetBytes(EncodeDictionaryUrl(parameters));
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
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        return reader.ReadToEnd();
                    }
                }
                throw new FacebookSharpException("Unknown Error occured when communicating with facebook.", ex);
            }
        }
#endif

#if SILVERLIGHT
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
            throw new NotImplementedException();
        }

#endif

        #region Json Converter Utils

        /// <summary>
        /// Parse Json string to IDictionary&lt;string, object>.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IDictionary<string, object> FromJson(string json)
        {
            return JsonParser.FromJson(json);
        }

        /// <summary>
        /// Parse Json string to IDictionary&lt;string, object>. 
        /// </summary>
        /// <param name="json"></param>
        /// <param name="throwFacebookException"></param>
        /// <returns></returns>
        public static IDictionary<string, object> FromJson(string json, bool throwFacebookException)
        {
            IDictionary<string, object> jsonBag = FromJson(json);

            FacebookException ex = ToFacebookException(json);

            if (!throwFacebookException)    // i think sometimes, it shouldn't throw error,
                return jsonBag;             // rather user should have more control over the behavior.

            if (ex != null)
                throw ex;

            return jsonBag;
        }


        /// <summary>
        /// Parse IDictionary&lt;string, object> to json string.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string ToJson(IDictionary<string, object> bag)
        {
            return JsonParser.ToJson(bag);
        }
        

        #endregion

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
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("use FromJson method instead.")]
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
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [Obsolete("use FromJson method instead.")]
        public static JToken ParseJson(string response, bool throwException)
        {
            JToken json;

            FacebookException ex = ToFacebookException(response, out json);

            if (!throwException) // i think sometimes, it shouldn't throw error,
                return json;     // rather user should have more control over the behavior.

            if (ex != null)
                throw ex;

            return json;
        }

        /// <summary>
        /// Deserializes the specified object to JSON object.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize.</typeparam>
        /// <param name="json">The object to deserialize.</param>
        /// <returns>Deserialized object.</returns>
        public static T DeserializeObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Serializes the specified object ot JSON string.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        /// <returns>Serialized json string.</returns>
        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}