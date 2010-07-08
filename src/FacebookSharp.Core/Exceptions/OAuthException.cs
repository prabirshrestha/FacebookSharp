namespace FacebookSharp
{
    public class OAuthException : FacebookException
    {
        public OAuthException()
            : this("Error processing access token.")
        {

        }
        public OAuthException(string message)
            : base(message, "OAuthException", 0)
        {
        }
    }
}