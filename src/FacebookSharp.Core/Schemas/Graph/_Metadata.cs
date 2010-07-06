
namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Metadata
    {
        [DataMember(Name = "connections")]
        public Connections Connections { get; set; }
    }
}