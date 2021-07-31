using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.Data;
using System.Globalization;
using System.Net.Sockets;

namespace PS_Checkin.Webpage
{
    public partial class InPerson : System.Web.UI.Page
    {
        string lvFaction = "";
        string LocalMachine = "";

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
                        txt_EmpID.Value = lvUserID;
                        FncLoadDetail();
                        ASPxButton1.Visible = false;
                    }
                    else
                    {
                        ASPxButton1.Visible = true;
                    }
                }
                catch
                {
                    btn_CheckOUT.Enabled = false;
                }

                txt_EmpOUT.Visible = false;
                
                //รับข้อมูลแผนกจาก Link
                var lvData = Request.QueryString["Data"].ToString();

                //ค้นหาหมายเลข Local IP
                //IPAddress host = IPAddress.None;
                //var ips = Dns.GetHostAddresses(Dns.GetHostName());
                //foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
                //{
                //    host = ip;
                //    if (ip.AddressFamily == AddressFamily.InterNetwork)
                //        break;
                //}

                //LocalMachine = host.ToString();
                ////LocalMachine = ips.ToString();
                //lb_local.Text = LocalMachine;
                lb_local.Text = "";

                //แยกข้อมูลต่างๆ
                string[] lvArr = lvData.Split(':');
                lvFaction = lvArr[1];
                var lvFactionCode = lvFaction;
                lb_Fac.Text = lvFactionCode;
                var factionName = GsysSQL.fncCheckFactionName(lvFactionCode);
                
                //เข้าฟังก์ชันโหลดข้อมูลลง Combobox
                lb_Faction.Text = factionName;
                lb_Date.Text = DateTime.Now.ToString("dd/MM/yyyy") + " ";
                txt_DateTime.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lb_Time.Text = DateTime.Now.ToString("HH:mm") + " ";
                txt_Time.Text = DateTime.Now.ToString("HH:mm");

                //การซ่อนกล่องข้อมูล
            }

            else
            {

            }
        }

        protected void btn_CheckIN_Click(object sender, EventArgs e)
        {
            //สร้าง Cookie
            string lvUserID = Gstr.fncGetDataCode(txt_EmpID.Text, ";");
            FncCreateCookie(lvUserID);

            try
            {
                //ประกาศตัวแปร
                var lvSQL = string.Empty;
                var Success = string.Empty;
                var Emp = txt_EmpID.Text;
                var EmpSplit = Emp.Split(';');
                var EmpID = EmpSplit[0];
                var EmpName = EmpSplit[1] + " " + EmpSplit[2];
                var Visitor = cmb_Visitor.Text;
                var VisitorID = "";
                var VisitorName = "";

                if (Visitor != "")
                {
                    var VisitorSplit = Visitor.Split(';');
                    VisitorID = VisitorSplit[0];
                    VisitorName = VisitorSplit[1] + " " + VisitorSplit[2];
                }

                var Subject = txt_Subject.Text;
                var DateIN = GsysFunc.fncChangeTDate(txt_DateTime.Text);
                var TimeIN = txt_Time.Text;
                var DateOUT = string.Empty;
                var TimeOUT = string.Empty;
                LocalMachine = lb_local.Text;
                var Faction = lb_Fac.Text;
                var Remark = string.Empty;

                var NameregCheck = GsysSQL.fncCheckCheckRegister(EmpName); //เช็คว่า Emp นี้เคย Register แล้วหรือไม่

                if (NameregCheck == "")
                {
                    lvSQL = "Insert Into ps_checkinreg (Name, EmpID_Tel) Values ('" + EmpName + "', '" + EmpID + "')";
                    Success = GsysSQL.fncExecuteQueryData(lvSQL);
                }

                if (Subject == "")
                {
                    MessageboxAlert("กรุณากรอกเรื่องที่ติดต่อ");
                    return;
                }

                //Insert ข้อมูล
                lvSQL = "Insert Into ps_checkin (EmpID, EmpName, VisitorID, VisitorName, Subject, DateIN, TimeIN, DateOUT, TimeOUT, LocalMachine, Faction, Remark) " +
                    "Values ('" + EmpID + "', '" + EmpName + "', '" + VisitorID + "', '" + VisitorName + "', '" + Subject + "', '" + DateIN + "', '" + TimeIN + "', " +
                    "'" + DateOUT + "', '" + TimeOUT + "', '" + LocalMachine + "', '" + Faction + "','" + Remark + "')";
                Success = GsysSQL.fncExecuteQueryData(lvSQL);

                //ถ้าสำเร็จเด้งไปหน้า Finish
                if (Success == "Success")
                {
                    Response.Redirect("Finish.aspx");
                }
            }

            catch (Exception ex)
            {

            }
            
        }

        protected void btn_CheckOUT_Click(object sender, EventArgs e)
        {
            try
            {
                //ประกาศตัวแปร
                var lvID = string.Empty;
                var Emp = txt_EmpID.Text;
                var EmpSplit = Emp.Split(';');
                var EmpID = EmpSplit[0];
                var EmpName = EmpSplit[1] + " " + EmpSplit[2];
                var Visitor = cmb_Visitor.Text;
                var VisitorID = "";
                var VisitorName = "";

                if (Visitor != "")
                {
                    var VisitorSplit = Visitor.Split(';');
                    VisitorID = VisitorSplit[0];
                    VisitorName = VisitorSplit[1] + " " + VisitorSplit[2];
                }
                

                var Subject = txt_Subject.Text;
                var DateIN = string.Empty;
                var TimeIN = string.Empty;
                var DateOUT = GsysFunc.fncChangeTDate(txt_DateTime.Text);
                var TimeOUT = txt_Time.Text;
                var Remark = string.Empty;
                var LocalMachine = lb_local.Text;

                //ค้นหาหมายเลข ID อันสุดท้ายที่เช็คอินเข้ามา
                DataTable DT = new DataTable();
                var lvSQL = "Select * From ps_checkin Where EmpID = '" + EmpID + "' And DateIN <> '' Order by id Desc LIMIT 1";
                DT = GsysSQL.fncGetQueryData(lvSQL, DT);

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    lvID = DT.Rows[i]["ID"].ToString();
                }

                //Update วันและเวลาตอนเช็คเอาท์
                lvSQL = "Update ps_checkin SET DateOUT = '" + DateOUT + "', TimeOUT = '" + TimeOUT + "' Where ID = '" + lvID + "'";
                var Success = GsysSQL.fncExecuteQueryData(lvSQL);


                //ถ้าสำเร็จเด้งไปหน้า Finish
                if (Success == "Success")
                {
                    Response.Redirect("Finish.aspx");
                }
            }
             catch(Exception ex)
            {
                //ประกาศตัวแปร
                var lvID = string.Empty;
                var Emp = txt_EmpID.Text;
                var EmpSplit = Emp.Split(';');
                var EmpID = EmpSplit[0];
                var EmpName = EmpSplit[1] + " " + EmpSplit[2];
                var Visitor = cmb_Visitor.Text;
                var VisitorID = "";
                var VisitorName = "";
                var Subject = txt_Subject.Text;
                var DateIN = string.Empty;
                var TimeIN = string.Empty;
                var DateOUT = GsysFunc.fncChangeTDate(txt_DateTime.Text);
                var TimeOUT = txt_Time.Text;
                var Remark = string.Empty;
                var LocalMachine = lb_local.Text;

                //ค้นหาหมายเลข ID อันสุดท้ายที่เช็คอินเข้ามา
                DataTable DT = new DataTable();
                var lvSQL = "Select * From ps_checkin Where EmpID = '" + EmpID + "' And DateIN <> '' Order by id Desc LIMIT 1";
                DT = GsysSQL.fncGetQueryData(lvSQL, DT);

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    lvID = DT.Rows[i]["ID"].ToString();
                }

                //Update วันและเวลาตอนเช็คเอาท์
                lvSQL = "Update ps_checkin SET DateOUT = '" + DateOUT + "', TimeOUT = '" + TimeOUT + "' Where ID = '" + lvID + "'";
                var Success = GsysSQL.fncExecuteQueryData(lvSQL);


                //ถ้าสำเร็จเด้งไปหน้า Finish
                if (Success == "Success")
                {
                    Response.Redirect("Finish.aspx");
                }
            }
           
        }

        protected void txtEmpID_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            FncLoadDetail();
        }

        private void FncCreateCookie(string lvID)
        {
            //HttpCookie userInfo = new HttpCookie("userInfo");
            //userInfo["UserID"] = lvID;
            //userInfo.Expires.Add(DateTime.Now.AddDays(30));
            //Response.Cookies.Add(userInfo);

            //HttpCookie cookie = new HttpCookie("UserID");
            //cookie.Expires = DateTime.Now.AddDays(10);
            //HttpContext.Current.Response.SetCookie(cookie);

            //*** Properties on the Cookies ***//
            Response.Cookies["userInfo"]["UserID"] = lvID;
            Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(7);

            ////*** Instance of the HttpCookies ***//
            //HttpCookie newCookie = new HttpCookie("UserID");
            //newCookie["UserID"] = "2009";
            //newCookie.Expires = DateTime.Now.AddDays(30);
            //Response.Cookies.Add(newCookie);

            //int LiveSessionsCount = (int)Application["LiveSessionsCount"];
            //string MyCookieValue = "V" + LiveSessionsCount.ToString();
            //Response.Cookies["MyCookieKey"][""] = MyCookieValue;
        }

        private void FncLoadDetail()
        {
            var Emp = txt_EmpID.Text;
            var EmpSplit = Emp.Split(';');
            var EmpID = EmpSplit[0];

            //ค้นหาหมายเลข ID อันสุดท้ายของคนนี้ //ไปลงใน Gridlookup
            DataTable DT = new DataTable();
            var lvSQL = "Select * From ps_checkin Where EmpID = '" + EmpID + "' Order by id Desc LIMIT 1";
            DT = GsysSQL.fncGetQueryData(lvSQL, DT);

            //ประกาศตัวแปร
            EmpID = string.Empty;
            var EmpName = string.Empty;
            var VisitorID = string.Empty;
            var VisitorName = string.Empty;
            var Subject = string.Empty;
            var DateIN = string.Empty;
            var TimeIN = string.Empty;
            var DateOUT = string.Empty;
            var TimeOUT = string.Empty;

            for (int i = 0; i < DT.Rows.Count; i++)
            {
                EmpID = DT.Rows[i]["EmpID"].ToString();
                EmpName = DT.Rows[i]["EmpName"].ToString();
                VisitorID = DT.Rows[i]["VisitorID"].ToString();
                VisitorName = DT.Rows[i]["VisitorName"].ToString();
                Subject = DT.Rows[i]["Subject"].ToString();
                DateIN = DT.Rows[i]["DateIN"].ToString();
                TimeIN = DT.Rows[i]["TimeIN"].ToString();
                DateOUT = DT.Rows[i]["DateOUT"].ToString();
                TimeOUT = DT.Rows[i]["TimeOUT"].ToString();
            }

            //ค้นหา Register ของคนนี้
            DataTable DTNew = new DataTable();
            lvSQL = "Select * From ps_checkinreg Where Name = '" + EmpName + "' ";
            DTNew = GsysSQL.fncGetQueryData(lvSQL, DTNew);

            var Name = string.Empty;
            var EmpID_Tel = string.Empty;

            for (int i = 0; i < DTNew.Rows.Count; i++)
            {
                Name = DTNew.Rows[i]["Name"].ToString();
                EmpID_Tel = DTNew.Rows[i]["EmpID_Tel"].ToString();
            }


            if (DateOUT == "" && Name != "")
            {
                var EmpNameSplit = EmpName.Split(' ');
                var Emp2 = EmpID + "; " + EmpNameSplit[1] + "; " + EmpNameSplit[3];
                txt_EmpID.Visible = true;
                txt_EmpOUT.Visible = false;
                var Visit = VisitorID + " " + VisitorName;
                cmb_Visitor.Value = VisitorID;
                txt_Subject.Text = Subject;

                //txt_EmpOUT.Enabled = false;
                cmb_Visitor.Enabled = false;
                cmb_Visitor.Visible = true;
                txt_Subject.Enabled = false;
                txt_Subject.Visible = true;
                btn_CheckIN.Visible = false;
                btn_CheckOUT.Enabled = true;
                btn_CheckOUT.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
                Label4.Visible = true;
                Label5.Visible = true;
                Label6.Visible = true;
                lb_Date.Visible = true;
                lb_Time.Visible = true;
            }

            else
            {
                Label2.Visible = true;
                cmb_Visitor.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
                Label4.Visible = true;
                Label5.Visible = true;
                Label6.Visible = true;
                Label7.Visible = true;
                txt_Subject.Enabled = true;
                txt_Subject.Visible = true;
                btn_CheckIN.Visible = true;
                btn_CheckOUT.Visible = false;
                lb_Date.Visible = true;
                lb_Time.Visible = true;
            }
                
        }

        protected void cmb_Visitor_ButtonClick(object source, DevExpress.Web.ButtonEditClickEventArgs e)
        {
            //var Emp = txt_EmpID.Text; //Gridbox ชื่อพนักงาน
            //var EmpSplit = Emp.Split(';');
            //var EmpID = EmpSplit[0];

            ////ค้นหาหมายเลข ID อันสุดท้ายของคนนี้ //ไปลงใน Gridlookup
            //DataTable DT = new DataTable();
            //var lvSQL = "Select * From ps_checkin Where EmpID = '" + EmpID + "' Order by id Desc LIMIT 1";
            //DT = GsysSQL.fncGetQueryData(lvSQL, DT);

            ////ประกาศตัวแปร
            //EmpID = string.Empty;
            //var EmpName = string.Empty;
            //var VisitorID = string.Empty;
            //var VisitorName = string.Empty;
            //var Subject = string.Empty;
            //var DateIN = string.Empty;
            //var TimeIN = string.Empty;
            //var DateOUT = string.Empty;
            //var TimeOUT = string.Empty;

            //for (int i = 0; i < DT.Rows.Count; i++)
            //{
            //    EmpID = DT.Rows[i]["EmpID"].ToString();
            //    EmpName = DT.Rows[i]["EmpName"].ToString();
            //    VisitorID = DT.Rows[i]["VisitorID"].ToString();
            //    VisitorName = DT.Rows[i]["VisitorName"].ToString();
            //    Subject = DT.Rows[i]["Subject"].ToString();
            //    DateIN = DT.Rows[i]["DateIN"].ToString();
            //    TimeIN = DT.Rows[i]["TimeIN"].ToString();
            //    DateOUT = DT.Rows[i]["DateOUT"].ToString();
            //    TimeOUT = DT.Rows[i]["TimeOUT"].ToString();
            //}

            ////ค้นหา Register ของคนนี้
            //DataTable DTNew = new DataTable();
            //lvSQL = "Select * From ps_checkinreg Where Name = '" + EmpName + "' ";
            //DTNew = GsysSQL.fncGetQueryData(lvSQL, DTNew);

            //var Name = string.Empty;
            //var EmpID_Tel = string.Empty;

            //for (int i = 0; i < DTNew.Rows.Count; i++)
            //{
            //    Name = DTNew.Rows[i]["Name"].ToString();
            //    EmpID_Tel = DTNew.Rows[i]["EmpID_Tel"].ToString();
            //}


            //if (DateOUT == "" && Name != "")
            //{
            //    var EmpNameSplit = EmpName.Split(' ');
            //    var Emp2 = EmpID + "; " + EmpNameSplit[1] + "; " + EmpNameSplit[3];
            //    txt_EmpID.Visible = true;
            //    txt_EmpOUT.Visible = false;
            //    var Visit = VisitorID + " " + VisitorName;
            //    cmb_Visitor.Text = Visit;
            //    txt_Subject.Text = Subject;

            //    txt_EmpOUT.Enabled = false;
            //    cmb_Visitor.Enabled = false;
            //    txt_Subject.Enabled = false;
            //    btn_CheckIN.Enabled = false;
            //    btn_CheckOUT.Enabled = true;
            //}
        }

        protected void txt_EmpID_TextChanged(object sender, EventArgs e)
        {
            //var Emp = txt_EmpID.Text;
            
            //var EmpSplit = Emp.Split(';');
            //var EmpID = EmpSplit[0];

            //var factionStat = GsysSQL.fncCheckCheckRegisterStat(EmpID);

            //if(factionStat == "checkin")
            //{
            //    ASPxButton1.Visible = true;
            //}
            //else
            //{
            //    ASPxButton1.Visible = false;
            //}
        }

        protected void txt_EmpID_Validation(object sender, DevExpress.Web.ValidationEventArgs e)
        {
            FncLoadDetail();
        }

        protected void cmb_Visitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Label3.Visible = true;
            //txt_Subject.Visible = true;
            //Label4.Visible = true;
            //lb_Date.Visible = true;
            //Label6.Visible = true;
            //lb_Time.Visible = true;
            //Label5.Visible = true;
            //btn_CheckIN.Visible = true;
            //btn_CheckOUT.Visible = true;
        }

        protected void cmb_Visitor_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void MessageboxAlert(string lvMessage)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert('" + lvMessage + "');", true);
        }

        protected void txt_Subject_TextChanged(object sender, EventArgs e)
        {
            Label4.Visible = true;
            lb_Date.Visible = true;
            Label6.Visible = true;
            lb_Time.Visible = true;
            Label5.Visible = true;
            btn_CheckIN.Visible = true;
            btn_CheckOUT.Visible = false;
        }
    }
}