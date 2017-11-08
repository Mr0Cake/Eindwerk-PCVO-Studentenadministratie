<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageLoggedOff.master" AutoEventWireup="true" CodeFile="Opleidingen.aspx.cs" Inherits="Pages_Opleidingen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="border">
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 text-center"></div>

            <div class="col-lg-8 col-md-8 col-sm-7 col-xs-12">
                <h1>Les aanbod</h1>
            </div>
            <img class="col-lg-4 col-md-4 col-sm-3 col-xs-12 img-responsive" src="/Images/PXLGreenSmall.png" />
            <div class="col-lg-8 col-md-8 col-sm-7 col-xs-12">
                <asp:ListView ID="lvStudieGebied" runat="server" DataSourceID="objStudieGebied" DataKeyNames="IDStudieGebied">
                    <LayoutTemplate>
                        <dl>
                            <dt runat="server" id="itemPlaceHolder"></dt>
                            <dd runat="server"></dd>
                        </dl>
                    </LayoutTemplate>

                    <ItemTemplate>
                        <dl>
                            <dt>
                                <asp:Label ID="NaamLabel"  runat="server" CssClass="" Text='<%# Eval("Naam") %>' />
                            </dt>



                            <dd>
                                <asp:ListView ID="ListView1" runat="server" DataKeyNames="IDOpleiding" DataSource='<%# Eval("Opleiding") %>'>
                                    <LayoutTemplate>
                                        <table runat="server">
                                            <tr runat="server">
                                                <td runat="server" id="itemPlaceHolder"></td>
                                            </tr>
                                            <tr runat="server">
                                                <td runat="server" style=""></td>
                                            </tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr style="">
                                            <td>
                                                <asp:Label ID="NaamLabelf" CssClass="margin-left" runat="server" Text='<%# "- " + Eval("Naam") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                            </dd>

                        </dl>
                    </ItemTemplate>

                </asp:ListView>
                <asp:ObjectDataSource ID="objStudieGebied" runat="server" SelectMethod="Select" TypeName="clsBLLStudieGebied" DataObjectTypeName="StudentApplication.Model.clsOpleiding" DeleteMethod="Delete" InsertMethod="Insert" UpdateMethod="Update"></asp:ObjectDataSource>
            </div>
        </div>
    </div>
</asp:Content>

