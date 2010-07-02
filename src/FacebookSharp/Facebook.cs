using System;
using System.Collections.Generic;

namespace FacebookSharp
{
    public class Facebook
    {
        public static readonly string Token = "access_token";
        public static readonly string Expires = "expires_in";

        #region Facebook Server endpoints.
        // May be modified in a subclass for testing.
        private static readonly string _oatuhEndpoint = "https://graph.facebook.com/oauth/authorize";
        public static string OatuhEndpoint { get { return _oatuhEndpoint; } }

        protected static string _graphBaseUrl = "https://graph.facebook.com/";
        public static string GraphBaseUrl { get { return _graphBaseUrl; } }

        protected static string _restServerUrl = "https://api.facebook.com/restserver.php";
        public static string RestServerUrl { get { return _restServerUrl; } }

        #endregion

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
        public string Request(String graphPath, IDictionary<string, string> parameters, string httpMethod)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks whether this object has an non-expired session token.
        /// </summary>
        /// <returns>Return true if session is valid otherwise false.</returns>
        public bool IsSessionValid()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the OAuth 2.0 access token for API access: treat with care.
        /// </summary>
        /// <remarks>
        /// Returns null if no session exists.
        /// </remarks>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the current session's expiration time (in milliseconds since Unix epoch),
        /// or 0 if the session doen't expire or doesn't exist.
        /// </summary>
        public long AccessExpires { get; set; }

        /// <summary>
        /// Set the current session's duration (in seconds since Unix epoch).
        /// </summary>
        /// <param name="expiresIn">Duration in seconds.</param>
        public void SetAccessExpiresIn(string expiresIn)
        {
            throw new NotImplementedException();
        }
    }
}