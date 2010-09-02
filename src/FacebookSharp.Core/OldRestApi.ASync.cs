namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;
    using RestSharp;

    public partial class Facebook
    {
        #region Helper methods

        /// <summary>
        /// Make a request to the Facebook Old Rest API with the given string parameters.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">key-value string parameters.</param>
        /// <param name="addAccessToken">Add whether to set the access token or not.</param>
        /// <returns>JSON string representation of the response.</returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is asynchronous.
        /// This method will not block waiting for a network response.
        /// 
        /// </remarks>
        [Obsolete("Use the graph api GetAsync() if possible.")]
        public void GetUsingRestApiAsync(string methodName, IDictionary<string, string> parameters, bool addAccessToken, Action<FacebookAsyncResult> callback)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();

            if (!parameters.ContainsKey("format"))
                parameters.Add("format", "json");

            RestApiContext.ExecuteAsync(
                new FacebookApiRestSharpMessage(this) { Resource = "/method/" + methodName, Parameters = parameters, AddAccessToken = addAccessToken },
                Method.GET, callback);
        }

        #endregion

        /// <summary>
        /// Make a request to the Facebook Old Rest API with the given string parameters.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">key-value string parameters.</param>
        /// <returns>JSON string represnetation of the response.</returns>
        /// <remarks>
        /// See http://developers.facebook.com/docs/api
        /// 
        /// Note that this method is asynchronous.
        /// This method will not block waiting for a network response.
        /// 
        [Obsolete("Use the graph api GetAsync() if possible.")]
        public void GetUsingRestApiAsync(string methodName, IDictionary<string, string> parameters, Action<FacebookAsyncResult> callback)
        {
            // by default facebook c# sdk tends to add access token, so we do the same here.
            GetUsingRestApiAsync(methodName, parameters, true, callback);
        }
    }
}