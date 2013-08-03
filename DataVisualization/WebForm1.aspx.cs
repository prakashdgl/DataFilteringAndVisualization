using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataVisualization.CodeBehind.Database;
using System.Data.SqlClient;
using System.Web.UI.DataVisualization.Charting;
namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String a = "SELECT * FROM [dvs_chart] WHERE [ChartId] =  " + 15;
            DataTable t = DatabaseClass.ExecuteQuery(a);
             drawchart(t);
            
        }
        public void drawchart(DataTable t)
        {
            String s = "SELECT "+t.Rows[0][5].ToString()+", " + t.Rows[0][7].ToString()+"   FROM [" + t.Rows[0][1].ToString() + " ];";
            //DrawSaved_ChartUserControl.assingDataPoint(s);
            //String connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            //SqlConnection connection = new SqlConnection(connectionstring);
            //connection.Open();
            //SqlCommand cmd = new SqlCommand(s, connection);
            //SqlDataReader reader = cmd.ExecuteReader();
            DrawSaved_ChartUserControl.READER = DatabaseClass.ExequteReaderQuery(s);
            
            //DrawSaved_ChartUserControl.READER = reader;
            //Chart1.Series["Series1"].Points.DataBindXY(reader, "xAxis", reader, "yaxis");
            //Chart1.DataManipulator.Sort(PointSortOrder.Descending, "Series1");
            //Chart1.DataManipulator.GroupByAxisLabel("SUM","Series1");
            
            if (DatabaseClass.getTableDatafromTable(t.Rows[0][1].ToString()) != null)
            {
                DrawSaved_ChartUserControl.Visible = true;
                
              ///  DrawSaved_ChartUserControl.DATASETFORCHART = DatabaseClass.getTableDatafromTable(t.Rows[0][1].ToString());
                
                DrawSaved_ChartUserControl.CHARTTITLE = t.Rows[0][3].ToString();
                DrawSaved_ChartUserControl.CHARTTYPE = t.Rows[0][4].ToString();
                DrawSaved_ChartUserControl.XVALUEMEMBER = t.Rows[0][5].ToString();
                DrawSaved_ChartUserControl.YVALUEMEMBER = t.Rows[0][7].ToString();
                DrawSaved_ChartUserControl.ENABLELEGEND = true;
                DrawSaved_ChartUserControl.XAXISINTERVAL = 1;
                DrawSaved_ChartUserControl.XAXISTITLE = t.Rows[0][6].ToString();
                DrawSaved_ChartUserControl.YAXISTITLE = t.Rows[0][8].ToString();
                DrawSaved_ChartUserControl.CHARTBACKGROUNDCOLOR = t.Rows[0][9].ToString();
                DrawSaved_ChartUserControl.CHARTSERIESCOLOR = t.Rows[0][10].ToString();
                if (t.Rows[0][11].ToString() == "1")
                    DrawSaved_ChartUserControl.ENABLE3D = true;
                else
                    DrawSaved_ChartUserControl.ENABLE3D = false;
                DrawSaved_ChartUserControl.SORTING = "Ascending";
                DrawSaved_ChartUserControl.FORMULA = "Sum";
               
                DrawSaved_ChartUserControl.CreateChart_UserControl();
                
            }
            DatabaseClass.closeReaderConnection();
            DatabaseClass.closeConnection();
          //  connection.Close();
        }
     
    }
}