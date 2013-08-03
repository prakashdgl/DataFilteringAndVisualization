<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardSectionUserControl.ascx.cs"
    Inherits="DataVisualization.UserControls.DashboardSectionUserControl" %>
<div class="chart">
    <p>
        <asp:Label ID="chartTitleLabel" runat="server" Text="Label"></asp:Label>
    </p>
    <asp:Label ID="Label1" runat="server" Text="CHART SHOULD BE PRESENT HERE"></asp:Label>
</div>
<div class="chartDDL">
    <asp:DropDownList ID="chartDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chartDDLSelectedIndexChanged">
    </asp:DropDownList>
</div>
<asp:Chart ID="Chart1" runat="server" Height="384px" Width="814px" Visible = "false">
    <Series>
        <asp:Series Name="Series1">
        </asp:Series>
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="Chart1Area">
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

