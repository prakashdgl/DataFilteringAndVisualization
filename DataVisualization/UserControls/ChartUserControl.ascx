<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChartUserControl.ascx.cs"
    Inherits="DataVisualization.ChartUserControl.ChartUserControl" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Chart ID="ucFinalChart" runat="server" Height="478px" Width="818px" >
    <Series>
        <asp:Series Name="Series1" Color="255, 128, 0" Legend="ChartLegend">
        </asp:Series>
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ucFinalChartArea" BackColor="255, 255, 128">
        </asp:ChartArea>
    </ChartAreas>
    <Legends>
        <asp:Legend Name="ChartLegend">
        </asp:Legend>
    </Legends>
    <Titles>
        <asp:Title Name="ucFinalTitle">
        </asp:Title>
    </Titles>
</asp:Chart>
