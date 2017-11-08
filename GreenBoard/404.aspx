<%@ Page Language="C#" AutoEventWireup="true" CodeFile="404.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="App_Themes/Green/GreenStyle.css" rel="stylesheet" />
    <title></title>
    <link href="bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="container text-center">
    <h1>Oops!</h1>
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/Page-Not-Found-18.jpg" OnClick="ImageButton1_Click" />
    </div>
    </form>
</body>
</html>
