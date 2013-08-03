<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DashboardUserControl.ascx.cs"
    Inherits="DataVisualization.UserControls.DashboardUserControl" %>
    <asp:Label runat="server" CssClass="dashboardLabel" ID="lbl" Text="My Dashboards"></asp:Label>

<div class="dashboardGridView">
    <asp:GridView ID="DashboardGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="DashboardId"
        DataSourceID="DashboardDataSource" EmptyDataText="Sorry, you have not created any dashboard."
        Width="600px" AllowSorting="True" AllowPaging="True" OnRowCommand="RowCommand">
        <Columns>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="dashboardIdLbl" runat="server" Text='<%# Eval("DashboardId") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ItemStyle-HorizontalAlign="Left" HeaderText="View">
                <ItemTemplate>
                    <asp:HyperLink ID="dashboardHyperlink" runat="server" NavigateUrl='<%# "~/Dashboard/ShowDashboard.aspx?mode=VIEW&dashboardId="+Eval("DashboardId")%>'
                        Text='<%# Eval("Title") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:HyperLink ID="dashboardEditHyperlink" runat="server" NavigateUrl='<%# "~/Dashboard/ShowDashboard.aspx?mode=EDIT&dashboardId="+Eval("DashboardId")%>'
                        Text="Edit" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:ButtonField HeaderText="Share this Dashboard" Text="Share" CommandName="ShareClicked" />
        </Columns>
    </asp:GridView>
</div>
<asp:SqlDataSource ID="DashboardDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
    ProviderName="<%$ ConnectionStrings:ApplicationServices.ProviderName %>" SelectCommand="SELECT [DashboardId], [UserId], [Title], [TemplateFileName] FROM [dvs_Dashboard] WHERE [UserId]=@LoggedInUserId "
    OnSelecting="OnSelectingDashboardDataSource">
    <SelectParameters>
        <asp:Parameter Name="LoggedInUserId" Type="String" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="DashboardId" Type="Int32" />
    </DeleteParameters>
    <InsertParameters>
        <asp:Parameter Name="UserId" Type="Object" />
        <asp:Parameter Name="Title" Type="String" />
        <asp:Parameter Name="TemplateId" Type="Int32" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="UserId" Type="Object" />
        <asp:Parameter Name="Title" Type="String" />
        <asp:Parameter Name="TemplateId" Type="Int32" />
        <asp:Parameter Name="DashboardId" Type="Int32" />
    </UpdateParameters>
</asp:SqlDataSource>
