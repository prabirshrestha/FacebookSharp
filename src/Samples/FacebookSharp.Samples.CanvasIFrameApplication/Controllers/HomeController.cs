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

namespace FacebookSharp.Samples.CanvasIFrameApplication.Controllers
{
    public class HomeController : Controller, IFacebookContext
    {
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
                        settings.AccessExpires = far.ExpiresIn;
                        settings.AccessToken = far.AccessToken;
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
