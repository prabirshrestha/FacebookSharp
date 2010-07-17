using System.Web;
using System.Configuration;
using System;

namespace FacebookSharp.Samples.Website
{
    public class SampleFacebookContext : IFacebookContext
    {
        private Facebook _facebook;

        #region Implementation of IFacebookContext

        public Facebook Facebook
        {
            get
            {
                if (_facebook == null)
                {
                    _facebook = HttpContext.Current.Session["access_token"] != null
                                    ? new Facebook(HttpContext.Current.Session["access_token"].ToString())
                                    : new Facebook();
                }
                _facebook.Settings.PostAuthorizeUrl = ConfigurationManager.AppSettings["FacebookSharp.PostAuthorizeUrl"];
                _facebook.Settings.ApplicationKey = ConfigurationManager.AppSettings["FacebookSharp.AppKey"];

                if (_facebook.Settings.PostAuthorizeUrl == "AppKey")
                    throw new ApplicationException("Please specify FacebookSharp.AppKey in web.config AppSettings.");
                if (_facebook.Settings.PostAuthorizeUrl == "PostAuthorizeUrl")
                    throw new ApplicationException("Please specify FacebookSharp.PostAuthorizeUrl in web.config AppSettings.");

                return _facebook;
            }
        }

        public virtual IFacebookMembershipProvider FacebookMembershipProvider
        {
            get { return null; }
        }

        #endregion
    }
}
