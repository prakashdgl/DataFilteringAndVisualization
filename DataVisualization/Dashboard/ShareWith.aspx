<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShareWith.aspx.cs" Inherits="DataVisualization.Dashboard.ShareWith" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:GridView ID="UserGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="ApplicationId,LoweredUserName"
        DataSourceID="UserDataSource" EmptyDataText="There are no data records to display."
        Width="265px">
        <Columns>
            <asp:TemplateField Visible="false" HeaderText="InvisibleUserId">
                <ItemTemplate>
                    <asp:Label runat="server" ID="userIdLabel" Text='<%# Bind("UserId") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName" />
            <asp:TemplateField HeaderText="Check">
                <ItemTemplate>
                    <asp:CheckBox runat="server" ID="userCheckBox" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:Button ID="ShareButton" runat="server" OnClick="ShareButton_Click" Text="Share the Dashboard" />
    <asp:SqlDataSource ID="UserDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ApplicationServices %>"
        DeleteCommand="DELETE FROM [aspnet_Users] WHERE [UserId] = @UserId" InsertCommand="INSERT INTO [aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (@ApplicationId, @UserId, @UserName, @LoweredUserName, @MobileAlias, @IsAnonymous, @LastActivityDate)"
        ProviderName="<%$ ConnectionStrings:ApplicationServices.ProviderName %>" SelectCommand="SELECT aspnet_Users.UserId, aspnet_Users.UserName, aspnet_Users.ApplicationId, aspnet_Users.LoweredUserName, aspnet_Users.MobileAlias, aspnet_Users.IsAnonymous, aspnet_Users.LastActivityDate 

FROM aspnet_Users 

WHERE 

aspnet_Users.UserId != @LoggedInUserId
AND
aspnet_Users.UserId NOT IN

(SELECT dvs_SharedDashboard.SharedWith FROM dvs_SharedDashboard
WHERE dvs_SharedDashboard.DashboardId = @DashboardId)
" OnSelecting="OnSelectingUserDataSource" UpdateCommand="UPDATE [aspnet_Users] SET [ApplicationId] = @ApplicationId, [UserName] = @UserName, [LoweredUserName] = @LoweredUserName, [MobileAlias] = @MobileAlias, [IsAnonymous] = @IsAnonymous, [LastActivityDate] = @LastActivityDate WHERE [UserId] = @UserId">
        <DeleteParameters>
            <asp:Parameter Name="UserId" Type="Object" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="ApplicationId" Type="Object" />
            <asp:Parameter Name="UserId" Type="Object" />
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="LoweredUserName" Type="String" />
            <asp:Parameter Name="MobileAlias" Type="String" />
            <asp:Parameter Name="IsAnonymous" Type="Boolean" />
            <asp:Parameter Name="LastActivityDate" Type="DateTime" />
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="LoggedInUserId" />
            <asp:Parameter Name="DashboardId" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="ApplicationId" Type="Object" />
            <asp:Parameter Name="UserName" Type="String" />
            <asp:Parameter Name="LoweredUserName" Type="String" />
            <asp:Parameter Name="MobileAlias" Type="String" />
            <asp:Parameter Name="IsAnonymous" Type="Boolean" />
            <asp:Parameter Name="LastActivityDate" Type="DateTime" />
            <asp:Parameter Name="UserId" Type="Object" />
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>
