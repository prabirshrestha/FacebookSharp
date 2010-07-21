namespace FacebookSharp.Schemas.Graph
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the Facebook Post Graph API
    /// </summary>
    [DataContract]
    public class Post : GraphObject
    {
        /// <summary>
        /// Gets or sets an object containig the id and name of the user who posted the message.
        /// </summary>
        [DataMember(Name = "from")]
        public From From { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets a link to the picture included with this post if available.
        /// </summary>
        [DataMember(Name = "picture")]
        public string Picture { get; set; }

        /// <summary>
        /// Gets or sets the link attached to this post.
        /// </summary>
        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the caption of the link.
        /// </summary>
        [DataMember(Name = "caption")]
        public string Caption { get; set; }

        /// <summary>
        /// Gets or sets the description of the link.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the source link attached to this post if available (for example: a flash or video file).
        /// </summary>
        [DataMember(Name = "source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets a link to an icon representing the type of this post.
        /// </summary>
        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets a string indicating which application was used to create this post.
        /// </summary>
        [DataMember(Name = "attribution")]
        public string Attribution { get; set; }

        /// <summary>
        /// Gets or sets the number of likes on this post.
        /// </summary>
        [DataMember(Name = "likes")]
        public long Likes { get; set; }

        /// <summary>
        /// Gets or sets the time the post was initially published.
        /// </summary>
        [DataMember(Name = "created_time")]
        public string CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the time of the last comment on this post.
        /// </summary>
        [DataMember(Name = "updated_time")]
        public string UpdatedTime { get; set; }

        /// <summary>
        /// Gets or sets the privacy settings for this post.
        /// </summary>
        [DataMember(Name = "privacy")]
        public Privacy Privacy { get; set; }

        /// <summary>
        /// Gets or sets a list of profiles mentioned or targeted in this post.
        /// </summary>
        [DataMember(Name = "to")]
        public FacebookCollection<CategorizedGraphObject> To { get; set; }

        private CommentCollection _comments;


        /// <summary>
        /// Gets or sets the comments for this post.
        /// </summary>
        [DataMember(Name = "comments")]
        public CommentCollection Comments
        {
            get { return _comments ?? (_comments = new CommentCollection()); }
            set { _comments = value; }
        }

        private PostActionCollection _actions;

        /// <summary>
        /// Gets or sets teh actions for this post.
        /// </summary>
        [DataMember(Name = "actions")]
        public PostActionCollection Actions
        {
            get { return _actions ?? (_actions = new PostActionCollection()); }
            set { _actions = value; }
        }

        //todo action
    }

    public class PostCollection : Connection<Post> { }
}