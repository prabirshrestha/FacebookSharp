namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Privacy
    {
        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}