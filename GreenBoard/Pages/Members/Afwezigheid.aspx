<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" EnableEventValidation="true" AutoEventWireup="true" CodeFile="Afwezigheid.aspx.cs" Inherits="Pages_Members_Afwezigheid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 text-center"></div>
            <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                <h1>Afwezigheid</h1>
                <br />
                <br />
                <fieldset>
                    <h3>Reden</h3>
                    <br />
                    <asp:TextBox ID="txtReden" TextMode="MultiLine" Width="300" Height="150px" CssClass="multiline form-control" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtReden" runat="server" ErrorMessage="Gelieve een reden in te vullen!"></asp:RequiredFieldValidator><br />
                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtReden" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{4,}$" runat="server" ErrorMessage="Gelieve een fatsoenlijke reden te geven!."></asp:RegularExpressionValidator>
                    <fieldset>
                        <h3>Bestand toevoegen</h3>
                        <br />
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btn btn-default btn-file" />
                    </fieldset>
                    <fieldset>
                        <h3>Datum</h3>
                        <br />
                        <asp:TextBox ID="txtDatum" TextMode="Date" Width="200px" CssClass="form-control" runat="server"></asp:TextBox>
                    </fieldset>
                    <br />
                    <asp:LinkButton ID="lnkVerstuur" CssClass="btn btn-default" OnClick="lnkVerstuur_Click" runat="server">Verstuur</asp:LinkButton>
                    <asp:Label ID="lblBevestiging" runat="server" CssClass="hidden" Text="Uw aanvraag is in behandeling genomen"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

