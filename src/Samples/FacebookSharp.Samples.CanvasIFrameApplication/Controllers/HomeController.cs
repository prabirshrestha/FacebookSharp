using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FacebookSharp.Extensions;
using FacebookSharp.Schemas.Graph;
using FacebookSharp.Web.Mvc;

namespace FacebookSharp.Samples.CanvasIFrameApplication.Controllers
{
    [P3PHeader]
    public class HomeController : Controller, IFacebookContext
    {

        /* Note: iframe Applications and P3P HTTP Headers
         *  references: http://github.com/prabirshrestha/FacebookSharp/issues#issue/6
         *              http://forum.developers.facebook.net/viewtopic.php?id=452
         *              
         * incase you are using iframe application, make sure you include P3P HTTP headers
         * 
         * Q: Why "make sure to implement P3P if using iframes" ?
         * A: If your application is inside iframe with parent belongs to another domain - cookies will not work for some very common configurations for example IE 6/7 with privacy set to medium. If cookies don't work - session won't work.
         * Therefore session state turns out useless for your application under Internet Explorer. See - Privacy in Internet Explorer 6.
         * Solution - need to implement P3P header to tell the browser that cookies for your application inside iframe are OK for user privacy.
         * So, ASP.NET implementation may look like the following (global.asax):
         * 
         * protected void Application_BeginRequest(Object sender, EventArgs e)
         * {
         *      HttpContext.Current.Response.AddHeader("p3p", "CP=\"CAO PSA OUR\"");
         * }
         * 
         * or in asp.net website .aspx page code behind.
         * 
         * protected override void OnPreRender(EventArgs e) 
         * {
         *      Response.AppendHeader("P3P", "CP=\"CAO PSA OUR\"");
         *      base.OnPreRender(e);
         * }
         * 
         * or in MVC for every controllers you can manually add P3P header
         * 
         * public ActionResult Index()
         * {
         *      Response.AppendHeader("P3P", "CP=\"CAO PSA OUR\"");
         *      // write your code here
         * }
         * 
         * to make it easier, FacebookSharp already contains an action filter called P3P header action filter
         * [P3PHeader]
         * 
         */

        #region Implementation of IFacebookContext

        private IFacebookMembershipProvider _facebookMembershipProvider;
        /// <summary>
        /// Returns Facebook MemershipProvider.
        /// </summary>
        /// <remarks>
        /// Returns null if IFacebookMembershipProvider has not been implemented.
        /// </remarks>
        public IFacebookMembershipProvider FacebookMembershipProvider
        {
            get { return null; }
        }

        private Facebook _facebookContext;
        /// <summary>
        /// Returns the Facebook object for the current logged in user.
        /// </summary>
        /// <remarks>
        /// If the FacebookMembershipProvider is not null, it will set the AccessToken if the user
        /// has one. 
        /// </remarks>
        public virtual Facebook FacebookContext
        {
            get
            {
                if (ConfigurationManager.AppSettings["FacebookSharp.AppKey"] == "AppKey")
                    throw new ApplicationException("Please specify FacebookSharp.AppKey in web.config AppSettings.");
                if (ConfigurationManager.AppSettings["FacebookSharp.AppSecret"] == "AppSecret")
                    throw new ApplicationException("Please specify FacebookSharp.AppSecret in web.config AppSettings.");
                if (ConfigurationManager.AppSettings["FacebookSharp.CanvasUrl"] == "CanvasUrl")
                    throw new ApplicationException("Please specify FacebookSharp.CanvasUrl in web.config AppSettings.");
                if (_facebookContext == null)
                {
                    FacebookSettings settings = new FacebookSettings();
                    settings.CanvasUrl = ConfigurationManager.AppSettings["FacebookSharp.CanvasUrl"];
                    settings.ApplicationKey = ConfigurationManager.AppSettings["FacebookSharp.AppKey"];
                    settings.ApplicationSecret = ConfigurationManager.AppSettings["FacebookSharp.AppSecret"];

                    var far = FacebookAuthenticationResult.Parse(Request.Url.ToString(), settings);
                    if (far.IsSuccess)
                    {
                        // Needs to persist across pages
                        Session["FB_AccessToken"] = far.AccessToken;
                        Session["FB_AccessExpires"] = far.ExpiresIn;
                    }
                    if (Session["FB_AccessToken"] != null)
                    {
                        settings.AccessToken = (string)Session["FB_AccessToken"];
                        settings.AccessExpires = (long)Session["FB_AccessExpires"];
                    }

                    _facebookContext = new Facebook(settings);
                }
                return _facebookContext;
            }
        }

        #endregion

        //
        // GET: /Home/

        public ActionResult Index()
        {
            // note: need to enable Canvas Session Parameter and OAuth 2.0 for Canvas (beta) in Migration Tab in app settings.

            

            if (FacebookContext.IsSessionValid())
                ViewData["name"] = FacebookContext.Get<User>("/me").Name;
            else
            {
                ViewData["FacebookContext"] = FacebookContext;
                return View("FacebookAuthorize", this);
            }


            return View();
        }

    }
}
