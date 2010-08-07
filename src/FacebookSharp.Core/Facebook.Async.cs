
namespace FacebookSharp
{
    using System;
    using RestSharp;

    public partial class Facebook
    {
        private void ExecuteAsync(RestRequest request, bool addAccessToken, Action<RestResponse> callback)
        {
            var client = new RestClient(GraphBaseUrl);

            if (addAccessToken && !string.IsNullOrEmpty(Settings.AccessToken)) // todo: check if acces_token already added.
                client.Authenticator = new OAuth2UriQueryParamaterAuthenticator(Settings.AccessToken);

            client.ExecuteAsync(
                request,
                response =>
                    {
                        callback(response);
                    });
        }
    }
}