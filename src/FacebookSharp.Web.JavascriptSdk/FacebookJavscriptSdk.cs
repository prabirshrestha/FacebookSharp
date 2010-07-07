namespace FacebookSharp.Web.JavascriptSdk
{
    public class FacebookJavscriptSdk
    {
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