using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PS_Checkin.Webpage
{
    public partial class HomeAccount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        void PersonIN_Click()
        {
            var lvUrl = "InPerson.aspx?Data=" + ":Account";
            Response.Redirect(lvUrl);
        }

        void PersonOut_Click()
        {
            //var lvUrl = "InPerson.aspx?Data=" + 
            //Response.Redirect("InPerson.aspx");
        }

        protected void btn_PersonIn_Click(object sender, EventArgs e)
        {
            PersonIN_Click();
        }

        protected void btn_PersonOut_Click(object sender, EventArgs e)
        {
            PersonOut_Click();
        }
    }
}