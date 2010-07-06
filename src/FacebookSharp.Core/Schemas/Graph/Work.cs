namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Work
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}