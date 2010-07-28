
namespace FacebookSharp
{
    using System;
    using RestSharp;

    public partial class Facebook
    {

#if !SILVERLIGHT

        public static string ExchangeAccessTokenForCode(string code, string applicationKey, string applicationSecret, string postAuthorizeUrl, string userAgent, out int expiresIn)
        {
            if (string.IsNullOrEmpty(applicationKey))
                throw new ArgumentNullException("applicationKey");
            if (string.IsNullOrEmpty(applicationSecret))
                throw new ArgumentNullException("applicationSecret");
            if (string.IsNullOrEmpty(postAuthorizeUrl))
                throw new FacebookSharpException("postAuthorizeUrl");

            var client = new RestClient(GraphBaseUrl);

            var request = new RestRequest(Method.GET) { Resource = "oauth/access_token" };
            request.AddParameter("client_id", applicationKey);
            request.AddParameter("redirect_uri", postAuthorizeUrl);
            request.AddParameter("client_secret", applicationSecret);
            request.AddParameter("code", code);

            var response = client.Execute(request);
            if (response.ResponseStatus == ResponseStatus.Completed)
            {   // facebook gave us some result.
                string result = response.Content;

                // but mite had been an error
                var fbException = (FacebookException)result;
                if (fbException != null)
                    throw fbException;

                var json = FacebookUtils.DecodeDictionaryUrl(result);
                if (json.ContainsKey("expires_in"))
                    expiresIn = Convert.ToInt32(json["expires_in"]);
                else
                    expiresIn = 0;
                return json["access_token"];
            }

            throw new FacebookRequestException(response);
        }

#endif

#if SILVERLIGHT

        public static string ExchangeAccessTokenForCode(string code, string applicationKey, string applicationSecret, string postAuthorizeUrl, string userAgent, out int expiresIn)
        {
            throw new NotImplementedException();
        }
#endif

        public string ExchangeAccessTokenForCode(string code, out int expiresIn)
        {
            if (string.IsNullOrEmpty(Settings.ApplicationKey))
                throw new ArgumentNullException("Settings.ApplicationKey missing.");
            if (string.IsNullOrEmpty(Settings.ApplicationSecret))
                throw new ArgumentNullException("Settings.ApplicationSecret missing.");
            if (string.IsNullOrEmpty(Settings.PostAuthorizeUrl))
                throw new ArgumentNullException("Settings.PostAuthorizeUrl missing.");

            return ExchangeAccessTokenForCode(code, Settings.ApplicationKey, Settings.ApplicationSecret,
                                              Settings.PostAuthorizeUrl, DefaultUserAgent, out expiresIn);
        }
    }
}