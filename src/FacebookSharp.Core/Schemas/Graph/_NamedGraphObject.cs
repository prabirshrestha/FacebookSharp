namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Base class for Facebook Graph API types that include a field called name.
    /// </summary>
    [DataContract]
    public abstract class NamedGraphObject : GraphObject
    {
        /// <summary>
        /// Gets or sets the 'name' field.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}