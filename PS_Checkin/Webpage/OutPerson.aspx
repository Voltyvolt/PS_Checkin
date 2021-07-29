<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OutPerson.aspx.cs" Inherits="PS_Checkin.Webpage.OutPerson" %>
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
    <title>บุคคลภายนอก</title>
</head>
<body>
    <form id="form1" runat="server">

         <asp:ScriptManager runat="server">
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
                    <asp:Label ID="Label4" Text="เบอร์โทรศัพท์" runat="server"></asp:Label>
                </div>

                <br />

                <div class="tbox">
                  <dx:ASPxTextBox ID="txt_Tel" runat="server" Width="300px" Theme="iOS" AutoPostBack="True" OnTextChanged="txt_Tel_TextChanged" Font-Size="Medium"></dx:ASPxTextBox>
                </div>

                <br />

                <div class="lael">
                    <asp:Label ID="Label1" Text="ชื่อ - สกุล" runat="server"></asp:Label>
                </div>

                <br />

                <div class="tbox">
                  <dx:ASPxTextBox ID="txt_Name" runat="server" Width="300px" Theme="iOS" Font-Size="Medium"></dx:ASPxTextBox>
                </div>

                <br />

                <div class="lael">
                    <asp:Label ID="Label2" Text="ผู้ที่ต้องการติดต่อ" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="Label8" Text="(หากไม่มีไม่ต้องระบุ)" runat="server" Visible="False"></asp:Label>
                     <asp:Label ID="lb_Fac" runat="server" Visible="False"></asp:Label>
                </div>

                <br />

                 <div class="tbox">
                                       <dx:ASPxGridLookup ID="cmb_Visitor" runat="server" DataSourceID="SQL_EMPLOYEE" Height="35px" Width="300px" KeyFieldName="Employee_ID" Theme="Default" Font-Size="Medium" Visible="False">
                        <GridViewProperties EnableCallBacks="False">
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                        </GridViewProperties>
                    </dx:ASPxGridLookup>
                     <asp:SqlDataSource ID="SQL_EMPLOYEE" runat="server" ConnectionString="<%$ ConnectionStrings:PSConnection %>" ProviderName="<%$ ConnectionStrings:PSConnection.ProviderName %>" SelectCommand="SELECT Employee_ID as 'รหัสพนักงาน', Employee_Name as 'ชื่อ', Employee_LName as 'นามสกุล' FROM employee"></asp:SqlDataSource>
                </div>

                <br />

                 <div class="lael">
                    <asp:Label ID="Label3" Text="เรื่องที่ติดต่อ" runat="server" Visible="False"></asp:Label>
                </div>

                <br />

                 <div class="tbox">
                  <dx:ASPxTextBox ID="txt_Subject" PlaceHolder="กรอกข้อมูล" runat="server" Width="300px" Theme="iOS" Visible="False" AutoPostBack="True" OnTextChanged="txt_Subject_TextChanged" Font-Size="Medium"></dx:ASPxTextBox>
                </div>

                <br />

                <div class="lael">
                    <asp:Label ID="Label6" Text="วันที่ : " runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lb_Date" Text="วันที่/เวลา" runat="server" Visible="False"></asp:Label>

                    <asp:Label ID="Label7" Text="เวลา : " runat="server" Visible="False"></asp:Label>
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

                 <br />

                <asp:Button runat="server" Text="เข้า" ID="btn_CheckIN" CssClass="btn" Font-Size="X-Large" OnClick="btn_CheckIN_Click" Visible="False"/>
                <asp:Button runat="server" Text="ออก" ID="btn_CheckOUT" CssClass="btn" Font-Size="X-Large" OnClick="btn_CheckOUT_Click" Visible="False" />

                <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadPanel" Modal="True" Theme="Moderno">
                    </dx:ASPxLoadingPanel>

                </ContentTemplate>
              </asp:UpdatePanel>
                <br />
            </center>
        </div>
    </form>
</body>
</html>
