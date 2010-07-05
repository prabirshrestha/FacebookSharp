namespace FacebookSharp
{
    public class FacebookSettings
    {
        public string AccessToken { get; set; }
        public string ApplicationID { get; set; }
        public string ApplicationSecret { get; set; }
        public string PostAuthorizeUrl { get; set; }
        public long AccessExpires { get; set; }
        public string[] DefaultApplicationPermissions { get; set; }

        public string FacebookAuthorizeUrl
        {
            get
            {
                return Facebook.GenerateFacebookAuthorizeUrl(ApplicationID, PostAuthorizeUrl, DefaultApplicationPermissions);
            }
        }
    }
}