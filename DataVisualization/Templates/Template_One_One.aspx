<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Template_One_One.aspx.cs" Inherits="DataVisualization.Templates.Template_One_One" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="titleLabel" runat="server" Text="Dashboard Title" cssClass="left" SkinID="titleLabel"></asp:Label>
        <asp:TextBox ID="titleTextBox" runat="server" SkinID="titleTextBox" CssClass="titleTextBox left"></asp:TextBox>
    </div>
    <br />
    <asp:Label ID="messageLabel" runat="server" Text=""></asp:Label>
    <br />
    <div class="left leftBorder rightBorder topBorder fullWidthSection">
        <uc:dsuc ID="dsucTop" runat="server" />
    </div>
    <br />
    <div class="allBorder left fullWidthSection">
        <uc:dsuc ID="dsucBottom" runat="server" />
    </div>
    <br />
    <div class="saveButton">
        <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="DerivedSaveButton_Click" />
    </div>
</asp:Content>
