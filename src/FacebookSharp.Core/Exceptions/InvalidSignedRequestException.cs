namespace FacebookSharp
{
    /// <summary>
    /// Exception thrown when the signed_request is not valid.
    /// </summary>
    public class InvalidSignedRequestException : FacebookSharpException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSignedRequestException"/> class.
        /// </summary>
        public InvalidSignedRequestException()
            : this("signed_request validation failed")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSignedRequestException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public InvalidSignedRequestException(string message)
            : base(message)
        {
        }
    }
}