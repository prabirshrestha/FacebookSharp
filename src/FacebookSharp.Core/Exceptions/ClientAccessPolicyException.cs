namespace FacebookSharp
{
    public class ClientAccessPolicyException : OAuthException
    {
        public ClientAccessPolicyException()
            : base("Some of the aliases you requested do not exist: clientaccesspolicy.xml")
        {
        }
    }
}