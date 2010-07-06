namespace FacebookSharp.Schemas.Graph
{
    /// <summary>
    /// Represents the Facebook Comment Graph API type.
    /// </summary>
    public class Comment : GraphObject
    {
        /// <summary>
        /// Gets or sets the user who posted the comment.
        /// </summary>
        public From From { get; set; }

        /// <summary>
        /// Gets or sets the text contents of the comment.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the date on which the comment was created.
        /// </summary>
        public string Created_Time { get; set; }
    }

    public class CommentCollection : Connection<Comment> { }
}