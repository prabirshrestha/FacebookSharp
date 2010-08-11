namespace FacebookSharp
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;

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

        public FacebookAuthenticationResult(string accessToken, long expiresIn, string errorReasonText)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
            ErrorReasonText = errorReasonText;
        }

        // todo: eliminate redundancy
        public FacebookAuthenticationResult(IDictionary<string, string> data)
        {
            if (data.ContainsKey("access_token"))
                AccessToken = data["access_token"];
            if (data.ContainsKey("expires_in"))
                ExpiresIn = Convert.ToInt64(data["expires_in"]);
            if (data.ContainsKey("user_id"))
                UserId = data["user_id"];
            if (data.ContainsKey("error_reason"))
                ErrorReasonText = data["error_reason"];
        }

        public FacebookAuthenticationResult(IDictionary<string, object> data)
        {
            if (data.ContainsKey("oauth_token"))
                AccessToken = data["oauth_token"].ToString();
            if (data.ContainsKey("expires"))
                ExpiresIn = Convert.ToInt64(data["expires"]);
            if (data.ContainsKey("user_id"))
                UserId = data["user_id"].ToString();
            if (data.ContainsKey("error_reason"))
                ErrorReasonText = data["error_reason"].ToString();
        }

        public string AccessToken { get; private set; }
        public long ExpiresIn { get; private set; }
        public string ErrorReasonText { get; private set; }
        public string UserId { get; private set; }

        public bool IsSuccess { get { return (string.IsNullOrEmpty(ErrorReasonText) && !string.IsNullOrEmpty(AccessToken)); } }
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
        
        /// <remarks>Returns null, if it can't parse</remarks>
        public static FacebookAuthenticationResult Parse(string url)
        {
            return Parse(url, null);
        }

        /// <remarks>Returns null, if it can't parse</remarks>
        public static FacebookAuthenticationResult Parse(string url, FacebookSettings facebookSettings)
        {
            IDictionary<string, string> paramters;

            if (url.StartsWith("http://www.facebook.com/connect/login_success.html"))
            {
                Uri uri = new Uri(url);
                if (!string.IsNullOrEmpty(uri.Fragment))
                {
                    var pars = FacebookUtils.ParseUrlQueryString(uri.Fragment);
                    paramters = new Dictionary<string, string>();
                    foreach (var p in pars)
                    {
                        if (p.Key.StartsWith("#"))
                            paramters.Add(p.Key.Substring(1), p.Value[0]);
                        else
                            paramters.Add(p.Key, p.Value[0]);
                    }
                }
                else
                {
                    var pars = FacebookUtils.ParseUrlQueryString(url);
                    paramters = new Dictionary<string, string>();
                    foreach (var p in pars)
                    {
                        paramters.Add(p.Key, p.Value[0]);
                    }
                }

                return new FacebookAuthenticationResult(paramters);
            }
            else
            {   // its from web
                var uri = new Uri(url);
                var pars = FacebookUtils.ParseUrlQueryString(uri.Query);

                paramters = new Dictionary<string, string>();
                foreach (var p in pars)
                {
                    paramters.Add(p.Key, p.Value[0]);
                }

                if (paramters.ContainsKey("signed_request"))
                {   // if we are accessing from iframe canvas
                    // note: needs to enable Canvas Session Parameter and OAuth 2.0 for Canvas (beta) in Migration Tab in app settings.
                    // might add other features later on.

                    if (facebookSettings == null)
                        throw new ArgumentNullException("facebookSettings");
                    if (string.IsNullOrEmpty(facebookSettings.ApplicationSecret))
                        throw new ArgumentNullException("facebookSettings.ApplicationSecret");

                    IDictionary<string, object> jsonObject;
                    if (!ValidateSignedRequest(paramters["signed_request"], facebookSettings.ApplicationSecret, out jsonObject))
                        throw new InvalidSignedRequestException();

                    return new FacebookAuthenticationResult(jsonObject);

                }
                else if (paramters.ContainsKey("code"))
                {   // incase this is from the web, we need to exchange the code with access token
                    if (facebookSettings == null)
                        throw new ArgumentNullException("facebookSettings");

                    long expiresIn;
                    string accessToken = Facebook.ExchangeAccessTokenForCode(paramters["code"],
                                                                      facebookSettings.ApplicationKey,
                                                                      facebookSettings.ApplicationSecret,
                                                                      facebookSettings.PostAuthorizeUrl,
                                                                      facebookSettings.UserAgent,
                                                                      out expiresIn);
                    return new FacebookAuthenticationResult(accessToken, expiresIn, null);
                }

                return null;
            }
        }
#endif

        #region signed_request helpers

        // http://developers.facebook.com/docs/authentication/canvas

        private static byte[] FromUrlBase64String(string Base64UrlSafe)
        {
            Base64UrlSafe = Base64UrlSafe.PadRight(Base64UrlSafe.Length + (4 - Base64UrlSafe.Length % 4) % 4, '=');
            Base64UrlSafe = Base64UrlSafe.Replace('-', '+').Replace('_', '/');
            return Convert.FromBase64String(Base64UrlSafe);
        }

        private static string ToUrlBase64String(byte[] Input)
        {
            return Convert.ToBase64String(Input).Replace("=", String.Empty)
                .Replace('+', '-')
                .Replace('/', '_');
        }

        private static byte[] SignWithHmac(byte[] dataToSign, byte[] keyBody)
        {
            using (var hmacAlgorithm = new HMACSHA256(keyBody))
            {
                hmacAlgorithm.ComputeHash(dataToSign);
                return hmacAlgorithm.Hash;
            }
        }

#if !SILVERLIGHT

        /// <summary>
        /// Validates facebook signed_request using the applicationSecret.
        /// </summary>
        /// <param name="signedRequest">
        /// The signed request.
        /// </param>
        /// <param name="applicationSecret">
        /// The application secret.
        /// </param>
        /// <param name="jsonObject">
        /// The json object if validation passes, else null.
        /// </param>
        /// <returns>
        /// Returns true if validation passes, else false.
        /// </returns>
        public static bool ValidateSignedRequest(string signedRequest, string applicationSecret, out IDictionary<string, object> jsonObject)
        {
            if (signedRequest.StartsWith("signed_request="))
                signedRequest = signedRequest.Substring(15);
            if (string.IsNullOrEmpty(applicationSecret))
                throw new ArgumentNullException("applicationSecret");

            jsonObject = null;

            string expectedSignature = signedRequest.Substring(0, signedRequest.IndexOf('.'));
            string payload = signedRequest.Substring(signedRequest.IndexOf('.') + 1);

            // Back & Forth with Signature 
            // byte[] actualSignature = FromUrlBase64String(expectedSignature);
            // string testSignature = ToUrlBase64String(actualSignature);

            // Back & Forth With Data
            byte[] actualPayload = FromUrlBase64String(payload);
            string json = (new UTF8Encoding()).GetString(actualPayload);
            // string testPayload = ToUrlBase64String(actualPayload);

            // Attempt to get same hash
            var hmac = SignWithHmac(
                Encoding.UTF8.GetBytes(payload),
                Encoding.UTF8.GetBytes(applicationSecret));

            var hmacBase64 = ToUrlBase64String(hmac);

            if (hmacBase64 != expectedSignature)
                return false;

            jsonObject = FacebookUtils.FromJson(json);

            return true;
        }

#endif
        #endregion

    }
}