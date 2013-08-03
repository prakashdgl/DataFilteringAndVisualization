using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataVisualization.CodeBehind;
using DataVisualization.CodeBehind.Database;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
namespace DataVisualization.UserControls
{
    public partial class DashboardSectionUserControl : System.Web.UI.UserControl
    {
        #region Variable Declaration
        /// <summary>
        /// Declare Variables
        /// </summary>
        public String mode
        {
            set { ViewState["mode"] = value; }
            get
            {
                if (ViewState["mode"] != null)
                {
                    return ViewState["mode"].ToString();
                }
                else return "";
            }
        }

        public int oldChartId
        {
            set { ViewState["oldChartId"] = value; }
            get
            {
                if (ViewState["oldChartId"] != null)
                {
                    return int.Parse(ViewState["oldChartId"].ToString());
                }
                else return 0;
            }
        }

        public int chartId
        {
            set { ViewState["chartId"] = value; }
            get
            {
                if (ViewState["chartId"] != null)
                {
                    return int.Parse(ViewState["chartId"].ToString());
                }
                else return 0;
            }
        }

        public int sectionId
        {
            set { ViewState["sectionId"] = value; }
            get { return int.Parse(ViewState["sectionId"].ToString()); }
        }

        public int dashboardId
        {
            set { ViewState["dashboardId"] = value; }
            get
            {
                if (ViewState["dashboardId"] != null)
                {
                    return int.Parse(ViewState["dashboardId"].ToString());
                }
                else return 0;
            }
        }

        public int width
        {
            set { ViewState["width"] = value; }
            get { return int.Parse(ViewState["width"].ToString()); }
        }

        public int height
        {
            set { ViewState["height"] = value; }
            get { return int.Parse(ViewState["height"].ToString()); }
        }

        protected String dropDownMessage = "--Select A Chart--";
        #endregion


        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {   //Drop down list must be initialized when, it shown for the first time 
            if (!Page.IsPostBack)
            {
                initializeDropDownList();
            }
        }

        protected void chartDDLSelectedIndexChanged(object sender, EventArgs e)
        {
            String chartTitle = chartDropDownList.SelectedItem.Text;
            if (chartTitle != dropDownMessage)
            {
                oldChartId = chartId;
                chartId = int.Parse(chartDropDownList.SelectedItem.Value);
                acceptchartid();
                //Chart user control should be retrieved from the database and drawn.
                //chartTitleLabel.Text += "Chart id (DDL)" + chartId.ToString() + " title: " + chartTitle;
                //Response.Write("Drop downlist chart id = " + chartId.ToString());
                //chartUserControl.Visible = true;
            }
            //When, the user selects a chart from the drop down list, that chart should be shown.

        }
        #endregion

        #region chartdisplay region
        public void acceptchartid()
        {
            if (ViewState["chartId"] != null)
            {

                string a = "SELECT * FROM [dvs_Chart] WHERE [ChartId] = " + chartId;
                DataTable t = DatabaseClass.ExecuteQuery(a);
                //sets the chart title
                Chart1.Titles["ucFinalTitle"].Text = t.Rows[0][3].ToString();
                //for enalbling the legend
                Chart1.Legends["ChartLegend"].Enabled = true; // enablelegend;
                Chart1.Series["Series1"].LegendText = "LegendTitle";
                #region datasource
                //Set the datasource
                try
                {
                    Chart1.DataSource = DatabaseClass.getTableDatafromTable(t.Rows[0][1].ToString());
                    Chart1.DataBind();

                }
                catch (Exception)
                {
                    //Do what to do with the error
                }
                #endregion
                //set axis interval
                Chart1.ChartAreas["Chart1Area"].AxisX.Interval = 1;
                //Type Of Chart
                #region chartType Region
                if (t.Rows[0][4].ToString() == "Column")
                    Chart1.Series["Series1"].ChartType = SeriesChartType.Column;
                if (t.Rows[0][4].ToString() == "Bar")
                    Chart1.Series["Series1"].ChartType = SeriesChartType.Bar;
                if (t.Rows[0][4].ToString() == "Stacked")
                    Chart1.Series["Series1"].ChartType = SeriesChartType.StackedBar;
                if (t.Rows[0][4].ToString() == "Pie")
                    Chart1.Series["Series1"].ChartType = SeriesChartType.Pie;
                if (t.Rows[0][4].ToString() == "Area")
                    Chart1.Series["Series1"].ChartType = SeriesChartType.Area;
                if (t.Rows[0][4].ToString() == "BoxPlot")
                    Chart1.Series["Series1"].ChartType = SeriesChartType.BoxPlot;
                if (t.Rows[0][4].ToString() == "Line")
                    Chart1.Series["Series1"].ChartType = SeriesChartType.Line;
                if (t.Rows[0][4].ToString() == "Point")
                    Chart1.Series["Series1"].ChartType = SeriesChartType.Point;
                if (t.Rows[0][4].ToString() == "Spline")
                    Chart1.Series["Series1"].ChartType = SeriesChartType.Spline;
                #endregion
                #region DataBound To Axises
                //xvaluemember
                Chart1.Series["Series1"].XValueMember = t.Rows[0][5].ToString();
                //yvaluemember
                Chart1.Series["Series1"].YValueMembers = t.Rows[0][7].ToString();
                #endregion
                #region Axis Titles
                //value shown as label
                Chart1.Series["Series1"].IsValueShownAsLabel = true;
                //xaxis title
                Chart1.ChartAreas["Chart1Area"].Axes[0].Title = t.Rows[0][6].ToString();
                //yaxis title
                Chart1.ChartAreas["Chart1Area"].Axes[1].Title = t.Rows[0][8].ToString();
                #endregion
                #region legend text
                //Legend Text
                Chart1.Series["Series1"].LegendText = t.Rows[0][5].ToString();
                #endregion
                #region chart BackGround color
                if (t.Rows[0][9].ToString() == "Black")
                    Chart1.BackColor = System.Drawing.Color.Black;
                if (t.Rows[0][9].ToString() == "Blue")
                    Chart1.BackColor = System.Drawing.Color.Blue;
                if (t.Rows[0][9].ToString() == "Green")
                    Chart1.BackColor = System.Drawing.Color.Green;
                if (t.Rows[0][9].ToString() == "Red")
                    Chart1.BackColor = System.Drawing.Color.Red;
                if (t.Rows[0][9].ToString() == "Yellow")
                    Chart1.BackColor = System.Drawing.Color.Yellow;
                if (t.Rows[0][9].ToString() == "Pink")
                    Chart1.BackColor = System.Drawing.Color.Pink;
                if (t.Rows[0][9].ToString() == "AliceBlue")
                    Chart1.BackColor = System.Drawing.Color.AliceBlue;
                if (t.Rows[0][9].ToString() == "Aqua")
                    Chart1.BackColor = System.Drawing.Color.Aqua;
                if (t.Rows[0][9].ToString() == "Aquamarine")
                    Chart1.BackColor = System.Drawing.Color.Aquamarine;
                if (t.Rows[0][9].ToString() == "Brown")
                    Chart1.BackColor = System.Drawing.Color.Brown;
                if (t.Rows[0][9].ToString() == "Chocolate")
                    Chart1.BackColor = System.Drawing.Color.Chocolate;
                if (t.Rows[0][9].ToString() == "DarkBlue")
                    Chart1.BackColor = System.Drawing.Color.DarkBlue;
                if (t.Rows[0][9].ToString() == "DarkCyan")
                    Chart1.BackColor = System.Drawing.Color.DarkCyan;
                if (t.Rows[0][9].ToString() == "Darkviolet")
                    Chart1.BackColor = System.Drawing.Color.DarkViolet;
                if (t.Rows[0][9].ToString() == "Ivory")
                    Chart1.BackColor = System.Drawing.Color.Ivory;
                if (t.Rows[0][9].ToString() == "Azure")
                    Chart1.BackColor = System.Drawing.Color.Azure;
                if (t.Rows[0][9].ToString() == "DimGray")
                    Chart1.BackColor = System.Drawing.Color.DimGray;

                #endregion
                #region chart Series Color

                if (t.Rows[0][10].ToString() == "Black")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.Black;
                if (t.Rows[0][10].ToString() == "Blue")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.Blue;
                if (t.Rows[0][10].ToString() == "Green")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.Green;
                if (t.Rows[0][10].ToString() == "Red")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.Red;
                if (t.Rows[0][10].ToString() == "Yellow")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.Yellow;
                if (t.Rows[0][10].ToString() == "Pink")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.Pink;
                if (t.Rows[0][10].ToString() == "AliceBlue")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.AliceBlue;
                if (t.Rows[0][10].ToString() == "Aqua")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.Aqua;
                if (t.Rows[0][10].ToString() == "Aquamarine")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.Aquamarine;
                if (t.Rows[0][10].ToString() == "Brown")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.Brown;
                if (t.Rows[0][10].ToString() == "Chocolate")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.Chocolate;
                if (t.Rows[0][10].ToString() == "DarkBlue")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.DarkBlue;
                if (t.Rows[0][10].ToString() == "DarkCyan")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.DarkCyan;
                if (t.Rows[0][10].ToString() == "Darkviolet")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.DarkViolet;
                if (t.Rows[0][10].ToString() == "Ivory")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.Ivory;
                if (t.Rows[0][10].ToString() == "Azure")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.Azure;
                if (t.Rows[0][10].ToString() == "DimGray")
                    Chart1.Series["Series1"].Color = System.Drawing.Color.DimGray;
                #endregion
                #region Enable 3D
                Chart1.ChartAreas["Chart1Area"].Area3DStyle.Enable3D = true;
                #endregion
                #region enableLegend
                Chart1.Legends["ChartLegend"].Enabled = true;
                #endregion
                ///Enable viewing            
                Chart1.EnableViewState = true;

                Chart1.Visible = true;
            }
        }

        #endregion

        #region Functions

        public void initializeDropDownList()
        {
            //Get the list of charts from the table
            chartDropDownList.Items.Clear();
            chartDropDownList.Items.Add(dropDownMessage);
            DataTable chartDT = getChartDT();
            for (int i = 0; i < chartDT.Rows.Count; i++)
            {
                int chartIdFromList = int.Parse(chartDT.Rows[i][0].ToString());
                String chartTitle = chartDT.Rows[i][1].ToString();
                chartDropDownList.Items.Add(chartTitle);
                chartDropDownList.Items[i + 1].Value = chartIdFromList.ToString();
            }
        }


        public void adjustAccordingToMode()
        {
            if (mode == DashboardModes.CREATE)
            {
                //chartUserControl.Visible = false;
                chartDropDownList.Visible = true;
                //Response.Write("my section id is " + sectionId + "<br/>");
            }
            else if (mode == DashboardModes.EDIT)
            {
                //chartUserControl.Visible = true;
                retrieve();
                chartDropDownList.Visible = true;
                if (chartId != 0)
                    chartDropDownList.SelectedValue = chartId.ToString();
            }
            else if (mode == DashboardModes.VIEW)
            {
                //chartUserControl.Visible = true;
                retrieve();
                //chartTitleLabel.Text = "View Mode: chart is present";
                chartDropDownList.Visible = false;
                chartDropDownList.SelectedValue = chartId.ToString();
            }
        }
        #endregion


        #region Database related functions
        public void insert()
        {
            if (chartId == 0)
                return;
            String insertQuery = "INSERT INTO dvs_Chart_Dashboard(ChartId,DashboardId,SectionId) VALUES (" + chartId + "," + dashboardId + "," + sectionId + ")";
            DatabaseClass.ExecuteNonQuery(insertQuery);
        }

        public void update()
        {
            if (chartId == 0 || ViewState["oldChartId"] == null)
                return;
            String updateQuery = "UPDATE dvs_Chart_Dashboard SET ChartId=" + chartId + " WHERE ChartId=" + oldChartId + " AND DashboardId = " + dashboardId + " AND SectionId=" + sectionId;
            DatabaseClass.ExecuteNonQuery(updateQuery);
        }

        public void retrieve()
        {
            String query = "SELECT ChartId FROM dvs_Chart_Dashboard WHERE DashboardId=" + dashboardId + " AND SectionId=" + sectionId;
            DataTable dt = DatabaseClass.ExecuteQuery(query);
            if (dt.Rows.Count > 0)
            {
                chartId = int.Parse(dt.Rows[0][0].ToString());
            }
        }

        public DataTable getChartDT()
        {
            String query = "SELECT ChartId, ChartTitle FROM dvs_Chart WHERE UserId='" + Session["LoggedInUserId"].ToString() + "'";
            return DatabaseClass.ExecuteQuery(query);
        }
        #endregion


    }
}