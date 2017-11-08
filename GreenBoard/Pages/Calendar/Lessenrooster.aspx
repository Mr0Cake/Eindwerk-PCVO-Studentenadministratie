<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="true" EnableViewState="true" CodeFile="Lessenrooster.aspx.cs" Inherits="Pages_Members_Lessenroosters" %>

<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="WeekCalendar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <h1>Mijn lessenrooster</h1>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                <asp:Calendar ID="Calendar1" runat="server" SelectionMode="DayWeek" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
            </div>
            <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                <WeekCalendar:DayPilotCalendar ID="WeekViewCalendar" runat="server"
                    DataTextField="name" DataIdField="id" DayFontSize="10pt"
                    DataStartField="Start" DataEndField="End"
                    TimeFormat="Clock24Hours" ViewType="Week" Width="100%"
                    BusinessBeginsHour="17" BusinessEndsHour="24"
                    BackColor="#FFFFD5" BorderColor="#000000" DayFontFamily="Tahoma"
                    CssClassPrefix="calendar_default" DataValueField="id"
                    Days="7" DurationBarColor="Blue" EventBackColor="#FFFFFF"
                    EventBorderColor="#000000" EventClickHandling="Disabled"
                    EventFontFamily="Tahoma" EventFontSize="8pt"
                    EventHoverColor="#DCDCDC" HourBorderColor="#EAD098"
                    HourFontFamily="Tahoma" HourFontSize="16pt"
                    HourHalfBorderColor="#F3E4B1" HourNameBackColor="#ECE9D8"
                    HourNameBorderColor="#ACA899" HoverColor="#FFED95"
                    NonBusinessBackColor="#FFF4BC" ScrollPositionHour="18"
                    StartDate="2016-06-04" HeaderDateFormat="dddd d-M"></WeekCalendar:DayPilotCalendar>
                <asp:LinkButton ID="lnkVorigeWeek" CssClass="btn btn-default" OnClick="lnkVorigeWeek_Click" runat="server">Vorige week</asp:LinkButton>
                <asp:LinkButton ID="lnkDezeWeek" CssClass="btn btn-default" OnClick="lnkDezeWeek_Click" runat="server">Deze week</asp:LinkButton>
                <asp:LinkButton ID="lnkVolgendeWeek" CssClass="btn btn-default" OnClick="lnkVolgendeWeek_Click" runat="server">Volgende week</asp:LinkButton>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

