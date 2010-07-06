namespace FacebookSharp.Schemas.Graph
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class User : GraphObject
    {
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }

        [DataMember(Name = "last_name")]
        public string LastName { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "about")]
        public string About { get; set; }

        [DataMember(Name = "birthday")]
        public string Birthday { get; set; }

        [DataMember(Name = "homeTown")]
        public HomeTownLocation HomeTown { get; set; }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "work")]
        public List<Work> Work { get; set; }

        // todo education
        //[DataMember(Name = "educations")]
        //public List<Education> Educations { get; set; }

        [DataMember(Name = "last_name")]
        public string Gender { get; set; }

        [DataMember(Name = "relationship_status")]
        public string RelationshipStatus { get; set; }

        [DataMember(Name = "significant_other")]
        public string SignificantOther { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "website")]
        public string Website { get; set; }

        [DataMember(Name = "timezone")]
        public int Timezone { get; set; }

        [DataMember(Name = "updated_time")]
        public string UpdatedTime { get; set; }

        [DataMember(Name = "hometown")]
        public string Hometown { get; set; }

        [DataMember(Name = "interested_in")]
        public List<string> InterestedIn { get; set; }

        [DataMember(Name = "meeting_for")]
        public List<string> MeetingFor { get; set; }

        [DataMember(Name = "religion")]
        public string Religion { get; set; }

        [DataMember(Name = "political")]
        public string Political { get; set; }

        // todo: posts, home,tagged,friends,family

        [DataMember(Name = "picture")]
        public string Picture { get; set; }

        [DataMember(Name = "verified")]
        public bool Verified { get; set; }

    }

    public class UserCollection : Connection<User> { }
}