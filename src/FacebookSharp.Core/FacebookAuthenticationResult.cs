namespace FacebookSharp
{
    using System;

    public class FacebookAuthenticationResult
    {
        public FacebookAuthenticationResult(string url)
        {
            // todo: room for optimization

            //if (!url.StartsWith("http://www.facebook.com/connect/login_success.html") ||
            //    (!string.IsNullOrEmpty(applicationPostAuthorizeUrl) && !url.StartsWith(applicationPostAuthorizeUrl)))
            //{
            //    throw new ArgumentException("Url specified is not a facebook post authorization url.", url);
            //}

            Uri uri = new Uri(url);
            string[] query;
            if (!string.IsNullOrEmpty(uri.Fragment))
                query = uri.Fragment.Split('&');
            else
                query = uri.Query.Split('&');
            for (int i = 0; i < query.Length; i++)
            {
                if (query[i].StartsWith("#") || query[i].StartsWith("?"))
                    query[i] = query[i].Substring(1, query[i].Length - 1);

                string[] keyvalue = query[i].Split('=');
                if (keyvalue[0].Equals("access_token", StringComparison.OrdinalIgnoreCase))
                    AccessToken = FacebookUtils.UrlDecode(keyvalue[1]);
                else if (keyvalue[0].Equals("expires_in", StringComparison.OrdinalIgnoreCase))
                    ExpiresIn = Convert.ToInt32(keyvalue[1]);
                else if (keyvalue[0].Equals("error_reason", StringComparison.OrdinalIgnoreCase))
                    ErrorReasonText = keyvalue[1];
            }
        }

        public FacebookAuthenticationResult(string accessToken, int expiresIn, string errorReasonText)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
            ErrorReasonText = errorReasonText;
        }

        public string AccessToken { get; private set; }
        public int ExpiresIn { get; private set; }
        public string ErrorReasonText { get; private set; }

        public bool IsSuccess { get { return string.IsNullOrEmpty(ErrorReasonText); } }

    }
}