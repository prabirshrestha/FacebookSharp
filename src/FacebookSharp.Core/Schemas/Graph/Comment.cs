namespace FacebookSharp.Schemas.Graph
{
    using System.Diagnostics.CodeAnalysis;
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
        public string CreatedTime { get; set; }
    }

    /// <summary>
    /// List of Facebook comments
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
        Justification = "Reviewed. Suppression is OK here."), DataContract]
    public class CommentCollection : Connection<Comment>
    {
        /// <summary>
        /// Gets or sets Total number of comments posted.
        /// </summary>
        [DataMember(Name = "count")]
        public int TotalComments { get; set; }
    }
}