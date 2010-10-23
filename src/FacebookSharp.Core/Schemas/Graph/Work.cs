namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Work
    {
        [DataMember(Name = "employer")]
        public Employer Employer { get; set; }

        [DataMember(Name = "location")]
        public EmployerLocation Location { get; set; }

        [DataMember(Name = "position")]
        public EmployerPosition Position { get; set; }

        [DataMember(Name = "start_date")]
        public string StartDate { get; set; }

        [DataMember(Name = "end_date")]
        public string EndDate { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}