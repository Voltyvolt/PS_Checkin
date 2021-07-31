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
            if (!IsPostBack)
            {
                //เช็ค Cookie
                try
                {
                    string lvUserID = Request.Cookies["userInfo"]["UserID"];
                    if (lvUserID != "")
                    {
                        var lvData = "121";
                        var lvUrl = "InPerson.aspx?Data=:" + lvData;
                        Response.Redirect(lvUrl);
                    }
                    else
                    {
                       
                    }
                }
                catch
                {

                }
            }

            //var QueryString = Request.QueryString["Data"].ToString(); //หารหัสแผนกจาก Link
            //Page.Response.Write("<script>console.log('" + QueryString + "');</script>");
        }

        void PersonIN_Click()
        {
            var QueryString = Request.QueryString["Data"].ToString(); //หารหัสแผนกจาก Link
            Page.Response.Write("<script>console.log('" + QueryString + "');</script>");
            string[] lvURlData = QueryString.Split(':');
            Page.Response.Write("<script>console.log('" + lvURlData + "');</script>");
            var lvData = lvURlData[1];
            //var lvData = "121";
            var lvUrl = "InPerson.aspx?Data=:" + lvData;
            Response.Redirect(lvUrl);
        }

        void PersonOut_Click()
        {
            var QueryString = Request.QueryString["Data"].ToString(); //หารหัสแผนกจาก Link
            Page.Response.Write("<script>console.log('" + QueryString + "');</script>");
            string[] lvURlData = QueryString.Split(':');
            Page.Response.Write("<script>console.log('" + lvURlData + "');</script>");
            var lvData = lvURlData[1];
            //var lvData = "125";
            var lvUrl = "OutPerson.aspx?Data=:" + lvData;
            Response.Redirect(lvUrl);
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