<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Vragen.aspx.cs" Inherits="Pages_Members_Vragen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 text-center"></div>
            <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                <h1>Stel een vraag</h1>
                <br />
                <br />
                <asp:TextBox ID="txtVraag" TextMode="MultiLine" Width="600" Height="150px" CssClass="multiline form-control" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtVraag" runat="server" ErrorMessage="Gelieve een vraag in te vullen!"></asp:RequiredFieldValidator><br />
                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtVraag" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{4,}$" runat="server" ErrorMessage="Gelieve een fatsoenlijke vraag te geven!."></asp:RegularExpressionValidator>

                <br />
                <asp:LinkButton ID="lnkVerstuur" CssClass="btn btn-default" OnClick="lnkVerstuur_Click" runat="server">Verstuur</asp:LinkButton>
                <asp:Label ID="lblBevestiging" runat="server" CssClass="hidden" Text="Uw aanvraag is in behandeling genomen"></asp:Label>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

