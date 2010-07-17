using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookSharp.Winforms;

namespace FacebookSharp.Samples.WinForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FacebookSettings fbSettings = new FacebookSettings { ApplicationID = txtApiKey.Text };
            FacebookLoginForm fbLoginDlg = new FacebookLoginForm(fbSettings);
            FacebookAuthenticationResult fbAuthResult;

            if (fbLoginDlg.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("You are logged in.");
                fbAuthResult = fbLoginDlg.FacebookAuthenticationResult;
                txtAccessToken.Text = fbAuthResult.AccessToken;
                txtExpiresIn.Text = fbAuthResult.ExpiresIn.ToString();
                btnGetMyInfo.Enabled = true;
            }
            else
            {
                MessageBox.Show("You must login inorder to access Facebook features.");
                fbAuthResult = fbLoginDlg.FacebookAuthenticationResult;
                MessageBox.Show(fbAuthResult.ErrorReasonText);
            }
        }

        private void btnGetMyInfo_Click(object sender, EventArgs e)
        {
            Facebook fb = new Facebook(txtAccessToken.Text);
            MessageBox.Show(fb.Request("me"));
        }
    }
}
