namespace FacebookSharp
{
    using RestSharp;
    using RestSharp.Authenticators;

    public partial class Facebook
    {
#if !SILVERLIGHT
        private RestResponse Execute(RestRequest request, bool addAccessToken)
        {
            var client = new RestClient(GraphBaseUrl);

            if (addAccessToken && !string.IsNullOrEmpty(Settings.AccessToken))
                client.Authenticator = new OAuth2UriQueryParamaterAuthenticator(Settings.AccessToken);

            return client.Execute(request);
        }

        private RestResponse<T> Execute<T>(RestRequest request, bool addAccessToken)
            where T : new()
        {
            var client = new RestClient(GraphBaseUrl);

            if (addAccessToken && !string.IsNullOrEmpty(Settings.AccessToken))
                client.Authenticator = new OAuth2UriQueryParamaterAuthenticator(Settings.AccessToken);

            return client.Execute<T>(request);
        }
#endif
    }
}