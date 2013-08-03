<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DrawSavedChart.aspx.cs" Inherits="DataVisualization.DrawSavedChart.DrawSavedChart" MasterPageFile = "~/Site.Master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        SavedChartWizard</h2>
        <p>YOUR SAVED CHART</p>
        <br />
        <br />
        <p>Please select the chart title which you want to visualize</p>
  Your Chart Titles:  <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
        DataTextField="ChartTitle" 
        DataValueField="ChartTitle" Height="16px" 
        onselectedindexchanged="selected" Width="377px">
    </asp:DropDownList>
        <br />
        <br />
    <asp:Chart ID="SavedChart" runat="server" Width="845px" Height="562px" 
        Visible = "false" >
        <Series>
            <asp:Series Name="Series1">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="SavedChartArea">
                <AxisY Title="y value member">
                </AxisY>
                <AxisX Title="x value member">
                </AxisX>
            </asp:ChartArea>
        </ChartAreas>
        <Titles>
            <asp:Title Name="title">
            </asp:Title>
        </Titles>
    </asp:Chart>
    <br />
    <br />
    <br />
<uc:chartUserControl ID="DrawSaved_ChartUserControl" runat="server" />
    <br />
    <br />
    <br />
</asp:Content>