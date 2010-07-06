
namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Base class which encapsulates behavior and properties common to most Facebook Graph API types.
    /// </summary>
    [DataContract]
    public abstract class GraphObject
    {
        /// <summary>
        /// Gets or sets the object's unique Facebook 'id'.
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the object's type metadata, available by including metadata=1 URL parameter during request.
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets metadata, available by including the metadata=1 URL parameter during request.
        /// </summary>
        [DataMember(Name = "metadata")]
        public Metadata Metadata { get; set; }

        /// <summary>
        /// Gets or sets Paging.
        /// </summary>
        [DataMember(Name = "paging")]
        public Paging Paging { get; set; }
    }
}