using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using DataVisualization.CodeBehind.Database;

namespace DataVisualization.CodeBehind
{
    public class ClassChartDashBoard
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
        private int ChartId;
#endregion
        public ClassChartDashBoard(int chartid)
        {
            this.ChartId = chartid;
        }
        public DataTable setAllTheChartData()
        {
            string chartidquery = "SELECT * FROM [dvs_user] WHERE [ChartId] = " + ChartId;
            DataTable allchartdata = DatabaseClass.ExecuteQuery(chartidquery);
            createChart(allchartdata);
            return allchartdata;
        }

        public void createChart(DataTable dt)
        {
            

          
                  
           
            
           
        }

       
        //getters and setters
        public int CHARTID
        {
            get { return ChartId; }
            set { ChartId = value;}
        }
        
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
    }
}