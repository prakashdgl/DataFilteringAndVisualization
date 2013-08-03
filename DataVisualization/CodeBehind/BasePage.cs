using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Windows.Forms;
using DataVisualization.CodeBehind.Theme;
namespace DataVisualization.CodeBehind
{
    public class BasePage:System.Web.UI.Page
    {
        public String LoggedInUserId;
        
        protected override void OnPreInit(EventArgs e)
        {
            //Try to read the UserId. UserId can be read only if the user is logged in.
            //The UserId is then stored in session and a public variable.
            try
            {
               LoggedInUserId = (Membership.GetUser().ProviderUserKey.ToString());
               Session["LoggedInUserId"] = LoggedInUserId;
               loadTheme();
            }
            catch (Exception)
            {
                //loadTheme();
                Session.Abandon();
                //Response.Redirect("~/About.aspx");
            }
        }

        //If the session has the information about the theme, then use it
        //otherwise check if from the ThemeClass
        public void loadTheme()
        {
            if (Session["themeId"] != null)
            {
                this.Theme = Session["themeId"].ToString();
            }
            else
            {
                ThemeClass themeClass = new ThemeClass();
                themeClass.findUserTheme();
                this.Theme = themeClass.getUserThemeId();
            }
        }
    }
}