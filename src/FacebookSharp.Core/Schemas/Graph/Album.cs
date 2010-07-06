namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the Album Graph API type.
    /// </summary>
    [DataContract]
    public class Album : NamedGraphObject
    {
        /// <summary>
        /// Gets or sets From.
        /// </summary>
        [DataMember(Name = "from")]
        public From From { get; set; }

        /// <summary>
        /// Gets or sets the description of the album.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the location of the album.
        /// </summary>
        [DataMember(Name = "location")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the link to this album on Facebook.
        /// </summary>
        [DataMember(Name = "link")]
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the number of photos in this album.
        /// </summary>
        [DataMember(Name = "count")]
        public long Count { get; set; }

        /// <summary>
        /// Gets or sets the time the photo album was initially created.
        /// </summary>
        [DataMember(Name = "created_time")]
        public string CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the last time the photo album was updated.
        /// </summary>
        [DataMember(Name = "updated_time")]
        public string UpdatedTime { get; set; }
    }
}