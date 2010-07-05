namespace FacebookSharp
{
    /// <summary>
    /// Represents the Album Graph API type.
    /// </summary>
    public class Album : NamedGraphObject
    {
        public From From { get; set; }

        /// <summary>
        /// Gets or sets the description of the album.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the location of the album.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the link to this album on Facebook.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the number of photos in this album.
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        /// Gets or sets the time the photo album was initially created.
        /// </summary>
        public string Created_Time { get; set; }

        /// <summary>
        /// Gets or sets the last time the photo album was updated.
        /// </summary>
        public string Updated_Time { get; set; }
    }
}