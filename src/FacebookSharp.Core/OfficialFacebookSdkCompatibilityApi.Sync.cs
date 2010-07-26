#if !SILVERLIGHT

namespace FacebookSharp
{
    using System.Collections.Generic;
    using RestSharp;

    public partial class Facebook
    {
        #region API interface based on the official Facebook C# SDK
        // this regions api is based on http://github.com/facebook/csharp-sdk
        // the only major difference is that it returns string rather than JSONObject.

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
        /// provide "/me", which will fetch http://graph.facebook.com/me
        /// </remarks>
        public string Get(string graphPath)
        {
            return Get(graphPath, null);
        }

        /// <summary>
        /// Make a request to the Facebook Graph API with the given string parameters.
        /// </summary>
        /// <param name="graphPath">Path to the resource in the Facebook graph.</param>
        /// <param name="parameters">key-value string parameters.</param>
        /// <returns>JSON string represnetation of the response.</returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
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
        public string Get(string graphPath, IDictionary<string, string> parameters)
        {
            // by default facebook c# sdk tends to add access token, so we do the same here.
            return Get(graphPath, parameters, true);
        }

        /// <summary>
        /// Make a request to the Facebook Graph API to delete a graph object.
        /// </summary>
        /// <param name="graphPath">Path to the resource in the Facebook graph.</param>
        /// <returns>Returns result.</returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
        /// 
        /// To delete objects in the graph,
        /// provide "/id", which will delete http://graph.facebook.com/id
        /// 
        /// You can delete a like by providing /POST_ID/likes (since likes don't have an ID).
        /// </remarks>
        public string Delete(string graphPath)
        {
            // temporary hack for restsharp delete method
            // in the official facebook c# sdk it expects to add / in the path,
            // but seems like in restsharp adds it automatically, 
            // so inorder to stick with the official facebook c# sdk,
            // we excpect the user to pass /, and remove it if it exsits.
            // have to file this issue to restsharp to double check if that is actually the expected behavior.
            // submitted it as an issue http://github.com/johnsheehan/RestSharp/issues#issue/44
            // 
            // **this issue has been fixed**
            // 
            // if (graphPath.StartsWith("/") && graphPath.Length > 1)
            //    graphPath = graphPath.Substring(1);

            var request = new RestRequest(graphPath, Method.DELETE);

            var response = Execute(request, true);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                var fbException = (FacebookException)response.Content;
                if (fbException != null)
                    throw fbException;

                return response.Content;
            }

            throw new FacebookRequestException(response);
        }

        /// <summary>
        /// Publish to the Facebook Graph API.
        /// </summary>
        /// <param name="graphPath">Path to the resource in the Facebook graph.</param>
        /// <param name="parameters">key-value string parameters.</param>
        /// <returns>JSON string represnetation of the response.</returns>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
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
        public string Post(string graphPath, IDictionary<string, string> parameters)
        {
            var request = new RestRequest(graphPath, Method.POST);

            if (parameters != null)
            {
                foreach (var keyValuePair in parameters)
                    request.AddParameter(keyValuePair.Key, keyValuePair.Value);
            }

            var response = Execute(request, true);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                var fbException = (FacebookException)response.Content;
                if (fbException != null)
                    throw fbException;

                return response.Content;
            }

            throw new FacebookRequestException(response);
        }

        #endregion

        #region Generic API interface based on official Facebook C# SDK

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
        /// provide "/me", which will fetch http://graph.facebook.com/me
        /// </remarks>
        public T Get<T>(string graphPath)
            where T : new()
        {
            return Get<T>(graphPath, null);
        }

        /// <summary>
        /// Make a request to the Facebook Graph API with the given string parameters.
        /// </summary>
        /// <param name="graphPath">Path to the resource in the Facebook graph.</param>
        /// <param name="parameters">key-value string parameters.</param>
        /// <returns>JSON string represnetation of the response.</returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
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
        public T Get<T>(string graphPath, IDictionary<string, string> parameters)
            where T : new()
        {
            // by default facebook c# sdk tends to add access token, so we do the same here.
            return Get<T>(graphPath, parameters, true);
        }

        /// <summary>
        /// Make a request to the Facebook Graph API to delete a graph object.
        /// </summary>
        /// <param name="graphPath">Path to the resource in the Facebook graph.</param>
        /// <returns>Returns result.</returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
        /// 
        /// To delete objects in the graph,
        /// provide "/id", which will delete http://graph.facebook.com/id
        /// 
        /// You can delete a like by providing /POST_ID/likes (since likes don't have an ID).
        /// </remarks>
        public T Delete<T>(string graphPath)
            where T : new()
        {
            // note: temporary hack for restsharp delete method
            // in the official facebook c# sdk it expects to add / in the path,
            // but seems like in restsharp adds it automatically, 
            // so inorder to stick with the official facebook c# sdk,
            // we excpect the user to pass /, and remove it if it exsits.
            // have to file this issue to restsharp to double check if that is actually the expected behavior.
            // submitted it as an issue http://github.com/johnsheehan/RestSharp/issues#issue/44

            if (graphPath.StartsWith("/") && graphPath.Length > 1)
                graphPath = graphPath.Substring(1);

            var request = new RestRequest(graphPath, Method.DELETE);

            var response = Execute(request, true);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                var fbException = (FacebookException)response.Content;
                if (fbException != null)
                    throw fbException;

                return FacebookUtils.DeserializeObject<T>(response.Content);
            }

            throw new FacebookRequestException(response);
        }

        /// <summary>
        /// Publish to the Facebook Graph API.
        /// </summary>
        /// <param name="graphPath">Path to the resource in the Facebook graph.</param>
        /// <param name="parameters">key-value string parameters.</param>
        /// <returns>JSON string represnetation of the response.</returns>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
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
        public T Post<T>(string graphPath, IDictionary<string, string> parameters)
            where T : new()
        {
            var request = new RestRequest(graphPath, Method.POST);

            if (parameters != null)
            {
                foreach (var keyValuePair in parameters)
                    request.AddParameter(keyValuePair.Key, keyValuePair.Value);
            }

            var response = Execute(request, true);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                var fbException = (FacebookException)response.Content;
                if (fbException != null)
                    throw fbException;

                return FacebookUtils.DeserializeObject<T>(response.Content);
            }

            throw new FacebookRequestException(response);
        }


        #endregion

        #region Helper Methods

        /// <summary>
        /// Make a request to the Facebook Graph API with the given string parameters.
        /// </summary>
        /// <param name="graphPath">Path to the resource in the Facebook graph.</param>
        /// <param name="parameters">key-value string parameters.</param>
        /// <param name="addAccessToken">Add whether to set the access token or not.</param>
        /// <returns>JSON string represnetation of the response.</returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
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
        public string Get(string graphPath, IDictionary<string, string> parameters, bool addAccessToken)
        {
            var request = new RestRequest(graphPath, Method.GET);

            if (parameters != null)
            {
                foreach (var keyValuePair in parameters)
                    request.AddParameter(keyValuePair.Key, keyValuePair.Value);
            }

            var response = Execute(request, addAccessToken);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                var fbException = (FacebookException)response.Content;
                if (fbException != null)
                    throw fbException;

                return response.Content;
            }

            throw new FacebookRequestException(response);
        }

        /// <summary>
        /// Make a request to the Facebook Graph API with the given string parameters.
        /// </summary>
        /// <param name="graphPath">Path to the resource in the Facebook graph.</param>
        /// <param name="parameters">key-value string parameters.</param>
        /// <param name="addAccessToken">Add whether to set the access token or not.</param>
        /// <returns>JSON string represnetation of the response.</returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
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
        public T Get<T>(string graphPath, IDictionary<string, string> parameters, bool addAccessToken)
            where T : new()
        {
            var request = new RestRequest(graphPath, Method.GET);

            if (parameters != null)
            {
                foreach (var keyValuePair in parameters)
                    request.AddParameter(keyValuePair.Key, keyValuePair.Value);
            }

            var response = Execute(request, addAccessToken);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                var fbException = (FacebookException)response.Content;
                if (fbException != null)
                    throw fbException;
                return FacebookUtils.DeserializeObject<T>(response.Content);
            }

            throw new FacebookRequestException(response);
        }

        #endregion

    }
}

#endif