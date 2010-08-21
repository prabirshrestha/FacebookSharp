
namespace FacebookSharp
{
    using System;
    using RestSharp;
    using System.Collections.Generic;

    public partial class Facebook
    {
        private void ExecuteAsync(RestRequest request, bool addAccessToken, string userAgent, Action<RestResponse> callback)
        {
            var client = new RestClient(GraphBaseUrl);
            client.UserAgent = userAgent;

            if (addAccessToken && !string.IsNullOrEmpty(Settings.AccessToken)) // todo: check if acces_token already added.
                client.Authenticator = new OAuth2UriQueryParameterAuthenticator(Settings.AccessToken);

            client.ExecuteAsync(
                request,
                response =>
                {
                    if (callback != null)
                        callback(response);
                });
        }

        private void ExecuteAsync(Method httpMethod, string graphPath, IDictionary<string, string> parameters, bool addAccessToken, Action<FacebookAsyncResult> callback)
        {
            var request = new RestRequest(graphPath, httpMethod);

            if (parameters != null)
            {
                foreach (var keyValuePair in parameters)
                    request.AddParameter(keyValuePair.Key, keyValuePair.Value);
            }

            ExecuteAsync(
                request,
                addAccessToken,
                Settings.UserAgent,
                response =>
                {
                    Exception exception;

                    if (response.ResponseStatus == ResponseStatus.Completed)
                        exception = (FacebookException) response.Content;
                    else
                        exception = new FacebookRequestException(response);

                    if (callback != null)
                        callback(new FacebookAsyncResult(response.Content, exception));
                });
        }
    }
}