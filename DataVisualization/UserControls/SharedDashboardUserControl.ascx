<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SharedDashboardUserControl.ascx.cs"
    Inherits="DataVisualization.UserControls.SharedDashboardUserControl" %>

<asp:Label runat="server" CssClass="dashboardLabel" ID="lbl" Text="Shared Dashboards"></asp:Label>


<div class="dashboardGridView">
    
    
    <asp:GridView ID="SharedDashboardGridView" runat="server" AutoGenerateColumns="False"
        DataKeyNames="SharedId" DataSourceID="SharedDashboardDataSource" EmptyDataText="There is no shared dashboard."
        AllowSorting="True" Width="600px" AllowPaging="True">
        <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="View">
                <ItemTemplate>
                    <asp:HyperLink ID="dashboardHyperlink" runat="server" NavigateUrl='<%# "~/Dashboard/ShowDashboard.aspx?mode=VIEW&dashboardId="+Eval("DashboardId")%>'
                        Text='<%# Eval("Title") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserName" HeaderText="Shared By" SortExpression="UserName" />
        </Columns>
    </asp:GridView>
</div>
<asp:SqlDataSource ID="SharedDashboardDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
    DeleteCommand="DELETE FROM [dvs_SharedDashboard] WHERE [SharedId] = @SharedId"
    InsertCommand="INSERT INTO [dvs_SharedDashboard] ([SharedWith], [DashboardId]) VALUES (@SharedWith, @DashboardId)"
    ProviderName="<%$ ConnectionStrings:ApplicationServices.ProviderName %>" SelectCommand="SELECT dvs_SharedDashboard.SharedId, dvs_SharedDashboard.SharedWith, dvs_SharedDashboard.DashboardId, dvs_Dashboard.UserId, aspnet_Users.UserName, dvs_Dashboard.Title FROM dvs_SharedDashboard INNER JOIN dvs_Dashboard ON dvs_SharedDashboard.DashboardId = dvs_Dashboard.DashboardId INNER JOIN aspnet_Users ON dvs_Dashboard.UserId = aspnet_Users.UserId WHERE (dvs_SharedDashboard.SharedWith = @LoggedInUserId)"
    OnSelecting="OnSelectingSharedDashboardDataSource" UpdateCommand="UPDATE [dvs_SharedDashboard] SET [SharedWith] = @SharedWith, [DashboardId] = @DashboardId WHERE [SharedId] = @SharedId">
    <SelectParameters>
        <asp:Parameter Name="LoggedInUserId" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="SharedId" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="SharedWith" Type="Object" />
        <asp:Parameter Name="DashboardId" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="SharedWith" Type="Object" />
        <asp:Parameter Name="DashboardId" Type="Int32" />
        <asp:Parameter Name="SharedId" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>
