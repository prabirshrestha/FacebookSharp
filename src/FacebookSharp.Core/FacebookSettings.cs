using System.Collections.Generic;

namespace FacebookSharp
{
    public class FacebookSettings
    {
        public string AccessToken { get; set; }
        public string ApplicationKey { get; set; }
        public string ApplicationSecret { get; set; }
        public string PostAuthorizeUrl { get; set; }
        public string CanvasUrl { get; set; }
        public long AccessExpires { get; set; }
        public string[] DefaultApplicationPermissions { get; set; }
        public string UserAgent { get; set; }

        public string FacebookAuthorizeUrl
        {
            get
            {
                return Facebook.GenerateFacebookAuthorizeUrl(ApplicationKey, PostAuthorizeUrl, DefaultApplicationPermissions);
            }
        }

        public string FacebookCanvasLoginUrl
        {
            get
            {
                return Facebook.GenerateFacebookLoginUrl(
                    new Dictionary<string, string>
                        {
                            { "api_key", ApplicationKey },
                            { "canvas", "1" },
                            { "next", CanvasUrl },
                            { "v", "1.0" }
                        },
                    DefaultApplicationPermissions);
            }
        }

        public string DesktopLoginUrl
        {
            get
            {
                return Facebook.GenerateFacebookAuthorizeUrl(ApplicationKey,
                                                             "http://www.facebook.com/connect/login_success.html",
                                                             DefaultApplicationPermissions) + "&display=popup&type=user_agent";
            }
        }
    }
}