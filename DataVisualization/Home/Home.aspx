<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="DataVisualization.Home.Home" %>


<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div id="leftSide">
        <uc:dbuc ID="dashboardUserControl" runat="server" />
    </div>
    <div id="rightSide">
        <uc:sdbuc ID="sharedDashboardUserControl" runat="server" />
    </div>
</asp:Content>
