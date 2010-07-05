
namespace FacebookSharp
{
    /// <summary>
    /// Base class which encapsulates behavior and properties common to most Facebook Graph API types.
    /// </summary>
    public abstract class GraphObject
    {
        /// <summary>
        /// Gets or sets the object's unique Facebook 'id'.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the object's type metadata, available by including metadata=1 URL parameter during request.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets metadata, available by including the metadata=1 URL parameter during request.
        /// </summary>
        public Metadata Metadata { get; set; }

        public Paging Paging { get; set; }
    }
}