namespace FacebookSharp.Schemas.Graph
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public abstract class Connection<T> : GraphObject
        where T : GraphObject
    {
        [DataMember(Name = "data")]
        public List<T> Data { get; set; }
    }

    [DataContract]
    public class FacebookCollection<T>
    {
        [DataMember(Name = "data")]
        public List<T> Data { get; set; }
    }
}