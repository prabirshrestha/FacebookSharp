namespace FacebookSharp
{
    using System.Collections.Generic;
    using System.Text;

    public class FacebookSettings
    {
        public FacebookSettings()
        {
            UserAgent = "FacebookSharp";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookSettings"/> class.
        /// </summary>
        /// <param name="facebookSettings">
        /// The facebook settings.
        /// </param>
        /// <remarks>
        /// Copy constructor
        /// </remarks>
        public FacebookSettings(FacebookSettings facebookSettings)
        {
            if (facebookSettings == null)
                return;

            AccessToken = facebookSettings.AccessToken;
            ApplicationKey = facebookSettings.ApplicationKey;
            ApplicationSecret = facebookSettings.ApplicationSecret;
            PostAuthorizeUrl = facebookSettings.PostAuthorizeUrl;
            CanvasUrl = facebookSettings.CanvasUrl;
            AccessExpires = facebookSettings.AccessExpires;
            DefaultApplicationPermissions = facebookSettings.DefaultApplicationPermissions;
            UserAgent = facebookSettings.UserAgent;
        }

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

        public string FacebookCanvasLoginStatusUrl
        {
            get
            {
                return Facebook.GenerateFacebookLoginStatusUrl(
                    new Dictionary<string, string>
						{
							{ "api_key", ApplicationKey },
							{ "no_session", FacebookCanvasLoginUrl },
							{ "no_user", FacebookCanvasLoginUrl },
							{ "ok_session", CanvasUrl },
							{ "session_version", "3" }
						});
            }
        }

        public string FacebookCanvasLoginJavascript
        {
            get
            {
                var jsredir = new StringBuilder();
                jsredir.AppendLine("if (parent != self) { top.location.href = '" + FacebookCanvasLoginStatusUrl + "'; }");
                jsredir.AppendLine("else { self.location.href = '" + FacebookCanvasLoginStatusUrl + "'; }");
                return jsredir.ToString();
            }
        }

        public string DesktopLoginUrl
        {
            get
            {
                return Facebook.GenerateFacebookAuthorizeUrl(ApplicationKey,
                                                             "http://www.facebook.com/connect/login_success.html",
                                                             DefaultApplicationPermissions) + "&type=user_agent" +
#if WINDOWS_PHONE
                                                             "&display=touch";
#else
                                                             "&display=popup";
#endif
            }
        }
    }
}