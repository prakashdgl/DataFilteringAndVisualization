using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataVisualization.CodeBehind;
using DataVisualization.UserControls;
using System.Data;
using DataVisualization.CodeBehind.Database;
namespace DataVisualization.Templates
{
    public abstract partial class ParentTemplate : BasePage
    {
        #region Variable Declaration
        public String mode
        {
            set { ViewState["mode"] = value; }
            get { return ViewState["mode"].ToString(); }
        }

        public String templateFileName
        {
            set { ViewState["templateFileName"] = value; }
            get { return ViewState["templateFileName"].ToString(); }
        }


        public int dashboardId
        {
            set { ViewState["dashboardId"] = value; }
            get { return int.Parse(ViewState["dashboardId"].ToString()); }
        }

        public String dashboardTitle
        {
            set { titleTextBox.Text = value; }
            get { return titleTextBox.Text; }
        }

        protected DashboardSectionUserControl[] dsuc;//dsuc = dashboard section user control
        protected DashboardClass dashboard;
        #endregion

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            checkLoginStatus();
            if (!Page.IsPostBack)
            {
                readQueryString();
                setTemplateFileName();
                initializeArray();
                assignMode();
                assignSections();
                assignWidthHeight();
                adjustAccordingToMode();
            }
            else
            {
                initializeArray();
                displaySavedMessage();
            }
            manageVisibility();
        }


        //If "Save Button" is clicked, then save the dashboard
        protected void SaveButton_Click(object sender, EventArgs e)
        {   //If the mode is "CREATE", then it needs to insert the information about dashboard and the charts it contains
            if (mode == DashboardModes.CREATE)
            {
                insertDashboard();
                insertDSUC();
                messageLabel = new Label();
                //Open the page in "Edit" mode once a newly created dashboard is saved
                Response.Redirect("~/Dashboard/ShowDashboard.aspx?mode=EDIT&dashboardId=" + dashboardId);
            }
            else if (mode == DashboardModes.EDIT)
            {
                dashboard.title = dashboardTitle;
                updateDashboard();
                messageLabel = new Label();
                messageLabel.Text = "The dashboard has been saved.";
            }
        }
        #endregion

        #region Other functions

        public void checkLoginStatus()
        {
            if (Session["LoggedInUserId"] == null)
            { Response.Redirect("~/Account/Login.aspx"); }
        }

        //Read the mode and the dashboardId
        public void readQueryString()
        {
            if (Request.QueryString["mode"] != null)
            {
                mode = Request.QueryString["mode"].ToString();
            }
            //By default, mode = "CREATE"
            else
                mode = "CREATE";

            if (Request.QueryString["dashboardId"] != null)
            {
                dashboardId = int.Parse(Request.QueryString["dashboardId"].ToString());
            }

        }



        //The controls have the same dashboard id as the parent page.
        public void assignDashboardIds()
        {
            for (int i = 0; i < dsuc.Length; i++)
            {
                dsuc[i].dashboardId = dashboardId;
            }
        }

        public void assignMode()
        {
            for (int i = 0; i < dsuc.Length; i++)
            {
                dsuc[i].mode = mode;
            }
        }
        //Section id is the index itself. Note: This standard should be maintained forever
        public void assignSections()
        {
            for (int i = 0; i < dsuc.Length; i++)
            {
                dsuc[i].sectionId = i;
            }
        }




        //Save Button is not visible in "View" mode
        public void manageVisibility()
        {
            if (mode == DashboardModes.VIEW)
            {
                titleTextBox.Enabled = false;
                SaveButton.Visible = false;
            }
            if (mode == DashboardModes.VIEW || mode == DashboardModes.EDIT)
            {
                retrieveDashboard();
                
            }
        }

        public void displaySavedMessage()
        {
            messageLabel = new Label();
            messageLabel.Text = "The dashboard has been saved.";
        }


        public void adjustAccordingToMode()
        {
            if (mode == DashboardModes.EDIT || mode == DashboardModes.VIEW)
            {
                assignDashboardIds();
                retrieveDashboard();
            }
            if (mode == DashboardModes.CREATE)
            {
                for (int i = 0; i < dsuc.Length; i++)
                {
                    dsuc[i].dashboardId = 0;
                    dsuc[i].chartId = 0;
                }
            }
            for (int i = 0; i < dsuc.Length; i++)
            {
                dsuc[i].adjustAccordingToMode();
            }
        }
        #endregion

        #region Abstract Functions
        //Child pages should set the file name of the template
        public abstract void setTemplateFileName();
        //Child pages should initialize the array of sections
        public abstract void initializeArray();
        //Child pages should assign width and height to the sections
        public abstract void assignWidthHeight();
        #endregion


        #region Database Functions
        //Inserts the dashboard
        public void insertDashboard()
        {
            dashboard = new DashboardClass();
            dashboard.title = dashboardTitle;
            dashboard.userId = Session["LoggedInUserId"].ToString();
            dashboard.templateFileName = templateFileName;
            dashboardId = dashboard.insert();
            assignDashboardIds();
        }

        //Inserts all the charts
        public void insertDSUC()
        {
            for (int i = 0; i < dsuc.Length; i++)
            {
                if (dsuc[i].chartId != 0)
                    dsuc[i].insert();
            }
        }

        //The title of the dashboard could have been editted
        //Editting a dashboard involves updating its charts
        public void updateDashboard()
        {
            dashboard.update();
            for (int i = 0; i < dsuc.Length; i++)
            {
                if(dsuc[i].chartId!=0)
                    dsuc[i].update();
            }
        }

        

        //Retrieve the dashboard from the database
        public void retrieveDashboard()
        {
            dashboard = new DashboardClass();
            dashboard.dashboardId = dashboardId;
            dashboard.retrieve();
            dashboardTitle = dashboard.title;
        }
        #endregion
    }
}