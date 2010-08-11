using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Security;
using FacebookSharp.Schemas.Graph;
using FacebookSharp.Web.Mvc;
using FacebookSharp.Extensions;

namespace FacebookSharp.Samples.Mvc.Controllers
{
    // you can dervie from BaseFacebookController which will make your life easier
    // but since most of u will already have another base controller i will not
    // derive from BaseFacebookController to show you a practical example
    public class FacebookController : Controller, IFacebookContext
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
            get
            {
                if (_facebookMembershipProvider == null)
                {
                    _facebookMembershipProvider =
                        new SqlFacebookMembershipProvider(
                            ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString, null,
                            Membership.Provider);
                }
                return _facebookMembershipProvider;
            }
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
                if (_facebookContext == null || !_facebookContext.IsSessionValid())
                {
                    string accessToken = null;
                    long expiresIn = 0;
                    if (FacebookMembershipProvider != null)
                    {
                        accessToken = FacebookMembershipProvider.GetFacebookAccessToken(User.Identity.Name);
                        expiresIn = FacebookMembershipProvider.GetFacebookExpiresIn(User.Identity.Name);
                    }
                    _facebookContext = new Facebook(accessToken, expiresIn);

                    _facebookContext.Settings.PostAuthorizeUrl = ConfigurationManager.AppSettings["FacebookSharp.PostAuthorizeUrl"];
                    _facebookContext.Settings.ApplicationKey = ConfigurationManager.AppSettings["FacebookSharp.AppKey"];
                    _facebookContext.Settings.ApplicationSecret = ConfigurationManager.AppSettings["FacebookSharp.AppSecret"];


                    if (_facebookContext.Settings.ApplicationKey == "AppKey")
                        throw new ApplicationException("Please specify FacebookSharp.AppKey in web.config AppSettings.");
                    if (_facebookContext.Settings.ApplicationSecret == "AppSecret")
                        throw new ApplicationException("Please specify FacebookSharp.AppSecret in web.config AppSettings.");
                    if (_facebookContext.Settings.PostAuthorizeUrl == "PostAuthorizeUrl")
                        throw new ApplicationException("Please specify FacebookSharp.PostAuthorizeUrl in web.config AppSettings.");

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

                }
                return _facebookContext;
            }
        }

        #endregion

        //
        // GET: /Facebook/
        /// <remarks>
        /// FacebookAuthorize derives from AuthorizeAttribute,
        /// so u dont have to add [Authorize].
        /// </remarks>
        [FacebookAuthorize]
        public ActionResult Index()
        {
            // you can access the facebook using the FacebookSharp.Core
            // but you will find this ugly and error prone.
            var fbUser = FacebookContext.Get<User>("/me");

            // instead of that add using FacebookSharp.Extensions; at the top
            // then use the extensions methods to acess facebook graph api in more
            // c# way :-) more of these extension methods comming soon.
            ViewData["ProfilePic"] = FacebookContext.GetProfilePictureUrl(fbUser.ID);

            // notice how the FacebookContext automatically gets the access token,
            // for the user and adds it to every request. thats the beauty of IFacebookContext
            return View(fbUser);
        }

        #region Linking/Unlink Facebook user from membership provider

        /// <remarks>
        /// By default RequiresLinkedFacebookAccount is set to true,
        /// meaning that the user must have a facebook account linked with
        /// the membership provider.
        /// if u want the opposite, like for example when u call this link
        /// url, the user must not have a facebook account, then set it to false
        /// it will redirect to the default loginurl with ReturnUrl and fb querystring.
        /// 
        /// So put your logic at AccountController's LogOn action (/Account/LogOn)
        /// 
        /// or what ever your login url is for the forms authentication.
        /// 
        /// </remarks>
        [FacebookAuthorize(RequiresLinkedFacebookAccount = false)]
        public ActionResult Link()
        {
            ViewData["FBLinkUrl"] = FacebookContext.Settings.FacebookAuthorizeUrl;
            return View();
        }

        /// <remarks>
        /// There is a magic happening by FacebookAuthenticationResultAttribute,
        /// it automatically makes a request to get the access token. so u dont need to worry.
        /// incase u dont like that black magic u can manually do request it and tell
        /// the ActionFilter not to do it by 
        /// 
        /// [FacebookAuthenticationResult(RetriveAccessTokenManually = true, FacebookCodeParameterName="code")]
        /// public ActionResult PostAuthorize(string code){
        ///     // then do what ever you want to do with your facebook code here
        /// }
        /// 
        /// By default RetriveAccessTokenManually is set to false. Just helping you out.
        /// </remarks>
        [FacebookAuthorize(RequiresLinkedFacebookAccount = false)]
        [FacebookAuthenticationResult(FacebookAuthenticationResultParameterName = "far")]
        public ActionResult PostAuthorize(FacebookAuthenticationResult far)
        {
            if (far != null && far.IsSuccess)
            {
                var fb = new Facebook(far.AccessToken); // Use that access token to get the facebook user id
                var user = fb.Get<User>("/me", new Dictionary<string, string> { { "fields", "id" } });

                // then save the access token 
                FacebookMembershipProvider.LinkFacebook(User.Identity.Name, user.ID, far.AccessToken, far.ExpiresIn);

                // You may want to actually redirect instead for just returning view.
                // so that the users dont's see the ugly and confusing url like this one
                // http://localhost:30326/Facebook/PostAuthorize?code=8b2055447e53702722917_doMoY.
                // for simplicity i will just return a view.
                return View("LinkSuccess");
            }

            return View("LinkError", far);
        }

        [FacebookAuthorize(RequiresLinkedFacebookAccount = true)]
        public ActionResult Unlink()
        {
            FacebookMembershipProvider.UnlinkFacebook(User.Identity.Name);
            return RedirectToAction("Index");
        }


        #endregion

    }
}
