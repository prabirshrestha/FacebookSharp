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
                if (ConfigurationManager.AppSettings["FacebookSharp.AppKey"] == "AppKey")
                    throw new ApplicationException("Please specify FacebookSharp.AppKey in web.config AppSettings.");
                if (ConfigurationManager.AppSettings["FacebookSharp.AppSecret"] == "AppSecret")
                    throw new ApplicationException("Please specify FacebookSharp.AppSecret in web.config AppSettings.");
                if (ConfigurationManager.AppSettings["FacebookSharp.PostAuthorizeUrl"] == "PostAuthorizeUrl")
                    throw new ApplicationException("Please specify FacebookSharp.PostAuthorizeUrl in web.config AppSettings.");
                if (_facebookContext == null)
                {
                    FacebookSettings settings = new FacebookSettings();
                    settings.PostAuthorizeUrl = ConfigurationManager.AppSettings["FacebookSharp.PostAuthorizeUrl"];
                    settings.ApplicationKey = ConfigurationManager.AppSettings["FacebookSharp.AppKey"];
                    settings.ApplicationSecret = ConfigurationManager.AppSettings["FacebookSharp.AppSecret"];
                    settings.DefaultApplicationPermissions = new[] { "publish_stream", "read_stream" };

                    if (HttpContext.Current.Session["access_token"] != null)
                    {
                        settings.AccessExpires = Convert.ToInt64(HttpContext.Current.Session["expires_in"]);
                        settings.AccessToken = HttpContext.Current.Session["access_token"].ToString();
                    }

                    _facebookContext = new Facebook(settings);
                }

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
