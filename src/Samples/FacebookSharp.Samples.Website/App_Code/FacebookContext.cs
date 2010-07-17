using System.Web;

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
