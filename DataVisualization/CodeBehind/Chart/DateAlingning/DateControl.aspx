<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DateControl.aspx.cs" MasterPageFile = "~/Site.Master" Inherits="DataVisualization.CodeBehind.Chart.DateAlingning.DateControl" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to Data Visualization</h2>
        <p>Please Do select the file you want to visualize</p>

    <asp:Chart ID= "DateControlChart" runat="server" Width="623px">
        <Series>
            <asp:Series Name="Series1" Legend="Legend1">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="datecontrolchartarea">
            </asp:ChartArea>
        </ChartAreas>
        <Legends>
            <asp:Legend Name="Legend1">
            </asp:Legend>
        </Legends>
    </asp:Chart>


    <br />
    <br />


    <br />
    &nbsp;<br />
    Select the IntervalType :
    <asp:DropDownList ID="TypeIntervalSelectDropDownList" runat="server" 
        onselectedindexchanged="DateIndexChanged" AutoPostBack="True">
        <asp:ListItem Selected = "False">--Select Interval--</asp:ListItem>
        <asp:ListItem>week</asp:ListItem>
        <asp:ListItem>2 weeks</asp:ListItem>
        <asp:ListItem>Month</asp:ListItem>
        <asp:ListItem>Year</asp:ListItem>
    </asp:DropDownList>
            </asp:Content>