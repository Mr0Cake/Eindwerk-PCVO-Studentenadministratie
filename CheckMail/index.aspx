<%@ Page Language="C#" AutoEventWireup="true" Inherits="Index" Debug="true" CodeBehind="index.aspx.cs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
	<meta name="viewport" content="width=device-width maximum-scale=1" />
	<title></title>

	<meta http-equiv="X-UA-Compatible" content="IE=100" />
	<asp:PlaceHolder ID="Meta" runat="server" />
	<asp:PlaceHolder ID="CSS" runat="server" />
	<asp:PlaceHolder ID="JS" runat="server" />

	<asp:PlaceHolder ID="headContent" runat="server" />

	<%--<link href="css/layout-temp.css" rel="stylesheet" />--%>
</head>

<body id="Body" runat="server">

	<div class="container">
		<div class="header">
			<div class="main">
				<div class="home"><a href="#"><span>Go to </span>ING.be</a></div>
				<div class="content">
					<div class="languages">
						<asp:Panel ID="DesktopLanguages" runat="server">
						</asp:Panel>
						<div class="wrap">
							<form id="LanguageSelectionResponsive" runat="server">
								<asp:DropDownList ID="languages" data-role="none" runat="server"></asp:DropDownList>
							</form>
						</div>
					</div>
				</div>
				<div class="clear"></div>
			</div>
		</div>

		<asp:PlaceHolder ID="BodyContent" runat="server" />

		<div class="legal-mentions">
			<div class="content">
				<div class="padding">
					<div id="LegalMentionsLandingNormal" runat="server"></div>
					<div id="LegalMentionsLandingMore" runat="server"></div>
				</div>
			</div>
		</div>
		<div class="legals">
			<div class="content">
				<div class="inner" id="LegalMentionsInnerOne" runat="server"></div>
				<div class="inner" id="LegalMentionsInnerTwo" runat="server"></div>
			</div>
		</div>
		<div class="socials">
			<div class="content">
				<div class="inner" id="SocialInner" runat="server"></div>
				<div class="clear"></div>
			</div>
		</div>
	</div>
</body>
</html>