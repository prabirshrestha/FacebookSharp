namespace FacebookSharp
{
    public class FacebookSettings
    {
        public string AccessToken { get; set; }
        public string ApplicationKey { get; set; }
        public string ApplicationSecret { get; set; }
        public string PostAuthorizeUrl { get; set; }
        public long AccessExpires { get; set; }
        public string[] DefaultApplicationPermissions { get; set; }
        
        public string FacebookAuthorizeUrl
        {
            get
            {
                return Facebook.GenerateFacebookAuthorizeUrl(ApplicationKey, PostAuthorizeUrl, DefaultApplicationPermissions);
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