namespace FacebookSharp
{
    /// <summary>
    /// </summary>
    public class UrlNotProperlyFormattedException : FacebookException
    {
        public UrlNotProperlyFormattedException(string message, string type, int code)
            : base(message, type, code)
        {
        }
    }
}