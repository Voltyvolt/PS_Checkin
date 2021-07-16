using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PS_Checkin
{
    public class GsysSQL
    {
        #region //Execute Data
        public static DataTable fncGetQueryData(string lvSQL, DataTable dt)
        {
            string query = lvSQL;
            DataTable DTReturn = new DataTable();
            MySqlDataAdapter DA = new MySqlDataAdapter(query, System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            DA.Fill(DTReturn);

            return DTReturn;
        }

        public static DataTable fncGetQueryDataMCSS(string lvSQL, DataTable dt)
        {
            string query = lvSQL;
            DataTable DTReturn = new DataTable();
            SqlDataAdapter DA = new SqlDataAdapter(query, System.Configuration.ConfigurationManager.ConnectionStrings["MCSS_Connection"].ToString());
            DA.Fill(DTReturn);

            return DTReturn;
        }

        public static string fncExecuteQueryData(string lvSQL)
        {
            string lvReturn = "";
            try
            {
                string query = lvSQL;
                MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
                MySqlCommand cmd = new MySqlCommand();

                cmd.Connection = con;
                con.Open();
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                con.Close();
                con.Dispose();

                lvReturn = "Success";
                return lvReturn;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region //Check Data
        public static string fncCheckLogin(string lvUser, string lvPass)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT * FROM SysUser WHERE us_UserID = '" + lvUser + "' AND us_Password = '" + lvPass + "' And us_Active = '1' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["us_UserID"].ToString();
                    //GVar.gvFirstUrl = dr["us_URL"].ToString();
                    //GVar.gvKet = dr["us_Ket"].ToString();
                    //GVar.gvUserType = dr["us_Type"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncCheckUser(string lvUser)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT us_UserID FROM SysUser WHERE us_UserID = '" + lvUser + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["us_UserID"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }
        public static string fncCheckPass(string lvUser)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT us_Password FROM SysUser WHERE us_UserID = '" + lvUser + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["us_Password"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }
        public static string fncCheckEmail(string lvUser)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT us_Email FROM SysUser WHERE us_UserID = '" + lvUser + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["us_Email"].ToString();
                }
            }
            else
            {
                lvReturn = "";
            }

            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncCheckNew(string lvUser, string lvCode)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Permission_New FROM syspermission WHERE Permission_UserID = '" + lvUser + "' and Permission_Code = '" + lvCode + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["Permission_New"].ToString();
                }
            }
            else
            {
                lvReturn = "";
            }

            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncCheckEdit(string lvUser, string lvCode)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Permission_Edit FROM syspermission WHERE Permission_UserID = '" + lvUser + "' and Permission_Code = '" + lvCode + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["Permission_Edit"].ToString();
                }
            }
            else
            {
                lvReturn = "";
            }

            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncCheckDelete(string lvUser, string lvCode)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Permission_Del FROM syspermission WHERE Permission_UserID = '" + lvUser + "' and Permission_Code = '" + lvCode + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["Permission_Del"].ToString();
                }
            }
            else
            {
                lvReturn = "";
            }

            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncCheckApprove(string lvUser, string lvCode)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Permission_AppDoc FROM syspermission WHERE Permission_UserID = '" + lvUser + "' and Permission_Code = '" + lvCode + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["Permission_AppDoc"].ToString();
                }
            }
            else
            {
                lvReturn = "";
            }

            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncCheckCancel(string lvUser, string lvCode)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Permission_CancelDoc FROM syspermission WHERE Permission_UserID = '" + lvUser + "' and Permission_Code = '" + lvCode + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["Permission_CancelDoc"].ToString();
                }
            }
            else
            {
                lvReturn = "";
            }

            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncCheckPrint(string lvUser, string lvCode)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Permission_Print FROM syspermission WHERE Permission_UserID = '" + lvUser + "' and Permission_Code = '" + lvCode + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["Permission_Print"].ToString();
                }
            }
            else
            {
                lvReturn = "";
            }

            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncCheckEntry(string lvUser, string lvCode)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Permission_Entry FROM syspermission WHERE Permission_UserID = '" + lvUser + "' and Permission_Code = '" + lvCode + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["Permission_Entry"].ToString();
                }
            }
            else
            {
                lvReturn = "";
            }

            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncGetEmpID(string lvUser)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT us_EmpID FROM SysUser WHERE us_UserID = '" + lvUser + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["us_EmpID"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncGetLastID()
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Permission_ID FROM syspermission Order by Permission_ID DESC LIMIT 1";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["Permission_ID"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncGetSecID(string lvSecname)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Section_ID FROM emp_section Where Section_Name = '" + lvSecname + "'";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["Section_ID"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncGetSegID(string lvSegname)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Segment_ID FROM emp_segment Where Segment_Name = '" + lvSegname + "'";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["Segment_ID"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncGetFacID(string lvFacname)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT Faction_ID FROM emp_faction Where Faction_Name = '" + lvFacname + "'";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["Faction_ID"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncGenDocNo(string lvDocCode)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT * FROM SysDocNo WHERE S_MCode = '" + lvDocCode + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    //GenDoc
                    string lvShort = dr["S_ShortCode"].ToString();
                    int lvYearChk = DateTime.Now.Year;
                    if (lvYearChk < 2500) lvYearChk += 543;
                    string lvYear = (lvYearChk - 2500).ToString();
                    int lvMonth = DateTime.Now.Month;
                    int lvDay = DateTime.Now.Day;
                    string lvSection = "121";
                    int lvRunDoc = GsysFunc.fncToInt(dr["S_RunNo"].ToString());

                    string lvDocID = "";
                    if (dr["S_TypeGen"].ToString() == "YYMMDept")
                        lvDocID = lvShort + lvYear.ToString() + lvMonth.ToString("00") + lvSection + lvRunDoc.ToString(dr["S_Format"].ToString());
                    else if (dr["S_TypeGen"].ToString() == "YYMMdd")
                        lvDocID = lvShort + lvYear.ToString() + lvMonth.ToString("00") + lvDay.ToString("00") + lvRunDoc.ToString(dr["S_Format"].ToString());

                    lvReturn = lvDocID;
                }
            }
            else
            {
                lvReturn = "";
            }

            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncCheckLicenseID(string lvProductKey)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "Select c_LicenseID From c_key Where c_ProductKey like '" + lvProductKey + "' ";
            // cmd.CommandText = "Select c_ProductKey From c_key Where c_Product like '" + lvProductKey + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["c_LicenseID"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncCheckEffecttive(string lvLicenseID)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "Select c_Effective From c_key Where C_LicenseID like '" + lvLicenseID + "' ";
            // cmd.CommandText = "Select c_ProductKey From c_key Where c_Product like '" + lvProductKey + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["c_Effective"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncCheckUnresolve(string lvLicenseID)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "Select c_Unresolve From c_key Where C_LicenseID like '" + lvLicenseID + "' ";
            // cmd.CommandText = "Select c_ProductKey From c_key Where c_Product like '" + lvProductKey + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["c_Unresolve"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncCheckMakActive(string lvLicenseID)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "Select c_MakActivate From c_key Where c_ProductKey like '" + lvLicenseID + "' ";
            // cmd.CommandText = "Select c_ProductKey From c_key Where c_Product like '" + lvProductKey + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["c_MakActivate"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }
        #endregion

        public static string fncCheckOnlineStatus(string lvUser)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT * FROM SysLoginTable WHERE L_UserName = '" + lvUser + "' ";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["L_UserName"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }

        public static string fncCheckFactionCode(string lvFaction)
        {
            #region //Connect Database 
            MySqlConnection con = new MySqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PSConnection"].ToString());
            MySqlCommand cmd = new MySqlCommand();
            MySqlDataReader dr;
            #endregion  

            string lvReturn = "";

            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "SELECT * FROM cane_activityimage";
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lvReturn = dr["PlanId"].ToString();
                }
            }
            dr.Close();
            con.Close();

            return lvReturn;
        }

        
    }
}