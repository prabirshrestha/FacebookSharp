namespace FacebookSharp.Web.Mvc
{
    using System.Web.Mvc;

    public abstract class BaseFacebookController : Controller, IFacebookContext
    {
        #region Implementation of IFacebookContext

        /// <summary>
        /// Returns Facebook MemershipProvider.
        /// </summary>
        /// <remarks>
        /// Returns null if IFacebookMembershipProvider has not been implemented.
        /// </remarks>
        public abstract IFacebookMembershipProvider FacebookMembershipProvider { get; }

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
                    long expiresIn = 0;
                    if (FacebookMembershipProvider != null)
                    {
                        accessToken = FacebookMembershipProvider.GetFacebookAccessToken(User.Identity.Name);
                        expiresIn = FacebookMembershipProvider.GetFacebookExpiresIn(User.Identity.Name);
                    }
                    _facebookContext = new Facebook(accessToken,expiresIn);
                }
                return _facebookContext;
            }
        }

        #endregion
    }
}