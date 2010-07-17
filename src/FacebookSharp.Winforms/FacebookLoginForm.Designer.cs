namespace FacebookSharp.Winforms
{
    partial class FacebookLoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.wbFacebookLogin = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbFacebookLogin
            // 
            this.wbFacebookLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbFacebookLogin.Location = new System.Drawing.Point(0, 0);
            this.wbFacebookLogin.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbFacebookLogin.Name = "wbFacebookLogin";
            this.wbFacebookLogin.ScrollBarsEnabled = false;
            this.wbFacebookLogin.Size = new System.Drawing.Size(556, 318);
            this.wbFacebookLogin.TabIndex = 0;
            // 
            // FacebookLoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(556, 318);
            this.Controls.Add(this.wbFacebookLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FacebookLoginForm";
            this.Text = "Facebook Login";
            this.Load += new System.EventHandler(this.FacebookLoginForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbFacebookLogin;
    }
}