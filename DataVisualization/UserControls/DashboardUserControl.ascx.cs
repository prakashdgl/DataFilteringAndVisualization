using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataVisualization.UserControls
{
    public partial class DashboardUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void setPagination(Boolean pagination)
        {
            this.DashboardGridView.AllowPaging = pagination;
        }

        public void setSelectCommand(String selectCommand)
        {
            this.DashboardDataSource.SelectCommand = selectCommand;
        }

        protected void OnSelectingDashboardDataSource(Object sender, SqlDataSourceSelectingEventArgs e)
        {   //@LoggedInUserId is used by the select command of the data source
            e.Command.Parameters["@LoggedInUserId"].Value = Session["LoggedInUserId"].ToString();
        }

        protected void RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //If "share" button is clicked, then, call the ShareClicked function
            if (e.CommandName == "ShareClicked")
            {
                ShareClicked(sender, e);
            }
        }

        protected void ShareClicked(object sender, GridViewCommandEventArgs e)
        {
            //To share a dashboard, find out the dashboard ID from the label of hidden template field
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            String dashboardIdStr = ((Label)DashboardGridView.Rows[rowIndex].FindControl("dashboardIdLbl")).Text;
            int dashboardId = Convert.ToInt32(dashboardIdStr);
            Response.Redirect("~/Dashboard/ShareWith.aspx?dashboardId=" + dashboardId);
        }

        public Boolean isEmpty()
        {
            if (DashboardGridView.Rows.Count == 0)
                return true;
            else return false;
        }
    }
}