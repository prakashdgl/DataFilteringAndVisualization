using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataVisualization.CodeBehind;

namespace DataVisualization.Home
{
    public partial class Home : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInUserId"] == null)
                Response.Redirect("~/Account/Login.aspx");
            dashboardUserControl.setPagination(false);
            dashboardUserControl.setSelectCommand("SELECT top 5 [DashboardId], [UserId], [Title], [TemplateId] FROM [dvs_Dashboard] WHERE [UserId]='"+Session["LoggedInUserId"].ToString()+"'");

            sharedDashboardUserControl.setPagination(false);
            sharedDashboardUserControl.setSelectCommand("SELECT dvs_SharedDashboard.SharedId, dvs_SharedDashboard.SharedWith, dvs_SharedDashboard.DashboardId, dvs_Dashboard.UserId, aspnet_Users.UserName, dvs_Dashboard.Title FROM dvs_SharedDashboard INNER JOIN dvs_Dashboard ON dvs_SharedDashboard.DashboardId = dvs_Dashboard.DashboardId INNER JOIN aspnet_Users ON dvs_Dashboard.UserId = aspnet_Users.UserId WHERE (dvs_SharedDashboard.SharedWith = '"+Session["LoggedInUserId"].ToString()+"')");
        }
    }
}