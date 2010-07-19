namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text;

    public class Facebook
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

        #region Facebook Server endpoints.
        // May be modified in a subclass for testing.
        protected static readonly string _oauthEndpoint = "https://graph.facebook.com/oauth/authorize";

        public static string OauthEndpoint
        {
            get { return _oauthEndpoint; }
        }

        protected static string _graphBaseUrl = "https://graph.facebook.com/";

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
        /// Make a request to the Facebook Graph API without any parameter.
        /// </summary>
        /// <param name="graphPath">Path to resource in the Facebook graph.</param>
        /// <returns>JSON string represnetation of the response.</returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
        /// 
        /// To fetch data about the currently logged authenticated user,
        /// provide "me", which will fetch http://graph.facebook.com/me
        /// </remarks>
        public string Request(string graphPath)
        {
            return Request(graphPath, null, "GET");
        }

        /// <summary>
        /// Make a request to the Facebook Graph API without any parameter.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize the result to.</typeparam>
        /// <param name="graphPath">Path to resource in the Facebook graph.</param>
        /// <returns>JSON string represnetation of the response.</returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
        /// 
        /// To fetch data about the currently logged authenticated user,
        /// provide "me", which will fetch http://graph.facebook.com/me
        /// </remarks>
        public T Request<T>(string graphPath)
        {
            var jsonString = Request(graphPath);
            FacebookUtils.ThrowIfFacebookException(jsonString);
            return FacebookUtils.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// Make a request to the Facebook Graph API with the given string
        /// parameters using an HTTP GET (default method).
        /// </summary>
        /// <param name="graphPath">Path to the resource in the Facebook graph.</param>
        /// <param name="parameters">key-value string parameters.</param>
        /// <returns></returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
        /// 
        /// To fetch data about the currently logged authenticated user,
        /// provide "me", which will fetch http://graph.facebook.com/me
        /// 
        /// For parameters:
        ///     key-value string parameters, e.g. the path "search" with
        /// parameters "q" : "facebook" would produce a query for the
        /// following graph resource:
        /// https://graph.facebook.com/search?q=facebook
        /// </remarks>
        public string Request(string graphPath, IDictionary<string, string> parameters)
        {
            return Request(graphPath, parameters, "GET");
        }

        /// <summary>
        /// Make a request to the Facebook Graph API with the given string
        /// parameters using an HTTP GET (default method).
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize the result to.</typeparam>
        /// <param name="graphPath">Path to the resource in the Facebook graph.</param>
        /// <param name="parameters">key-value string parameters.</param>
        /// <returns></returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
        /// 
        /// To fetch data about the currently logged authenticated user,
        /// provide "me", which will fetch http://graph.facebook.com/me
        /// 
        /// For parameters:
        ///     key-value string parameters, e.g. the path "search" with
        /// parameters "q" : "facebook" would produce a query for the
        /// following graph resource:
        /// https://graph.facebook.com/search?q=facebook
        /// </remarks>
        public T Request<T>(string graphPath, IDictionary<string, string> parameters)
        {
            var jsonString = Request(graphPath, parameters);
            FacebookUtils.ThrowIfFacebookException(jsonString);
            return FacebookUtils.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// Synchronously make a requst to the Facebook Graph API with the given HTTP method and string parameters.
        /// </summary>
        /// <param name="graphPath">Path to the resource in the Facebook graph.</param>
        /// <param name="parameters">key-value string parameters.</param>
        /// <param name="httpMethod">HTTP ver, e.g. "GET", "POST", "DELETE"</param>
        /// <returns></returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that binary data parameters (e.g. pictures) are not yet supported by this helper function.
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
        /// 
        /// For parameters:
        ///     key-value string parameters, e.g. the path "search" with
        /// parameters "q" : "facebook" would produce a query for the
        /// following graph resource:
        /// https://graph.facebook.com/search?q=facebook
        /// </remarks>
        public string Request(string graphPath, IDictionary<string, string> parameters, string httpMethod)
        {
            if (IsSessionValid())
            {
                if (parameters == null)
                    parameters = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(Settings.AccessToken))
                    parameters.Add(Token, AccessToken);
            }
            string url = GraphBaseUrl + graphPath; // note: facebook android sdk uses rest based if graphPath is null. we don't. all is graph in ours.
            return FacebookUtils.OpenUrl(url, httpMethod, parameters);
        }

        /// <summary>
        /// Synchronously make a requst to the Facebook Graph API with the given HTTP method and string parameters.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize the result to.</typeparam>
        /// <param name="graphPath">Path to the resource in the Facebook graph.</param>
        /// <param name="parameters">key-value string parameters.</param>
        /// <param name="httpMethod">HTTP ver, e.g. "GET", "POST", "DELETE"</param>
        /// <returns></returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that binary data parameters (e.g. pictures) are not yet supported by this helper function.
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
        /// 
        /// For parameters:
        ///     key-value string parameters, e.g. the path "search" with
        /// parameters "q" : "facebook" would produce a query for the
        /// following graph resource:
        /// https://graph.facebook.com/search?q=facebook
        /// </remarks>
        public T Request<T>(string graphPath, IDictionary<string, string> parameters, string httpMethod)
        {
            var jsonString = Request(graphPath, parameters, httpMethod);
            FacebookUtils.ThrowIfFacebookException(jsonString);
            return FacebookUtils.DeserializeObject<T>(jsonString);
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
            {
                foreach (string extendedPermission in extendedPermissions)
                {
                    sb.Append(extendedPermission);
                    sb.Append(",");
                }
                sb = sb.Remove(sb.Length - 1, 1); // remove the last comma.
            }
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
                       ? string.Format(GraphBaseUrl + "oauth/authorize?client_id={0}&redirect_uri={1}",
                                       facebookApplicationKey, redirectUri)
                       : string.Format(GraphBaseUrl + "oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                                       facebookApplicationKey, redirectUri, extendedPermissions);
        }

        public static string ExchangeAccessTokenForCode(string code, string applicationKey, string applicationSecret, string postAuthorizeUrl)
        {
            int expiresIn;
            return ExchangeAccessTokenForCode(code, applicationKey, applicationSecret, postAuthorizeUrl, out expiresIn);
        }

#if !SILVERLIGHT
 public static string ExchangeAccessTokenForCode(string code, string applicationKey, string applicationSecret, string postAuthorizeUrl, out int expiresIn)
        {
            if (string.IsNullOrEmpty(applicationKey))
                throw new ArgumentNullException("applicationKey");
            if (string.IsNullOrEmpty(applicationSecret))
                throw new ArgumentNullException("applicationSecret");
            if (string.IsNullOrEmpty(postAuthorizeUrl))
                throw new FacebookSharpException("postAuthorizeUrl");

            string url =
                string.Format(
                    "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}",
                    applicationKey, postAuthorizeUrl, applicationSecret, code);

            var wc = new WebClient();
            string result = wc.DownloadString(url);

            IDictionary<string, string> r = FacebookUtils.DecodeUrl(result);
            if (r.ContainsKey("expires_in"))
                expiresIn = Convert.ToInt32(r["expires_in"]);
            else
                expiresIn = 0;
            return r["access_token"];
        }

#endif

#if SILVERLIGHT
        public static string ExchangeAccessTokenForCode(string code, string applicationKey, string applicationSecret, string postAuthorizeUrl, out int expiresIn)
        {
            throw new NotImplementedException();
        }
#endif

        public string ExchangeAccessTokenForCode(string code)
        {
            if (string.IsNullOrEmpty(Settings.ApplicationKey))
                throw new FacebookSharpException("Settings.ApplicationKey missing.");
            if (string.IsNullOrEmpty(Settings.ApplicationSecret))
                throw new FacebookSharpException("Settings.ApplicationSecret missing.");
            if (string.IsNullOrEmpty(Settings.PostAuthorizeUrl))
                throw new FacebookSharpException("Settings.PostAuthorizeUrl missing.");

            int expiresIn;
            return ExchangeAccessTokenForCode(code, Settings.ApplicationKey, Settings.ApplicationSecret,
                                              Settings.PostAuthorizeUrl, out expiresIn);
        }

    }
}