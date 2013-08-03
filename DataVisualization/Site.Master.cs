using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataVisualization.CodeBehind.Database;
using DataVisualization.CodeBehind.Theme;
using System.Data;

namespace DataVisualization
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        ThemeClass themeClass;
        
        protected void Page_PreRender(object sender, EventArgs e)
        {   //Generate drop down list
            if (Session["LoggedInUserId"] != null)
            {
                themeClass = new ThemeClass(ThemeDropDownList);
                themeClass.run();
            }
            else
            {
                ThemeDropDownList.Visible = false;
            }
        }

        protected void ThemeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //If the user selects a different theme, then store that value in the database and refresh the page
            //so that new theme is applied dynamically
            changeThemeId(ThemeDropDownList.SelectedValue);
            Response.Redirect(Request.RawUrl);
        }

        protected void changeThemeId(String selectedThemeId)
        {
            //When the user selects a theme, it is inserted into the database
            //Later on, ThemeClass uses it to find out the active theme id
            String userThemeQuery = "SELECT ThemeId FROM dvs_User_Theme WHERE UserId = '" + Session["LoggedInUserId"].ToString() + "'";
            DataTable userThemeDT = DatabaseClass.ExecuteQuery(userThemeQuery);
            if (userThemeDT.Rows.Count > 0)
            {
                updateThemeId(selectedThemeId);
            }
            else
            {
                insertThemeId(selectedThemeId);
            }
        }

        protected void updateThemeId(String selectedThemeId)
        {
            String updateThemeQuery = "UPDATE dvs_User_Theme SET ThemeId='" +
                        selectedThemeId +
                        "' WHERE UserId = '" + Session["LoggedInUserId"].ToString() + "'";
            DatabaseClass.ExecuteNonQuery(updateThemeQuery);
        }


        protected void insertThemeId(String selectedThemeId)
        {
            String insertThemeQuery = "INSERT INTO dvs_User_Theme(ThemeId,UserId)" +
                        " VALUES('" + selectedThemeId + "','" + Session["LoggedInUserId"].ToString() + "')";
            DatabaseClass.ExecuteNonQuery(insertThemeQuery);
        }

    }
}
