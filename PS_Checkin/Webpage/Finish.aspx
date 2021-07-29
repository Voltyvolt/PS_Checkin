<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Finish.aspx.cs" Inherits="PS_Checkin.Webpage.Finish" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<link href="https://fonts.googleapis.com/css2?family=Prompt:wght@500&display=swap" rel="stylesheet" />
<link href="Content/font-awesome.min.css" rel="stylesheet" />
<script src="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.11.2/js/all.min.js" integrity="sha256-qM7QTJSlvtPSxVRjVWNM2OfTAz/3k5ovHOKmKXuYMO4=" crossorigin="anonymous"></script>

    <style>
        body {
	        background-color: #FAEBD7;
            font-family: 'Prompt', sans-serif;
            font-size: 24px;
            color: black;
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

<meta name="viewport" content="width=device-width, initial-scale=1">

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Success</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <dx:ASPxImage ID="ASPxImage1" runat="server" ShowLoadingImage="true" ImageUrl="~/PS_Checkin/PSSugar.jpg" Width="300px"></dx:ASPxImage>
                <br />
                <br />

                <dx:ASPxImage ID="ASPxImage2" runat="server" ShowLoadingImage="true" ImageUrl="~/PS_Checkin/Success_img.png" Width="300px"></dx:ASPxImage>
                <br />
                <br />
                <asp:Label Text="บันทึกข้อมูลสำเร็จ" runat="server"></asp:Label>

                <br />
                <br />
                <dx:ASPxButton ID="ASPxButton1" runat="server" Height="35px" OnClick="ASPxButton1_Click" Text="ปิด" Theme="RedWine" Width="150px" Visible="False">
                </dx:ASPxButton>

            </center>
        </div>
    </form>
</body>
</html>
