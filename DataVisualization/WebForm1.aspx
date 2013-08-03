<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" MasterPageFile = "~/Site.Master" Inherits="WebApplication1.WebForm1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <uc:chartUserControl ID="DrawSaved_ChartUserControl" runat="server" />
    <br />
   
</asp:Content>

