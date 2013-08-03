using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Web.UI.DataVisualization.Charting;

namespace DataVisualization.CodeBehind.Chart.FunctionChart
{
    public partial class DisplayFormula : BasePage
    {
        public  void PopulateData()
        {
    
            if (Session["LoggedInUserId"] == null)
                Response.Redirect( "~/CodeBehind/Home/NotLoggedIn.aspx");
           // Populate series data
            changeformulaChart.Visible = false;
            double[]	yValues = {645.62, 745.54, 360.45, 534.73, 285.42, 832.12, 455.18, 667.15, 256.24, 523.65, 356.56, 475.85, 156.78, 450.67};
            string[]	xValues = {"John", "Peter", "Dave", "Alex", "Scott", "Peter", "Alex", "Dave", "John", "Peter", "Dave", "Scott", "Alex", "Peter"};
            FormulaChart.Series["Series1"].Points.DataBindXY(xValues, yValues);
            changeformulaChart.Series["formulachange"].Points.DataBindXY(xValues, yValues);
            foreach(String s in xValues)
            {
                FormulaChart.Series["Series1"].XValueMember = s;
            }
            FormulaChart.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            FormulaChart.Series["Series1"].IsValueShownAsLabel = true;
            // Group series data points by Axis Label (sales person name)   
        }

        public void Page_Load(object sender, System.EventArgs e)
        {            
            PopulateData();
        }

        protected void FormulaExecute(object sender, System.EventArgs e)
        {            

           if(FormulaSelectDropDownList.SelectedItem.ToString() == "Sum")
            changeformulaChart.DataManipulator.GroupByAxisLabel(FormulaSelectDropDownList.SelectedItem.ToString(), "formulachange", "Total by Person(SUM)");
           if(FormulaSelectDropDownList.SelectedItem.ToString() == "Minimum")
               changeformulaChart.DataManipulator.GroupByAxisLabel("MIN, X:CENTER", "formulachange", "Mnimum by Person(Minimum)");
           if (FormulaSelectDropDownList.SelectedItem.ToString() == "Maximum")
               changeformulaChart.DataManipulator.GroupByAxisLabel("MAX", "formulachange", "Maximum by Person(Maximum)");
           if (FormulaSelectDropDownList.SelectedItem.ToString() == "Average")
               changeformulaChart.DataManipulator.GroupByAxisLabel("AVE", "formulachange", "Average by Person(Average)");
           if (FormulaSelectDropDownList.SelectedItem.ToString() == "Count")
               changeformulaChart.DataManipulator.GroupByAxisLabel("Count", "formulachange", "Count by Person(Count)");
           if (FormulaSelectDropDownList.SelectedItem.ToString() == "HiLo")
               changeformulaChart.Series["formulachange"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.SplineRange;
           if (FormulaSelectDropDownList.SelectedItem.ToString() == "HiLoOpCl")
               changeformulaChart.Series["formulachange"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Stock;
           if (FormulaSelectDropDownList.SelectedItem.ToString() == "Variance")
               changeformulaChart.DataManipulator.GroupByAxisLabel("VARIANCE", "formulachange", "Variation by Person(Variation)");
           if (FormulaSelectDropDownList.SelectedItem.ToString() == "Deviation")
               changeformulaChart.DataManipulator.GroupByAxisLabel("STDDEV", "formulachange", "Deviation by Person(Deviation)");
           if (FormulaSelectDropDownList.SelectedItem.ToString() == "DistinctCount")
               changeformulaChart.DataManipulator.GroupByAxisLabel("Count", "formulachange", "Distinct Count by Person(Distinct Count)");
           changeformulaChart.ChartAreas["changeformulaChartArea"].AxisX.Interval = 1;
           
           changeformulaChart.Visible = true;

        }
   

        
    }
}