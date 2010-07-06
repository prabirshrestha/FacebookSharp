namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the Facebook Group Graph API type.
    /// </summary>
    [DataContract]
    public class Group : NamedGraphObject
    {
        /// <summary>
        /// Gets or sets an object containing the name and id of the user who owns the group.
        /// </summary>
        [DataMember(Name = "owner")]
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the group description.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the url for the group's website.
        /// </summary>
        [DataMember(Name = "link")]
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the location of the group.
        /// </summary>
        [DataMember(Name = "venue")]
        public Location Venue { get; set; }

        /// <summary>
        /// Gets or sets the privacy settings of the group, either 'OPEN', 'CLOSED', or 'SECRET'.
        /// </summary>
        [DataMember(Name = "privacy")]
        public string Privacy { get; set; }

        /// <summary>
        /// Gets or sets the last time the group was updated.
        /// </summary>
        [DataMember(Name = "updated_time")]
        public string UpdatedTime { get; set; }

    }
}