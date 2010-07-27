namespace FacebookSharp
{
    using RestSharp;

    abstract class OAuth2Authenticator : IAuthenticator
    {
        #region Implementation of IAuthenticator

        public abstract void Authenticate(RestRequest request);

        #endregion
    }

    /// <remarks>
    /// based on http://tools.ietf.org/html/draft-ietf-oauth-v2-10#section-5.1.2
    /// </remarks>
    class OAuth2UriQueryParamaterAuthenticator : OAuth2Authenticator
    {
        private readonly string _accessToken;

        public OAuth2UriQueryParamaterAuthenticator(string accessToken)
        {
            _accessToken = accessToken;
        }

        #region Overrides of OAuth2Authenticator

        public override void Authenticate(RestRequest request)
        {
            request.AddParameter("oauth_token", _accessToken, ParameterType.GetOrPost);
        }

        #endregion
    }
}