namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the Facebook Event Graph API type.
    /// </summary>
    [DataContract]
    public class Event : NamedGraphObject
    {
        /// <summary>
        /// Gets or sets the name and ID of the user who owns the event.
        /// </summary>
        [DataMember(Name = "owner")]
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the long-form HTML description of the event.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }
        
        /// <summary>
        /// Gets or sets the start time of the event.
        /// </summary>
        [DataMember(Name = "start_time")]
        public string StartTime { get; set; }
        
        /// <summary>
        /// Gets or sets the end time of the event.
        /// </summary>
        [DataMember(Name = "end_time")]
        public string EndTime { get; set; }
        
        /// <summary>
        /// Gets or sets the last time the event was updated.
        /// </summary>
        [DataMember(Name = "updated_time")]
        public string UpdatedTime { get; set; }

        /// <summary>
        /// Gets or sets the location of this event.
        /// </summary>
        [DataMember(Name = "location")]
        public string Location { get; set; }
        
        /// <summary>
        /// Gets or sets the visibility of this event. Can be 'OPEN', 'CLOSED', or 'SECRET'.
        /// </summary>
        [DataMember(Name = "privacy")]
        public string Privacy { get; set; }
        
        /// <summary>
        /// Gets or sets the location of this event.
        /// </summary>
        [DataMember(Name = "venue")]
        public Location Venue { get; set; }
    }

    public class EventCollection : Connection<Event> { }
}