<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyDashboards.aspx.cs" Inherits="DataVisualization.Dashboard.MyDashboards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc:dbuc ID="dashboardUserControl" runat="server" />
    <br />
</asp:Content>
