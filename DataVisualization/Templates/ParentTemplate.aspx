<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ParentTemplate.aspx.cs" Inherits="DataVisualization.Templates.ParentTemplate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:TextBox ID="titleTextBox" runat="server" CssClass="titleTextBox"></asp:TextBox>
    <br/>
    <asp:Label ID="messageLabel" runat="server" Text=""></asp:Label>
    <br/>
    <div class="leftDashboard">
        
        <uc:dsuc ID="dsucTopLeft" runat="server" />
    </div>
    
    <div id="rightDashboard">
        <uc:dsuc ID="dsucTopRight" runat="server" />
    </div>
    <br/>
    <asp:Button ID="SaveButton" runat="server" OnClick="SaveButton_Click" Text="Save Dashboard" />
    
</asp:Content>
