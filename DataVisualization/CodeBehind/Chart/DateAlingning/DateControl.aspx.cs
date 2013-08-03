using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data.OleDb;
using System.Data;
namespace DataVisualization.CodeBehind.Chart.DateAlingning
{
    public partial class DateControl : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInUserId"] == null)
                Response.Redirect("~/CodeBehind/Home/NotLoggedIn.aspx");
            populatedata();

        }

        protected void DateIndexChanged(object sender, EventArgs e)
        {
            if (TypeIntervalSelectDropDownList.SelectedItem.Text == "week")
                DateControlChart.DataManipulator.Group("AVE, X:CENTER", 1, IntervalType.Weeks, "Series1", "1 week Grouped Chart");
            else if (TypeIntervalSelectDropDownList.SelectedItem.Text == "2 Weeks")
            {
                DateControlChart.DataManipulator.Group("AVE, X:CENTER", 2, IntervalType.Weeks,
                    "Series1", "Grouped Sales");
            }
            else if (TypeIntervalSelectDropDownList.SelectedItem.Text == "Month")
            {
                DateControlChart.DataManipulator.Group("AVE, X:CENTER", 1, IntervalType.Months,
                    "Series1", "Grouped Sales");
            }
            else if (TypeIntervalSelectDropDownList.SelectedItem.Text == "Year")
            {
                DateControlChart.DataManipulator.Group("AVE, X:CENTER", 1, IntervalType.Years,
                    "Series1", "Grouped Sales");
               
            }
            populatedata();
        }

        public void populatedata()
        {
            String connectionpath = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\\Users\\User\\Documents\\Visual Studio 2010\\Projects\\DataVisualization\\DataVisualization\\App_Data\\chartdata.mdb;Persist Security Info=True";
            String mySelectQuery = "SELECT * FROM SalesEarned ORDER BY Time";
            OleDbConnection myConnection = new OleDbConnection(connectionpath);
            OleDbCommand myCommand = new OleDbCommand(mySelectQuery, myConnection);
            myCommand.Connection.Open();

            // Create a database reader    
            OleDbDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            // Data bind to the reader
            DateControlChart.Series["Series1"].Points.DataBindXY(myReader, "Time", myReader, "Earned");

            // close the reader and the connection
            myReader.Close();
            myConnection.Close();



        }

        protected void sortchanged(object sender, EventArgs e)
        {

        }
    }
}