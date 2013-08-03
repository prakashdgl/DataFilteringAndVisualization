
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DataVisualization.CodeBehind.Database;
using DataVisualization.CodeBehind;
using System.Drawing;
#region namespace Datavisualization.createchart

/// <summary>
/// variable summary:
/// 
/// selectFile_FileUpload: this type is not used until now. I am only working on the default database.
/// The default databse is loaded in the page_load() method.
///  
/// TableNameDropDownList : This is the DropDownList control which generates the table name of selected database.
/// xAxisDropDownList: This is also a DropDownList control which generates the table schemas.The selected item 
///                    in this dropdownlist will not appear in yAxisDopDownList.This dropdownlist is for selecting 
///                    x-axis for chart.
///yAxisDropDownList: This is also a DropDownList control which generates the TableSchems.This is for selecting 
///                   the yAxis for the chart control
///chartTypeDropDownList : This is a DropDownList for selecting the type of chart that user wants to display.
///                    
/// </summary>


namespace DataVisualization.CreateChart
{
    public partial class CreateChart : BasePage
    {
        /// <summary>
        /// At this page load method, database connection is created with the default database.
        /// chartTypeDropDownList is initialized with the possible chart types.
        /// </summary>

        #region alldatabase and dropdownlist events
        protected void Page_Load(object sender, EventArgs e)
        {
            CreateChartChartUserControl.Visible = false;
            if (Session["LoggedInUserId"] == null)
                Response.Redirect("~/Account/Login.aspx");

            if (!Page.IsPostBack)
            {

                chartTypeDropDownList.Items.Clear();
                chartTypeDropDownList.Items.Add("--Select Type Of Chart--");
                String q = "Select * From dvs_ChartType;";
                DataTable t = DatabaseClass.ExecuteQuery(q);
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    chartTypeDropDownList.Items.Add(t.Rows[i][0].ToString());
                }
                getConnection();

            }
        }

        /// <summary>
        /// This method gets the table names of the databse
        /// </summary>


        public void getConnection()
        {
            TableNameDropDownList.Items.Clear();
            TableNameDropDownList.Items.Add("--Select Table--");
            string a = "SELECT [TableId] FROM [dvs_User_Table] WHERE [UserId] = '" + Session["LoggedInUserId"].ToString() + "';";
            DataTable t = DatabaseClass.ExecuteQuery(a);
            for (int i = 0; i < t.Rows.Count; i++ )
            {
                for (int j = 0; j < t.Columns.Count; j++)
                {
                    TableNameDropDownList.Items.Add(t.Rows[i][j].ToString());
                }
            }           
        }

        /// <summary>
        /// This method loads the data of the selected table of the 
        /// selected item in TableNameDropDownList in gridview
        /// </summary>

        public void displaydataGrid()
        {
            TabledataGridView.Visible = true;
            TabledataGridView.DataSource = DatabaseClass.getTableDatafromTable(TableNameDropDownList.SelectedItem.ToString());
            TabledataGridView.DataBind();
        }

        /// <summary>
        /// This method is the selectedIndexChangedMethod of the TableNameDropDownLIst 
        /// This method is to get the Table Information about schemas and attributes.
        /// GetSchemaOfTable is the class that gets all the table schema information
        /// and loads into the Datatable datatable
        /// xAxisDropDownList and yAxisDropDownList is initialized with the table schemas
        /// </summary>


        protected void getTabelSchemaName(object sender, EventArgs e)
        {
            TabledataGridView.Visible = false;
            DataTable t = TableSchemaGenerator.GetSchemaNameOfTable(TableNameDropDownList.SelectedItem.ToString());
            xAxisDropDownList.Items.Clear();
            yAxisDropDownList.Items.Clear();
            xAxisDropDownList.Items.Add("--Select X Axis--");
            yAxisDropDownList.Items.Add("--Select y Axis--");
            for (int i = 0; i < t.Rows.Count; i++)
            {
                xAxisDropDownList.Items.Add(t.Rows[i][3].ToString());
                yAxisDropDownList.Items.Add(t.Rows[i][3].ToString());
            }


        }

        /// <summary>
        /// This method is selectedIndexChanged for the xAxisDropDownList.
        /// In this method the items is removed in the yAxisDropDownList for which xAxisDropDownList is selected.
        /// </summary>

        protected void xAxisSelect(object sender, EventArgs e)
        {
            yAxisDropDownList.Items.Remove(xAxisDropDownList.SelectedItem.ToString());
        }

        /// <summary>
        /// This method is selectedIndexChanged for the chartTypeDropDownList.
        /// For the given selected item in xAxisDropDownList and yAxisDropDownList
        /// it generates the corresponding chart of the selected charttype.
        /// </summary>
        /// 

        public void testChartControl()
        {
            chartuserdrawpanel.Visible = true;
            if (TableNameDropDownList.SelectedItem.Text != null && xAxisDropDownList.SelectedItem.Text != null
                   && yAxisDropDownList.SelectedItem.Text != null)
            {
                if (Enable3dCheckBox.Checked)
                {
                    CreateChartChartUserControl.ENABLE3D = true;
                }
                else
                {
                    CreateChartChartUserControl.ENABLE3D = false;
                }                
                CreateChartChartUserControl.Visible = true;              
                CreateChartChartUserControl.CHARTTITLE = chartTile_TextBox.Text;
                CreateChartChartUserControl.CHARTTYPE = chartTypeDropDownList.SelectedItem.Text;
                CreateChartChartUserControl.XVALUEMEMBER = xAxisDropDownList.SelectedItem.Text;
                CreateChartChartUserControl.YVALUEMEMBER = yAxisDropDownList.SelectedItem.Text;
                CreateChartChartUserControl.LEGENDTITLE =  xAxisDropDownList.SelectedItem.Text;
                CreateChartChartUserControl.ENABLELEGEND = true;
                CreateChartChartUserControl.XAXISINTERVAL = 1;
                CreateChartChartUserControl.CHARTBACKGROUNDCOLOR = chartBackgroundColorPickerDropDownList.SelectedItem.ToString();
                CreateChartChartUserControl.CHARTSERIESCOLOR = SeriesColorPickerDropDownList.SelectedItem.ToString();
                if (SortDropDownList.SelectedIndex >= 0)
                { 
                    CreateChartChartUserControl.SORTING = SortDropDownList.SelectedItem.Text;
                }
                if (FormulaDropDownList.SelectedIndex >= 0)
                {
                    CreateChartChartUserControl.FORMULA = FormulaDropDownList.SelectedItem.Text;
                }             
                if (xAxisLabel_TextBox != null)
                    CreateChartChartUserControl.XAXISTITLE = "XAXIS TITLE : " + xAxisLabel_TextBox.Text + ".";
                else
                    CreateChartChartUserControl.XAXISTITLE = "XAXIS TITLE : " + xAxisDropDownList.SelectedItem.Text + " . ";
                if (yaxisLabel_TextBox.Text != null)
                    CreateChartChartUserControl.YAXISTITLE = "YAXIS TITLE :  " + yaxisLabel_TextBox.Text + ".";
                else
                    CreateChartChartUserControl.YAXISTITLE = "YAXIS TITLE :  " + yaxisLabel_TextBox.Text + " . " ;
                String chartcreatequery = "SELECT " + xAxisDropDownList.SelectedItem.Text + " , " + yAxisDropDownList.SelectedItem.Text + " FROM [" + TableNameDropDownList.SelectedItem.Text + "]";           
                CreateChartChartUserControl.READER = DatabaseClass.ExequteReaderQuery(chartcreatequery);
                CreateChartChartUserControl.CreateChart_UserControl();
                DatabaseClass.closeReaderConnection();
                DatabaseClass.closeConnection();                
            }
            
        }

        protected void GenerateChart_SelectedEvent(object sender, EventArgs e)
        {
            yAxisDropDownList.Items.Add(xAxisDropDownList.SelectedItem.Text); 
            testChartControl();
        }
        /// <summary>
        /// This method is selectedTextChanged event handeler for the TableNameDropDownList
        /// </summary>

        protected void displaydata_Grid(object sender, EventArgs e)
        {
            displaydataGrid();
        }
        /// <summary>
        /// This method is checkbox check  event handeler for the enable3d checkbox
        /// </summary>
        #endregion
        #region enable 3d
        protected void Enable3DChart(object sender, EventArgs e)
        {
            if (Enable3dCheckBox.Checked)
            {
                CreateChartChartUserControl.ENABLE3D = true;                
                testChartControl();
            }
            else
            {
                CreateChartChartUserControl.ENABLE3D = false;
                testChartControl();
            }
        }
        #endregion
        #region changeseries color
        protected void changeseriesColor(object sender, EventArgs e)
        {
            CreateChartChartUserControl.CHARTSERIESCOLOR = SeriesColorPickerDropDownList.SelectedItem.Text;
            testChartControl();
        }
        #endregion
        #region backgroundcolor change
        protected void changebackgroundcolor(object sender, EventArgs e)
        {
            CreateChartChartUserControl.CHARTBACKGROUNDCOLOR = chartBackgroundColorPickerDropDownList.SelectedItem.Text;
            testChartControl();

        }
        #endregion
        #region sortchanged slectedevent
        protected void SortChanged(object sender, EventArgs e)
        {           
            CreateChartChartUserControl.SORTING = SortDropDownList.SelectedItem.Text;
            testChartControl();  
        }
        #endregion
        #region getters and setters
        //getters
        public String GetxaxisTitle()
        {
            
            return xAxisDropDownList.SelectedItem.Text;
        }
        public String GetYaxisTitle()
        {
            return yAxisDropDownList.SelectedItem.Text;
        }
        public String GetChartTitle()
        {
            return chartTile_TextBox.Text;
        }

        public String GetbackGroundColor()
        {
            if (chartBackgroundColorPickerDropDownList.SelectedItem.Text != null)
                return chartBackgroundColorPickerDropDownList.SelectedItem.Text;
            else
                return "Green"; //if no color is set then default color is set to the chart

        }

        public String GetSeriesColor()
        {
            if (SeriesColorPickerDropDownList.SelectedItem.Text != null)
                return SeriesColorPickerDropDownList.SelectedItem.Text;
            else
                return "Red";
        }

        public int Get3D()
        {
            if (Enable3dCheckBox.Checked == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public String GetChartType()
        {
            return chartTypeDropDownList.SelectedItem.Text;
        }

        public String GetTableNameForChart()
        {
            return TableNameDropDownList.SelectedItem.Text;
        }
        public String GetXlableName()
        {
            if (xAxisLabel_TextBox.Text != null)
                return xAxisLabel_TextBox.Text;
            else
                return "XAXIS TITLE :" +xAxisDropDownList.SelectedItem.Text + ".";
        }
        public String GetylableName()
        {
            if(yaxisLabel_TextBox.Text != null)
            return yaxisLabel_TextBox.Text;
            else
                return "YAXIS TITLE :" + yAxisDropDownList.SelectedItem.Text + ".";
        }
        public String GetFunctionName()
        {
            return FormulaDropDownList.SelectedItem.Text;
        }
        #endregion
        #region saveButton Event
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            if (TableNameDropDownList.SelectedItem.Text != null && xAxisDropDownList.SelectedItem.Text != null
                   && yAxisDropDownList.SelectedItem.Text != null)
            {
                #region useless
                //string save_ExecuteNonQuery = "INSERT INTO [dvs_Chart ] ([UserId] , [TableId], [ChartTitle] , [ChartTypeName] , [xColumnName] , [xLabelName], [yColumnName], [yLabelName],[BackColor],[SeriesColor],[3D]) VALUES ('" + Session["LoggedInUserId"] + "' , '" + TableNameDropDownList.SelectedItem.Text + "' ,'" + GetChartTitle()
                //    + "'  , '" + GetChartType() + "', ' " + GetxaxisTitle() + "' , '" + GetXlableName()
                //    + "', '" + GetYaxisTitle() + "', '" + GetylableName()
                //    + "', '" + GetbackGroundColor() + "' ,'" + GetSeriesColor() + "' ,'" + Get3D()
                //    + "' ); ";
#endregion
                string save_ExecuteNonQuery = "INSERT INTO [dvs_Chart ] ([UserId] , [TableId], [ChartTitle] , [ChartTypeName] , [xColumnName] , [xLabelName], [yColumnName], [yLabelName],[BackColor],[SeriesColor],[3D], [Formula], [Sort]) VALUES ('" + Session["LoggedInUserId"] + "' , '" + TableNameDropDownList.SelectedItem.Text + "' ,'" + GetChartTitle()
                    + "'  , '" + GetChartType() + "', ' " + GetxaxisTitle() + "' , '" + GetXlableName()
                    + "', '" + GetYaxisTitle() + "', '" + GetylableName()
                    + "', '" + GetbackGroundColor() + "' ,'" + GetSeriesColor() + "' ,'" + Get3D()
                    + "' , '"+ GetFunctionName() +"' , '"+SortDropDownList.SelectedItem.Text +"' ); ";

                DatabaseClass.ExecuteNonQuery(save_ExecuteNonQuery);

                testChartControl();
            }
        }
        #endregion
        #region change axidtitles
        protected void ChangeXAxisTitle(object sender, EventArgs e)
        {
            CreateChartChartUserControl.XAXISTITLE = xAxisLabel_TextBox.Text;
            testChartControl();
        }

        protected void ChangeYaxisTitle(object sender, EventArgs e)
        {
            CreateChartChartUserControl.YAXISTITLE = yaxisLabel_TextBox.Text;
            testChartControl();
        }
        #endregion
        #region enable edit or not
        protected void EnableEditChartItems(object sender, EventArgs e)
        {
            if (EdititemCheckBox.Checked)
            {
                ChartColorPanel.Visible = true;
            }
            else
            {
                ChartColorPanel.Visible = false;
            }
            testChartControl();
        }
        #endregion
        #region enable formula and sorting related functions
        /// <summary>
        /// enable formula checkchanged method.
        /// </summary>
        
        protected void DisplayFormulaDropDownList(object sender, EventArgs e)
        {
            if (EnableFormulaCheckBox.Checked)
            {
                FormulaLabel.Visible = true;
                FormulaDropDownList.Visible = true;                
                testChartControl();
            }
            else 
            {
                FormulaLabel.Visible = false;
                FormulaDropDownList.Visible = false;
                testChartControl();
            }

        }

        protected void EnableSort(object sender, EventArgs e)
        {
            if (SortCheckBox.Checked)
            {
                SortingLabel.Visible = true;
                SortDropDownList.Visible = true;
                
            }
            else 
            {
                SortingLabel.Visible = false;
                SortDropDownList.Visible = false;
            }
            testChartControl();
            
        }

        protected void FormulaChange(object sender, EventArgs e)
        {
            CreateChartChartUserControl.FORMULA = FormulaDropDownList.SelectedItem.Text;            
            testChartControl();
        }


        #endregion
         
    }

}
#endregion

