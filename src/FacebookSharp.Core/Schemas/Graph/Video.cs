namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the Facebook Video Graph API type.
    /// </summary>
    [DataContract]
    public class Video : GraphObject
    {
        /// <summary>
        /// Gets or sets an object containing the name and id of the user who posted the video.
        /// </summary>
        public From From { get; set; }

        /// <summary>
        /// Gets or sets the video title/caption.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the long-form HTML description of the video.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the time the video was initially published.
        /// </summary>
        public string CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the last time the video or its caption were updated.
        /// </summary>
        public string UpdatedTime { get; set; }
    }
}