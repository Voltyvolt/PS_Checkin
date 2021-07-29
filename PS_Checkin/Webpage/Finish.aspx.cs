using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PS_Checkin.Webpage
{
    public partial class Finish : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            string script = "window.opener = 'Self';window.open('','_parent',''); window.close();";
            ScriptManager.RegisterStartupScript(Page, typeof(string), "Close Window", script, true);
        }
    }
}