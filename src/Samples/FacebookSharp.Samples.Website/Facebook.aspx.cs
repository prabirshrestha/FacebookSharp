using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FacebookSharp.Samples.Website;

public partial class Facebook : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lblAboutMe.Text = FacebookContext.Facebook.Request("me");
    }
}