<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Template_Two.aspx.cs" Inherits="DataVisualization.Templates.Template_Two" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="titleLabel" runat="server" Text="Dashboard Title" CssClass="left"
            SkinID="titleLabel"></asp:Label>
        <asp:TextBox ID="titleTextBox" runat="server" SkinID="titleTextBox" CssClass="titleTextBox left"></asp:TextBox>
    </div>
    <div>
        <div class="left fullHeightSection leftBorder topBorder bottomBorder">
            <uc:dsuc ID="dsucLeft" runat="server" />
        </div>
        <div class="left fullHeightSection allBorder">
            <uc:dsuc ID="dsucRight" runat="server" />
        </div>
    </div>
    <br />
    <div class="saveButton">
        <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="DerivedSaveButton_Click" />
    </div>
</asp:Content>
