namespace FacebookSharp
{
    using System;

    /// <summary>
    /// Generic exception for Facebook Sharp Library.
    /// </summary>
    public class FacebookSharpException : Exception
    {
        public FacebookSharpException()
        {

        }

        public FacebookSharpException(string message)
            : base(message)
        {

        }

        public FacebookSharpException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}