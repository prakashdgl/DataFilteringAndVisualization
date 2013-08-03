using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataVisualization.CodeBehind.Database;
using System.Data;
using DataVisualization.CodeBehind;

namespace DataVisualization.Dashboard
{
    public partial class ShowDashboard : System.Web.UI.Page
    {
        protected int dashboardId;
        protected String mode;
        protected String templateFileName;
        protected String templateName;
        protected String redirectString;
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            readMode();
            //If mode==create then, read the templatefileName from the query string and redirect the response
            if (mode == DashboardModes.CREATE)
            {
                createModeRedirect();
            }
            else
            {
                otherModeRedirect();
            }
            Response.Write("The server cannot redirect properly.");
        }


        //Read the value of "mode" from the query string
        public void readMode()
        {
            if (Request.QueryString["mode"] != null)
            {
                mode = Request.QueryString["mode"].ToString();
            }
        }

        //For "CREATE" mode
        public void createModeRedirect()
        {
            if (Request.QueryString["templateFileName"] != null)
            {
                templateFileName = Request.QueryString["templateFileName"].ToString();
                redirectString = "~/Templates/" + templateFileName + ".aspx?mode=" + mode;
                Response.Redirect(redirectString);
            }
        }

        //For "EDIT" and "DELETE" modes
        public void otherModeRedirect()
        {
            if (Request.QueryString["dashboardId"] != null)
            {
                dashboardId = int.Parse(Request.QueryString["dashboardId"].ToString());
                findTemplateFileName();
                if (templateFileName != null)
                {
                    redirectString = "~/Templates/"+templateFileName+".aspx"+"?mode="+mode+"&dashboardId="+dashboardId;
                    Response.Redirect(redirectString);
                }
            }
            
        }

        //Finds the filename of the template from the dashboard id
        //It is not required during "CREATE" mode
        public void findTemplateFileName()
        {
            String query = "SELECT TemplateFileName FROM dvs_Dashboard WHERE DashboardId=" + dashboardId;
            DataTable dt = DatabaseClass.ExecuteQuery(query);
            //retrieve the template id from the database
            templateFileName = dt.Rows[0][0].ToString();
        }

    }
}