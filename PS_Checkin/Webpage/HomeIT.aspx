<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomeIT.aspx.cs" Inherits="PS_Checkin.Webpage.HomeIT" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<link href="https://fonts.googleapis.com/css2?family=Prompt:wght@500&display=swap" rel="stylesheet" />
<link href="Content/font-awesome.min.css" rel="stylesheet" />
<script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.11.2/js/all.min.js" integrity="sha256-qM7QTJSlvtPSxVRjVWNM2OfTAz/3k5ovHOKmKXuYMO4=" crossorigin="anonymous"></script>

    <style>
            body {
	            background-color: #FAEBD7;
            }

        .panel {
            font-family: 'Prompt', sans-serif;
            font-size: 24px;
            color: black;
        }

        .btnsubmit {
            font-family: 'Prompt', sans-serif;
            font-size: 26px;
            border: none;
            outline: none;
            height: 150px;
            width: 200px;
            font-size: 16px;
            color: white;
            background-color: black;
            cursor: pointer;
            border-radius: 20px;
            transition: .3s ease-in-out;
        }

            .btnsubmit:hover {
                background-color: darkgrey;
            }
}
</style>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <br />
                <br />
                <br />
                <br />
                <br />
                <dx:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="true" ImageUrl="~/Webpage/PSSugar.jpg" Width="300px"></dx:ASPxImage>
                <br />
                <br />
                <br />
                <div class="panel">
                    <asp:Label Text="ประเภทบุคคล" runat="server"></asp:Label>
                    <br />
                    <br />
                    <asp:Button runat="server" CssClass="btnsubmit" Text="บุคคลภายใน" ID="btn_PersonIn" OnClick="btn_PersonIn_Click" />
                    <asp:Button runat="server" CssClass="btnsubmit" Text="บุคคลภายนอก" ID="btn_PersonOut" OnClick="btn_PersonOut_Click" />
                </div>
            </center>
        </div>
    </form>
</body>
</html>
