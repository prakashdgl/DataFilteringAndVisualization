using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataVisualization.CodeBehind;
namespace DataVisualization.Dashboard
{
    public partial class MyDashboards : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInUserId"] == null)
                Response.Redirect("~/Account/Login.aspx");
            dashboardUserControl.setPagination(true);
        }
    }
}