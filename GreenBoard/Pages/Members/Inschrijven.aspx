<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Inschrijven.aspx.cs" Inherits="Pages_Members_Inschrijven" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 text-center"></div>
    <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
        <asp:Label ID="lblTitel" runat="server" Text="Inschrijving" CssClass="h1"></asp:Label><br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:DropDownList ID="ddlModuleTypes" DataSourceID="objModules" OnDataBound="ddlModuleTypes_DataBound" AutoPostBack="true" runat="server" DataTextField="Datatextfield" DataValueField="IDModule" OnSelectedIndexChanged="ddlModuleTypes_SelectedIndexChanged">
                </asp:DropDownList><br />
                <br />
                <asp:ObjectDataSource ID="objModules" runat="server" SelectMethod="SelectAlleNietIngeschreven"
                    TypeName="clsBLLModuleModuleType">
                    <SelectParameters>
                        <asp:SessionParameter Name="id" SessionField="CurrentUserID" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:LinkButton ID="lnkSchrijfMeIn" OnClick="lnkSchrijfMeIn_Click" CssClass="btn btn-default" runat="server">Schrijf me in</asp:LinkButton>
        <asp:Label ID="lblBevestiging" runat="server" CssClass="hidden" Text="Uw aanvraag is in behandeling genomen"></asp:Label>
    </div>
</asp:Content>

