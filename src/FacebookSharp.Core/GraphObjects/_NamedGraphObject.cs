namespace FacebookSharp
{
    /// <summary>
    /// Base class for Facebook Graph API types that include a field called name.
    /// </summary>
    public abstract class NamedGraphObject : GraphObject
    {
        /// <summary>
        /// Gets or sets the 'name' field.
        /// </summary>
        public string Name { get; set; }
    }
}