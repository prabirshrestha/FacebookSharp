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
                if (_facebookContext == null)
                {
                    _facebookContext = new Facebook();

                    _facebookContext.Settings.ApplicationKey = ConfigurationManager.AppSettings["FacebookSharp.AppKey"];
                    _facebookContext.Settings.ApplicationSecret = ConfigurationManager.AppSettings["FacebookSharp.AppSecret"];
                    _facebookContext.Settings.CanvasUrl = ConfigurationManager.AppSettings["FacebookSharp.CanvasUrl"];

                    if (_facebookContext.Settings.ApplicationKey == "AppKey")
                        throw new ApplicationException("Please specify FacebookSharp.AppKey in web.config AppSettings.");
                    if (_facebookContext.Settings.ApplicationSecret == "AppSecret")
                        throw new ApplicationException("Please specify FacebookSharp.AppSecret in web.config AppSettings.");
                    if (_facebookContext.Settings.CanvasUrl == "CanvasUrl")
                        throw new ApplicationException("Please specify FacebookSharp.CanvasUrl in web.config AppSettings.");

                    _facebookContext.Settings.DefaultApplicationPermissions =
                        new[]
                            {
                                "offline_access", "publish_stream",
                                "email", "user_about_me", "user_birthday", "user_education_history",
                                "user_events", "user_groups", "user_hometown", "user_interests",
                                "user_likes", "user_location",
                                "user_photos", "user_relationships", "user_religion_politics", "user_status",
                                "user_work_history"
                            };

                    // in case its canvas check the signed_request
                    var far = FacebookAuthenticationResult.Parse(
                        Request.Url.ToString(), FacebookContext.Settings);

                    _facebookContext.Settings.AccessToken = far.AccessToken;
                    _facebookContext.Settings.AccessExpires = far.ExpiresIn;

                    // incase its offline_access, you mite want to save it using IFacebookMembershipProvider.

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
