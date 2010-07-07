namespace FacebookSharp.Web.JavascriptSdk
{
    using System.Text;

    public partial class FacebookJavscriptSdk
    {
        /// <remarks>
        /// http://developers.facebook.com/docs/reference/javascript/FB.init
        /// 
        /// Name Type Description 
        /// options Object Property Type Description Argument Default 
        /// appId String Your application ID. Optional null 
        /// cookie Boolean true to enable cookie support. Optional false 
        /// logging Boolean false to disable logging. Optional true 
        /// session Object Use specified session object. Optional null 
        /// status Boolean true to fetch fresh status. Optional false 
        /// xfbml Boolean true to parse [[wiki:XFBML]] tags. Optional false 
        /// </remarks>
        public static string Init(FacebookJavascriptSdkSettings javascriptSdkSettings, bool createScriptTags)
        {
            var sb = new StringBuilder();
            if (createScriptTags)
                sb.Append("<script type='text/javascript'>");

            javascriptSdkSettings.AssertApplicationIDRequired();

            sb.AppendFormat("FB.init({0});", javascriptSdkSettings);

            if (createScriptTags)
                sb.Append("</script>");

            return sb.ToString();
        }

        public static string Init(FacebookJavascriptSdkSettings javascriptSdkSettings)
        {
            return Init(javascriptSdkSettings, false);
        }

        /// <remarks>
        /// http://developers.facebook.com/docs/reference/javascript/FB.init
        /// </remarks>
        public string Init(bool createScriptTags)
        {
            return Init(Settings, createScriptTags);
        }

        /// <remarks>
        /// http://developers.facebook.com/docs/reference/javascript/FB.init
        /// </remarks>
        public string Init()
        {
            return Init(Settings, false);
        }

        public string Init(string appId, bool enableCookie, bool enableLogging, bool checkLoginStatus, bool parseXfbml, bool createScriptTags)
        {
            return
                Init(
                    new FacebookJavascriptSdkSettings(appId, enableCookie, enableLogging, checkLoginStatus, parseXfbml),
                    createScriptTags);
        }

        public string Init(string appId, bool enableCookie, bool enableLogging, bool checkLoginStatus, bool parseXfbml)
        {
            return
                Init(
                    new FacebookJavascriptSdkSettings(appId, enableCookie, enableLogging, checkLoginStatus, parseXfbml),
                    false);
        }
    }
}