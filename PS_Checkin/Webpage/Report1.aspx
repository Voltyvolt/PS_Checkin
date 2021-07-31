<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report1.aspx.cs" Inherits="PS_Checkin.Webpage.Report1" %>
<%@ Register Assembly="DevExpress.Web.Bootstrap.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.Bootstrap" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
<link href="https://fonts.googleapis.com/css2?family=Prompt:wght@500&display=swap" rel="stylesheet" />
<link href="Content/font-awesome.min.css" rel="stylesheet" />
<link href="../Content/bootstrap.min.css" rel="stylesheet" />

<style>
    body {
	            background-color: white;
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
    <title>รายงานการเข้า - ออก</title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div class="panel-control">
                <br />
                    <div>
                       <asp:Label ID="lb_Faction" Text="รายงานการเข้า - ออก บริษัทน้ำตาลพิษณุโลก จำกัด" runat="server"></asp:Label>
                    </div>

                <br />

                <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server">
                    <Items>
                        <dx:LayoutGroup Caption="ค้นหาข้อมูล" ColCount="12">
                            <Items>
                                <dx:LayoutItem Caption="วันที่" ColSpan="3">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxDateEdit ID="txt_Date" runat="server"  DisplayFormatString="dd/MM/yyyy" EditFormat="Custom" EditFormatString="dd/MM/yyyy">
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="แผนก" ColSpan="3">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxComboBox ID="cmb_Faction" runat="server">
                                            </dx:ASPxComboBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="" ColSpan="3">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer runat="server">
                                            <dx:ASPxButton ID="btn_Search" runat="server" OnClick="btn_Search_Click" Text="ค้นหา" Theme="MetropolisBlue" Width="100px">
                                            </dx:ASPxButton>
                                            <dx:ASPxButton ID="btn_Clear" runat="server" OnClick="btn_Clear_Click" Text="ล้างข้อมูล" Theme="MetropolisBlue" Width="100px">
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>

                    <br />

   <dx:ASPxPanel ID="ASPxPanel1" Width="80%" runat="server"><PanelCollection>
    <dx:PanelContent runat="server">
        <dx:BootstrapGridView ID="BootstrapGridView1" runat="server" AutoGenerateColumns="False" OnPageIndexChanged="BootstrapGridView1_PageIndexChanged">
            <SettingsPager PageSize="20">
            </SettingsPager>
        <Columns>
            <dx:BootstrapGridViewTextColumn Caption="ลำดับ" FieldName="Id" VisibleIndex="0">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn Caption="ผู้ติดต่อ" VisibleIndex="2" FieldName="EmpName">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn Caption="ผู้รับเรื่อง" VisibleIndex="3" FieldName="VisitorName">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn Caption="เรื่องที่ติดต่อ" VisibleIndex="4" FieldName="Subject">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn Caption="วันที่" VisibleIndex="5" FieldName="DateIN">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn Caption="แผนก" VisibleIndex="8" FieldName="Faction">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn Caption="เวลาเข้า" VisibleIndex="6" FieldName="TimeIN">
            </dx:BootstrapGridViewTextColumn>
            <dx:BootstrapGridViewTextColumn Caption="เวลาออก" VisibleIndex="7" FieldName="TimeOUT">
            </dx:BootstrapGridViewTextColumn>
        </Columns>
    </dx:BootstrapGridView>
        <br />
        <dx:ASPxButton ID="btn_Print" runat="server" OnClick="btn_Print_Click" Text="พิมพ์รายงาน" Theme="Material">
        </dx:ASPxButton>
   </dx:PanelContent>
</PanelCollection>
                </dx:ASPxPanel>

            </div>
            
        </center>
        
    </form>
</body>
</html>
