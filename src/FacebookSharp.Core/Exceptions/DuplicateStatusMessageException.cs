namespace FacebookSharp
{
    /// <summary>
    /// Exception thrown by Facebook when duplicate status message is posted.
    /// </summary>
    public class DuplicateStatusMessageException : FacebookException
    {
        internal const string MESSAGE = "(#506) Duplicate status message";

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateStatusMessageException"/> class.
        /// </summary>
        public DuplicateStatusMessageException()
            : this(MESSAGE)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateStatusMessageException"/> class.
        /// </summary>
        /// <param name="message">
        /// The error message message.
        /// </param>
        public DuplicateStatusMessageException(string message) :
            base(message, "Exception", 506)
        {

        }
    }
}