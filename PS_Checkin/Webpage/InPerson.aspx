﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InPerson.aspx.cs" Inherits="PS_Checkin.Webpage.InPerson" %>

<%@ Register Assembly="DevExpress.Web.Bootstrap.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

 <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

<webopt:bundlereference runat="server" path="~/Content/css" />
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


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>บุคคลภายใน</title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager runat="server">
            <Scripts>
                <%--To learn more about bundling scripts in ScriptManager see https://go.microsoft.com/fwlink/?LinkID=301884 --%>
                <%--Framework Scripts--%>
                <asp:ScriptReference Name="MsAjaxBundle" />
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Name="WebForms.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebForms.js" />
                <asp:ScriptReference Name="WebUIValidation.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebUIValidation.js" />
                <asp:ScriptReference Name="MenuStandards.js" Assembly="System.Web" Path="~/Scripts/WebForms/MenuStandards.js" />
                <asp:ScriptReference Name="GridView.js" Assembly="System.Web" Path="~/Scripts/WebForms/GridView.js" />
                <asp:ScriptReference Name="DetailsView.js" Assembly="System.Web" Path="~/Scripts/WebForms/DetailsView.js" />
                <asp:ScriptReference Name="TreeView.js" Assembly="System.Web" Path="~/Scripts/WebForms/TreeView.js" />
                <asp:ScriptReference Name="WebParts.js" Assembly="System.Web" Path="~/Scripts/WebForms/WebParts.js" />
                <asp:ScriptReference Name="Focus.js" Assembly="System.Web" Path="~/Scripts/WebForms/Focus.js" />
                <asp:ScriptReference Name="WebFormsBundle" />
                <%--Site Scripts--%>
            </Scripts>
        </asp:ScriptManager>

        <div class="panel-control">
            <center>
                <br />
                <dx:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="true" ImageUrl="~/Webpage/PSSugar.jpg" Width="300px"></dx:ASPxImage>
                <br />
                <br />
                <div>
                    <asp:Label ID="lb_Faction" Text="แผนกต่างๆ" runat="server"></asp:Label>
                    <br />
                    <br />
                </div>

                
                <div class="lael">
                    <asp:Label ID="Label1" Text="ชื่อ/รหัสพนักงาน" runat="server"></asp:Label>
                </div>


            <br />

            
                <div class="tbox">
                    <dx:ASPxGridLookup ID="txtEmpID" runat="server" DataSourceID="SQL_EMPLOYEE" Height="35px" HorizontalAlign="Center" Width="500px" KeyFieldName="Employee_ID" AutoPostBack="True" Theme="Glass">
                        <GridViewProperties EnableCallBacks="False">
                            <SettingsBehavior AllowFocusedRow="True" AllowSelectSingleRowOnly="True" />
                        </GridViewProperties>
                    </dx:ASPxGridLookup>
                     <asp:SqlDataSource ID="SQL_EMPLOYEE" runat="server" ConnectionString="<%$ ConnectionStrings:PSConnection %>" ProviderName="<%$ ConnectionStrings:PSConnection.ProviderName %>" SelectCommand="SELECT Employee_ID, Employee_Name, Employee_LName FROM employee"></asp:SqlDataSource>
                </div>
            <br />

            
                <div class="lael">
                    <asp:Label ID="Label2" Text="ผู้ติดต่อ" runat="server"></asp:Label>
                </div>

                <br />

                <div class="tbox">
                   <dx:ASPxComboBox ID="cmb_Visitor" CssClass="form-control" Width="500px" runat="server" ValueType="System.String" Theme="Glass"></dx:ASPxComboBox>
                </div>

                <br />

                <div class="lael">
                    <asp:Label ID="Label3" Text="เรื่องที่ติดต่อ" runat="server"></asp:Label>
                </div>

                <br />

                <div class="tbox">
                  <dx:ASPxTextBox ID="txt_Subject" PlaceHolder="กรอกข้อมูล" runat="server" Width="500px" Theme="iOS"></dx:ASPxTextBox>
                </div>

                <br />

                <div class="lael">
                    <asp:Label ID="Label4" Text="วันที่/เวลา" runat="server"></asp:Label>
                </div>

                <br />

                <div>
                    <dx:ASPxDateEdit ID="txt_DateTime" Date="2021-07-16" Font-Size="Medium" DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy" CssClass="form-control" Width="250px" runat="server" Theme="Glass"></dx:ASPxDateEdit>
                   <dx:ASPxTimeEdit ID="txt_Time" runat="server" Width="250px" Font-Size="Medium" CssClass="form-control" DisplayFormatString="HH:mm" EditFormat="Custom" EditFormatString="HH:mm" Theme="Glass"></dx:ASPxTimeEdit>
                </div>
                  
                
                <br />

                <div class="lael">
                    <asp:Label ID="Label5" Text="กรุณาเช็คอินหรือเช็คเอาท์" runat="server"></asp:Label>
                </div>

                <br />

                <asp:Button runat="server" Text="เข้า" ID="btn_CheckIN" CssClass="btn" OnClick="btn_CheckIN_Click"/>
                <asp:Button runat="server" Text="ออก" ID="btn_CheckOUT" CssClass="btn" OnClick="btn_CheckOUT_Click" />

            </center>

            
             

        </div>
    </form>
</body>
</html>