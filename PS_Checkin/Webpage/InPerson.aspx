<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InPerson.aspx.cs" Inherits="PS_Checkin.Webpage.InPerson" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
<link href="https://fonts.googleapis.com/css2?family=Prompt:wght@500&display=swap" rel="stylesheet" />
<link href="Content/font-awesome.min.css" rel="stylesheet" />
<link href="../Content/bootstrap.min.css" rel="stylesheet" />

<style>
    body {
	            background-color: #FAEBD7;
            }

        .panel {
            font-family: 'Prompt', sans-serif;
            font-size: 24px;
            color: black;
        }

        .panel-control {
            font-family: 'Prompt', sans-serif;
            font-size: 24px;
            color: black;
        }

        .lael{
            font-size: 20px
        }

        .tbox{
            font-size: 20px;
        }

        .btn {
            width: 200px;
            height: 150px;
            background-color: #5F9EA0;
            color: white;
        }

</style>
<meta name="viewport" content="width=device-width, initial-scale=1">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>บุคคลภายใน</title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager EnablePartialRendering="true" ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div class="panel-control">
            <center>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <br />
                <dx:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="true" ImageUrl="~/PS_Checkin/PSSugar.jpg" Width="300px"></dx:ASPxImage>
                <br />
                <br />
                <div>
                    <asp:Label ID="lb_Faction" Text="แผนกต่างๆ" runat="server"></asp:Label>
                </div>

                <br />
                
                <div class="lael">
                    <asp:Label ID="Label1" Text="ชื่อ/รหัสพนักงาน ผู้ติดต่อ" runat="server"></asp:Label>
                </div>

               
                    <dx:ASPxGridLookup ID="txt_EmpID" runat="server" DataSourceID="SQL_EMPLOYEE" Height="35px" Width="300px" KeyFieldName="Employee_ID" Theme="Default">
                        <GridViewProperties EnableCallBacks="False">
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                        </GridViewProperties>
                    </dx:ASPxGridLookup>
                     <asp:SqlDataSource ID="SQL_EMPLOYEE" runat="server" ConnectionString="<%$ ConnectionStrings:PSConnection %>" ProviderName="<%$ ConnectionStrings:PSConnection.ProviderName %>" SelectCommand="SELECT Employee_ID, Employee_Name, Employee_LName FROM employee"></asp:SqlDataSource>
                     <dx:ASPxTextBox ID="txt_EmpOUT" PlaceHolder="ชื่อผู้เข้าใช้" runat="server" Width="300px" Theme="iOS"></dx:ASPxTextBox>
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="ตรวจสอบข้อมูล" Theme="Material" OnClick="ASPxButton1_Click" style="height: 26px">
                    </dx:ASPxButton>

                <br />
                <br />

            
                <div class="lael">
                    <asp:Label ID="Label2" Text="ผู้รับเรื่อง" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lb_local" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="lb_Fac" runat="server" Visible="False"></asp:Label>
                </div>

                <div class="tbox">
                   <dx:ASPxComboBox ID="cmb_Visitor" CssClass="form-control" Width="300px" runat="server" ValueType="System.String" Theme="Glass" AutoPostBack="True" OnSelectedIndexChanged="cmb_Visitor_SelectedIndexChanged" OnValueChanged="cmb_Visitor_ValueChanged" Visible="False"></dx:ASPxComboBox>
                </div>

                <br />

                <div class="lael">
                    <asp:Label ID="Label3" Text="เรื่องที่ติดต่อ" runat="server" Visible="False"></asp:Label>
                </div>

                <div class="tbox">
                  <dx:ASPxTextBox ID="txt_Subject" PlaceHolder="กรอกข้อมูล" runat="server" Width="300px" Theme="iOS" Visible="False"></dx:ASPxTextBox>
                </div>

                <br />

                <div class="lael">
                    <asp:Label ID="Label4" Text="วันที่ : " runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lb_Date" Text="วันที่/เวลา" runat="server" Visible="False"></asp:Label>
                    </div>
                   
                <br />

                <div class="lael">
                     <asp:Label ID="Label6" Text="เวลา : " runat="server" Visible="False"></asp:Label>
                 <asp:Label ID="lb_Time" Text="วันที่/เวลา" runat="server" Visible="False"></asp:Label>
                    </div>

                <div>
                    <dx:ASPxDateEdit ID="txt_DateTime" Date="2021-07-16" Font-Size="Medium" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" CssClass="form-control" Width="250px" runat="server" Theme="Glass" Visible="False"></dx:ASPxDateEdit>
                   <dx:ASPxTimeEdit ID="txt_Time" runat="server" Width="250px" Font-Size="Medium" CssClass="form-control" DisplayFormatString="HH:mm" EditFormat="Custom" EditFormatString="HH:mm" Theme="Glass" Visible="False"></dx:ASPxTimeEdit>
                </div>
                  
                <br />

                <div class="lael">
                    <asp:Label ID="Label5" Text="กรุณาเช็คอินหรือเช็คเอาท์" runat="server" Visible="False"></asp:Label>
                </div>


                <div>
                    <asp:Button runat="server" Text="เข้า" ID="btn_CheckIN" CssClass="btn" OnClick="btn_CheckIN_Click" Font-Size="X-Large" Visible="False"/>
                </div>

                <br />

                <div>
                     <asp:Button runat="server" Text="ออก" ID="btn_CheckOUT" CssClass="btn" OnClick="btn_CheckOUT_Click" Font-Size="X-Large" Visible="False" />
                </div>
                
                  <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadPanel" Modal="True" Theme="Moderno">
                    </dx:ASPxLoadingPanel>

                 </ContentTemplate>
              </asp:UpdatePanel>
            </center>
        </div>
    </form>
</body>
</html>
