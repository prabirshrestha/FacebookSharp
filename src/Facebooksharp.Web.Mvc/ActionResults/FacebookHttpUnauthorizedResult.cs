namespace FacebookSharp.Web.Mvc
{
    using System.Web.Mvc;
    using System.Web.Security;
    using System.Web;

    /// <remarks>
    /// Adds a query string fb and sets to true if requires facebook account linked and false
    /// if it shouldn't have a facebook account linked.
    /// </remarks>
    public class FacebookHttpUnauthorizedResult : HttpUnauthorizedResult
    {
        public FacebookHttpUnauthorizedResult()
            : this(true)
        {
        }

        public FacebookHttpUnauthorizedResult(bool requiresFacebookLinked)
        {
            RequiresFacebookLinked = requiresFacebookLinked;
        }

        public bool RequiresFacebookLinked { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;

            var url = FormsAuthentication.LoginUrl;
            if (!string.IsNullOrEmpty(url))
                url = string.Format("{0}?ReturnUrl={1}&fb={2}", url,
                                    HttpUtility.UrlEncode(context.HttpContext.Request.Url.PathAndQuery),
                                    RequiresFacebookLinked);
            response.Clear();
            response.StatusCode = 302;
            response.RedirectLocation = url;
        }
    }
}