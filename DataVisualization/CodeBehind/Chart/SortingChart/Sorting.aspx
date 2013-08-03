<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Sorting.aspx.cs" MasterPageFile = "~/Site.Master" Inherits="DataVisualization.CodeBehind.Chart.SortingChart.Sorting" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to Data Visualization</h2>
        <p>
            Sorting Chart
        </p>

    <asp:Chart ID="SortingChart" runat="server" Width="821px">
        <Series>
            <asp:Series Name="Unsorted">
            </asp:Series>
            
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="SortingChartArea">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    <br />



    <asp:Chart ID="sortedchart" runat="server" Width="786px">
        <Series>
           <asp:Series Name="sorted">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="sortedChartArea">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>

    <br />
    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
        onselectedindexchanged="sortingchanges">
        <asp:ListItem>--select--</asp:ListItem>
        <asp:ListItem>Ascending</asp:ListItem>
        <asp:ListItem>Desending</asp:ListItem>
    </asp:DropDownList>


</asp:Content>
