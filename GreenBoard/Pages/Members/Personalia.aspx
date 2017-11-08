<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Personalia.aspx.cs" Inherits="Pages_Members_Personalia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 margin-bottom text-center">
        <h1>Personalia</h1>
    </div>
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
        <asp:Label ID="lblNaam" runat="server" Text="Label" CssClass="col-lg-7 col-md-7 col-sm-12 col-xs-12 h3"></asp:Label>
        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
            <fieldset class="form-group">
                <asp:Image ID="imgGebruiker" Width="200px" runat="server" />
            </fieldset>
            <asp:FileUpload ID="fuUploadFoto" CssClass="hidden" runat="server" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="foto" runat="server" ErrorMessage="Gelieve een foto te kiezen van het type .png, .jpeg, .jpg of .bmp"
                ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.bmp|.jpeg)$" ControlToValidate="fuUploadFoto"></asp:RegularExpressionValidator>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
            <asp:LinkButton ID="lnkWijzigFoto" CssClass="btn btn-default" runat="server" OnClick="lnkWijzigFoto_Click">Wijzig foto</asp:LinkButton>
            <asp:LinkButton ID="lnkBevestigFoto" CssClass="hidden" ValidationGroup="foto" OnClick="lnkBevestigFoto_Click" runat="server">Aanvraag tot wijziging foto</asp:LinkButton>
            <asp:LinkButton ID="lnkCancelFoto" CssClass="hidden" runat="server" OnClick="lnkCancelFoto_Click">Cancel</asp:LinkButton>
        </div>
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <h3>Ingeschreven modules</h3>
            <asp:GridView runat="server" DataSourceID="objModuleByStudentID" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="OfficieleNaam" HeaderStyle-CssClass="module-header" HeaderText="Module" ItemStyle-CssClass="module-item" SortExpression="OfficieleNaam" />
                    <asp:BoundField DataField="TotaalAantalUren" HeaderStyle-CssClass="module-header" ItemStyle-CssClass="module-item" HeaderText="Aantal lestijden" SortExpression="AantalUren" />
                    <asp:TemplateField HeaderText="Coördinator" HeaderStyle-CssClass="module-header" ItemStyle-CssClass="module-item">
                        <ItemTemplate>
                            <%# Eval("Voornaam") + " "  + Eval("Naam") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    u bent niet ingeschreven voor een cursus
                </EmptyDataTemplate>
            </asp:GridView>

            <asp:ObjectDataSource ID="objModuleByStudentID" runat="server" TypeName="clsBLLModuleByOpleiding" SelectMethod="GetModuleByStudentID">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="0" Name="fk" SessionField="CurrentUserID" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <form>

                    <fieldset class="form-group">
                        <label for="txtPostcode">Postcode:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPostcode" ErrorMessage="Gelieve een postcode in te vullen!">*</asp:RequiredFieldValidator><br />
                        <asp:TextBox ID="txtPostcode" runat="server" CssClass="form-control" AutoPostBack="true" ReadOnly="true"></asp:TextBox>
                    </fieldset>

                    <fieldset>
                        <label>Gemeente:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGemeente" ErrorMessage="Als er geen gemeente is, gelieve langs het secertariaat te gaan!">*</asp:RequiredFieldValidator><br />
                        <asp:DropDownList ID="ddlGemeente" Enabled="false" CssClass="form-control" runat="server" DataSourceID="objGemeente"
                            DataTextField="Plaatsnaam" DataValueField="IDPostCode" AutoPostBack="false">
                        </asp:DropDownList>
                        <asp:ObjectDataSource ID="objGemeente" runat="server" SelectMethod="SelectByFK" TypeName="clsBLLPostcode">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtPostcode" DefaultValue="0" Name="fk" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>

                    </fieldset>
                    <fieldset class="form-group">
                        <label for="txtStraat">Straat:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtStraat" ErrorMessage="Gelieve een straat in te vullen!">*</asp:RequiredFieldValidator><br />
                        <asp:TextBox ID="txtStraat" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                    </fieldset>
                    <fieldset class="form-group">
                        <label for="txtNummer">Huisnummer:</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNummer" ErrorMessage="Gelieve een huisnummer in te vullen!">*</asp:RequiredFieldValidator><br />
                        <asp:TextBox ID="txtNummer" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                    </fieldset>
                    <fieldset class="form-group">
                        <label for="txtMail">Email:</label>
                        <asp:TextBox ID="txtMail" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                    </fieldset>
                    <fieldset class="form-group">
                        <label for="txtTelefoon">Telefoon:</label>
                        <asp:TextBox ID="txtTelefoon" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                    </fieldset>
                    <fieldset class="form-group">
                        <label for="txtGSM">GSM:</label>
                        <asp:TextBox ID="txtGSM" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                    </fieldset>
                    <fieldset class="form-group">
                        <label>Mijn taal</label>
                        <asp:DropDownList ID="ddlTaal" Enabled="False" CssClass="form-control" runat="server" AutoPostBack="True"
                            DataSourceID="objTaal" DataValueField="IDTaal" DataTextField="TaalNaam">
                        </asp:DropDownList>

                        <asp:ObjectDataSource ID="objTaal" runat="server" TypeName="clsBLLTaal" SelectMethod="Select"></asp:ObjectDataSource>
                    </fieldset>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                </form>
                <asp:LinkButton ID="lnkWijzigData" CssClass="btn btn-default" OnClick="lnkWijzigData_Click" runat="server">Wijzig gegevens</asp:LinkButton>
                <asp:LinkButton ID="lnkBevistigWijzigen" CssClass="hidden" OnClick="lnkBevistigWijzigen_Click" runat="server">Aanvraag tot wijziging</asp:LinkButton>
                <asp:LinkButton ID="lnkCancel" CssClass="hidden" OnClick="lnkCancel_Click" runat="server">Cancel</asp:LinkButton><br />
                <asp:Label ID="lblBevestiging" runat="server" CssClass="hidden" Text="Uw aanvraag is ingediend, deze wordt pas zichtbaar als deze is goedgekeurd."></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>



    </div>

</asp:Content>

