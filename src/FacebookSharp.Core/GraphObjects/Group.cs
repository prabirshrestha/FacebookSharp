namespace FacebookSharp
{
    /// <summary>
    /// Represents the Facebook Group Graph API type.
    /// </summary>
    public class Group : NamedGraphObject
    {
        /// <summary>
        /// Gets or sets an object containing the name and id of the user who owns the group.
        /// </summary>
        public Owner Owner { get; set; }
        /// <summary>
        /// Gets or sets the group description.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the url for the group's website.
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// Gets or sets the location of the group.
        /// </summary>
        public Location Venue { get; set; }
        /// <summary>
        /// The privacy settings of the group, either 'OPEN', 'CLOSED', or 'SECRET'.
        /// </summary>
        public string Privacy { get; set; }
        /// <summary>
        /// Gets or sets the last time the group was updated.
        /// </summary>
        public string Updated_Time { get; set; }

    }
}