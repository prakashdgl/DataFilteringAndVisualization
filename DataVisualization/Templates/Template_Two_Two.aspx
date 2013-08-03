<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Template_Two_Two.aspx.cs" Inherits="DataVisualization.Templates.Template_Two_Two" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="titleLabel" runat="server" Text="Dashboard Title" CssClass="left"
            SkinID="titleLabel"></asp:Label>
        <asp:TextBox ID="titleTextBox" runat="server" SkinID="titleTextBox" CssClass="titleTextBox left"></asp:TextBox>
    </div>
<div>
    <div class="left twoSections leftBorder topBorder bottomBorder">
        <uc:dsuc ID="dsucTopLeft" runat="server" />
    </div>
    <div class="left twoSections allBorder">
        <uc:dsuc ID="dsucTopRight" runat="server" />
    </div>
</div>
<br/>
<div>
    <div class="left twoSections leftBorder bottomBorder">
        <uc:dsuc ID="dsucBottomLeft" runat="server" />
    </div>
    <div class="left twoSections leftBorder rightBorder bottomBorder">
        <uc:dsuc ID="dsucBottomRight" runat="server" />
    </div>
</div>
<br/>
<div class="saveButton">
    <asp:Button ID="SaveButton" runat="server" Text="Save" 
        onclick="DerivedSaveButton_Click" />
</div>
</asp:Content>
