
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
        /// Make an asynchronous request to the Facebook Graph API.
        /// </summary>
        /// <param name="graphPath">
        /// Path to the resource in the Facebook graph.
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
        /// To fetch data about the currently logged authenticated user,
        /// provide "/me", which will fetch http://graph.facebook.com/me
        /// </remarks>
        public void GetAsync(string graphPath, Action<FacebookAsyncResult> callback)
        {
            GetAsync(graphPath, null, callback);
        }

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

        /// <summary>
        /// Makes an asynchronous request to the Facebook Graph API to delete a graph object.
        /// </summary>
        /// <param name="graphPath">
        /// Path to the resource in the Facebook graph.
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
        /// To delete objects in the graph,
        /// provide "/id", which will delete http://graph.facebook.com/id
        /// 
        /// You can delete a like by providing /POST_ID/likes (since likes don't have an ID).
        /// </remarks>
        public void DeleteAsync(string graphPath, Action<FacebookAsyncResult> callback)
        {
            DeleteAsync(graphPath, null, callback);
        }

        /// <summary>
        /// Makes an asynchronous request to the Facebook Graph API to delete a graph object.
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
        /// To delete objects in the graph,
        /// provide "/id", which will delete http://graph.facebook.com/id
        /// 
        /// You can delete a like by providing /POST_ID/likes (since likes don't have an ID).
        /// </remarks>
        public void DeleteAsync(string graphPath, IDictionary<string, string> parameters, Action<FacebookAsyncResult> callback)
        {
            GraphContext.ExecuteAsync(
               new FacebookRestSharpMessage(this) { Resource = graphPath, Parameters = parameters, AddAccessToken = true },
               Method.DELETE, callback);
        }

        /// <summary>
        /// Publish to the Facebook Graph API.
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
        /// To post to the wall of the currently logged authenticated user,
        /// provide "/me/feed", which will make a request to
        /// http://graph.facebook.com/me/feed
        /// 
        /// For parameters:
        ///     key-value string parameters, e.g. 
        /// parameters "message" : "this is a message" would produce the 
        /// follwing parameters
        /// message=this is a message
        /// </remarks>
        public void PostAsync(string graphPath, IDictionary<string, string> parameters, Action<FacebookAsyncResult> callback)
        {
            GraphContext.ExecuteAsync(
                new FacebookRestSharpMessage(this) { Resource = graphPath, Parameters = parameters, AddAccessToken = true },
                Method.PUT, callback);
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
            GraphContext.ExecuteAsync(
                new FacebookRestSharpMessage(this) { Resource = graphPath, Parameters = parameters, AddAccessToken = addAccessToken },
                Method.GET, callback);
        }

        #endregion

    }
}