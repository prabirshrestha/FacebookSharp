using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Security;

namespace FacebookSharp.Samples.Mvc.Controllers
{
    // you can dervie from BaseFacebookController which will make your life easier
    // but since most of u will already have another base controller i will not
    // derive from BaseFacebookController
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
                if (_facebookContext == null)
                {
                    string accessToken = null;
                    if (FacebookMembershipProvider != null)
                        accessToken = FacebookMembershipProvider.GetFacebookAccessToken(User.Identity.Name);
                    _facebookContext = new Facebook(accessToken);
                }
                return _facebookContext;
            }
        }

        #endregion

        //
        // GET: /Facebook/

        public ActionResult Index()
        {
            return View();
        }

    }
}
