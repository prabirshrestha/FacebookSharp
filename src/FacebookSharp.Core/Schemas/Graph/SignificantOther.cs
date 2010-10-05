namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    [DataContract]
    public class SignificantOther : NamedGraphObject
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }
    }
}
