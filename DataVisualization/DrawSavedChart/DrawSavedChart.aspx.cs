using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataVisualization.CodeBehind.Database;
using System.Windows.Forms;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using DataVisualization.CodeBehind;
namespace DataVisualization.DrawSavedChart
{
    #region class area
    public partial class DrawSavedChart : BasePage
    {  ///@t datatable for storing the table information
        ///@start the row which is equal to the chart name
        private static System.Data.DataTable t;
        private static int start;
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            DrawSaved_ChartUserControl.Visible = false;

            if (Session["LoggedInUserId"] == null)
                Response.Redirect("~/Home/NotLoggedIn.aspx");

            if (!Page.IsPostBack)
            {
                DropDownList1.Items.Clear();
                SavedChart.Visible = false;
                String query = "SELECT * FROM [dvs_Chart] WHERE [UserId] = '"+Session["LoggedInuserId"]+"';";
                t = DatabaseClass.ExecuteQuery(query);
                DropDownList1.Items.Add("--Select Chart To Display--");
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    DropDownList1.Items.Add(t.Rows[i][3].ToString());
                }
            }
        }
        #endregion
        //call the usercontrol function to draw the chart
        #region creat saved chart
        public void CreateChartSaved_FromUserControl()
        {
            if (DatabaseClass.getTableDatafromTable(t.Rows[start][1].ToString()) != null)
            {
                DrawSaved_ChartUserControl.Visible = true;
                
                //DrawSaved_ChartUserControl.DATASETFORCHART = DatabaseClass.getTableDatafromTable(t.Rows[start][1].ToString());
                DrawSaved_ChartUserControl.CHARTTITLE = t.Rows[start][3].ToString();
                DrawSaved_ChartUserControl.CHARTTYPE = t.Rows[start][4].ToString();
                DrawSaved_ChartUserControl.XVALUEMEMBER = t.Rows[start][5].ToString();
                DrawSaved_ChartUserControl.YVALUEMEMBER = t.Rows[start][7].ToString();
                DrawSaved_ChartUserControl.ENABLELEGEND = true;
                DrawSaved_ChartUserControl.XAXISINTERVAL = 1;
                DrawSaved_ChartUserControl.XAXISTITLE = t.Rows[start][6].ToString();
                DrawSaved_ChartUserControl.YAXISTITLE = t.Rows[start][8].ToString();
                DrawSaved_ChartUserControl.CHARTBACKGROUNDCOLOR = t.Rows[start][9].ToString();
                DrawSaved_ChartUserControl.CHARTSERIESCOLOR = t.Rows[start][10].ToString();
                DrawSaved_ChartUserControl.FORMULA = t.Rows[start][11].ToString();
                DrawSaved_ChartUserControl.SORTING = t.Rows[start][12].ToString();
                if (t.Rows[start][11].ToString() == "1")
                    DrawSaved_ChartUserControl.ENABLE3D = true;
                else
                    DrawSaved_ChartUserControl.ENABLE3D = false;
                String chartcreatequery = "SELECT " + t.Rows[start][5].ToString() + " , " + t.Rows[start][7].ToString() + " FROM [" + t.Rows[start][1].ToString() + "]";
                DrawSaved_ChartUserControl.READER = DatabaseClass.ExequteReaderQuery(chartcreatequery);
                DrawSaved_ChartUserControl.CreateChart_UserControl();
                
                DrawSaved_ChartUserControl.CreateChart_UserControl();
                DatabaseClass.closeReaderConnection();
                DatabaseClass.closeConnection();
            }
        }
        #endregion
        /// <summary>
        /// Find the row which has the chart name
        /// </summary>
        /// <param name="sender">chart names</param>
        /// <param name="e">dropdownlist event args</param>

        #region drop downlist select
        protected void selected(object sender, EventArgs e)
        {
            String select = DropDownList1.SelectedItem.Text;
            for (int i = 0; i < t.Rows.Count; i++)
            {
                for (int j = 0; j < t.Columns.Count; j++)
                {
                    if (t.Rows[i][j].ToString() == select)
                    {
                        start = i;
                        break;
                    }
                }
            }
            CreateChartSaved_FromUserControl();
        }
        #endregion
    }
    #endregion
}