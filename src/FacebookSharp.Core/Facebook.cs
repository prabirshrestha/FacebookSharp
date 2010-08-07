namespace FacebookSharp
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    public partial class Facebook
    {
        private FacebookSettings _settings;
        public FacebookSettings Settings
        {
            get { return _settings; }
        }


        public Facebook()
            : this((FacebookSettings)null)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessToken">The Facebook OAuth 2.0 access token for API access.</param>
        public Facebook(string accessToken)
            : this(new FacebookSettings { AccessToken = accessToken })
        {
        }

        public Facebook(FacebookSettings facebookSettings)
        {
            _settings = facebookSettings ?? (_settings = new FacebookSettings());
        }

        public static readonly string Token = "access_token";
        public static readonly string Expires = "expires_in";
        internal static readonly string DefaultUserAgent = "FacebookSharp";

        #region Facebook Server endpoints.
        // May be modified in a subclass for testing.
        protected static readonly string _oauthEndpoint = "https://graph.facebook.com/oauth/authorize";

        public static string OauthEndpoint
        {
            get { return _oauthEndpoint; }
        }

        protected static string _graphBaseUrl = "https://graph.facebook.com";

        public static string GraphBaseUrl
        {
            get { return _graphBaseUrl; }
        }

        #endregion

        /// <summary>
        /// Gets the OAuth 2.0 access token for API access: treat with care.
        /// </summary>
        /// <remarks>
        /// Returns null if no session exists.
        /// </remarks>
        public string AccessToken
        {
            get { return Settings.AccessToken; }
        }

        /// <summary>
        /// Gets the current session's expiration time (in milliseconds since Unix epoch),
        /// or 0 if the session doen't expire or doesn't exist.
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public long AccessExpires
        {
            get { return Settings.AccessExpires; }
        }

        /// <summary>
        /// Checks whether this object has an non-expired session token.
        /// </summary>
        /// <returns>Return true if session is valid otherwise false.</returns>
        public bool IsSessionValid()
        {
            // todo: not complete yet.
            return !string.IsNullOrEmpty(AccessToken);
        }

        /// <summary>
        /// Set the current session's duration (in seconds since Unix epoch).
        /// </summary>
        /// <param name="expiresIn">Duration in seconds.</param>
        public void SetAccessExpiresIn(string expiresIn)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the url to authenticate with Facebook.
        /// </summary>
        /// <param name="facebookApplicationKey"></param>
        /// <param name="redirectUri"></param>
        /// <param name="extendedPermissions"></param>
        /// <returns></returns>
        public static string GenerateFacebookAuthorizeUrl(string facebookApplicationKey, string redirectUri, string[] extendedPermissions)
        {
            StringBuilder sb = new StringBuilder();
            if (extendedPermissions != null && extendedPermissions.Length > 0)
                sb.Append(String.Join(",", extendedPermissions));
            return GenerateFacebookAuthorizeUrl(facebookApplicationKey, redirectUri, sb.ToString());
        }

        /// <summary>
        /// Returns the url to authenticate with Facebook.
        /// </summary>
        /// <param name="facebookApplicationKey"></param>
        /// <param name="redirectUri"></param>
        /// <param name="extendedPermissions"></param>
        /// <returns></returns>
        public static string GenerateFacebookAuthorizeUrl(string facebookApplicationKey, string redirectUri, string extendedPermissions)
        {
            return string.IsNullOrEmpty(extendedPermissions)
                       ? string.Format(GraphBaseUrl + "/oauth/authorize?client_id={0}&redirect_uri={1}",
                                       facebookApplicationKey, redirectUri)
                       : string.Format(GraphBaseUrl + "/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                                       facebookApplicationKey, redirectUri, extendedPermissions);
        }

        /// <summary>
        /// Returns the url to login with Facebook.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GenerateFacebookLoginUrl(IDictionary<string, string> parameters)
        {
            return GenerateFacebookLoginUrl(parameters, new string[0]);
        }

        /// <summary>
        /// Returns the url to login with Facebook.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="extendedPermissions"></param>
        /// <returns></returns>
        public static string GenerateFacebookLoginUrl(IDictionary<string, string> parameters, string extendedPermissions)
        {
            return GenerateFacebookLoginUrl(parameters, new string[1] { extendedPermissions });
        }

        /// <summary>
        /// Returns the url to login with Facebook.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="extendedPermissions"></param>
        /// <returns></returns>
        public static string GenerateFacebookLoginUrl(IDictionary<string, string> parameters, string[] extendedPermissions)
        {
            // todo: make these static somewhere else... (maybe setup defaults somewhere too)
            List<string> allowedParams = new List<string>();
            allowedParams.Add("api_key");
            allowedParams.Add("next");
            allowedParams.Add("canvas");
            allowedParams.Add("display");
            allowedParams.Add("cancel_url");
            allowedParams.Add("fbconnect");
            allowedParams.Add("return_session");
            allowedParams.Add("session_version");
            allowedParams.Add("v");

            StringBuilder loginUrl = new StringBuilder();
            loginUrl.Append("http://www.facebook.com/login.php?");
            List<string> paramList = new List<string>();

            // todo: encode parameter values
            foreach (KeyValuePair<string, string> p in parameters)
            {
                if (allowedParams.Contains(p.Key))
                    paramList.Add(p.Key + "=" + p.Value);
            }

            if (extendedPermissions.Length > 0)
                paramList.Add("req_perms=" + String.Join(",", extendedPermissions));

            loginUrl.Append(String.Join("&", paramList.ToArray()));

            return loginUrl.ToString();
        }

    }
}