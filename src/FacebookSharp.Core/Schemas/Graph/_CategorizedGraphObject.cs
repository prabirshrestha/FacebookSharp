namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Base class for Facebook Graph API types that include a 'category' field.
    /// </summary>
    [DataContract]
    public class CategorizedGraphObject : NamedGraphObject
    {
        /// <summary>
        /// The 'category' field for the Graph Object.
        /// </summary>
        [DataMember(Name = "category")]
        public string Category { get; set; }
    }
}