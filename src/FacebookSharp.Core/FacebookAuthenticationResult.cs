using System.Collections.Generic;

namespace FacebookSharp
{
    using System;

    public class FacebookAuthenticationResult
    {
        public FacebookAuthenticationResult(string url)
            : this(url, null)
        {
        }

        public FacebookAuthenticationResult(string url, FacebookSettings facebookSettings)
        {
            IDictionary<string, string> paramters;
            if (url.StartsWith("http://www.facebook.com/connect/login_success.html"))
            {
                Uri uri = new Uri(url);
                if (!string.IsNullOrEmpty(uri.Fragment))
                    paramters = FacebookUtils.DecodeUrl(uri.Fragment);
                else
                    paramters = FacebookUtils.ParseUrl(url);
                
                if (paramters.ContainsKey("access_token"))
                    AccessToken = paramters["access_token"];
                if (paramters.ContainsKey("expires_in"))
                    ExpiresIn = Convert.ToInt32(paramters["expires_in"]);
                
            }
            else
            {   // its from web
                paramters = FacebookUtils.ParseUrl(url);

                if (paramters.ContainsKey("code"))
                {   // incase this is from the web, we need to exchange the code with access token
                    if (facebookSettings == null)
                        throw new ArgumentNullException("facebookSettings");

                    int expiresIn;
                    AccessToken = Facebook.ExchangeAccessTokenForCode(paramters["code"],
                                                                      facebookSettings.ApplicationKey,
                                                                      facebookSettings.ApplicationSecret,
                                                                      facebookSettings.PostAuthorizeUrl,
                                                                      out expiresIn);
                    ExpiresIn = expiresIn;

                }
            }
            if (paramters.ContainsKey("error_reason"))
                ErrorReasonText = paramters["error_reason"];
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