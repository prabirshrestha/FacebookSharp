
namespace FacebookSharp
{
    using System;
    using RestSharp;
    using System.Collections.Generic;

    public partial class Facebook
    {
        internal void ExecuteGraphApiAsync(RestRequest request, bool addAccessToken, string userAgent, Action<RestResponse> callback)
        {
            var client = new RestClient(GraphBaseUrl) { UserAgent = userAgent };

            if (request.Method == Method.DELETE)
            {
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Content-Length", "0");
            }

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

        internal void ExecuteGraphApiAsync(Method httpMethod, string graphPath, IDictionary<string, string> parameters, bool addAccessToken, Action<FacebookAsyncResult> callback)
        {
            var request = new RestRequest(graphPath, httpMethod);

            if (parameters != null)
            {
                foreach (var keyValuePair in parameters)
                    request.AddParameter(keyValuePair.Key, keyValuePair.Value);
            }

            ExecuteGraphApiAsync(
                request,
                addAccessToken,
                Settings.UserAgent,
                response =>
                {
                    Exception exception;

                    if (response.ResponseStatus == ResponseStatus.Completed)
                        exception = (FacebookException)response.Content;
                    else
                        exception = new FacebookRequestException(response);

                    if (callback != null)
                        callback(new FacebookAsyncResult(response.Content, exception));
                });
        }
    }
}