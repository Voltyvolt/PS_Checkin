using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PS_Checkin.Webpage
{
    public partial class Report1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }

            fncLoadAllGridview();

            txt_Date.Text = DateTime.Now.ToString("dd/MM/yyyy");
            fncLoadFaction();
        }

        private void fncLoadAllGridview()
        {
            DataTable DT = new DataTable();
            var SQL = "Select * From ps_checkin Where Remark <> 'Test' ORDER BY id DESC LIMIT 30";
            DT = GsysSQL.fncGetQueryData(SQL, DT);

            var Numrow = DT.Rows.Count;

            var ID = string.Empty;
            var EmpID = string.Empty;
            var EmpName = string.Empty;
            var VisitorID = string.Empty;
            var VisitorName = string.Empty;
            var Subject = string.Empty;
            var DateIN = string.Empty;
            var TimeIN = string.Empty;
            var TimeOUT = string.Empty;
            var Faction = string.Empty;

            DataTable NewDT = new DataTable();
            NewDT.Columns.Add("Id");
            NewDT.Columns.Add("EmpID");
            NewDT.Columns.Add("EmpName");
            NewDT.Columns.Add("VisitorID");
            NewDT.Columns.Add("VisitorName");
            NewDT.Columns.Add("Subject");
            NewDT.Columns.Add("DateIN");
            NewDT.Columns.Add("TimeIN");
            NewDT.Columns.Add("TimeOUT");
            NewDT.Columns.Add("Faction");

            for (int i = 0; i < Numrow; i++)
            {
                ID = (i + 1).ToString();
                EmpID = DT.Rows[i]["EmpID"].ToString();
                EmpName = DT.Rows[i]["EmpName"].ToString();
                VisitorID = DT.Rows[i]["VisitorID"].ToString();
                VisitorName = DT.Rows[i]["VisitorName"].ToString();
                Subject = DT.Rows[i]["Subject"].ToString();
                DateIN = GsysFunc.fncChangeSDate(DT.Rows[i]["DateIN"].ToString());
                TimeIN = DT.Rows[i]["TimeIN"].ToString();
                TimeOUT = DT.Rows[i]["TimeOUT"].ToString();
                Faction = DT.Rows[i]["Faction"].ToString();

                NewDT.Rows.Add(new object[] { ID, EmpID, EmpName, VisitorID, VisitorName, Subject, DateIN, TimeIN, TimeOUT, Faction });
            }
            
            BootstrapGridView1.DataSource = null;
            BootstrapGridView1.DataSource = NewDT;
            BootstrapGridView1.DataBind();
        }

        private void fncLoadFaction()
        {
            DataTable DT = new DataTable();
            var SQL = "Select Faction_Name From emp_faction";
            DT = GsysSQL.fncGetQueryData(SQL, DT);

            cmb_Faction.Items.Clear();
            for (int i = 0; i < DT.Rows.Count; i++)
            {
                var items = DT.Rows[i]["Faction_Name"].ToString();
                cmb_Faction.Items.Add(items);
            }
        }

        private void fncLoadGridviewWhere(string lvFaction, string lvDate)
        {
            DataTable DT = new DataTable();
            var SQL = "Select * From ps_checkin Inner Join emp_faction on ps_checkin.Faction = emp_faction.Faction_ID Where DateIN = '" + lvDate + "' AND Faction_Name = '" + lvFaction + "' AND Remark <> 'Test' ORDER BY id DESC LIMIT 30";
            DT = GsysSQL.fncGetQueryData(SQL, DT);

            var Numrow = DT.Rows.Count;

            var ID = string.Empty;
            var EmpID = string.Empty;
            var EmpName = string.Empty;
            var VisitorID = string.Empty;
            var VisitorName = string.Empty;
            var Subject = string.Empty;
            var DateIN = string.Empty;
            var TimeIN = string.Empty;
            var TimeOUT = string.Empty;
            var Faction = string.Empty;

            DataTable NewDT = new DataTable();
            NewDT.Columns.Add("Id");
            NewDT.Columns.Add("EmpID");
            NewDT.Columns.Add("EmpName");
            NewDT.Columns.Add("VisitorID");
            NewDT.Columns.Add("VisitorName");
            NewDT.Columns.Add("Subject");
            NewDT.Columns.Add("DateIN");
            NewDT.Columns.Add("TimeIN");
            NewDT.Columns.Add("TimeOUT");
            NewDT.Columns.Add("Faction");

            for (int i = 0; i < Numrow; i++)
            {
                ID = (i + 1).ToString();
                EmpID = DT.Rows[i]["EmpID"].ToString();
                EmpName = DT.Rows[i]["EmpName"].ToString();
                VisitorID = DT.Rows[i]["VisitorID"].ToString();
                VisitorName = DT.Rows[i]["VisitorName"].ToString();
                Subject = DT.Rows[i]["Subject"].ToString();
                DateIN = GsysFunc.fncChangeSDate(DT.Rows[i]["DateIN"].ToString());
                TimeIN = DT.Rows[i]["TimeIN"].ToString();
                TimeOUT = DT.Rows[i]["TimeOUT"].ToString();
                Faction = DT.Rows[i]["Faction"].ToString();

                NewDT.Rows.Add(new object[] { ID, EmpID, EmpName, VisitorID, VisitorName, Subject, DateIN, TimeIN, TimeOUT, Faction });
            }

            BootstrapGridView1.DataSource = null;
            BootstrapGridView1.DataSource = NewDT;
            BootstrapGridView1.DataBind();
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            var lvDate = GsysFunc.fncChangeTDate(txt_Date.Text);
            var lvFaction = cmb_Faction.Text;
            fncLoadGridviewWhere(lvFaction, lvDate);
        }

        protected void BootstrapGridView1_PageIndexChanged(object sender, EventArgs e)
        {
            var lvDate = GsysFunc.fncChangeTDate(txt_Date.Text);
            var lvFaction = cmb_Faction.Text;
            if(lvFaction != "")
            {
                fncLoadGridviewWhere(lvFaction, lvDate);
            }
            else
            {
                fncLoadAllGridview();
            }
           
        }

        protected void btn_Clear_Click(object sender, EventArgs e)
        {
            txt_Date.Text = "";
            cmb_Faction.Text = "";
            fncLoadAllGridview();
        }

        protected void btn_Print_Click(object sender, EventArgs e)
        {
            try
            {
                int index = BootstrapGridView1.VisibleRowCount;

                var SQL = string.Empty;
                var Success = string.Empty;
                var S_Project = "PS_Checkin";

                SQL = "Delete From systemp Where S_Project = '" + S_Project + "' ";
                Success = GsysSQL.fncExecuteQueryData(SQL);

                var EmpName = string.Empty;
                var VisitorName = string.Empty;
                var Subject = string.Empty;
                var DateIN = string.Empty;
                var Date = string.Empty;
                var TimeIN = string.Empty;
                var TimeOUT = string.Empty;

                for (int i = 0; i < index; i++)
                {
                    EmpName = BootstrapGridView1.GetRowValues(i, "EmpName").ToString(); //1
                    VisitorName = BootstrapGridView1.GetRowValues(i, "VisitorName").ToString(); //2
                    Subject = BootstrapGridView1.GetRowValues(i, "Subject").ToString(); //3
                    DateIN = BootstrapGridView1.GetRowValues(i, "DateIN").ToString(); //4
                    TimeIN = BootstrapGridView1.GetRowValues(i, "TimeIN").ToString(); //5
                    TimeOUT = BootstrapGridView1.GetRowValues(i, "TimeOUT").ToString(); //6

                    SQL = "Insert Into systemp (S_Field1, S_Field2, S_Field3, S_Field4, S_Field5, S_Field6, S_Project) " +
                        "Values ('" + EmpName + "', '" + VisitorName + "', '" + Subject + "', '" + DateIN + "', '" + TimeIN + "', '" + TimeOUT + "', '" + S_Project + "')";
                    Success = GsysSQL.fncExecuteQueryData(SQL);
                }

                if (Success == "Success")
                {
                    Response.Redirect("frmPrint.aspx");
                }
            }
            catch(Exception ex)
            {

            }
           
        }
    }
}