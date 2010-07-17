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

        public FacebookLoginForm(FacebookSettings facebookSettings)
        {
            _loginUri = new Uri(facebookSettings.DesktopLoginUrl);
            Clipboard.SetText(_loginUri.ToString());
            InitializeComponent();
        }

        private void FacebookLoginForm_Load(object sender, EventArgs e)
        {
            wbFacebookLogin.Url = _loginUri;
        }

    }
}
