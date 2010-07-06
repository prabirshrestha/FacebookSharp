using System.Runtime.Serialization;

namespace FacebookSharp.Schemas.Graph
{
    /// <summary>
    /// Represents the Facebook Tag Graph API.
    /// </summary>
    [DataContract]
    public class Tag : NamedGraphObject
    {
        /// <summary>
        /// Gets or sets X coordinate (as a percentage of distance from left vs width).
        /// </summary>
        [DataMember(Name = "x")]
        public int X { get; set; }

        /// <summary>
        /// Gets or sets Y coordinate (as a percentage of distance from top vs height).
        /// </summary>
        [DataMember(Name = "y")]
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets the time the photo was initially published.
        /// </summary>
        [DataMember(Name = "created_time")]
        public string CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the last time the photo or its caption was updated.
        /// </summary>
        [DataMember(Name = "updated_time")]
        public string UpdatedTime { get; set; }

        /// <summary>
        /// Gets or sets an object containing the name and ID of the user who posted the photo.
        /// </summary>
        [DataMember(Name = "from")]
        public From From { get; set; }

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
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the width of the photo, in pixels.
        /// </summary>
        [DataMember(Name = "width")]
        public int Width { get; set; }

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
    }
}