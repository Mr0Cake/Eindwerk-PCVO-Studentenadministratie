<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="ModuleInBehandeling.aspx.cs" Inherits="Pages_Members_ModuleInBehandeling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="center text-center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <h1>Modules</h1>

                <asp:GridView runat="server" AutoGenerateColumns="False" HorizontalAlign="Center" DataSourceID="objModulesBehandeling">
                    <Columns>
                        <asp:BoundField DataField="OfficieleNaam" HeaderStyle-CssClass="module-header" HeaderText="Module" ItemStyle-CssClass="module-item" SortExpression="OfficieleNaam">
                            <HeaderStyle CssClass="module-header" />
                            <ItemStyle CssClass="module-item" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="StarDatum" HeaderStyle-CssClass="module-header" ItemStyle-CssClass="module-item">
                            <ItemTemplate>
                                <%# Eval("StartDatum", "{0:d}") %>
                            </ItemTemplate>
                            <HeaderStyle CssClass="module-header" />
                            <ItemStyle CssClass="module-item" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Aantal lestijden" HeaderStyle-CssClass="module-header" ItemStyle-CssClass="module-item">
                            <ItemTemplate>
                                <%# Eval("TotaalAantalUren") %>
                            </ItemTemplate>
                            <HeaderStyle CssClass="module-header" />
                            <ItemStyle CssClass="module-item" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Annuleer" HeaderStyle-CssClass="module-header" ItemStyle-CssClass="module-item">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkInschrijving" runat="server" CommandArgument='<%# Eval("IDModule") + ";" + Eval("IDInschrijving")%>' OnClick="lnkInschrijving_Click">Annuleer</asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle CssClass="module-header" />
                            <ItemStyle CssClass="module-item" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        Deze opleiding heeft geen modules!
                    </EmptyDataTemplate>
                </asp:GridView>

                <asp:ObjectDataSource ID="objModulesBehandeling" runat="server" TypeName="clsBLLModuleByOpleiding" SelectMethod="GetIngeschrevenModulesStudentNietGevalideerd">
                    <SelectParameters>
                        <asp:SessionParameter Name="fk" SessionField="CurrentUserID" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

