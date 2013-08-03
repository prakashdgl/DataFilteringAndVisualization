using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataVisualization.CodeBehind;
using DataVisualization.Templates;
using DataVisualization.UserControls;
namespace DataVisualization.Dashboard
{
    public partial class CreateDashboard : BasePage
    {
        #region Variable declaration
        //templateFileName is the id of the radio button
        public String templateFileName
        {
            get
            {
                return getCheckedRadioButtonID();
            }
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
            SaveButton.Enabled = true;
            initializeArray();
            assignMode();
            assignSections();
            assignWidthHeight();
            manageVisibility();
            adjustAccordingToMode();

        }


        //If "Save Button" is clicked, then save the dashboard
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            Response.Write("hello");
            insertDashboard();
            insertDSUC();
            //Open the page in "Edit" mode once a newly created dashboard is saved
            Response.Redirect("~/Dashboard/ShowDashboard.aspx?mode=EDIT&dashboardId=" + dashboardId);
        }
        #endregion

        #region Other functions

        public void checkLoginStatus()
        {
            if (Session["LoggedInUserId"] == null)
            { Response.Redirect("~/Account/Login.aspx"); }
        }


        //The controls have the same dashboard id as the parent page.
        public void assignDashboardIds()
        {
            for (int i = 0; i < dsuc.Length; i++)
            {
                dsuc[i].dashboardId = dashboardId;
            }
        }

        //Each user control is in "CREATE" mode
        public void assignMode()
        {
            for (int i = 0; i < dsuc.Length; i++)
            {
                dsuc[i].mode = "CREATE";
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


        public void adjustAccordingToMode()
        {

            for (int i = 0; i < dsuc.Length; i++)
            {
                dsuc[i].dashboardId = 0;
                dsuc[i].chartId = 0;
            }
            for (int i = 0; i < dsuc.Length; i++)
            {
                dsuc[i].adjustAccordingToMode();
            }
        }
        #endregion

        #region Initialization function
        //The array is initialized according to the current template
        public void initializeArray()
        {
            if (templateFileName == "Template_One_One")
            {
                dsuc = new DashboardSectionUserControl[2];
                dsuc[0] = t1DsucTop;
                dsuc[1] = t1DsucBottom;

            }
            else if (templateFileName == "Template_Three_One")
            {
                dsuc = new DashboardSectionUserControl[4];
                dsuc[0] = t2DsucTopLeft;
                dsuc[1] = t2DsucTopMiddle;
                dsuc[2] = t2DsucTopRight;
                dsuc[3] = t2DsucBottom;
            }
            else if (templateFileName == "Template_Two")
            {
                dsuc = new DashboardSectionUserControl[2];
                dsuc[0] = t3DsucLeft;
                dsuc[1] = t3DsucRight;
            }
            else if (templateFileName == "Template_Two_Two")
            {
                dsuc = new DashboardSectionUserControl[4];
                dsuc[0] = t4DsucTopLeft;
                dsuc[1] = t4DsucTopRight;
                dsuc[2] = t4DsucBottomLeft;
                dsuc[3] = t4DsucBottomRight;
            }
        }

        //Assigns with and height based on which template has been chosen
        public void assignWidthHeight()
        {
            if (templateFileName == "Template_One_One")
            {
                dsuc[0].width = 900;
                dsuc[0].height = 400;
                dsuc[1].width = 900;
                dsuc[1].height = 400;
            }

            else if (templateFileName == "Template_Three_One")
            {
                dsuc[0].width = 299;
                dsuc[1].width = 299;
                dsuc[2].width = 298;
                dsuc[0].height = 300;
                dsuc[1].height = 300;
                dsuc[2].height = 300;
            }
        }


        //Decide which divs to show
        public void manageVisibility()
        {
            div_Template_One_One.Visible = false;
            div_Template_Three_One.Visible = false;
            div_Template_Two.Visible = false;
            div_Template_Two_Two.Visible = false;
            if (templateFileName == "Template_One_One")
            {
                div_Template_One_One.Visible = true;
            }
            else if (templateFileName == "Template_Three_One")
            {
                div_Template_Three_One.Visible = true;
            }
            else if (templateFileName == "Template_Two")
            {
                div_Template_Two.Visible = true;
            }
            else if (templateFileName == "Template_Two_Two")
            {
                div_Template_Two_Two.Visible = true;
            }
        }


        //Returns the radio button which is checked
        public String getCheckedRadioButtonID()
        {
            if (Template_One_One.Checked)
                return Template_One_One.ID;
            else if (Template_Three_One.Checked)
                return Template_Three_One.ID;
            else if (Template_Two.Checked)
                return Template_Two.ID;
            else if (Template_Two_Two.Checked)
                return Template_Two_Two.ID;
            else return "";
        }//end GetCheckedRadioButton

        //Is executed when another radio button is chosen
        protected void dashboardCheckedChanged(object sender, EventArgs e)
        {

        }
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
        #endregion

    }
}