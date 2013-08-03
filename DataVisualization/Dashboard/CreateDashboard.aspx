<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CreateDashboard.aspx.cs" Inherits="DataVisualization.Dashboard.CreateDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--List of dashboards
    The IDs of each radio button should be same as the template file name it corresponds to
    --%>
    <div id="listOfDashboards">
        <%--Row 1--%>
        <div class="dashboardRow">
            <%--Dashboard 1--%>
            <div id="Div1" class="dashboardChoice" runat="server">
                <asp:RadioButton GroupName="dashboardRadioButtons" runat="server" CssClass="radioBtn"
                    ID="Template_One_One" Checked="true" OnCheckedChanged="dashboardCheckedChanged"
                    AutoPostBack="true" />
                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Structure/images/theme_preview/Template_One_One.png" />
            </div>
            <div id="Div2" class="dashboardChoice" runat="server">
                <asp:RadioButton GroupName="dashboardRadioButtons" runat="server" CssClass="radioBtn"
                    ID="Template_Three_One" OnCheckedChanged="dashboardCheckedChanged" AutoPostBack="true" />
                <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Structure/images/theme_preview/Template_Three_One.png" />
            </div>
            <div id="Div3" class="dashboardChoice" runat="server">
                <asp:RadioButton GroupName="dashboardRadioButtons" runat="server" CssClass="radioBtn"
                    ID="Template_Two" OnCheckedChanged="dashboardCheckedChanged" AutoPostBack="true" />
                <asp:Image ID="Image3" runat="server" ImageUrl="~/App_Themes/Structure/images/theme_preview/Template_Two.png" />
            </div>
        </div>
        <br/>
        <br/>
        <div class="dashboardRow">
            <div id="Div4" class="dashboardChoice" runat="server">
                <asp:RadioButton GroupName="dashboardRadioButtons" runat="server" CssClass="radioBtn"
                    ID="Template_Two_Two" OnCheckedChanged="dashboardCheckedChanged" AutoPostBack="true" />
                <asp:Image ID="Image4" runat="server" ImageUrl="~/App_Themes/Structure/images/theme_preview/Template_Two_Two.png" />
            </div>
        </div>
    </div>
    <%--End of list of dashboards--%>
    <%--Title of the dashboard--%>
    <div class="clear">
    <br/>
    <br/>
        <asp:Label ID="titleLabel" runat="server" Text="Dashboard Title" CssClass="left"
            SkinID="titleLabel"></asp:Label>
        <asp:TextBox ID="titleTextBox" runat="server" SkinID="titleTextBox" CssClass="titleTextBox left"></asp:TextBox>
    </div>
    <%--End of the title of the dashboard--%>
    <%--Actual dashboards--%>
    <%--Template_One_One--%>
    <div id="div_Template_One_One" runat="server">
        <div class="left leftBorder rightBorder topBorder fullWidthSection">
            <uc:dsuc ID="t1DsucTop" runat="server" />
        </div>
        <br />
        <div class="allBorder left fullWidthSection">
            <uc:dsuc ID="t1DsucBottom" runat="server" />
        </div>
    </div>
    <%--End of Template_One_One--%>
    <%--Template_Three_One--%>
    <div id="div_Template_Three_One" runat="server">
        <div>
            <div class="left threeSections leftBorder topBorder">
                <uc:dsuc ID="t2DsucTopLeft" runat="server" />
            </div>
            <div class="left threeSections leftBorder topBorder">
                <uc:dsuc ID="t2DsucTopMiddle" runat="server" />
            </div>
            <div class="left lastOfThreeSections leftBorder topBorder rightBorder">
                <uc:dsuc ID="t2DsucTopRight" runat="server" />
            </div>
        </div>
        <br />
        <div class="fullWidthSection allBorder">
            <uc:dsuc ID="t2DsucBottom" runat="server" />
        </div>
    </div>
    <%--End of Template_Three_One--%>
    <%--Template_Two--%>
    <div id="div_Template_Two" runat="server">
        <div class="left fullHeightSection leftBorder topBorder bottomBorder">
            <uc:dsuc ID="t3DsucLeft" runat="server" />
        </div>
        <div class="left fullHeightSection allBorder">
            <uc:dsuc ID="t3DsucRight" runat="server" />
        </div>
    </div>
    <%--End of Template_Two--%>
    <%--Template_Two_Two--%>
    <div id="div_Template_Two_Two" runat="server">
        <div>
            <div class="left twoSections leftBorder topBorder bottomBorder">
                <uc:dsuc ID="t4DsucTopLeft" runat="server" />
            </div>
            <div class="left twoSections allBorder">
                <uc:dsuc ID="t4DsucTopRight" runat="server" />
            </div>
        </div>
        <br />
        <div>
            <div class="left twoSections leftBorder bottomBorder">
                <uc:dsuc ID="t4DsucBottomLeft" runat="server" />
            </div>
            <div class="left twoSections leftBorder rightBorder bottomBorder">
                <uc:dsuc ID="t4DsucBottomRight" runat="server" />
            </div>
        </div>
    </div>
    <%--End of Template_Two_Two--%>
    <%--Save Button--%>
    <br/>
        <asp:Button ID="SaveButton" runat="server" Text="Save Dashboard" OnClick="SaveButton_Click" />
   
    <%--End of save button--%>
</asp:Content>
