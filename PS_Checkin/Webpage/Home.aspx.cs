using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PS_Checkin.Webpage
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var QueryString = Request.QueryString["Data"].ToString(); //หารหัสแผนกจาก Link
            //Page.Response.Write("<script>console.log('" + QueryString + "');</script>");
        }

        void PersonIN_Click()
        {
            var QueryString = Request.QueryString["Data"].ToString(); //หารหัสแผนกจาก Link
            Page.Response.Write("<script>console.log('" + QueryString + "');</script>");
            string[] lvURlData = QueryString.Split('=');
            Page.Response.Write("<script>console.log('" + lvURlData + "');</script>");
            var lvData = lvURlData[1];
            var lvUrl = "InPerson.aspx?Data=" + lvData;
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