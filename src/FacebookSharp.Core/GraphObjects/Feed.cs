namespace FacebookSharp
{
    public class Feed : GraphObject
    {
        public Friend From { get; set; }

        public string Message { get; set; }

        public string Picture { get; set; }

        public string Link { get; set; }

        public string Name { get; set; }

        public string Caption { get; set; }

        public string Description { get; set; }

        public string Source { get; set; }

        public string Icon { get; set; }

        public string Attribution { get; set; }

        public long Likes { get; set; }

        public string Created_Time { get; set; }

        public string Updated_Time { get; set; }
    }

    public class FeedCollection : Connection<Feed> { }
}