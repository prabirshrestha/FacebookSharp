namespace FacebookSharp.Web.Mvc
{
    using System.Web.Mvc;

    public class FacebookAuthenticationResultAttribute : ActionFilterAttribute
    {
        public FacebookAuthenticationResultAttribute()
        {
            FacebookAuthenticationResultParameterName = "facebookAuthenticationResult";
            FacebookCodeParameterName = "code";
        }

        public string FacebookAuthenticationResultParameterName { get; set; }
        public string FacebookCodeParameterName { get; set; }

        public bool RetriveAccessTokenManually { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var fbContext = filterContext.Controller as IFacebookContext;
            if (fbContext == null)
                throw new FacebookSharpException(
                    "Controller must implement IFacebookContext inorder to use FacebookAuthorizeAttribute.");

            string errorReason = filterContext.HttpContext.Request["error_reason"];
            string code = filterContext.HttpContext.Request["code"];

            FacebookAuthenticationResult far;
            if (string.IsNullOrEmpty(errorReason))
            {
                if (RetriveAccessTokenManually)
                {
                    far = new FacebookAuthenticationResult(string.Empty, 0, errorReason);
                }
                else
                {
                    var settings = fbContext.FacebookContext.Settings;
                    long expiresIn;
                    string accessToken = Facebook.ExchangeAccessTokenForCode(code, settings.ApplicationKey, settings.ApplicationSecret,
                                                                             settings.PostAuthorizeUrl, settings.UserAgent, out expiresIn);
                    far = new FacebookAuthenticationResult(accessToken, expiresIn, errorReason);
                }
            }
            else
            {
                far = new FacebookAuthenticationResult(string.Empty, 0, errorReason);
            }

            filterContext.ActionParameters[FacebookAuthenticationResultParameterName] = far;
            filterContext.ActionParameters["code"] = code;

            base.OnActionExecuting(filterContext);
        }
    }
}