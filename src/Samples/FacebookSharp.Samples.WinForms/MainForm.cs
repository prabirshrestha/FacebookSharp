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
            FacebookSettings fbSettings = new FacebookSettings { ApplicationKey = txtApiKey.Text };
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
                if (fbLoginDlg.FacebookAuthenticationResult != null)
                {   // it can be null if the user just cancelled.
                    fbAuthResult = fbLoginDlg.FacebookAuthenticationResult;
                    MessageBox.Show(fbAuthResult.ErrorReasonText);
                }
            }
        }

        private void btnGetMyInfo_Click(object sender, EventArgs e)
        {
            Facebook fb = new Facebook(txtAccessToken.Text);
            MessageBox.Show(fb.Get("/me"));

            // example for posting on the wall:
            //string resultPost = fb.Post("/me/feed", new Dictionary<string, string>
            //                        {
            //                            {"message", "testing from FB# restsharp."}
            //                        });
            //MessageBox.Show(resultPost); // this result is the id of the new post
            
            // example for deleting
            //var r = fb.Get("/me/feed");
            //string resultDelete = fb.Delete("/100001327642026_105241676198495");
            //MessageBox.Show(resultDelete);
        }
    }
}
