using System;
using System.Collections.Generic;
using System.Linq;
using DataVisualization.CodeBehind;
using System.Data.SqlClient;
using manualCodes;

namespace DataVisualization.FileUpload
{
    public partial class FileUploadCharting : BasePage
    {
        protected string defaultFileName = "z_1_class_name_list__2012-07-10_07-05-37-AM";
        private List<string> select = new List<string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedInUserId"] == null)
                Response.Redirect("~/Account/Login.aspx");
            else if (isSelectionChecked() && Request.Form["filter"] != null)
            {
                filteringFormSubmittedActionWithSelected();
               // Literal1.Text = "The filter button is clicked and checkbox are selected";
            }
            else if (Request.Form["filter"] != null)//the filtering menu button was clicked
            {
                filteringFormSubmittedAction();
                //Literal1.Text = "The filter button is clicked";
            }
            else if (Request.Form["advancedQuery"] != null)
            {
                advancedQuerySubmittedAction();
                string query = Request.Form["advancedQueryText"];
                //Literal2.Text = "Under construction " + query ;
                Literal2.Text = DatabaseAccessLayer.queryToHtmlTable(query);
            }
        }

        private bool isSelectionChecked()
        {
            string tableName = Request.Form["tableName"];
            string[] names = DatabaseAccessLayer.getNames(tableName);
            int len = names.Length;
            bool ans = false;
            Literal2.Text = "";
            for (int i = 0; i < len; i++)
            {
                if (Request.Form["checkbox_" + names[i]] != null)
                {
                    select.Add(Request.Form["checkbox_" + names[i]]);
                    Literal2.Text += "<br/>" + select.ElementAt(select.Count - 1);
                    ans = true;

                }
            }
            return ans;
        }
        private void filteringFormSubmittedActionWithSelected()
        {
            DataBaseTable dbTable = new DataBaseTable();
            string tableName = Request.Form["tableName"];
            dbTable.setTableName(tableName);

            string[] names = dbTable.getColumnNames();
            string[] dataTypes = new string[names.Length];
            List<string[]> conditions = new List<string[]>();//list of array of where conditions for filtering

            for (int i = 0; i < names.Length; i++)//get dataType for each field
            {
                dataTypes[i] = Request.Form[names[i] + "_dataType"];
            }
            //check if the user had manually changed the datatypes of the menu
            bool flag = DatabaseAccessLayer.areDataTypesMatched(tableName, dataTypes);
            if (flag)//ie datatypes are same in database and form submitted
            {
                for (int i = 0; i < names.Length; i++)
                {
                    conditions.Add(createCondition3(names[i], dataTypes[i]));
                }
                Literal1.Text = DatabaseAccessLayer.getFilteringForm(tableName);//display filtering form
                Literal2.Text = DatabaseAccessLayer.getFilteredTableData(select, conditions, tableName);//data
            }
            else
            {
                DatabaseAccessLayer.updateTableDataTypes(tableName, names, dataTypes);
                Literal1.Text = DatabaseAccessLayer.getFilteringForm(tableName);
                Literal2.Text = DatabaseAccessLayer.getTableDataWhole(tableName);
            }
        }
        private void filteringFormSubmittedAction()
        {
            DataBaseTable dbTable = new DataBaseTable();
            string tableName = Request.Form["tableName"];
            dbTable.setTableName(tableName);

            string[] names = dbTable.getColumnNames();
            string[] dataTypes = new string[names.Length];
            List<string[]> conditions = new List<string[]>();//list of array of where conditions for filtering

            for (int i = 0; i < names.Length; i++)//get dataType for each field
            {
                dataTypes[i] = Request.Form[names[i] + "_dataType"];
            }
            //check if the user had manually changed the datatypes of the menu
            bool flag = DatabaseAccessLayer.areDataTypesMatched(tableName, dataTypes);
            if (flag)//ie datatypes are same in database and form submitted
            {
                for (int i = 0; i < names.Length; i++)
                {
                    conditions.Add(createCondition3(names[i], dataTypes[i]));
                }
                Literal1.Text = DatabaseAccessLayer.getFilteringForm(tableName);//display filtering form
                Literal2.Text = DatabaseAccessLayer.getFilteredTableData(conditions, tableName);//data
            }
            else
            {
                DatabaseAccessLayer.updateTableDataTypes(tableName, names, dataTypes);
                Literal1.Text = DatabaseAccessLayer.getFilteringForm(tableName);
                Literal2.Text = DatabaseAccessLayer.getTableDataWhole(tableName);
            }

        }
        private void advancedQuerySubmittedAction()
        {
            string query = Request.Form["advancedQueryText"];
            if (query.Trim() != "")
            {
                //Literal2.Text = query;//working
                SqlDataReader s;
                try
                {
                    s = DatabaseAccessLayer.executeQuery(query);
                }
                catch
                {
                    Literal2.Text = "something is wrong with your query "+query;
                    return;
                }
                int length = s.FieldCount;
                string[] names = new string[length];
                for (int i = 0; i < length; i++)
                {
                    names[i] = s.GetName(i);
                }
                string table = "<table border=\"1\">";
                table += "<tr>";
                for (int i = 0; i < length; i++)
                {
                    table += string.Format("<th>{0}</th>", names[i]);
                }
                table += "</tr>";
                if (s != null)
                {
                    while (s.Read())
                    {
                        table += "<tr>\n";
                        for (int i = 0; i < length; i++)
                        {
                            table += string.Format("<td>{0}</td>", s.GetValue(i).ToString());
                        }
                        table += "<tr/>";
                    }
                }//if
                table += "</table>";

                s.Close();
            }
        }
        /// <summary>
        /// returns either null or the array of string for the conditions
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string[] createCondition3(string columnName, string type)
        {
            string[] text = null;// = "";
            if (type == "text")
            {
                text = new string[2];
                text[0] = Request.Form[columnName + "_equals"].Trim();
                text[1] = Request.Form[columnName + "_contains"].Trim();
            }
            else if (type == "number")
            {
                text = new string[3];
                text[0] = Request.Form[columnName + "_equals"].Trim();
                text[1] = Request.Form[columnName + "_lessThen"].Trim();
                text[2] = Request.Form[columnName + "_greaterThen"].Trim();
            }
            else if (type == "date")
            {
                text = new string[3];
                text[0] = Request.Form[columnName + "_equals"].Trim();
                text[1] = Request.Form[columnName + "_lessThen"].Trim();
                text[2] = Request.Form[columnName + "_greaterThen"].Trim();
            }
            return text;
        }

        protected void CreateChartButton_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/CreateChart/CreateChart.aspx");

        }

        protected void FileUploadButton_Click(object sender, EventArgs e)
        {
            /*
            * This function checks the extension, filesize,
            * then saves the file with a unique name 
            * */
            if (FileUpload1.HasFile)
            {
                string fileName = Server.HtmlEncode(FileUpload1.FileName);
                string extension = System.IO.Path.GetExtension(fileName);
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(fileName);
                Console.WriteLine(extension);
                string userId = Session["LoggedInUserId"].ToString();//getFromSessionVariables

                //extension check
                if (!((extension == ".txt") || (extension == ".csv")))
                {
                    FinalDataArea.Text = "The Uploaded File does not have a txt or csv extension  " + fileNameWithoutExtension;
                }
                //filesize check
                else if (FileUpload1.PostedFile.ContentLength > 1024 * 1024)
                {
                    FinalDataArea.Text += "The Uploaded file is greater than 1MB";
                }
                //other things check
                else
                {

                    try
                    {
                        string uniqueString = string.Format(fileNameWithoutExtension + "__{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
                        string datetimeString = uniqueString + extension;
                        string filename = FileUpload1.FileName;
                        string path = "C:\\z_rajan\\uploads\\"+ datetimeString;//to store file
                        FileUpload1.SaveAs(path);
                        string actualTableName = "z_" + uniqueString;
                        //FileInfo f = new FileInfo(path);

                        /**main function begins here**/
                        //rjaan part
                        FileTabular tab = new FileTable(path);
                        //string actualTableName = "z_" + uniqueString;
                        tab.storeInTable(userId, actualTableName);
                        //tableName for the file created
                        tableName.Value = actualTableName;
                        //insert index of file into Database
                        DatabaseAccessLayer.insertFileInfo(userId, path, fileName);
                        //presenting  filtering form and data to the user
                        Literal1.Text = DatabaseAccessLayer.getFilteringForm(actualTableName);
                        Literal2.Text = DatabaseAccessLayer.getTableDataWhole(actualTableName);//filteredData
                         
                        //rams code
                     /*   manualCodes.Validator v = new Validator(path);
                        if (v.validate() == true)
                        {
                            Console.WriteLine("it is valid");
                            List<string[]> da = v.getData();
                            DataTypeFinder d = new DataTypeFinder();
                            d.setData(da);
                            d.operate();
                            int[] datatype = d.getDataType2();

                           // string actualTableName = tableName;
                            manualCodes.DatabaseAccessLayer.insertUserInfoTable(userId, actualTableName);
                            manualCodes.DatabaseAccessLayer.createAndFillData(actualTableName, da, datatype);
                            string[] types2 = getDataTypes(datatype);//solves the mapping error from team work
                            manualCodes.DatabaseAccessLayer.insertTableDataTypes(actualTableName, types2);
                            manualCodes.DatabaseAccessLayer.insertUserTableIdTableNameMap(userId, actualTableName, actualTableName);
                        }
                        else
                        {
                            //Console.WriteLine("Error type is " + v.getErrorType() + " and error line is " + v.geterrorLineNumber());
                            //Console.WriteLine("it is invalid");
                            this.FinalDataArea.Text = v.getErrorType()+v.geterrorLineNumber() ;
                            DatabaseAccessLayer.saveException(userId, v.getErrorType() +" "+ v.geterrorLineNumber());
                        }*/
                    }
                    catch (Error ex)
                    //catch (Exception ex)
                    {
                       //Literal2.Text= ex.StackTrace;
                        //ie some error was there while parsing the file
                        //displays the error message for the user
                        this.FinalDataArea.Text = ex.getMessage();
                        //inserts the error log in the database
                        DatabaseAccessLayer.saveException(userId, ex.getMessage());
                    }
                }
            }//if
            else    //ie the fileUpload area doesnot have any file attached
            {
                //present the default table Filtering menu and Data
                Literal1.Text = DatabaseAccessLayer.getFilteringForm(defaultFileName);
                Literal2.Text = DatabaseAccessLayer.getTableDataWhole(defaultFileName);
            }

      
        }
        private string[] getDataTypes(int[] a)
        {
            string[] type = new string[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == 0)
                {
                    type[i] = "boolean";
                }
                else if (a[i] == 1)
                {
                    type[i] = "number";
                }
                else if (a[i] == 2)
                {
                    type[i] = "text";
                }
                else if (a[i] == 3)
                {
                    type[i] = "date";
                }
                else
                {
                    type[i] = "text";
                }

            }
            return type;
        }
    }
}