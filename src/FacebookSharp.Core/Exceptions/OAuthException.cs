namespace FacebookSharp
{
    public class OAuthException : FacebookException
    {
        public OAuthException()
            : this("Error processing access token.")
        {

        }

        public OAuthException(string message)
            : this(message, 0)
        {
        }

        public OAuthException(string message, int code)
            : base(message, "OAuthException", code)
        {

        }
    }
}