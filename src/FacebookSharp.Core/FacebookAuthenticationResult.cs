namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;

    public class FacebookAuthenticationResult
    {
        public FacebookAuthenticationResult()
        {
            // try not to use this ctor
            // we dont need it at all.
            // its required for mvc, coz when using
            // FacebookAuthenticationResultAttribute we get
            // No parameterless constructor defined for this object. error
            // ugly hack :-(
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
        public bool IsUserDenied
        {
            get
            {
                if (string.IsNullOrEmpty(ErrorReasonText))
                    return false;
                return ErrorReasonText.Equals("user_denied", StringComparison.OrdinalIgnoreCase);
            }
        }

#if !SILVERLIGHT
        public static FacebookAuthenticationResult Parse(string url)
        {
            return Parse(url, null);
        }

        public static FacebookAuthenticationResult Parse(string url, FacebookSettings facebookSettings)
        {
            string accessToken = null;
            string errorReasonText = null;
            int expiresIn = 0;
            IDictionary<string, string> paramters;

            if (url.StartsWith("http://www.facebook.com/connect/login_success.html"))
            {
                Uri uri = new Uri(url);
                if (!string.IsNullOrEmpty(uri.Fragment))
                    paramters = FacebookUtils.DecodeDictionaryUrl(uri.Fragment);
                else
                {
                    var pars = FacebookUtils.ParseUrlQueryString(url);
                    paramters = new Dictionary<string, string>();
                    foreach (var p in pars)
                    {
                        paramters.Add(p.Key, p.Value[0]);
                    }
                }

                if (paramters.ContainsKey("access_token"))
                    accessToken = paramters["access_token"];
                if (paramters.ContainsKey("expires_in"))
                    expiresIn = Convert.ToInt32(paramters["expires_in"]);

            }
            else
            {   // its from web
                var pars = FacebookUtils.ParseUrlQueryString(url);
                paramters = new Dictionary<string, string>();
                foreach (var p in pars)
                {
                    paramters.Add(p.Key, p.Value[0]);
                }

                if (paramters.ContainsKey("code"))
                {   // incase this is from the web, we need to exchange the code with access token
                    if (facebookSettings == null)
                        throw new ArgumentNullException("facebookSettings");

                    accessToken = Facebook.ExchangeAccessTokenForCode(paramters["code"],
                                                                      facebookSettings.ApplicationKey,
                                                                      facebookSettings.ApplicationSecret,
                                                                      facebookSettings.PostAuthorizeUrl,
                                                                      facebookSettings.UserAgent,
                                                                      out expiresIn);

                }
            }

            if (paramters.ContainsKey("error_reason"))
                errorReasonText = paramters["error_reason"];

            return new FacebookAuthenticationResult(accessToken, expiresIn, errorReasonText);
        }
#endif


    }
}