using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DataVisualization.UserControls
{
    public partial class SharedDashboardUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnSelectingSharedDashboardDataSource(Object sender, SqlDataSourceSelectingEventArgs e)
        {
            e.Command.Parameters["@LoggedInUserId"].Value = Session["LoggedInUserId"];
        }

        public void setPagination(Boolean allowPaging)
        {
            this.SharedDashboardGridView.AllowPaging = allowPaging;
        }

        public void setSelectCommand(String selectCommand)
        {
            this.SharedDashboardDataSource.SelectCommand = selectCommand;
        }

        public Boolean isEmpty()
        {
            Response.Write(SharedDashboardGridView.Rows.Count);
            if (SharedDashboardGridView.Rows.Count == 0)
                return true;
            else return false;
        }
    }
}