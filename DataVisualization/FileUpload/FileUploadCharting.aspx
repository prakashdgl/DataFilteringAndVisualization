<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUploadCharting.aspx.cs"
    MasterPageFile="~/Site.Master" Inherits="DataVisualization.FileUpload.FileUploadCharting" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<link type="text/css" href="/Styles/StyleSheet1.css" rel="Stylesheet" />
    <%//javascript manually added for dropdown change %>
    <script type="text/javascript" src="/Scripts/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        function typeChanged(v) {//column name and datatype is required to generate new menu
            var type = v.value; //current Selected Data Type
            var name = v.name.substring(0, v.name.lastIndexOf('_')); //name of the select
            var divName = name.substring(0, name.lastIndexOf('_')); //div name of the menu
            //alert(name);
            var toServer = name + "&datatype=" + type;
            $.ajax({
                url: "WebForm1.aspx",
                method: "post",
                data: {
                    Name: name,
                    Type: type
                },
                success: function (data) {
                    //$("#new_dropdown_placeholder").html(data);
                    $("#" + name).html(data);
                }
            });
        }
    </script>

</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to Data Visualization</h2>
   
        <p>
            <asp:FileUpload ID="FileUpload1" runat="server" ToolTip="click here to choose file" SkinID = "none" />
            <asp:HiddenField ID="tableName" runat="server" />
            <br />
            <br />
            <asp:Button ID="FileUploadButton" runat="server" OnClick="FileUploadButton_Click"  SkinID = "none"
                Text="Upload" />
        </p>
<p>
            <asp:Literal ID="FinalDataArea" runat="server"></asp:Literal>
            <br />
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </p>
<p>
            <asp:Literal ID="Literal2" runat="server"></asp:Literal>
        </p>
         <asp:Panel ID="ErrorPanel" runat="server" Visible = "false"  
    SkinID = "none" Height="156px">
        <br />
        <br />
        <br />
        <asp:Button ID="CreateChartButton" runat="server" Text="CreateChart"  SkinID = "none" Visible="false"
            OnClick="CreateChartButton_Click" />
    </asp:Panel>


</asp:Content>