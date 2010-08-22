
namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using RestSharp;

    public partial class Facebook
    {
        internal class FacebookRestSharpSettings
        {
            private string _baseUrl;
            public string BaseUrl
            {
                get { return _baseUrl ?? (_baseUrl = GraphBaseUrl); }
                set { _baseUrl = value; }
            }

            public string UserAgent { get; set; }
            public IAuthenticator Authenticator { get; set; }

            public FacebookSettings FacebookSettings { get; set; }

            public bool AddAccessToken { get; set; }
            public IDictionary<string, string> Parameters { get; set; }
        }

        #region Helpers

        private static RestRequest PrepareRestSharpRequest(Method httpMethod, FacebookRestSharpSettings restSharpSettings)
        {
            var request = new RestRequest(restSharpSettings.BaseUrl, httpMethod);

            if (restSharpSettings.Parameters != null)
            {
                foreach (var keyValuePair in restSharpSettings.Parameters)
                    request.AddParameter(keyValuePair.Key, keyValuePair.Value);
            }

            if (restSharpSettings.AddAccessToken && !string.IsNullOrEmpty(restSharpSettings.FacebookSettings.AccessToken))
                restSharpSettings.Authenticator = new OAuth2UriQueryParameterAuthenticator(restSharpSettings.FacebookSettings.AccessToken);

            if (httpMethod == Method.DELETE)
            {
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Content-Length", "0");
            }

            return request;
        }

        private static RestClient PrepareRestSharpClient(RestRequest request, FacebookRestSharpSettings restSharpSettings)
        {
            var client = new RestClient(restSharpSettings.BaseUrl);

            client.UserAgent = restSharpSettings.UserAgent;
            client.Authenticator = restSharpSettings.Authenticator;

            return client;
        }

        private static string ProcessRestSharpResponse(RestResponse response, FacebookRestSharpSettings restSharpSettings)
        {
            Exception exception;
            var result = ProcessRestSharpResponse(response, restSharpSettings, out exception);

            if (exception != null)
                throw exception;

            return result;
        }

        private static FacebookAsyncResult ProcessAsyncRestSharpResponse(RestResponse response, FacebookRestSharpSettings restSharpSettings)
        {
            Exception exception;
            var result = ProcessRestSharpResponse(response, restSharpSettings, out exception);

            return new FacebookAsyncResult(result, exception);
        }

        private static string ProcessRestSharpResponse(RestResponse response, FacebookRestSharpSettings restSharpSettings, out Exception exception)
        {
            string result = string.Empty;

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                    exception = null;
                else
                    exception = (FacebookException)response.Content;

                result = response.Content;
            }
            else
            {
                exception = new FacebookRequestException(response);
            }

            return result;
        }


        #endregion

#if FRAMEWORK

        #region Synchronous Helpers

        internal static RestResponse Execute(RestRequest request, FacebookRestSharpSettings restSharpSettings)
        {
            var client = PrepareRestSharpClient(request, restSharpSettings);

            return client.Execute(request);
        }

        internal static string Execute(Method httpMethod, FacebookRestSharpSettings restSharpSettings)
        {
            var request = PrepareRestSharpRequest(httpMethod, restSharpSettings);

            var response = Execute(request, restSharpSettings);

            return ProcessRestSharpResponse(response, restSharpSettings);
        }

        #endregion

#endif

        #region Asynchronous Helpers

        internal static void ExecuteAsync(RestRequest request, FacebookRestSharpSettings restSharpSettings, Action<RestResponse> callback)
        {
            var client = PrepareRestSharpClient(request, restSharpSettings);

            client.ExecuteAsync(
                request,
                response =>
                {
                    if (callback != null)
                        callback(response);
                });
        }

        internal static void ExecuteAsync(Method httpMethod, FacebookRestSharpSettings restSharpSettings, Action<FacebookAsyncResult> callback)
        {
            var request = PrepareRestSharpRequest(httpMethod, restSharpSettings);

            ExecuteAsync(
                request,
                restSharpSettings,
                response =>
                {
                    var asyncResult = ProcessAsyncRestSharpResponse(response, restSharpSettings);

                    if (callback != null)
                        callback(asyncResult);
                });
        }

        #endregion

    }
}