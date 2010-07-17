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
            FacebookSettings fbSettings = new FacebookSettings {ApplicationID = txtApiKey.Text};
            FacebookLoginForm fbLoginDlg = new FacebookLoginForm(fbSettings);
            fbLoginDlg.ShowDialog();
        }
    }
}
