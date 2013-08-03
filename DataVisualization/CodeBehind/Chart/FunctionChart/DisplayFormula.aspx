<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DisplayFormula.aspx.cs" Inherits="DataVisualization.CodeBehind.Chart.FunctionChart.DisplayFormula" MasterPageFile ="~/Site.Master" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
       Formula Workouts</h2>
        
        <p>     
            <asp:Chart ID="FormulaChart" runat="server"  
            
                Width="862px" style="margin-right: 0px">
                
                 <Series>
                    <asp:Series ChartArea="ChartArea1" Name="Series1" 
                        CustomProperties="DrawSideBySide=True" IsValueShownAsLabel="True" 
                        MarkerSize = "0" Legend="Legend1" XValueMember="xValues" 
                        XValueType="String" YValueMembers="yValues">
                        <Points>
                            <asp:DataPoint MarkerSize="1" YValues="0" />
                        </Points>
                        <SmartLabelStyle AllowOutsidePlotArea="Yes" IsMarkerOverlappingAllowed="True" />
                    </asp:Series>
                </Series>
                
                <ChartAreas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </ChartAreas>
                <Legends>
                    <asp:Legend Name="Legend1">
                    </asp:Legend>
                </Legends>
            </asp:Chart>           
        </br>
         </p>

        
       
    <p>     


           <asp:Chart ID="changeformulaChart" runat="server" Width="862px">
            <Series>
                <asp:Series Name="formulachange">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="changeformulaChartArea">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart></p>
    <p>     
        



            <asp:DropDownList ID="FormulaSelectDropDownList" runat="server" 
                onselectedindexchanged="FormulaExecute" AutoPostBack="True" Width="106px">
                <asp:ListItem Selected = "False">Select Formula</asp:ListItem>
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
        
        
        
        
        
        
        </p>


   </asp:Content>
