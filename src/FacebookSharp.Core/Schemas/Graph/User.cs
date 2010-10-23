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

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "locale")]
        public string Locale { get; set; }

        [DataMember(Name = "work")]
        public List<Work> Work { get; set; }

        [DataMember(Name = "education")]
        public List<Education> Education { get; set; }

        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        [DataMember(Name = "relationship_status")]
        public string RelationshipStatus { get; set; }

        [DataMember(Name = "significant_other")]
        public SignificantOther SignificantOther { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "website")]
        public string Website { get; set; }

        [DataMember(Name = "timezone")]
        public int Timezone { get; set; }

        [DataMember(Name = "updated_time")]
        public string UpdatedTime { get; set; }

        [DataMember(Name = "hometown")]
        public HomeTownLocation Hometown { get; set; }

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

    /// <summary>
    /// Represents the user info who likes a Facebook Object.
    /// </summary>
    [DataContract]
    public class BasicUserInfo
    {
        /// <summary>
        /// Gets or sets Facebook User ID.
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets Facebook Username.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }


    /// <summary>
    /// Collection for Facebook Likes
    /// </summary>
    public class BasicUserInfoCollection : FacebookCollection<BasicUserInfo>
    {
    }
}