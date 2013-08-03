<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Template_Three_One.aspx.cs" Inherits="DataVisualization.Templates.Template_Three_One" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="titleLabel" runat="server" Text="Dashboard Title" CssClass="left"
            SkinID="titleLabel"></asp:Label>
        <asp:TextBox ID="titleTextBox" runat="server" SkinID="titleTextBox" CssClass="titleTextBox left"></asp:TextBox>
    </div>
    <div>
        <div>
            <div class="left threeSections leftBorder topBorder">
                <uc:dsuc ID="dsucTopLeft" runat="server" />
            </div>
            <div class="left threeSections leftBorder topBorder">
                <uc:dsuc ID="dsucTopMiddle" runat="server" />
            </div>
            <div class="left lastOfThreeSections leftBorder topBorder rightBorder">
                <uc:dsuc ID="dsucTopRight" runat="server" />
            </div>
        </div>
        <br />
        <div class="fullWidthSection allBorder">
            <uc:dsuc ID="dsucBottom" runat="server" />
        </div>
    </div>
    <br />
    <div class="saveButton">
        <asp:Button ID="SaveButton" runat="server" Text="Save" OnClick="DerivedSaveButton_Click" />
    </div>
</asp:Content>
