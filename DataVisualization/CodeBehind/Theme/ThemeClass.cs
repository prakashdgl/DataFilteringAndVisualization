using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using DataVisualization.CodeBehind.Database;

namespace DataVisualization.CodeBehind.Theme
{
    public class ThemeClass:System.Web.UI.Page
    {
        protected DataTable themeDataTable;
        public const String THEME_ID = "themeId";//It is the value of the drop down list option
        public const String THEME_NAME = "themeName";//It is the text in the drop down list
        protected String userThemeId = "Blue";
        //protected const string defaultThemeId = "Blue";
        //protected const string defaultThemeName = "Blue";
        protected DropDownList themeDropDownList;
        protected int selectedIndex = 0;//This is the index which is to be selected when the dropdown list shows up
        //If the user has not selected any theme, then it should be 0 (i.e. the default theme)
        //If the user has selected a theme before, then that theme would be saved in the database. That theme should be selected/



        public ThemeClass()
        { }

        public ThemeClass(DropDownList themeDropDownList)
        {
            this.themeDropDownList = themeDropDownList;
        }


        public void run()
        { 
            themeDropDownList.ClearSelection();
            defineThemeDataTable();
            findUserTheme();
            findThemeList();
            generateThemeDDL();
        }

        protected void initializeThemeDropDownList()
        {
            //If the user is logged in, retrieve the theme he/she selected the last time.
            if (Session["LoggedInUserId"] != null)
            {
                //Retrieve the theme selected by the user
                themeDropDownList.ClearSelection();
                defineThemeDataTable();
                findUserTheme();
                findThemeList();
                generateThemeDDL();
            }
        }

        protected void defineThemeDataTable()
        {
            //Add two columns to themeDataTable
            //The first one is the id of the theme while the second one is the name of the theme
            themeDataTable = new DataTable();
            themeDataTable.Columns.Add(new DataColumn(THEME_ID, typeof(string)));
            themeDataTable.Columns.Add(new DataColumn(THEME_NAME, typeof(string)));
        }

        public void findUserTheme()
        {
            //Get the id of the theme from the database
            String userThemeIdQuery = "SELECT ThemeId " +
               " FROM dvs_User_Theme WHERE UserId = '" +Session["LoggedInUserId"].ToString()+"'";
            
            DataTable userThemeIdDT = DatabaseClass.ExecuteQuery(userThemeIdQuery);
            //If a theme id has been saved (for the user who is logged in), then retrieve the  theme id
            if (userThemeIdDT.Rows.Count > 0)
            {
                userThemeId = userThemeIdDT.Rows[0][0].ToString();
            }
        }

        protected void findThemeList()
        { 
            //Get the list of themes from the table
            String themeQuery = "SELECT ThemeId, ThemeName FROM dvs_Themes";
            DataTable themeDT = DatabaseClass.ExecuteQuery(themeQuery);

            //Store all the themes inside the themeDataTable
            for (int i = 0; i < themeDT.Rows.Count; i++)
            {
                String themeId = themeDT.Rows[i][0].ToString();
                String themeName = themeDT.Rows[i][1].ToString();
                if (userThemeId != null && themeId == userThemeId)
                {
                    selectedIndex = i;//By default, the selectedIndex = 0
                    //But if the user's theme id is stored, it should be the selected index
                }
                //Add the retrieved theme to the drop down list
                DataRow dr = themeDataTable.NewRow();
                dr[0] = themeId;
                dr[1] = themeName;
                themeDataTable.Rows.Add(dr);
            }
        }

        //It assigns ids and names to the drop down list
        public void generateThemeDDL()
        {
            themeDropDownList.DataSource = new DataView(themeDataTable);
            //Each option is identified by its id and the value shown is the name of the theme
            themeDropDownList.DataValueField = THEME_ID;
            themeDropDownList.DataTextField = THEME_NAME;
            themeDropDownList.DataBind();
            themeDropDownList.SelectedIndex = selectedIndex;//The selected index = 0 by default, otherwise it is the theme selected by the user
        }


        public DropDownList getThemeDropDownList()
        {
            return themeDropDownList;
        }

        public String getUserThemeId()
        {
            return userThemeId;
        }

    }
}