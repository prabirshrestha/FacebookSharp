namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using RestSharp;

    /// <summary>
    /// Exception occurs when processing Facebook.
    /// </summary>
    /// <remarks>
    /// This exception is thrown when there is no internet connection, firewall and so on.
    /// </remarks>
    public class FacebookRequestException : FacebookSharpException
    {
        private readonly RestResponseBase _response;

        public FacebookRequestException(string message)
            : this(message, null)
        {

        }

        internal FacebookRequestException(RestResponseBase response)
            : this("Error occured while processing your Facebook request.", response)
        {

        }

        // internal coz, need to hide the RestResponse
        internal FacebookRequestException(string message, RestResponseBase response)
            : base(message)
        {
            _response = response ?? new RestResponse();
        }


        public string ContentType { get { return _response.ContentType; } }
        public long ContentLength { get { return _response.ContentLength; } }
        public string ContentEncoding { get { return _response.ContentEncoding; } }
        public string Content { get { return _response.Content; } }
        public HttpStatusCode StatusCode { get { return _response.StatusCode; } }
        public string StatusDescription { get { return _response.StatusDescription; } }
        public byte[] RawBytes { get { return _response.RawBytes; } }
        public Uri ResponseUri { get { return _response.ResponseUri; } }
        public string Server { get { return _response.Server; } }

        // todo: try removing dependency on Parameter
        public IList<Parameter> Cookies { get { return _response.Cookies; } }
        public IList<Parameter> Headers { get { return _response.Headers; } }
    }
}