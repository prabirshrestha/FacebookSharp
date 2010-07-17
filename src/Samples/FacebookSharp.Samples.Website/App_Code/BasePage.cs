namespace FacebookSharp.Samples.Website
{
    public abstract class BasePage : System.Web.UI.Page
    {
        private IFacebookContext _facebookContext;
        public IFacebookContext FacebookContext
        {
            get { return _facebookContext ?? (_facebookContext = new SampleFacebookContext()); }
        }
    }
}