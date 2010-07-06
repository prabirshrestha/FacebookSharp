using System.Diagnostics.CodeAnalysis;

namespace FacebookSharp.Schemas.Graph
{
    /// <summary>
    /// Represents the Facebook Photo Graph API.
    /// </summary>
    public class Photo : NamedGraphObject
    {
        public CategorizedGraphObject From { get; set; }
        public string Picture { get; set; }
        public string Source { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }

        // todo tags
    }
}