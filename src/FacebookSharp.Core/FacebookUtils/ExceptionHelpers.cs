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
        /// <param name="response">Json string</param>
        public static void ThrowIfFacebookException(string response)
        {
            var ex = ToFacebookException(response);
            if (ex != null)
                throw ex;
        }

        /// <summary>
        /// Converts the json string to FacebookException.
        /// </summary>
        /// <param name="response">Json string</param>
        /// <returns>Returns an instance of FacebookException if the json string contatins exception, otherwise null.</returns>
        public static FacebookException ToFacebookException(string response)
        {
            JToken tmp;
            return ToFacebookException(response, out tmp);
        }

        public static FacebookException ToFacebookException(string response, out JToken json)
        {
            using (StringReader reader = new StringReader(response))
            {
                using (JsonTextReader jsonTextReader = new JsonTextReader(reader))
                {
                    json = JToken.ReadFrom(jsonTextReader);
                }
            }

            // edge case: when sending a POST request to /[post_id]/likes
            // the return value is 'true' or 'false'.
            // just throw normal FacebookException.
            if (response.Equals("false", StringComparison.OrdinalIgnoreCase))
                throw new FacebookException("request failed.");
            if (response.Equals("true", StringComparison.OrdinalIgnoreCase))
                response = "{value:true}";

            JToken error = json["error"];
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
