using System.Runtime.Serialization;

namespace FacebookSharp.Schemas.Graph
{
    /// <summary>
    /// Represents the Facebook Link Graph API type.
    /// </summary>
    [DataContract]
    public class Link : NamedGraphObject
    {
        /// <summary>
        /// Gets or sets the object containing the name and id of the user who posted the link.
        /// </summary>
        [DataMember(Name = "from")]
        public From From { get; set; }
        /// <summary>
        /// Gets or sets the link message content.
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets te picture associated with the link.
        /// </summary>
        [DataMember(Name = "picture")]
        public string Picture { get; set; }
        /// <summary>
        /// Gets or sets the actual URL that was shared.
        /// </summary>
        [DataMember(Name = "link")]
        public string Url { get; set; }
        /// <summary>
        /// Gets or sets the link description
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }
        /// <summary>
        /// Gets or sets the link icon.
        /// </summary>
        [DataMember(Name = "icon")]
        public string Icon { get; set; }
    }
}