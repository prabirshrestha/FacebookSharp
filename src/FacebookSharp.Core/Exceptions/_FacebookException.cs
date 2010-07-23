
namespace FacebookSharp
{
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
            return FacebookUtils.ToFacebookException(jsonString);
        }
    }
}