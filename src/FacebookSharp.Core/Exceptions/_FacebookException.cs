
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
    }
}