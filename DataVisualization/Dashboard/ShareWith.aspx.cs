using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using DataVisualization.CodeBehind;
using DataVisualization.CodeBehind.Database;
namespace DataVisualization.Dashboard
{
    public partial class ShareWith : BasePage
    {
        protected int dashboardId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["dashboardId"] != null)
            {
                //Read the dashboard id from the query string
                try
                {
                    dashboardId = int.Parse(Request.QueryString["dashboardId"]);
                }
                catch (Exception) { Response.Redirect("~/About.aspx"); }
            }
            else
                Response.Redirect("~/About.aspx");
        }

        protected void OnSelectingUserDataSource(object sender, SqlDataSourceSelectingEventArgs e)
        {   //@LoggedInUserId and @DashboardId are used by the select command of the data source
            e.Command.Parameters["@LoggedInUserId"].Value = LoggedInUserId;
            e.Command.Parameters["@DashboardId"].Value = dashboardId;
        }


        protected void ShareButton_Click(object sender, EventArgs e)
        {
            //When the "share" button is clicked, then, for each "username" checked by the user,
            //share the dashboard to that "username". 
            foreach (GridViewRow row in UserGridView.Rows)
            {
                CheckBox userCheckBox = (CheckBox)row.FindControl("userCheckBox");
                if (userCheckBox != null && userCheckBox.Checked)
                {
                    String selectedUserId = ((Label)row.FindControl("userIdLabel")).Text;
                    Share(dashboardId, selectedUserId);
                    Response.Redirect("~/Dashboard/MyDashboards.aspx");
                }
            }
        }

        private void Share(int dashboardIdToBeShared, string selectedUserId)
        {
            String query = "INSERT INTO dvs_SharedDashboard(SharedWith,DashboardId) " +
                            "VALUES('" + selectedUserId + "'," + dashboardIdToBeShared + ");";
            DatabaseClass.ExecuteNonQuery(query);
        }
    }
}