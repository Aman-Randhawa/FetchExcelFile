using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HSP_Loader
{
    public partial class Masterpage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //sponse.Redirect("UpLoadFile.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("dailyTotal.aspx");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
           Response.Redirect("StateTotal.aspx");

        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            Response.Redirect("UpLoadFile.aspx");
        }
    }
}