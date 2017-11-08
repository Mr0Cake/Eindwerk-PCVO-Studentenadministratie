<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageLoggedOff.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Pages_Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="border">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 text-center">
                <h1>Login</h1>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-12 text-center"></div>
            <%--            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">--%>
            <img class="col-lg-3 col-md-3 col-sm-3 col-xs-12 img-responsive" src="/Images/PXLGreenSmall.png" />
            <%--            </div>--%>
            <div class="col-lg-7 col-md-7 col-sm-7 col-xs-12">
                <asp:Panel ID="p" runat="server" DefaultButton="btnLogin">
                    <h3>Gebruikersnaam</h3>
                    <br />
                    <asp:TextBox ID="txtEmail" Width="280px" runat="server" Text="Gebruikersnaam"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="Gelieve een gebruikersnaam in te vullen!">*</asp:RequiredFieldValidator><br />

                    <h3>Wachtwoord</h3>
                    <br />
                    <asp:TextBox ID="txtWachtwoord" Width="280px" TextMode="Password" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtWachtwoord" ErrorMessage="Gelieve een wachtwoord in te vullen!">*</asp:RequiredFieldValidator><br />

                    <br />
                    <asp:LinkButton ID="btnLogin" CssClass="btn btn-default" Width="140px" runat="server" OnClick="btnLogin_Click">Login</asp:LinkButton><br />
                    <asp:Label ID="lblFailedLogin" CssClass="hidden" runat="server" Text="Gebruikers naam of paswoord zijn incorrect. Probeer opnieuw!"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>

