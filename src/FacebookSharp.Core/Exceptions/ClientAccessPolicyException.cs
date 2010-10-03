namespace FacebookSharp
{
    public class ClientAccessPolicyException : OAuthException
    {
        public ClientAccessPolicyException()
            : base("clientaccesspolicy.xml or crossdomain.xml does not exist.")
        {
        }
    }
}