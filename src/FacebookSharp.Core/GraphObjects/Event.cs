namespace FacebookSharp
{
    /// <summary>
    /// Represents the Facebook Event Graph API type.
    /// </summary>
    public class Event : NamedGraphObject
    {
        /// <summary>
        /// Gets or sets the name and ID of the user who owns the event.
        /// </summary>
        public Owner Owner { get; set; }
        /// <summary>
        /// Gets or sets the long-form HTML description of the event.
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the start time of the event.
        /// </summary>
        public string Start_Time { get; set; }
        /// <summary>
        /// Gets or sets the end time of the event.
        /// </summary>
        public string End_Time { get; set; }
        /// <summary>
        /// Gets or sets the last time the event was updated.
        /// </summary>
        public string Updated_Time { get; set; }
        /// <summary>
        /// Gets or sets the location of this event.
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// Gets or sets the visibility of this event. Can be 'OPEN', 'CLOSED', or 'SECRET'.
        /// </summary>
        public string Privacy { get; set; }
        /// <summary>
        /// Gets or sets the location of this event.
        /// </summary>
        public Location Venue { get; set; }
    }

    public class EventCollection : Connection<Event> { }
}