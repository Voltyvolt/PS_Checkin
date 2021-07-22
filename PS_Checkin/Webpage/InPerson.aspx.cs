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
                txt_EmpOUT.Visible = false;
                btn_CheckOUT.Enabled = false;
                //รับข้อมูลแผนกจาก Link
                var lvData = Request.QueryString["Data"].ToString();

                //ค้นหาหมายเลข Local IP
                IPAddress host = IPAddress.None;
                var ips = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (IPAddress ip in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    host = ip;
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        break;
                }

                LocalMachine = host.ToString();
                //LocalMachine = ips.ToString();
                lb_local.Text = LocalMachine;


                //แยกข้อมูลต่างๆ
                string[] lvArr = lvData.Split(':');
                lvFaction = lvArr[1];
                var lvFactionCode = lvFaction;
                var factionName = GsysSQL.fncCheckFactionName(lvFactionCode);

                //เข้าฟังก์ชันโหลดข้อมูลลง Combobox
                lb_Faction.Text = factionName;
                fncLoadComboboxEmp(lvFactionCode);
                txt_DateTime.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txt_Time.Text = DateTime.Now.ToString("HH:mm");


            }
        }
        
        private void fncLoadComboboxEmp(string lvFactionCode)
        {
            //โหลดรหัสพนักงาน/ชื่อ 
            DataTable DT = new DataTable();
            var lvSQL = "Select Employee_ID, Employee_Name, Employee_LName From employee Where Employee_Faction IN ('" + lvFactionCode + "')";
            DT = GsysSQL.fncGetQueryData(lvSQL, DT);

            var EmpID = string.Empty;
            var EmpName = string.Empty;
            var EmpLName = string.Empty;
            var EmpFullname = string.Empty;

            cmb_Visitor.Items.Clear();

            //วนลูปป้อนข้อมูลลงใน Combobox
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                EmpID = DT.Rows[i]["Employee_ID"].ToString();
                EmpName = DT.Rows[i]["Employee_Name"].ToString();
                EmpLName = DT.Rows[i]["Employee_LName"].ToString();
                EmpFullname = EmpID + " " + EmpName + " " + EmpLName;

                cmb_Visitor.Items.Add(EmpFullname);
            }
        }

        protected void btn_CheckIN_Click(object sender, EventArgs e)
        {
            //ประกาศตัวแปร
            var lvSQL = string.Empty;
            var Success = string.Empty;
            var Emp = txtEmpID.Text;
            var EmpSplit = Emp.Split(';');
            var EmpID = EmpSplit[0];
            var EmpName = EmpSplit[1] + " " + EmpSplit[2];
            var Visitor = cmb_Visitor.Text;
            var VisitorSplit = Visitor.Split(' ');
            var VisitorID = VisitorSplit[0];
            var VisitorName = VisitorSplit[1] + " " + VisitorSplit[2];
            var Subject = txt_Subject.Text;
            var DateIN = GsysFunc.fncChangeTDate(txt_DateTime.Text);
            var TimeIN = txt_Time.Text;
            var DateOUT = string.Empty;
            var TimeOUT = string.Empty;
            LocalMachine = lb_local.Text;
            var Remark = string.Empty;

            var NameregCheck = GsysSQL.fncCheckCheckRegister(EmpName); //เช็คว่า Emp นี้เคย Register แล้วหรือไม่

            if(NameregCheck == "")
            {
                lvSQL = "Insert Into ps_checkinreg (Name, EmpID_Tel) Values ('" + EmpName + "', '" + EmpID + "')";
                Success = GsysSQL.fncExecuteQueryData(lvSQL);
            }

            //Insert ข้อมูล
            lvSQL = "Insert Into ps_checkin (EmpID, EmpName, VisitorID, VisitorName, Subject, DateIN, TimeIN, DateOUT, TimeOUT, LocalMachine, Remark) " +
                "Values ('" + EmpID + "', '" + EmpName + "', '" + VisitorID + "', '" + VisitorName + "', '" + Subject + "', '" + DateIN + "', '" + TimeIN + "', " +
                "'" + DateOUT + "', '" + TimeOUT + "', '" + LocalMachine + "', '" + Remark + "')";
            Success = GsysSQL.fncExecuteQueryData(lvSQL);

            //ถ้าสำเร็จเด้งไปหน้า Finish
            if(Success == "Success")
            {
                Response.Redirect("Finish.aspx");
            }
        }

        protected void btn_CheckOUT_Click(object sender, EventArgs e)
        {
            //ประกาศตัวแปร
            var lvID = string.Empty;
            var Emp = txtEmpID.Text;
            var EmpSplit = Emp.Split(';');
            var EmpID = EmpSplit[0];
            var EmpName = EmpSplit[1] + " " + EmpSplit[2];
            var Visitor = cmb_Visitor.Text;
            var VisitorSplit = Visitor.Split(' ');
            var VisitorID = VisitorSplit[0];
            var VisitorName = VisitorSplit[1] + " " + VisitorSplit[2];
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
            if(Success == "Success")
            {
                Response.Redirect("Finish.aspx");
            }
        }

        protected void txtEmpID_TextChanged(object sender, EventArgs e)
        {
            //var Emp = txtEmpID.Text;
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
            //    txtEmpID.Visible = true;
            //    txt_EmpOUT.Visible = false;
            //    txtEmpID.Value = Emp2;
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

        protected void txtEmpID_DataBound(object sender, EventArgs e)
        {

        }
    }
}