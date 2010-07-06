namespace FacebookSharp.Schemas.Graph
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the Facebook Photo Graph API.
    /// </summary>
    [DataContract]
    public class Photo : NamedGraphObject
    {
        /// <summary>
        /// Gets or sets an object containing the name and id of the user who posted the photo.
        /// </summary>
        [DataMember(Name = "from")]
        public CategorizedGraphObject From { get; set; }

        /// <summary>
        /// Gets or sets the album-sized view of the photo.
        /// </summary>
        [DataMember(Name = "picture")]
        public string Picture { get; set; }

        /// <summary>
        /// Gets or sets the full-sized source of the photo.
        /// </summary>
        [DataMember(Name = "source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the height of the photo, in pixels.
        /// </summary>
        [DataMember(Name = "height")]
        public string Height { get; set; }

        /// <summary>
        /// Gets or sets the width of the photo, in pixels.
        /// </summary>
        [DataMember(Name = "width")]
        public string Width { get; set; }

        /// <summary>
        /// Gets or sets the link to the photo on Facebook.
        /// </summary>
        [DataMember(Name = "link")]
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the icon-sized source of the photo.
        /// </summary>
        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the time the photo was initally published.
        /// </summary>
        [DataMember(Name = "created_time")]
        public string CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the last time the photo or its caption was updated.
        /// </summary>
        [DataMember(Name = "updated_time")]
        public string UpdatedTime { get; set; }

        /// <summary>
        /// Gets or sets the users and their positions in the photo.
        /// </summary>
        [DataMember(Name = "tags")]
        public List<Tag> Tags { get; set; }
    }
}