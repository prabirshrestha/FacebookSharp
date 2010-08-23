#if !SILVERLIGHT

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
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
        /// 
        /// </remarks>
        [Obsolete("Use the graph api Get() if possible.")]
        public string GetUsingRestApi(string methodName, IDictionary<string, string> parameters, bool addAccessToken)
        {
            return
                RestApiContext.Execute(
                    new FacebookApiRestSharpMessage(this) { Resource = "/method/" + methodName, Parameters = parameters, AddAccessToken = addAccessToken },
                    Method.GET);
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
        /// Note that this method is synchronous.
        /// This method blocks waiting for a network reponse,
        /// so do not call it in a UI thread.
        /// 
        [Obsolete("Use the graph api Get() if possible.")]
        public string GetUsingRestApi(string methodName, IDictionary<string, string> parameters)
        {
            // by default facebook c# sdk tends to add access token, so we do the same here.
            return GetUsingRestApi(methodName, parameters, true);
        }
    }
}

#endif