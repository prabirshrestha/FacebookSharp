
namespace FacebookSharp.Schemas.Graph
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Account : CategorizedGraphObject
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }
    }

    public class AccountCollection : Connection<Account>
    {

    }
}