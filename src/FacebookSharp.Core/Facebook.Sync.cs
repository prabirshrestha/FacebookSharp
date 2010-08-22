
namespace FacebookSharp
{
    using RestSharp;
    using System.Collections.Generic;

    public partial class Facebook
    {
#if !SILVERLIGHT

        internal RestResponse Execute(RestRequest request, bool addAccessToken, string userAgent)
        {
            var client = new RestClient(GraphBaseUrl) { UserAgent = userAgent };

            if (request.Method == Method.DELETE)
            {
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Content-Length", "0");
            }

            if (addAccessToken && !string.IsNullOrEmpty(Settings.AccessToken)) // todo: check if acces_token already added.
                client.Authenticator = new OAuth2UriQueryParameterAuthenticator(Settings.AccessToken);

            return client.Execute(request);
        }

        internal string Execute(Method httpMethod, string graphPath, IDictionary<string, string> parameters, bool addAccessToken, string userAgent)
        {
            var request = new RestRequest(graphPath, httpMethod);

            if (parameters != null)
            {
                foreach (var keyValuePair in parameters)
                    request.AddParameter(keyValuePair.Key, keyValuePair.Value);
            }

            var response = Execute(request, addAccessToken, Settings.UserAgent);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                var fbException = (FacebookException)response.Content;
                if (fbException != null)
                    throw fbException;

                return response.Content;
            }

            throw new FacebookRequestException(response);
        }
#endif
    }
}