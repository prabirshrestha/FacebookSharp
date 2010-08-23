namespace FacebookSharp
{
    using System;
    using System.IO;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Encapsulation of a Facebook Error: a Facebook request that could not be fulfilled.
    /// </summary>
    public class FacebookException : FacebookSharpException
    {
        private readonly int _errorCode;
        private readonly string _errorType;

        public FacebookException(string message)
            : base(message)
        {

        }

        public FacebookException(string message, string type, int code)
            : base(message)
        {
            _errorType = type;
            _errorCode = code;
        }

        public string ErrorType
        {
            get { return _errorType; }
        }

        public int ErrorCode
        {
            get { return _errorCode; }
        }

        /// <summary>
        /// Convert Json string to FacebookException
        /// </summary>
        /// <param name="jsonString">Json string to parse</param>
        /// <returns>
        /// Returns an instance of FacebookException if jsonString is an exception otherwise null.
        /// </returns>
        public static explicit operator FacebookException(string jsonString)
        {
            JToken json;

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
            // don't throw normal FacebookException.
            if (jsonString.Equals("false", StringComparison.OrdinalIgnoreCase))
                jsonString = "{value:false}";
            else if (jsonString.Equals("true", StringComparison.OrdinalIgnoreCase))
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
                    case "Exception":
                        if (message.Equals(DuplicateStatusMessageException.MESSAGE, StringComparison.OrdinalIgnoreCase))
                            return new DuplicateStatusMessageException(message);
                        break;
                }

                // Just return generic if couldn't resolve. 
                // todo: add more exceptions
                return new FacebookException(message, type, 0);
            }

            JToken errorCode = json.SelectToken("error_code", false);
            JToken errorMsg = json.SelectToken("error_msg", false);

            if (errorCode != null && errorMsg != null)
                return new FacebookException(errorMsg.Value<string>(), string.Empty, int.Parse(errorCode.Value<string>()));
            if (errorCode != null)
                return new FacebookException("request faild", string.Empty, int.Parse(errorCode.Value<string>()));
            if (errorMsg != null)
                return new FacebookException(errorMsg.Value<string>());

            JToken errorReason = json.SelectToken("error_reason", false);
            return errorReason != null ? new FacebookException(errorReason.Value<string>()) : null;
        }
    }
}