
namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents the user info who likes a Facebook Object.
    /// </summary>
    [DataContract]
    public class Like
    {
        /// <summary>
        /// Gets or sets Facebook User ID.
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets Facebook Username.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    /// <summary>
    /// Collection for Facebook Likes
    /// </summary>
    public class LikeCollection : FacebookCollection<Like>
    {
    }
}