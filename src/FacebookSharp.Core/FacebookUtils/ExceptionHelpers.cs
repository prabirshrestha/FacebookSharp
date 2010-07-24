namespace FacebookSharp
{
    using System;
    using System.IO;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static partial class FacebookUtils
    {
        /// <summary>
        /// Throws an exception if the json string contains facebook exception.
        /// </summary>
        /// <param name="jsonString">Json string</param>
        public static void ThrowIfFacebookException(string jsonString)
        {
            var ex = (FacebookException)jsonString;
            if (ex != null)
                throw ex;
        }

        /// <summary>
        /// Converts the json string to FacebookException.
        /// </summary>
        /// <param name="jsonString">Json string</param>
        /// <returns>Returns an instance of FacebookException if the json string contatins exception, otherwise null.</returns>
        [Obsolete("Use explicit casting to FacebookException instead. var ex = (FacebookException)jsonString;")]
        public static FacebookException ToFacebookException(string jsonString)
        {
            JToken tmp;
            return ToFacebookException(jsonString, out tmp);
        }

        [Obsolete("Try not to use this method. Most probably will be removed in future version.")]
        public static FacebookException ToFacebookException(string jsonString, out JToken json)
        {
            using (StringReader reader = new StringReader(jsonString))
            {
                using (JsonTextReader jsonTextReader = new JsonTextReader(reader))
                {
                    try
                    {
                        json = JToken.ReadFrom(jsonTextReader);
                    }
                    catch (JsonReaderException exception)
                    {
                        json = null;
                        jsonString = "true"; // todo: need to fix this: wat if input jsonString is not json?
                    }
                }
            }
            if (json == null)
                return null;

            // edge case: when sending a POST request to /[post_id]/likes
            // the return value is 'true' or 'false'.
            // just throw normal FacebookException.
            if (jsonString.Equals("false", StringComparison.OrdinalIgnoreCase))
                throw new FacebookException("request failed.");
            if (jsonString.Equals("true", StringComparison.OrdinalIgnoreCase))
                jsonString = "{value:true}";

            JToken error = json.SelectToken("error", false);
            if (error != null)
            {
                string type = error.Value<string>("type");
                string message = error.Value<string>("message");

                switch (type)
                {
                    case "OAuthException":
                        return new OAuthException(message);
                    case "QueryParseException":
                        return new QueryParseException(message);
                }

                // Just return generic if couldn't resolve. 
                // todo: add more exceptions
                return new FacebookException(message, type, 0);
            }

            JToken errorCode = json["error_code"];
            JToken errorMsg = json["error_msg"];

            if (errorCode != null && errorMsg != null)
                return new FacebookException(errorMsg.Value<string>(), string.Empty, int.Parse(errorCode.Value<string>()));
            if (errorCode != null)
                return new FacebookException("request faild", string.Empty, int.Parse(errorCode.Value<string>()));
            if (errorMsg != null)
                return new FacebookException(errorMsg.Value<string>());

            JToken errorReason = json["error_reason"];
            if (errorReason != null)
                return new FacebookException(errorReason.Value<string>());

            return null;
        }
    }
}
