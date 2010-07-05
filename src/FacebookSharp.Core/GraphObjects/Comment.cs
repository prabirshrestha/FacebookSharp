namespace FacebookSharp
{
    public class Comment : GraphObject
    {
        public string ID { get; set; }
        public CommentFrom From { get; set; }
        public string Message { get; set; }
        public string CreatedTime { get; set; }
    }

    public class CommentCollection : Connection<Comment> { }
}