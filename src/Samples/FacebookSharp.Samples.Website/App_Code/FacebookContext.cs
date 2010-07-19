using System.Web;
using System.Configuration;
using System;

namespace FacebookSharp.Samples.Website
{
    public class SampleFacebookContext : IFacebookContext
    {
        private Facebook _facebookContext;

        #region Implementation of IFacebookContext

        public Facebook FacebookContext
        {
            get
            {
                if (_facebookContext == null)
                {
                    _facebookContext = HttpContext.Current.Session["access_token"] != null
                                    ? new Facebook(HttpContext.Current.Session["access_token"].ToString())
                                    : new Facebook();
                }
                _facebookContext.Settings.PostAuthorizeUrl = ConfigurationManager.AppSettings["FacebookSharp.PostAuthorizeUrl"];
                _facebookContext.Settings.ApplicationKey = ConfigurationManager.AppSettings["FacebookSharp.AppKey"];
                _facebookContext.Settings.ApplicationSecret = ConfigurationManager.AppSettings["FacebookSharp.AppSecret"];


                if (_facebookContext.Settings.ApplicationKey == "AppKey")
                    throw new ApplicationException("Please specify FacebookSharp.AppKey in web.config AppSettings.");
                if(_facebookContext.Settings.ApplicationSecret=="AppSecret")
                    throw new ApplicationException("Please specify FacebookSharp.AppSecret in web.config AppSettings.");
                if (_facebookContext.Settings.PostAuthorizeUrl == "PostAuthorizeUrl")
                    throw new ApplicationException("Please specify FacebookSharp.PostAuthorizeUrl in web.config AppSettings.");

                return _facebookContext;
            }
        }

        public virtual IFacebookMembershipProvider FacebookMembershipProvider
        {
            get { return null; }
        }

        #endregion
    }
}
