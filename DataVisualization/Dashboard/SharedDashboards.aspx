<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SharedDashboards.aspx.cs" Inherits="DataVisualization.Dashboard.SharedDashboards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <uc:sdbuc ID="sharedDashboardUserControl" runat="server" />
</asp:Content>
