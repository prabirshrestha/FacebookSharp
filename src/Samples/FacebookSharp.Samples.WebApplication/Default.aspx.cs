using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FacebookSharp;
using FacebookSharp.Samples.Website;

public partial class _Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (FacebookContext.FacebookContext.IsSessionValid())
            Response.Redirect("~/Facebook.aspx");
    }
}
