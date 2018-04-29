<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoAccess.aspx.cs" Inherits="NoAccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="style.css" rel="stylesheet" />
    <title>Nucleus - No Access</title>
	<meta charset="utf-8" />
    <script src="script.js"></script>
</head>
<body>

    <div id="header">
        <table id="headerTable">
            <tr>
                <th><img src="img/logo.png" style="width: 18%; margin: 5px" /></th>
            </tr>
        </table>
    </div>

    <div id="regContainer">

        <form id="Form" runat="server">
            <div class="main" style="text-align: center">
                        <span style="font-size: 20px;   ">You are not allowed to access this page.</span><br /><p></p>
                <a href="Default.aspx" style="color:#46629e">Click here to go to the home page</a>

            </div>
        </form>

    </div>
</body>
</html>
