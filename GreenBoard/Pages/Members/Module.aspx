<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" CodeFile="Module.aspx.cs" Inherits="Pages_Members_Module" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="center text-center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <h1>Modules</h1>
                <asp:DropDownList ID="ddlModules" AutoPostBack="True" runat="server" DataSourceID="objOpleiding" DataTextField="Naam" DataValueField="IDOpleiding" CssClass="margin-bottom"></asp:DropDownList>

                <asp:ObjectDataSource ID="objOpleiding" runat="server" SelectMethod="Select" TypeName="clsBLLOpleiding"></asp:ObjectDataSource>


                <asp:GridView runat="server" DataSourceID="objModuleByStudentID" AutoGenerateColumns="False" HorizontalAlign="Center">
                    <Columns>
                        <asp:BoundField DataField="OfficieleNaam" HeaderStyle-CssClass="module-header" HeaderText="Module" ItemStyle-CssClass="module-item" SortExpression="OfficieleNaam"></asp:BoundField>
                        <asp:TemplateField HeaderText="StarDatum" HeaderStyle-CssClass="module-header" ItemStyle-CssClass="module-item">
                            <ItemTemplate>
                                <%# Eval("StartDatum", "{0:d}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Leerkracht" HeaderStyle-CssClass="module-header" ItemStyle-CssClass="module-item">
                            <ItemTemplate>
                                <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Eval("Leraar") %>'>
                                    <ItemTemplate>
                                        <%# Eval("Voornaam") + " "  + Eval("Naam") %><br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Inschrijving" HeaderStyle-CssClass="module-header" ItemStyle-CssClass="module-item">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkInschrijving" runat="server" CommandArgument='<%# Eval("Ingeschreven") + ";" + Eval("IDModule")%>' OnClick="lnkInschrijving_Click">
                    <%# Boolean.Parse(Eval("Ingeschreven").ToString()) ? "Uitschrijven" : "Inschrijven" %>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        Deze opleiding heeft geen modules!
                    </EmptyDataTemplate>
                </asp:GridView>

                <asp:ObjectDataSource ID="objModuleByStudentID" runat="server" TypeName="clsBLLModuleByOpleiding" SelectMethod="GetModuleByOpleiding">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="ddlModules" Name="fk" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

