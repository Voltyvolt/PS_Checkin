﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text;
using System.Data;

namespace PS_Checkin.Webpage
{
    public partial class InPerson : System.Web.UI.Page
    {
        string lvFaction = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //รับข้อมูลแผนกจาก Link
                var lvData = Request.QueryString["Data"].ToString();

                //แยกข้อมูลต่างๆ
                string[] lvArr = lvData.Split(':');
                lvFaction = lvArr[1];
                var lvFactionCode = lvFaction;
                
                //ค้นหารหัสแผนก
                

                //เข้าฟังก์ชันโหลดข้อมูลลง Combobox
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
            var Remark = string.Empty;

            //Insert ข้อมูล
            var lvSQL = "Insert Into ps_checkin (EmpID, EmpName, VisitorID, VisitorName, Subject, DateIN, TimeIN, DateOUT, TimeOUT, Remark) " +
                "Values ('" + EmpID + "', '" + EmpName + "', '" + VisitorID + "', '" + VisitorName + "', '" + Subject + "', '" + DateIN + "', '" + TimeIN + "', " +
                "'" + DateOUT + "', '" + TimeOUT + "', '" + Remark + "')";
            var Success = GsysSQL.fncExecuteQueryData(lvSQL);

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
    }
}