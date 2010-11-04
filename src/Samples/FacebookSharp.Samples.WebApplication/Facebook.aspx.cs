using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FacebookSharp.Samples.Website;
using FacebookSharp.Extensions;

namespace FacebookSharp.Samples.WebApplication
{
    public partial class Facebook : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblAboutMe.Text = FacebookContext.FacebookContext.Get("/me");
        }

        protected void btnPostMessage_Click(object sender, EventArgs e)
        {
            var fbMessageId = FacebookContext.FacebookContext.PostToWall(txtMessage.Text, null);

            lblMessagePostStatus.Text = FacebookContext.FacebookContext.GetPost(fbMessageId).Message;
        }
    }
}