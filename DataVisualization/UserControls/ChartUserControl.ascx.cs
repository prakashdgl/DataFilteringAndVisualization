using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Data.SqlClient;
namespace DataVisualization.ChartUserControl
{
    #region class
    public partial class ChartUserControl : System.Web.UI.UserControl
    {
        #region variable declaration
        private String ChartTitle { get; set; }
        private String ChartTypeofchart { get; set; }
        private String ChartName { get; set; }
        private String ChartBackGroundColor { get; set; }
        private String ChartSeriesColor { get; set; }
        private String xaxisvaluemember { get; set; }
        private String yaxisvaluemember { get; set; }
        private String xaxistitle { get; set; }
        private String yaxistitle { get; set; }
        private Boolean enable3d { get; set; }
        private String sorting { get; set; }
        private String formula { get; set; }
        private Boolean enablelegend { get; set; }
        private DataSet datasetforchart { get; set; }
        private int xaxisinterval { get; set; }
        private String legendtitle { get; set; }
        private int chartheight { get; set; }
        private int chartwidth { get; set; }
        private SqlDataReader reader{get; set;}   
        #endregion

       

        #region formula
        public void FormulaGrouping_UserControl()
        {
           
            if (formula == "Sum")
                ucFinalChart.DataManipulator.GroupByAxisLabel("SUM", "Series1", "SUM");
            if (formula == "Minimum")
                ucFinalChart.DataManipulator.GroupByAxisLabel("MIN, X:CENTER", "Series1", "Minimum");
            if (formula == "Maximum")
                ucFinalChart.DataManipulator.GroupByAxisLabel("MAX", "Series1", "Maximum");
            if (formula == "Average")
                ucFinalChart.DataManipulator.GroupByAxisLabel("AVE", "Series1", "Average");
            if (formula == "Count")
                ucFinalChart.DataManipulator.GroupByAxisLabel("Count", "Series1", "Count");
            if (formula == "HiLo")
                ucFinalChart.Series["Series1"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.SplineRange;
            if (formula == "HiLoOpCl")
                ucFinalChart.Series["Series1"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Stock;
            if (formula == "Variance")
                ucFinalChart.DataManipulator.GroupByAxisLabel("VAR", "Series1", "Variation");
            if (formula == "Deviation")
                ucFinalChart.DataManipulator.GroupByAxisLabel("DEV", "Series1", "Deviation");
            if (formula == "DistinctCount")
                ucFinalChart.DataManipulator.GroupByAxisLabel("DISTCOUNT", "Series1", "Distinct Count");
        }

        
        #endregion
        public void CreateChart_UserControl()
        {
            #region title, legend, legentitle
            //sets the chart title
            ucFinalChart.Titles["ucFinalTitle"].Text = ChartTitle;
            //for enabling the legend
            ucFinalChart.Legends["ChartLegend"].Enabled = true; // enablelegend;
            ucFinalChart.Series["Series1"].LegendText = legendtitle;
            #endregion
            #region databind
            try
            {
                ucFinalChart.Series["Series1"].Points.DataBindXY(reader, xaxisvaluemember, reader, yaxisvaluemember);
            }
            catch (Exception e1)
            {
                try
                {
                    ucFinalChart.Series["Series1"].Points.DataBindXY(reader, xaxisvaluemember.Trim(' '), reader, yaxisvaluemember.Trim(' '));
                }
                catch (Exception e2)
                {
                    try
                    {
                        ucFinalChart.Series["Series1"].Points.DataBindXY(reader, xaxisvaluemember.Trim(' '), reader, yaxisvaluemember);
                    }
                    catch (Exception e3)
                    {
                        ucFinalChart.Series["Series1"].Points.DataBindXY(reader, xaxisvaluemember, reader, yaxisvaluemember.Trim(' '));
                    }

                }
            }
#endregion
            //not using this 
            #region datasource



            //assingDataPoint();
            //Set the datasource
            //try
            //{
            //ucFinalChart.DataSource = datasetforchart;
            //ucFinalChart.DataBind();

            //}
            //catch (Exception)
            //{
            //    //Do what to do with the error
            ////}
            #endregion 
           
            #region set axis interval
            ucFinalChart.ChartAreas["ucFinalChartArea"].AxisX.Interval = xaxisinterval;
#endregion           
            #region chartType Region
            if (ChartTypeofchart == "Column")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Column;
            if (ChartTypeofchart == "Bar")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Bar;
            if (ChartTypeofchart == "Stacked")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.StackedBar;
            if (ChartTypeofchart == "Pie")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Pie;
            if (ChartTypeofchart == "Area")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Area;
            if (ChartTypeofchart == "BoxPlot")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.BoxPlot;
            if (ChartTypeofchart == "Line")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Line;
            if (ChartTypeofchart == "Point")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Point;
            if (ChartTypeofchart == "Spline")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Spline;
            #endregion
            //not using this either
            #region DataBound To Axises
            ////xvaluemember
            //ucFinalChart.Series["Series1"].XValueMember = xaxisvaluemember;
            ////yvaluemember
            //ucFinalChart.Series["Series1"].YValueMembers = yaxisvaluemember;
            #endregion
            #region Axis Titles
            //value shown as label
            ucFinalChart.Series["Series1"].IsValueShownAsLabel = true;
            //xaxis title
            ucFinalChart.ChartAreas["ucFinalChartArea"].Axes[0].Title = xaxistitle;
            //yaxis title
            ucFinalChart.ChartAreas["ucFinalChartArea"].Axes[1].Title = yaxistitle;
            #endregion
            #region legend text
            //Legend Text
            ucFinalChart.Series["Series1"].LegendText = xaxisvaluemember;
            #endregion
            #region chart BackGround color
            if (ChartBackGroundColor == "Black")
                ucFinalChart.BackColor = System.Drawing.Color.Black;
            if (ChartBackGroundColor == "Blue")
                ucFinalChart.BackColor = System.Drawing.Color.Blue;
            if (ChartBackGroundColor == "Green")
                ucFinalChart.BackColor = System.Drawing.Color.Green;
            if (ChartBackGroundColor == "Red")
                ucFinalChart.BackColor = System.Drawing.Color.Red;
            if (ChartBackGroundColor == "Yellow")
                ucFinalChart.BackColor = System.Drawing.Color.Yellow;
            if (ChartBackGroundColor == "Pink")
                ucFinalChart.BackColor = System.Drawing.Color.Pink;
            if (ChartBackGroundColor == "AliceBlue")
                ucFinalChart.BackColor = System.Drawing.Color.AliceBlue;
            if (ChartBackGroundColor == "Aqua")
                ucFinalChart.BackColor = System.Drawing.Color.Aqua;
            if (ChartBackGroundColor == "Aquamarine")
                ucFinalChart.BackColor = System.Drawing.Color.Aquamarine;
            if (ChartBackGroundColor == "Brown")
                ucFinalChart.BackColor = System.Drawing.Color.Brown;
            if (ChartBackGroundColor == "Chocolate")
                ucFinalChart.BackColor = System.Drawing.Color.Chocolate;
            if (ChartBackGroundColor == "DarkBlue")
                ucFinalChart.BackColor = System.Drawing.Color.DarkBlue;
            if (ChartBackGroundColor == "DarkCyan")
                ucFinalChart.BackColor = System.Drawing.Color.DarkCyan;
            if (ChartBackGroundColor == "Darkviolet")
                ucFinalChart.BackColor = System.Drawing.Color.DarkViolet;
            if (ChartBackGroundColor == "Ivory")
                ucFinalChart.BackColor = System.Drawing.Color.Ivory;
            if (ChartBackGroundColor == "Azure")
                ucFinalChart.BackColor = System.Drawing.Color.Azure;
            if (ChartBackGroundColor == "DimGray")
                ucFinalChart.BackColor = System.Drawing.Color.DimGray;
            ucFinalChart.ChartAreas["ucFinalChartArea"].BackColor = Color.AliceBlue;
            #endregion
            #region chart Series Color

            if (ChartSeriesColor == "Black")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.Black;
            if (ChartSeriesColor == "Blue")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.Blue;
            if (ChartSeriesColor == "Green")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.Green;
            if (ChartSeriesColor == "Red")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.Red;
            if (ChartSeriesColor == "Yellow")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.Yellow;
            if (ChartSeriesColor == "Pink")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.Pink;
            if (ChartSeriesColor == "AliceBlue")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.AliceBlue;
            if (ChartSeriesColor == "Aqua")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.Aqua;
            if (ChartSeriesColor == "Aquamarine")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.Aquamarine;
            if (ChartSeriesColor == "Brown")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.Brown;
            if (ChartSeriesColor == "Chocolate")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.Chocolate;
            if (ChartSeriesColor == "DarkBlue")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.DarkBlue;
            if (ChartSeriesColor == "DarkCyan")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.DarkCyan;
            if (ChartSeriesColor == "Darkviolet")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.DarkViolet;
            if (ChartSeriesColor == "Ivory")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.Ivory;
            if (ChartSeriesColor == "Azure")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.Azure;
            if (ChartSeriesColor == "DimGray")
                ucFinalChart.Series["Series1"].Color = System.Drawing.Color.DimGray;
            #endregion
            #region Enable 3D
            ucFinalChart.ChartAreas["ucFinalChartArea"].Area3DStyle.Enable3D = enable3d;
            #endregion
            #region sorting

            if (sorting == "Ascending")
            {
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Column;
                ucFinalChart.DataManipulator.Sort(PointSortOrder.Ascending, "Series1");
            }
            else if(sorting == "Descending")
            {
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Column;
                ucFinalChart.DataManipulator.Sort(PointSortOrder.Descending, "Series1");
            }
            #endregion
            #region formula region
            
            if (formula == "Sum")           
                ucFinalChart.DataManipulator.GroupByAxisLabel("SUM", "Series1", "SUM");      
            if (formula == "Minimum")
                ucFinalChart.DataManipulator.GroupByAxisLabel("MIN, X:CENTER", "Series1", "Minimum");
            if (formula == "Maximum")
                ucFinalChart.DataManipulator.GroupByAxisLabel("MAX", "Series1", "Maximum");
            if (formula == "Average")
                ucFinalChart.DataManipulator.GroupByAxisLabel("AVE", "Series1", "Average");
            if (formula == "Count")
                ucFinalChart.DataManipulator.GroupByAxisLabel("Count", "Series1", "Count");
            if (formula == "HiLo")
                ucFinalChart.Series["Series1"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.SplineRange;
            if (formula == "HiLoOpCl")
                ucFinalChart.Series["Series1"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Stock;
            if (formula == "Variance")
                ucFinalChart.DataManipulator.GroupByAxisLabel("VAR", "Series1", "Variation");
            if (formula == "Deviation")
                ucFinalChart.DataManipulator.GroupByAxisLabel("DEV", "Series1", "Deviation");
            if (formula == "DistinctCount")
                ucFinalChart.DataManipulator.GroupByAxisLabel("DISTCOUNT", "Series1", "Distinct Count");
            
            #endregion
            #region enableLegend
            ucFinalChart.Legends["ChartLegend"].Enabled = enablelegend;
            #endregion
            ///Enable viewing            
            ucFinalChart.EnableViewState = true;
        }
      
        #region Page_Load
        /*protected void Page_Load(object sender, EventArgs e)
        {
            //sets the chart title
            ucFinalChart.Titles["ucFinalTitle"].Text = ChartTitle;
            //for enalbling the legend
                                  //  ucFinalChart.Legends["ChartLegend"].Enabled = true; // enablelegend;
            //Set the datasource
            try
            {
                ucFinalChart.DataSource = datasetforchart;
                ucFinalChart.DataBind();

            }
            catch (Exception)
            { 
             //Do what to do with the error
            }

            //set axis interval
            ucFinalChart.ChartAreas["ucFinalChartArea"].AxisX.Interval = xaxisinterval;
            //Type Of Chart
            if (ChartTypeofchart == "Column")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Column;
            if (ChartTypeofchart == "Bar")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Bar;
            if (ChartTypeofchart == "Stacked")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.StackedBar;
            if (ChartTypeofchart == "Pie")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Pie;
            if (ChartTypeofchart == "Area")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Area ;
            if (ChartTypeofchart == "BoxPlot")
                ucFinalChart.Series["Series1"].ChartType =  SeriesChartType.BoxPlot;
            if (ChartTypeofchart == "Line")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Line;
            if (ChartTypeofchart == "Point")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Point;
            if (ChartTypeofchart == "Spline")
                ucFinalChart.Series["Series1"].ChartType = SeriesChartType.Spline;
            //xvaluemember
            ucFinalChart.Series["Series1"].XValueMember = xaxisvaluemember;
            //yvaluemember
            ucFinalChart.Series["Series1"].YValueMembers = yaxisvaluemember;
            //value shown as label
            ucFinalChart.Series["Series1"].IsValueShownAsLabel = true;
            //xaxis title
            ucFinalChart.ChartAreas["ucFinalChartArea"].Axes[0].Title =xaxistitle;
            //yaxis title
            ucFinalChart.ChartAreas["ucFinalChartArea"].Axes[1].Title = yaxistitle;            
            //Legend Text
            ucFinalChart.Series["Series1"].LegendText = xaxisvaluemember;
            ///Enable viewing
            ucFinalChart.EnableViewState = true;
        }
  */
        #endregion
        #region getters and setters
        public string CHARTTYPE
        {
            get { return ChartTypeofchart; }
            set { ChartTypeofchart = value; }
        }
        public String CHARTTITLE
        {
            get { return this.ChartTitle; }
            set { ChartTitle = value; }
        }
        public string CHARTNAME
        {
            get { return ChartName; }
            set { ChartName = value; }
        }
        public String CHARTBACKGROUNDCOLOR
        {
            get { return ChartBackGroundColor; }
            set { ChartBackGroundColor = value; }
        }
        public String CHARTSERIESCOLOR
        {
            get { return ChartSeriesColor; }
            set { ChartSeriesColor = value; }
        }

        public String XVALUEMEMBER
        {
            get { return xaxisvaluemember; }
            set { xaxisvaluemember = value; }
        }
        public String YVALUEMEMBER
        {
            get { return yaxisvaluemember; }
            set { yaxisvaluemember = value; }
        }
        public String XAXISTITLE
        {
            get { return xaxistitle; }
            set { xaxistitle = value; }
        }
        public String YAXISTITLE
        {
            get { return yaxistitle; }
            set { yaxistitle = value; }
        }
        public Boolean ENABLE3D
        {
            get { return enable3d; }
            set { enable3d = value; }
        }
        public String FORMULA
        {
            get { return formula; }

            set { formula = value; }
        }
        public String SORTING
        {
            get { return sorting; }
            set { sorting = value; }
        }
        public DataSet DATASETFORCHART
        {
            get { return datasetforchart; }
            set { datasetforchart = value; }
        }
        public int XAXISINTERVAL
        {
            get { return xaxisinterval; }
            set { xaxisinterval = value; }
        }
        public Boolean ENABLELEGEND
        {
            get { return enablelegend; }
            set { enablelegend = value; }
        }

        public string LEGENDTITLE
        {
            get { return legendtitle; }
            set { legendtitle = value; }
        }
        public int CHARTWIDTH
        {
            get { return chartwidth; }
            set { chartwidth = value; }
        }

        public int CHARTHEIGHT
        {
            get { return chartheight; }
            set { chartheight = value; }
        }
                
        public SqlDataReader READER
        {
            get { return reader; }
            set { reader = value;}
        }
        
        #endregion
    }
    #endregion
}