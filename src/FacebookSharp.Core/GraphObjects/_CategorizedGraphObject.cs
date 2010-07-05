namespace FacebookSharp
{
    /// <summary>
    /// Base class for Facebook Graph API types that include a 'category' field.
    /// </summary>
    public abstract class CategorizedGraphObject : NamedGraphObject
    {
        /// <summary>
        /// The 'category' field for the Graph Object.
        /// </summary>
        public string Category { get; set; }
    }
}