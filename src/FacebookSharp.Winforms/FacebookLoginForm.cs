using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FacebookSharp.Winforms
{
    public partial class FacebookLoginForm : Form
    {
        private Uri _loginUri;

        public FacebookAuthenticationResult FacebookAuthenticationResult { get; private set; }

        public FacebookLoginForm(FacebookSettings facebookSettings)
        {
            _loginUri = new Uri(facebookSettings.DesktopLoginUrl);
            InitializeComponent();
        }

        private void FacebookLoginForm_Load(object sender, EventArgs e)
        {
            wbFacebookLogin.Url = _loginUri;
        }

        private void wbFacebookLogin_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            string fullPath = e.Url.ToString();
            if (fullPath.StartsWith("http://www.facebook.com/connect/login_success.html"))
            {
                FacebookAuthenticationResult = FacebookAuthenticationResult.Parse(fullPath);
                if (FacebookAuthenticationResult != null && FacebookAuthenticationResult.IsSuccess)
                    DialogResult = DialogResult.OK;
                else
                    DialogResult = DialogResult.Cancel;
            }
        }

    }
}
