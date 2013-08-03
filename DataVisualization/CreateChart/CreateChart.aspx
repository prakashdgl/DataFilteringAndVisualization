<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="CreateChart.aspx.cs" Inherits="DataVisualization.CreateChart.CreateChart" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to Data Visualization</h2>
    &nbsp;<asp:Panel ID="ChartSelectionPanel" runat="server" SkinID="none">
        <div class="dropdown" style="border: 1px solid red">
            <asp:GridView ID="TabledataGridView" runat="server" SkinID="none" Height="139px"
                HorizontalAlign="Right" Width="500px" AllowPaging="True" PageIndex="10" PageSize="12"
                RowStyle-Wrap="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle Wrap="False" BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <br />
            <br />
            <asp:Label ID="TableNameLabel" runat="server" Text="Choose TableName:"></asp:Label>
            &nbsp;
            <asp:DropDownList ID="TableNameDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="getTabelSchemaName"
                OnTextChanged="displaydata_Grid" Width="150px">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Choose x-Axis :&nbsp;"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="xAxisDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="xAxisSelect"
                Width="150px">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="yLabel" runat="server" Text="Chosse Y-Axis :"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="yAxisDropDownList" runat="server" Width="150" AutoPostBack="True">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="ChartTitlLabel" runat="server" Text="Enter Chart Title :&nbsp;&nbsp;"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="chartTile_TextBox" runat="server" CausesValidation="True" 
                Width="143px" AutoPostBack="True"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" BackColor="Yellow"
                ControlToValidate="chartTile_TextBox" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                SkinID="none"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text="Choose Chart Type :"></asp:Label>
            &nbsp;<asp:DropDownList ID="chartTypeDropDownList" runat="server" OnSelectedIndexChanged="GenerateChart_SelectedEvent"
                AutoPostBack="True">
            </asp:DropDownList>
            <br />
            <br />
        </div>
    </asp:Panel>
    <asp:Panel ID="chartuserdrawpanel" Visible="false" runat="server" SkinID="none">
        <uc:chartUserControl ID="CreateChartChartUserControl" runat="server" SkinID="none"
            Visible="false" />
        <br />
        <asp:Label ID="SortLabel" runat="server" Text="Enable Sort"></asp:Label>
        &nbsp;<asp:CheckBox ID="SortCheckBox" runat="server" OnCheckedChanged="EnableSort"
            AutoPostBack="True" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="SortingLabel" runat="server" Text="Sort :" Visible="false"></asp:Label>
        <asp:DropDownList ID="SortDropDownList" runat="server" AutoPostBack="True"
            OnSelectedIndexChanged="SortChanged" Visible="false">
            <asp:ListItem Selected="False">--Select Sort--</asp:ListItem>
            <asp:ListItem>Ascending</asp:ListItem>
            <asp:ListItem>Descending</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Edit Chart :"></asp:Label>
        <asp:CheckBox ID="EdititemCheckBox" runat="server" AutoPostBack="True" OnCheckedChanged="EnableEditChartItems" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label2" runat="server" Text="Enable Formula : "></asp:Label>
        <asp:CheckBox ID="EnableFormulaCheckBox" runat="server" AutoPostBack="True" OnCheckedChanged="DisplayFormulaDropDownList" />
        &nbsp;<asp:Label ID="FormulaLabel" runat="server" SkinID=" " Text="ChooseFormula :"
            Visible="False"></asp:Label>
        &nbsp;<asp:DropDownList ID="FormulaDropDownList" runat="server" AutoPostBack="True" 
            onselectedindexchanged="FormulaChange" Visible="false">
            <asp:ListItem Selected="False">Select Formula</asp:ListItem>
            <asp:ListItem>Sum</asp:ListItem>
            <asp:ListItem>Minimum</asp:ListItem>
            <asp:ListItem>Maximum</asp:ListItem>
            <asp:ListItem>Average</asp:ListItem>
            <asp:ListItem>Count</asp:ListItem>
            <asp:ListItem>HiLo</asp:ListItem>
            <asp:ListItem>DistinctCount</asp:ListItem>
            <asp:ListItem>Variance</asp:ListItem>
            <asp:ListItem>Deviation</asp:ListItem>
            <asp:ListItem>HiLoOpCl</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="SaveButton_Click" Text="Save Chart"
            Width="84px" />
        <br />
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="ChartColorPanel" runat="server" Visible="false" SkinID="none">
        <p>
            <asp:Label ID="HelpLabel" runat="server" Text="Set the following attributes accordingly. If not then the default parameters will be used. "></asp:Label>
        </p>
        <br />
        &nbsp;<asp:Label ID="BackGroundColorLabel" runat="server" Text="Select Chart Background Color:&nbsp;"></asp:Label>
        <asp:DropDownList ID="chartBackgroundColorPickerDropDownList" runat="server" AutoPostBack="True"
            Width="120px" OnSelectedIndexChanged="changebackgroundcolor">
            <asp:ListItem Selected = "False">Select Color</asp:ListItem>
            <asp:ListItem>Black</asp:ListItem>
            <asp:ListItem>Blue</asp:ListItem>
            <asp:ListItem>Green</asp:ListItem>
            <asp:ListItem>Red</asp:ListItem>
            <asp:ListItem>Yellow</asp:ListItem>
            <asp:ListItem>Pink</asp:ListItem>
            <asp:ListItem>AliceBlue</asp:ListItem>
            <asp:ListItem>Aqua</asp:ListItem>
            <asp:ListItem>Aquamarine</asp:ListItem>
            <asp:ListItem>Brown</asp:ListItem>
            <asp:ListItem>BurlyWood</asp:ListItem>
            <asp:ListItem>Chocolate</asp:ListItem>
            <asp:ListItem>DarkBlue</asp:ListItem>
            <asp:ListItem>DarkCyan</asp:ListItem>
            <asp:ListItem>Darkviolet</asp:ListItem>
            <asp:ListItem>Ivory</asp:ListItem>
            <asp:ListItem>Azure</asp:ListItem>
            <asp:ListItem>DimGray</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="xaxisLabel" runat="server" Text=" X axis Label&nbsp;:"></asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="xAxisLabel_TextBox" runat="server" OnTextChanged="ChangeXAxisTitle"
            Width="241px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="xAxisLabel_TextBox"
            Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" SkinID="none"></asp:RequiredFieldValidator>
        <br />
        <br />
        &nbsp;<asp:Label ID="SeriesColorLabel" runat="server" Text="Select Chart Series Color :"></asp:Label>
        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="SeriesColorPickerDropDownList" runat="server" Width="120px"
            OnSelectedIndexChanged="changeseriesColor" AutoPostBack="True">
            <asp:ListItem Selected = "False">Select Color</asp:ListItem>
            <asp:ListItem>Blue</asp:ListItem>
            <asp:ListItem>Black</asp:ListItem>
            <asp:ListItem>Green</asp:ListItem>
            <asp:ListItem>Red</asp:ListItem>
            <asp:ListItem>Yellow</asp:ListItem>
            <asp:ListItem>Pink</asp:ListItem>
            <asp:ListItem>AliceBlue</asp:ListItem>
            <asp:ListItem>Aqua</asp:ListItem>
            <asp:ListItem>Aquamarine</asp:ListItem>
            <asp:ListItem>Brown</asp:ListItem>
            <asp:ListItem>BurlyWood</asp:ListItem>
            <asp:ListItem>Chocolate</asp:ListItem>
            <asp:ListItem>DarkBlue</asp:ListItem>
            <asp:ListItem>DarkCyan</asp:ListItem>
            <asp:ListItem>Darkviolet</asp:ListItem>
            <asp:ListItem>Ivory</asp:ListItem>
            <asp:ListItem>Azure</asp:ListItem>
            <asp:ListItem>DimGray</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="yaxisLabel" runat="server" Text="y axis Label&nbsp;:"></asp:Label>
        &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="yaxisLabel_TextBox" runat="server" Width="238px" OnTextChanged="ChangeYaxisTitle"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="yaxisLabel_TextBox"
            ErrorMessage="*" SetFocusOnError="True" SkinID="none"></asp:RequiredFieldValidator>
        <br />
        <br />
        <br />
        &nbsp;<asp:CheckBox ID="Enable3dCheckBox" runat="server" Text="Enable 3D" AutoPostBack="True"
            OnCheckedChanged="Enable3DChart" />
       <div class="chartcontrol">
            
            <br />
            <br />
        </div>
    </asp:Panel>
    <!-- This is my added chart usercontrol-->
    <br />
    <br />
</asp:Content>
