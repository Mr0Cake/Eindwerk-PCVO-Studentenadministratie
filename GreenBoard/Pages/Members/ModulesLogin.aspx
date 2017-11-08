<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ModulesLogin.aspx.cs" Inherits="Pages_Members_ModulesLogin" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 text-center"></div>
            <div class="col-lg-8 col-md-8 col-sm-7 col-xs-12 margin-bottom">
                <h1>Modules</h1>
                <asp:DropDownList ID="ddlModules" AutoPostBack="True" runat="server" DataSourceID="objOpleiding" DataTextField="Naam" DataValueField="IDOpleiding" OnSelectedIndexChanged="ddlModules_SelectedIndexChanged"></asp:DropDownList>
                <asp:ObjectDataSource ID="objOpleiding" runat="server" SelectMethod="Select" TypeName="clsBLLOpleiding"></asp:ObjectDataSource>

            </div>
            <img class="col-lg-4 col-md-4 col-sm-3 col-xs-12 img-responsive" src="/Images/PXLGreenSmall.png" />
            <div class="col-lg-8 col-md-8 col-sm-7 col-xs-12">
                <asp:GridView ID="GridView1" runat="server" DataSourceID="objModuleByOpleidingID" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="OfficieleNaam" HeaderStyle-CssClass="module-header" HeaderText="Module" ItemStyle-CssClass="module-item" SortExpression="OfficieleNaam" />
                        <asp:BoundField DataField="TotaalAantalUren" HeaderStyle-CssClass="module-header" ItemStyle-CssClass="module-item" HeaderText="Aantal lestijden" SortExpression="AantalUren" />
                        <asp:TemplateField HeaderText="Coördinator" HeaderStyle-CssClass="module-header" ItemStyle-CssClass="module-item">
                            <ItemTemplate>
                                <%# Eval("Voornaam") + " "  + Eval("Naam") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="objModuleByOpleidingID" runat="server" TypeName="clsBLLModuleByOpleiding" SelectMethod="GetModuleByOpleiding">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlModules" Name="fk" PropertyName="SelectedValue" Type="Int32" />

                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

