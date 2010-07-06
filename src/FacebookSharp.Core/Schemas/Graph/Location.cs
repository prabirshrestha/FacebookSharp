namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Location : NamedGraphObject
    {
        [DataMember(Name = "street")]
        public string Street { get; set; }
        
        [DataMember(Name = "city")]
        public string City { get; set; }
        
        [DataMember(Name = "state")]
        public string State { get; set; }
        
        [DataMember(Name = "zip")]
        public string Zip { get; set; }
        
        [DataMember(Name = "country")]
        public string Country { get; set; }
        
        [DataMember(Name = "latitude")]
        public string Latitude { get; set; }
        
        [DataMember(Name = "longitude")]
        public string Longitude { get; set; }
    }
}