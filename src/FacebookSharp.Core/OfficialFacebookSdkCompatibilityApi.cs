namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;

    public partial class Facebook
    {
        #region API interface based on the official Facebook C# SDK
        // this regions api is based on http://github.com/facebook/csharp-sdk
        // the only major difference is that it returns string rather than JSONObject.

        public string Get(string graphPath)
        {
            throw new NotImplementedException();
        }

        public string Get(string graphPath, IDictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        public string Delete(string graphPath, IDictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        public string Post(string graphPath, IDictionary<string, string> parameters)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}