
namespace FacebookSharp.Web.Mvc
{
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// Facebook Authorization Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class FacebookAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookAuthorizeAttribute"/> class.
        /// </summary>
        public FacebookAuthorizeAttribute()
        {
            RequiresLinkedFacebookAccount = true;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user should have the facebook account linked.
        /// </summary>
        public bool RequiresLinkedFacebookAccount { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        /// <exception cref="FacebookSharpException">
        /// Occurs if the Controller doesn't inherit from <see cref="IFacebookContext"/>.
        /// </exception>
        /// <exception cref="FacebookSharpException">
        /// Occurs if the IFacebookContext's FacebookMembershipProvider is null.
        /// </exception>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            var fbContext = filterContext.Controller as IFacebookContext;
            if (fbContext == null)
                throw new FacebookSharpException(
                    "Controller must implement IFacebookContext inorder to use FacebookAuthorizeAttribute.");

            if (fbContext.FacebookMembershipProvider == null)
                throw new FacebookSharpException("Current FacebookContext doesn't support IFacebookMembershipProvider.");

            string username = filterContext.HttpContext.User.Identity.Name;

            var hasLinkedFacebook = fbContext.FacebookMembershipProvider.HasLinkedFacebook(username);

            if (RequiresLinkedFacebookAccount)
            {
                if (!hasLinkedFacebook)
                    filterContext.Result = new FacebookHttpUnauthorizedResult(true);
            }
            else
            {
                if (hasLinkedFacebook)
                    filterContext.Result = new FacebookHttpUnauthorizedResult(false);
            }
        }
    }
}