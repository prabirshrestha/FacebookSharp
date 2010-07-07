namespace FacebookSharp.Web.JavascriptSdk
{
    using System.Runtime.Serialization;

    [DataContract]
    public class FacebookJavascriptSdkSettings
    {
        public FacebookJavascriptSdkSettings()
        {
            EnableLogging = true;
        }

        public FacebookJavascriptSdkSettings(string appId)
            : this()
        {
            ApplicationID = appId;
        }

        public FacebookJavascriptSdkSettings(string appId, bool enableCookie, bool enableLogging, bool checkLoginStatus, bool parseXfbml)
            : this()
        {
            ApplicationID = appId;
            EnableCookie = enableCookie;
            EnableLogging = enableLogging;
            CheckLoginStatus = checkLoginStatus;
            ParseXfbml = parseXfbml;
        }

        /// <summary>
        /// Gets or sets the Facebook Application ID.
        /// </summary>
        [DataMember(Name = "appId")]
        public string ApplicationID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to enable cookies to allow the server to access the session.
        /// </summary>
        [DataMember(Name = "cookie")]
        public bool EnableCookie { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Logging.
        /// </summary>
        [DataMember(Name = "logging")]
        public bool EnableLogging { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to check login status.
        /// </summary>
        [DataMember(Name = "status")]
        public bool CheckLoginStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to parse xfbml.
        /// </summary>
        [DataMember(Name = "xfbml")]
        public bool ParseXfbml { get; set; }

        /// <summary>
        /// Returns the JSON formatted string for FacebookJavascriptSdkSettings.
        /// </summary>
        public override string ToString()
        {
            return FacebookUtils.SerializeObject(this);
        }

        internal void AssertApplicationIDRequired()
        {
            if (string.IsNullOrEmpty(ApplicationID))
                throw new FacebookSharpException("Facebook Javascript SDK - Application ID required");
        }
    }
}