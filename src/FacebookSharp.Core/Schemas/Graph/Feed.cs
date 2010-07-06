namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Feed : GraphObject
    {
        [DataMember(Name = "from")]
        public Friend From { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "picture")]
        public string Picture { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "caption")]
        public string Caption { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "source")]
        public string Source { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [DataMember(Name = "attribution")]
        public string Attribution { get; set; }

        [DataMember(Name = "likes")]
        public long Likes { get; set; }

        [DataMember(Name = "created_time")]
        public string CreatedTime { get; set; }

        [DataMember(Name = "updated_time")]
        public string UpdatedTime { get; set; }
    }

    public class FeedCollection : Connection<Feed> { }
}