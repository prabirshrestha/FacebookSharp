namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Paging
    {
        [DataMember(Name = "next")]
        public string Next { get; set; }

        [DataMember(Name = "previous")]
        public string Previous { get; set; }
    }
}