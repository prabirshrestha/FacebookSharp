namespace FacebookSharp.Schemas.Graph
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class Education
    {
        [DataMember(Name = "school")]
        public School School { get; set; }

        [DataMember(Name = "degree")]
        public Degree Degree { get; set; }

        [DataMember(Name = "year")]
        public SchoolYear Year { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "concentration")]
        public List<SchoolConcentration> Concentration { get; set; }
    }
}