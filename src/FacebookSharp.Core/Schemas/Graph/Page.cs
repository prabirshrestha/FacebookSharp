namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represetns the Facebook Page Graph API type.
    /// </summary>
    [DataContract]
    public class Page : CategorizedGraphObject
    {
        /// <summary>
        /// Gets or sets the page's picture.
        /// </summary>
        [DataMember(Name = "picture")]
        public string Picture { get; set; }

        /// <summary>
        /// Gets or sets the page's link.
        /// </summary>
        [DataMember(Name = "link")]
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the page's username.
        /// </summary>
        [DataMember(Name = "username")]
        public string Username { get; set; }

        /// <summary>
        /// gets or sets when the page was founded.
        /// </summary>
        [DataMember(Name = "founded")]
        public string Founded { get; set; }

        /// <summary>
        /// Gets or sets the overview of the page's company.
        /// </summary>
        [DataMember(Name = "company_overview")]
        public string CompanyOverview { get; set; }

        /// <summary>
        /// Gets or sets the page's mission.
        /// </summary>
        [DataMember(Name = "mission")]
        public string Mission { get; set; }

        /// <summary>
        /// Gets or sets the page's products.
        /// </summary>
        [DataMember(Name = "products")]
        public string Products { get; set; }

        /// <summary>
        /// Gets or sets the number of fans the page has.
        /// </summary>
        [DataMember(Name = "fan_count")]
        public long FanCount { get; set; }

    }
}