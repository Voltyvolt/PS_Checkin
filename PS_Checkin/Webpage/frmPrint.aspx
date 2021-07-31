<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmPrint.aspx.cs" Inherits="PS_Checkin.Webpage.frmPrint" %>

<%@ Register Assembly="DevExpress.XtraReports.v17.1.Web, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายงาน</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1" runat="server" Height="1100px" ReportTypeName="Report1" Theme="Glass" Width="100%">
            </dx:ASPxDocumentViewer>
        </div>
    </form>
</body>
</html>
