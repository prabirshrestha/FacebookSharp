#if !SILVERLIGHT

namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;
    using RestSharp;

    public partial class Facebook
    {
        #region API interface based on the official Facebook C# SDK
        // this regions api is based on http://github.com/facebook/csharp-sdk
        // the only major difference is that it returns string rather than JSONObject.

        public string Get(string graphPath)
        {
            return Get(graphPath, null);
        }

        public string Get(string graphPath, IDictionary<string, string> parameters)
        {
            // by default facebook c# sdk tends to add access token, so we do the same here.
            return Get(graphPath, parameters, true);
        }

        public string Delete(string graphPath)
        {
            throw new NotImplementedException();

            // note: temporary hack for restsharp delete method
            // in the official facebook c# sdk it expects to add / in the path,
            // but seems like in restsharp adds it automatically, 
            // so inorder to stick with the official facebook c# sdk,
            // we excpect the user to pass /, and remove it if it exsits.
            // have to file this issue to restsharp to double check if that is actually the expected behavior.

            if (graphPath.StartsWith("/") && graphPath.Length > 1)
                graphPath = graphPath.Substring(1);

            var request = new RestRequest(graphPath, Method.DELETE) { Resource = graphPath };

            var response = Execute(request, true);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                var fbException = (FacebookException)response.Content;
                if (fbException != null)
                    throw fbException;

                return response.Content;
            }

            // todo wat if there r some other types of errors? no internet connection
            throw new NotImplementedException();
        }

        public string Post(string graphPath, IDictionary<string, string> parameters)
        {
            var request = new RestRequest(graphPath, Method.POST) { Resource = graphPath };

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

            // todo wat if there r some other types of errors? no internet connection
            throw new NotImplementedException();
        }

        #endregion

        public string Get(string graphPath, IDictionary<string, string> parameters, bool addAccessToken)
        {
            var request = new RestRequest(graphPath, Method.GET) { Resource = graphPath };

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

            // todo wat if there r some other types of errors? no internet connection
            throw new NotImplementedException();
        }
    }
}

#endif