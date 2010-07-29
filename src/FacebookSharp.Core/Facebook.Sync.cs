
namespace FacebookSharp
{
    using RestSharp;

    public partial class Facebook
    {
#if !SILVERLIGHT
        internal RestResponse Execute(RestRequest request, bool addAccessToken)
        {
            var client = new RestClient(GraphBaseUrl);

            if (addAccessToken && !string.IsNullOrEmpty(Settings.AccessToken)) // todo: check if acces_token already added.
                client.Authenticator = new OAuth2UriQueryParamaterAuthenticator(Settings.AccessToken);

            return client.Execute(request);
        }
#endif  
    }
}