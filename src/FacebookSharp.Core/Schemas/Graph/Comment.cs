namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the Facebook Comment Graph API type.
    /// </summary>
    [DataContract]
    public class Comment : GraphObject
    {
        /// <summary>
        /// Gets or sets the user who posted the comment.
        /// </summary>
        [DataMember(Name = "from")]
        public From From { get; set; }

        /// <summary>
        /// Gets or sets the text contents of the comment.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the date on which the comment was created.
        /// </summary>
        [DataMember(Name = "created_time")]
        public string Created_Time { get; set; }
    }

    public class CommentCollection : Connection<Comment>
    {
    }
}