
namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using RestSharp;

    internal class RestSharpContext<TMessage, TSyncResult, TAsyncResult>
    {
        private readonly Func<TMessage, RestRequest, RestClient> _prepareRestSharpClient;
        private readonly Func<TMessage, Method, RestRequest> _prepareRestSharpRequest;
        private readonly Func<TMessage, RestResponse, TSyncResult> _processSyncRestSharpResponse;
        private readonly Func<TMessage, RestResponse, TAsyncResult> _processAsyncRestSharpResponse;

        public RestSharpContext(
            Func<TMessage, RestRequest, RestClient> prepareRestSharpClient,
            Func<TMessage, Method, RestRequest> prepareRestSharpRequest,
            Func<TMessage, RestResponse, TSyncResult> processSyncRestSharpResponse,
            Func<TMessage, RestResponse, TAsyncResult> processAsyncRestSharpResponse)
        {
            _prepareRestSharpClient = prepareRestSharpClient;
            _prepareRestSharpRequest = prepareRestSharpRequest;
            _processSyncRestSharpResponse = processSyncRestSharpResponse;
            _processAsyncRestSharpResponse = processAsyncRestSharpResponse;
        }

#if FRAMEWORK

        #region Synchronous Helpers

        public RestResponse Execute(TMessage data, RestRequest request)
        {
            var client = _prepareRestSharpClient(data, request);
            return client.Execute(request);
        }

        public TSyncResult Execute(TMessage data, Method httpMethod)
        {
            var request = _prepareRestSharpRequest(data, httpMethod);

            var response = Execute(data, request);

            return _processSyncRestSharpResponse(data, response);
        }

        #endregion

#endif

        #region Asynchronous Helpers

        public void ExecuteAsync(TMessage data, RestRequest request, Action<RestResponse> callback)
        {
            var client = _prepareRestSharpClient(data, request);

            client.ExecuteAsync(
                request,
                response =>
                {
                    if (callback != null)
                        callback(response);
                });
        }

        public void ExecuteAsync(TMessage data, Method httpMethod, Action<TAsyncResult> callback)
        {
            var request = _prepareRestSharpRequest(data, httpMethod);

            ExecuteAsync(
                data,
                request,
                response =>
                {
                    var asyncResult = _processAsyncRestSharpResponse(data, response);
                    if (callback != null)
                        callback(asyncResult);
                });
        }

        #endregion
    }

    public partial class Facebook
    {
        internal class FacebookRestSharpMessage
        {
            public FacebookRestSharpMessage(Facebook fb)
            {
                Facebook = fb;

                BaseUrl = GraphBaseUrl;
                AddAccessToken = true;
            }

            public string Resource { get; set; }
            public string BaseUrl { get; set; }

            public Facebook Facebook { get; private set; }
            public bool AddAccessToken { get; set; }
            public IDictionary<string, string> Parameters { get; set; }

            public OAuth2Authenticator GetAuthenticator()
            {
                if (!string.IsNullOrEmpty(Facebook.Settings.AccessToken))
                    return new OAuth2UriQueryParameterAuthenticator(Facebook.Settings.AccessToken);

                return null;
            }
        }

        private static RestSharpContext<FacebookRestSharpMessage, string, FacebookAsyncResult> _graphContext;
        internal static RestSharpContext<FacebookRestSharpMessage, string, FacebookAsyncResult> GraphContext
        {
            get
            {
                return _graphContext ??
                       (_graphContext =
                        new RestSharpContext<FacebookRestSharpMessage, string, FacebookAsyncResult>(
                            PrepareRestSharpClient,
                            PrepareRestSharpRequest,
                            ProcessSyncRestSharpResponse,
                            ProcessAsyncRestSharpResponse));
            }
        }

        #region Helpers

        private static RestClient PrepareRestSharpClient(FacebookRestSharpMessage message, RestRequest request)
        {
            var client = new RestClient(message.BaseUrl);

            client.UserAgent = message.Facebook.Settings.UserAgent;

            if (message.AddAccessToken)
                client.Authenticator = message.GetAuthenticator();

            return client;
        }

        private static RestRequest PrepareRestSharpRequest(FacebookRestSharpMessage message, Method httpMethod)
        {
            var request = new RestRequest(message.Resource, httpMethod);

            if (message.Parameters != null)
            {
                foreach (var keyValuePair in message.Parameters)
                    request.AddParameter(keyValuePair.Key, keyValuePair.Value);
            }

            if (httpMethod == Method.DELETE)
            {
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Content-Length", "0");
            }

            return request;
        }

        private static string ProcessSyncRestSharpResponse(FacebookRestSharpMessage message, RestResponse response)
        {
            Exception exception;
            var result = ProcessRestSharpResponse(message, response, out exception);

            if (exception != null)
                throw exception;

            return result;
        }

        private static FacebookAsyncResult ProcessAsyncRestSharpResponse(FacebookRestSharpMessage message, RestResponse response)
        {
            Exception exception;
            var result = ProcessRestSharpResponse(message, response, out exception);

            return new FacebookAsyncResult(result, exception);
        }

        private static string ProcessRestSharpResponse(FacebookRestSharpMessage message, RestResponse response, out Exception exception)
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
                // incase the net is not connected or some other exception
                exception = new FacebookRequestException(response);
            }

            return result;
        }

        #endregion

    }
}