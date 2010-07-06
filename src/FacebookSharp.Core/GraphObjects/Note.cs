using System.Runtime.Serialization;

namespace FacebookSharp
{
    /// <summary>
    /// Represents the facebook Note Graph API type.
    /// </summary>
    [DataContract]
    public class Note : GraphObject
    {
        /// <summary>
        /// Gets or sets the id and name of the user who posted the note. 
        /// </summary>
        [DataMember(Name = "from")]
        public From From { get; set; }

        /// <summary>
        /// Gets or sets the title of the note.
        /// </summary>
        [DataMember(Name = "subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the note html contents .
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the note icon.
        /// </summary>
        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the time the note was initially published.
        /// </summary>
        [DataMember(Name = "created_time")]
        public string CreatedTime { get; set; }

        /// <summary>
        /// Gets or sets the time the note was last updated.
        /// </summary>
        [DataMember(Name = "updated_time")]
        public string UpdatedTime { get; set; }
    }
}