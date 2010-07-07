namespace FacebookSharp.Web.JavascriptSdk
{
    public partial class FacebookJavscriptSdk
    {
        public FacebookJavascriptSdkSettings Settings { get; set; }

        public FacebookJavscriptSdk()
        {

        }

        public FacebookJavscriptSdk(FacebookJavascriptSdkSettings javascriptSdkSettings)
        {
            Settings = javascriptSdkSettings;
        }

        /// <summary>
        /// Gets the Facebook Javascript Sdk Url.
        /// </summary>
        /// <param name="langCode"></param>
        /// <param name="useHttps"></param>
        /// <returns></returns>
        public static string GetFacebookJsSdkUrl(string langCode, bool useHttps)
        {
            langCode = langCode.Replace('-', '_') ?? "en_US";
            return string.Format("http{0}://connect.facebook.net/{1}/all.js", useHttps ? "s" : string.Empty, langCode);
        }

        public static string GetFacebookJsSdkUrl(bool userHttps)
        {
            return GetFacebookJsSdkUrl(string.Empty, userHttps);
        }

        public static string GetFacebookJsSdkUrl()
        {
            return GetFacebookJsSdkUrl(string.Empty, false);
        }

        /// <summary>
        /// Returns the feature loader for the specified version. ex.: 0.4
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <returns>
        /// </returns>
        public static string GetFeatureLoaderUrlForFacebookConnectWebsite(string version)
        {
            return string.Format("http://static.ak.connect.facebook.com/js/api_lib/v{0}/FeatureLoader.js.php", version);
        }

        /// <summary>
        /// Returns the latest feature loader
        /// </summary>
        /// <returns>
        /// http://static.ak.connect.facebook.com/js/api_lib/v0.4/FeatureLoader.js.php
        /// </returns>
        public static string GetFeatureLoaderUrlForFacebookConnectWebsite()
        {
            return GetFeatureLoaderUrlForFacebookConnectWebsite("0.4");
        }
    }
}