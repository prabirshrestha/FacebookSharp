
namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;
    using RestSharp;

    public partial class Facebook
    {
        #region API interface based on the official Facebook C# SDK, but with Async
        // this regions api is based on http://github.com/facebook/csharp-sdk
        // the only major difference is that it returns string rather than JSONObject and is async.

        /// <summary>
        /// Make an asynchronous request to the Facebook Graph API with the given string parameters.
        /// </summary>
        /// <param name="graphPath">
        /// Path to the resource in the Facebook graph.
        /// </param>
        /// <param name="parameters">
        /// key-value string parameters.
        /// </param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is asynchronous.
        /// This method will not block waiting for a network reponse.
        /// 
        /// To fetch data about the currently logged authenticated user,
        /// provide "/me", which will fetch http://graph.facebook.com/me
        /// 
        /// For parameters:
        ///     key-value string parameters, e.g. the path "search" with
        /// parameters "q" : "facebook" would produce a query for the
        /// following graph resource:
        /// https://graph.facebook.com/search?q=facebook
        /// </remarks>
        public void GetAsync(string graphPath, IDictionary<string, string> parameters, Action<FacebookAsyncResult> callback)
        {
            GetAsync(graphPath, parameters, true, response => { if (callback != null) callback(response); });
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Make an async request to the Facebook Graph API with the given string parameters.
        /// </summary>
        /// <param name="graphPath">Path to the resource in the Facebook graph.</param>
        /// <param name="parameters">key-value string parameters.</param>
        /// <param name="addAccessToken">Add whether to set the access token or not.</param>
        /// <param name="callback">
        /// The callback.
        /// </param>
        ///  /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is asynchronous.
        /// This method will not block waiting for a network reponse.
        /// 
        /// To fetch data about the currently logged authenticated user,
        /// provide "/me", which will fetch http://graph.facebook.com/me
        /// 
        /// For parameters:
        ///     key-value string parameters, e.g. the path "search" with
        /// parameters "q" : "facebook" would produce a query for the
        /// following graph resource:
        /// https://graph.facebook.com/search?q=facebook
        /// </remarks>
        public void GetAsync(string graphPath, IDictionary<string, string> parameters, bool addAccessToken, Action<FacebookAsyncResult> callback)
        {
            var request = new RestRequest(graphPath, Method.GET);

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
                    {
                        exception = (FacebookException)response.Content;
                    }
                    else
                    {
                        exception = new FacebookRequestException(response);
                    }

                    callback(new FacebookAsyncResult(response.Content, exception));
                });

        }

        #endregion

    }
}