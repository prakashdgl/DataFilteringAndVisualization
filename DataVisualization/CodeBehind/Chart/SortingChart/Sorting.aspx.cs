using System; 
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.UI.DataVisualization.Charting;

namespace DataVisualization.CodeBehind.Chart.SortingChart
{
    public partial class Sorting : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInUserId"] == null)
                Response.Redirect( "~/CodeBehind/Home/NotLoggedIn.aspx");
            sortedchart.Visible = false;
            SortingChart.ChartAreas["SortingChartArea"].AxisX.Interval = 1;
            double[]    valueY = {120, 530, 670, 430, 860, 240, 350, 890, 540, 180 };
            string[]    valueX = {"D", "A", "B", "A", "C", "C", "B", "A", "C", "B"};
            SortingChart.Series["Unsorted"].Points.DataBindXY(valueX, valueY);
            sortedchart.Series["sorted"].Points.DataBindXY(valueX, valueY);
           // object a, b;
            // Sort series data points

       //     SortingChart.DataManipulator.Sort(new MyComparer(), SortingChart.Series["sorted"]);


        }

        protected void sortingchanges(object sender, EventArgs e)
        {
            if (DropDownList1.SelectedItem.Text == "Ascending")
                sortedchart.DataManipulator.Sort(PointSortOrder.Ascending, "sorted");
            else
                sortedchart.DataManipulator.Sort(PointSortOrder.Descending, "sorted");
            // Use point index for drawing the chart
            sortedchart.ChartAreas["sortedChartArea"].AxisX.Interval = 1;
            sortedchart.Series["sorted"].IsXValueIndexed = true;
            sortedchart.Visible = true;
        }
       

    }
    
}
