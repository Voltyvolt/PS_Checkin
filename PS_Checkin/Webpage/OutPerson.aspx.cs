using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PS_Checkin.Webpage
{
    public partial class OutPerson : System.Web.UI.Page
    {
        string lvFaction = "";
        string LocalMachine = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btn_CheckOUT.Enabled = false;

                var lvData = Request.QueryString["Data"].ToString();

                //แยกข้อมูลต่างๆ
                string[] lvArr = lvData.Split(':');
                lvFaction = lvArr[1];
                var lvFactionCode = lvFaction;
                lb_Fac.Text = lvFactionCode;
                var factionName = GsysSQL.fncCheckFactionName(lvFactionCode);

                //เข้าฟังก์ชันโหลดข้อมูลลง Combobox
                lb_Faction.Text = factionName;
                txt_DateTime.Text = DateTime.Now.ToString("dd/MM/yyyy");
                lb_Date.Text = DateTime.Now.ToString("dd/MM/yyyy") + " ";
                txt_Time.Text = DateTime.Now.ToString("HH:mm");
                lb_Time.Text = DateTime.Now.ToString("HH:mm") + " ";
            }
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            FncLoadDetail();
        }

        private void FncLoadDetail()
        {
            var Emp = txt_Name.Text;
            var EmpID = txt_Tel.Text; ;

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
                txt_Name.Visible = true;
                txt_Name.Text = Name;
                var Visit = VisitorID + " " + VisitorName;
                cmb_Visitor.Value = VisitorID;
                txt_Subject.Text = Subject;

                //txt_EmpOUT.Enabled = false;
                cmb_Visitor.Enabled = false;
                cmb_Visitor.Visible = true;
                txt_Subject.Enabled = false;
                txt_Subject.Visible = true;
                btn_CheckIN.Enabled = false;
                btn_CheckIN.Visible = false;
                btn_CheckOUT.Enabled = true;
                btn_CheckOUT.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
                Label4.Visible = true;
                Label5.Visible = true;
                Label6.Visible = true;
                Label7.Visible = true;
                lb_Date.Visible = true;
                lb_Time.Visible = true;
            }

            else
            {
                Label2.Visible = true;
                cmb_Visitor.Visible = true;
                Label2.Visible = true;
                Label3.Visible = true;
                txt_Subject.Enabled = true;
                txt_Subject.Visible = true;
                btn_CheckOUT.Visible = false;
                btn_CheckIN.Visible = false;
            }
            
        }

        protected void btn_CheckIN_Click(object sender, EventArgs e)
        {
            try
            {
                //ประกาศตัวแปร
                var lvSQL = string.Empty;
                var Success = string.Empty;
                var lvName = txt_Name.Text;
                var lvID = txt_Tel.Text;
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
                LocalMachine = "";
                var Faction = lb_Fac.Text;
                var Remark = string.Empty;

                var NameregCheck = GsysSQL.fncCheckCheckRegister(lvName); //เช็คว่า Emp นี้เคย Register แล้วหรือไม่

                if (NameregCheck == "")
                {
                    lvSQL = "Insert Into ps_checkinreg (Name, EmpID_Tel) Values ('" + lvName + "', '" + lvID + "')";
                    Success = GsysSQL.fncExecuteQueryData(lvSQL);
                }

                //Insert ข้อมูล
                lvSQL = "Insert Into ps_checkin (EmpID, EmpName, VisitorID, VisitorName, Subject, DateIN, TimeIN, DateOUT, TimeOUT, LocalMachine, Faction, Remark) " +
                    "Values ('" + lvID + "', '" + lvName + "', '" + VisitorID + "', '" + VisitorName + "', '" + Subject + "', '" + DateIN + "', '" + TimeIN + "', " +
                    "'" + DateOUT + "', '" + TimeOUT + "', '" + LocalMachine + "', '" + Faction + "','" + Remark + "')";
                Success = GsysSQL.fncExecuteQueryData(lvSQL);

                //ถ้าสำเร็จเด้งไปหน้า Finish
                if (Success == "Success")
                {
                    Response.Redirect("Finish.aspx");
                }
            }
            catch(Exception ex)
            {
                Response.Redirect("Finish.aspx");
            }
            
        }

        protected void btn_CheckOUT_Click(object sender, EventArgs e)
        {
            try
            {
                var lvID = string.Empty;
                var lvSQL = string.Empty;
                var Success = string.Empty;
                var lvName = txt_Name.Text;
                var lvTel = txt_Tel.Text;
                var Visitor = cmb_Visitor.Text;
                var VisitorID = "";
                var VisitorName = "";

                if (Visitor != "")
                {
                    var VisitorSplit = Visitor.Split(' ');
                    VisitorID = VisitorSplit[0];
                    VisitorName = VisitorSplit[1] + " " + VisitorSplit[2];
                }

                var Subject = txt_Subject.Text;
                var DateIN = GsysFunc.fncChangeTDate(txt_DateTime.Text);
                var TimeIN = txt_Time.Text;
                var DateOUT = GsysFunc.fncChangeTDate(txt_DateTime.Text);
                var TimeOUT = txt_Time.Text;
                LocalMachine = "";
                var Faction = lb_Fac.Text;
                var Remark = string.Empty;

                //ค้นหาหมายเลข ID อันสุดท้ายที่เช็คอินเข้ามา
                DataTable DT = new DataTable();
                lvSQL = "Select * From ps_checkin Where EmpID = '" + lvTel + "' And DateIN <> '' Order by id Desc LIMIT 1";
                DT = GsysSQL.fncGetQueryData(lvSQL, DT);

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    lvID = DT.Rows[i]["ID"].ToString();
                }

                //Update วันและเวลาตอนเช็คเอาท์
                lvSQL = "Update ps_checkin SET DateOUT = '" + DateOUT + "', TimeOUT = '" + TimeOUT + "' Where ID = '" + lvID + "'";
                Success = GsysSQL.fncExecuteQueryData(lvSQL);


                //ถ้าสำเร็จเด้งไปหน้า Finish
                if (Success == "Success")
                {
                    Response.Redirect("Finish.aspx");
                }
            }
            catch (Exception ex)
            {
                var lvID = string.Empty;
                var lvSQL = string.Empty;
                var Success = string.Empty;
                var lvName = txt_Name.Text;
                var lvTel = txt_Tel.Text;
                var Visitor = cmb_Visitor.Text;
                var VisitorID = "";
                var VisitorName = "";
                var Subject = txt_Subject.Text;
                var DateIN = GsysFunc.fncChangeTDate(txt_DateTime.Text);
                var TimeIN = txt_Time.Text;
                var DateOUT = GsysFunc.fncChangeTDate(txt_DateTime.Text);
                var TimeOUT = txt_Time.Text;
                LocalMachine = "";
                var Faction = lb_Fac.Text;
                var Remark = string.Empty;

                //ค้นหาหมายเลข ID อันสุดท้ายที่เช็คอินเข้ามา
                DataTable DT = new DataTable();
                lvSQL = "Select * From ps_checkin Where EmpID = '" + lvTel + "' And DateIN <> '' Order by id Desc LIMIT 1";
                DT = GsysSQL.fncGetQueryData(lvSQL, DT);

                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    lvID = DT.Rows[i]["ID"].ToString();
                }

                //Update วันและเวลาตอนเช็คเอาท์
                lvSQL = "Update ps_checkin SET DateOUT = '" + DateOUT + "', TimeOUT = '" + TimeOUT + "' Where ID = '" + lvID + "'";
                Success = GsysSQL.fncExecuteQueryData(lvSQL);


                //ถ้าสำเร็จเด้งไปหน้า Finish
                if (Success == "Success")
                {
                    Response.Redirect("Finish.aspx");
                }
            }
            
        }

        protected void txt_Tel_TextChanged(object sender, EventArgs e)
        {
            FncLoadDetail();
        }

        protected void cmb_Visitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        protected void txt_Subject_TextChanged(object sender, EventArgs e)
        {
            Label3.Visible = true;
            txt_Subject.Visible = true;
            Label6.Visible = true;
            lb_Date.Visible = true;
            Label7.Visible = true;
            lb_Time.Visible = true;
            Label5.Visible = true;
            btn_CheckIN.Visible = true;
            btn_CheckOUT.Visible = false;
        }
    }
}