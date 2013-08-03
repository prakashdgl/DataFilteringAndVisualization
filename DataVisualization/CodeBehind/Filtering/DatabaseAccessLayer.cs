using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace manualCodes
{
    public class DatabaseAccessLayer
    {
        static String Connection_String = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
               
       // static string Connection_String = "Data Source=.\\SQLEXPRESS;AttachDbFilename=C:\\Users\\power\\Desktop\\Edited\\DataVisualization\\DataVisualization\\App_Data\\ASPNETDB.MDF;Integrated Security=True;User Instance=True";
       // static string Connection_String = "Data Source=.\\sqlexpress;Initial Catalog=DVS;Integrated Security=True";
        static SqlConnection con = new SqlConnection(Connection_String);
        static SqlCommand cmd = new SqlCommand();

        //most general functions
        public static void executeNonQuery(string abc)
        {
            try
            {
                cmd.CommandText = abc;
                cmd.Prepare();
                cmd.Connection = con;
                con.Close();
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception)
            {
                con.Close();
                //Console.WriteLine(e.Message);
            }
        }
        public static SqlDataReader executeQuery(string abc)
        {
            SqlDataReader s = null;
            try
            {
                cmd.CommandText = abc;
                cmd.Prepare();
                cmd.Connection = con;
                con.Close();
                con.Open();
                s = cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                con.Close();
            }
            return s;

        }

        //Other table specific functions start

        //table insertion section
        public static void saveException(string userId, string message)
        {
            string sql = "INSERT into DVS_ExceptionTable (userId,message,timeStamp) values ('" + userId + "','" + message + "',DEFAULT)";
            executeNonQuery(sql);
        }
        public static void insertFileInfo(string userId, string filePath, string fileName)
        {//Insert file path and file name record in the fileInfo table
            string sql = "INSERT into DVS_FileInfo (userId,filePath,fileName) values ('" + userId + "','" + filePath + "','" + fileName + "')";
            executeNonQuery(sql);
        }
        public static void insertUserInfoTable(string userId, string tableName)
        {
            string sql = "INSERT into DVS_userTableInfo (userId,tableName) values ('" + userId + "','" + tableName + "')";
            executeNonQuery(sql);
        }
        public static void insertTableDataTypes(string tableName, string[] datatypes)
        {
            string containerTable = "DVS_DataType";
            string[] columnNames = getNames(tableName);
            string sql;
            for (int i = 0; i < datatypes.Length; i++)
            {
                sql = string.Format
                    ("INSERT INTO [{0}] (tableName, fieldName, dataType) VALUES ('{1}','{2}','{3}')",
                    containerTable, tableName, columnNames[i], datatypes[i]);
                executeNonQuery(sql);
            }
        }
        public static void updateTableDataTypes(string tableName, string[] columnNames, string[] datatypes)
        {
            string containerTable = "DVS_DataType";
            string sql = "";
            for (int i = 0; i < datatypes.Length; i++)
            {
                sql = string.Format
                    ("UPDATE [{0}] set dataType='{1}' where (tableName = '{2}' AND fieldName='{3}') ",
                    containerTable, datatypes[i], tableName, columnNames[i]);

                executeNonQuery(sql);
            }
        }

        public static void insertUserTableIdTableNameMap(string userId, string tableId, string tableTitle)
        {
            string sql = "INSERT into dvs_User_Table (userId,TableId,TableTitle) values ('" + userId +"','"+tableId+ "','" + tableTitle + "')";
            executeNonQuery(sql);
        }
        public static void updateUserTableIdTableNameMap(string userId, string tableId, string tableTitle)
        {
            string sql = "UPDATE dvs_User_Table set TableTitle='"+tableTitle+"' where (userId like '"+userId+"' and tableId like '"+tableId+"')";
            executeNonQuery(sql);
        }

        //returns the count of columns in the following table Name
        private static int getColumnCount(string tableName)
        {
            string sql1 = string.Format("SELECT        COUNT(*) AS Expr1 " +
           "FROM            (SELECT        COLUMN_NAME" +
                         " FROM            INFORMATION_SCHEMA.COLUMNS " +
                         " WHERE        (TABLE_NAME = '{0}')) AS derivedtbl_1", tableName);
            SqlDataReader s = executeQuery(sql1);
            int columnCount = 0;
            if (s != null)
            {
                if (s.Read())
                {

                    columnCount = Int32.Parse(s.GetValue(0).ToString());
                    con.Close();//convert.toInt32
                }
            }
            con.Close();
            return columnCount;

        }
        //return array of column/field Names of a table in the database
        public static string[] getNames(string tableName)
        {
            int columnCount = getColumnCount(tableName);
            string sql1 = string.Format("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='{0}'", tableName);
            SqlDataReader s = executeQuery(sql1);
            string[] names = new string[columnCount];
            int i = 0;
            if (s != null)//store tables field names in string array
            {
                while (s.Read())
                {
                    names[i++] = s.GetValue(0).ToString();
                }
            }
            con.Close();
            return names;
        }
        //returns the dataTypes array of the tableName using the index/information in database
        public static string[] getDataTypes(string tableName)
        {
            string[] names = getNames(tableName);
            string[] types = new string[names.Length];
            string sql = string.Format("SELECT dataType FROM [DVS_DataType] WHERE (tableName = '{0}')", tableName);//And fieldName={1};//needs to query many times
            SqlDataReader s = executeQuery(sql);
            int i = 0;
            if (s != null)
            {
                while (s.Read())
                {
                    types[i++] = s.GetValue(0).ToString();
                }
            }
            con.Close();
            return types;
        }
        public static bool areDataTypesMatched(string tableName, string[] queryDataTypes)
        {
            string[] actualTypes = getDataTypes(tableName);
            for (int i = 0; i < actualTypes.Length; i++)
            {
                if (actualTypes[i] != queryDataTypes[i])
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// CREATEA A TABLE with name = id_unique-file-name and FILLS THE TABLE with datas provided in the 
        /// corresponding csv data file
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="data"></param>
        /// <param name="dataType"></param>
        public static void createAndFillData(string tableName, List<string[]> data, int[] dataType)
        {
            int length = dataType.Count();//number of column in the table
            string[] header = data.ElementAt(0);//first row is supposed to contain the Header Names
            string names = "";
            string[] datanames = { "nvarchar(50)", " int ", "nvarchar(50)", "date", "nvarchar(50)" };//lookuptable
            for (int i = 0; i < length; i++)
            {
                names += header[i] + "  " + datanames[dataType[i]] + "\n";
                if (i < length - 1)
                {
                    names += ",";
                }
            }
            string sql = string.Format("CREATE table [{0}] ({1});", tableName, names);
            executeNonQuery(sql);
            int rowLength = data.Count();//number of lines/rows in the table
            string values = "";
            string[] fieldDatas;
            for (int i = 1; i < rowLength; i++)
            {
                fieldDatas = data.ElementAt(i);
                values = "";
                for (int j = 0; j < length; j++)
                {
                    values += "'" + fieldDatas.ElementAt(j).Replace("'", " ") + "'";
                    if (j < length - 1)
                    {
                        values += " ,";
                    }
                }
                sql = string.Format("Insert into [{0}]  values ({1});", tableName, values);
                executeNonQuery(sql);
            }//for
        }

        //return the filtering form/Menu HTML for the tableName table of Data
        public static string getFilteringForm(string tableName)
        {
            int columnCount = getColumnCount(tableName);
            string[] names = getNames(tableName);

            string html = "<div id=\"filtering_form\" ><form action=\"Default.aspx\" method=\"post\">";
            html += "<input value=\"cmdSubmit\" name=\"__EVENTTARGET\" type=\"hidden\" />";
            html += string.Format("<input type=\"hidden\" name=\"tableName\" value=\"{0}\"/>\n", tableName);
            html += "<h2><u>Filtering Menu</u></h2>";
            html += "<div id=\"selectbox\">" + getSelectCheckBox(names) + "</div>";///for selection
            string[] dataTypes = getDataTypes(tableName);

            for (int j = 0; j < columnCount; j++)
            {
                //html += getFilterMenu(names[j],dataTypes[j]);
                html += string.Format("<div class=\"column\" id=\"{0}\" >{1} </div>", names[j], getFilterMenu(names[j], dataTypes[j]));
            }
            html += "<div class=\"submit\"><table><tr><td>\t<input type=\"submit\" name=\"filter\" value=\"filter\" style=\"width:250px\" /></td></tr><table/></div><hr/>";
            //html += createAdvancedFilteringMenu(tableName);
            html += "<h3>Advanced Query Option</h3>TableName:" + tableName + "<br/>";
            html += "<input type=\"text\" size=\"100\" name=\"advancedQueryText\" value=\"\" />";
            html += "<br/><input type=\"submit\" name=\"advancedQuery\" value=\"AdvancedQuery\" /><hr/>";
            html += "</form></div>";
            return html;
        }
        public static string createAdvancedFilteringMenu(string tableName)
        {
            int columnCount = getColumnCount(tableName);
            string[] names = getNames(tableName);

            string adv = "<h3>Advanced Filtering Area </h3>";
            adv += "<div id=\"advancedFilter\">";
            adv += "<h4>Select</h4>";
            adv += getSelectCheckBox(names);//returns the check boxes for the selection of fields required
            adv += "<h4>Where</h4>";
            adv += "<input type=\"text\" name=\"advancedWhere\" />";
            adv += "<input type=\"button\" name=\"advancedFilter\" value=\"CommitAdvancedSearch\"";
            adv += "</div>";

            return adv;
        }
        public static string getSelectCheckBox(string[] names)
        {
            int len = names.Length;
            string check = "";
            for (int i = 0; i < len; i++)
            {
                check += string.Format("  {0}: <input type=\"checkbox\" name=\"checkbox_{0}\" value=\"{1}\"> <br/>", names[i], names[i], names[i]);
            }
            return check;
        }
        public static string getFilterMenu(string column, string dataType)
        {//for ajax also
            //for particular field
            string menu = string.Format("\n<table>");
            string type = dataType;//getType(columnName);
            if (type == "text")//text
            {
                menu += string.Format("<tr><td>{0}<hr/></td></tr><tr><td>DataType :</td><td>{1}</td></tr>\n", column, getSelection(type, column));
                menu += string.Format("<tr><td>Equals:</td><td> <input type=\"text\" name=\"{0}\"/></td></tr> <br/>", column + "_equals");
                menu += string.Format("<tr><td>Contains:</td><td> <input type=\"text\" name=\"{0}\"/></td></tr><br/>", column + "_contains");
            }
            else if (type == "number" || type == "date")//number or date
            {
                menu += string.Format("<tr><td>{0}<hr/></td></tr><tr><td>DataType :</td><td>{1}</td></tr>\n", column, getSelection(type, column));
                menu += string.Format("<tr><td>Equals:</td><td> <input type=\"text\" name=\"{0}\"/></td></tr> <br/>", column + "_equals");
                menu += string.Format("<tr><td>Greater_Then:</td><td> <input type=\"text\" name=\"{0}\"/></td></tr><br/>", column + "_greaterThen");
                menu += string.Format("<tr><td>Less_Then:</td><td> <input type=\"text\" name=\"{0}\"/></td></tr><br/>", column + "_lessThen");
            }
            else
            {
                //menu += string.Format("<tr><td><h3><b>{0}</b></h3></td></tr><tr><td>DataType :</td><td>{1}</td></tr>\n", column, getSelection(type, column));
                //menu += string.Format("<tr><td>Equals:</td><td> <input type=\"text\" name=\"{0}\"/></td></tr> <br/>", column + "_equals");
                //menu += string.Format("<tr><td>Contains:</td><td> <input type=\"text\" name=\"{0}\"/></td></tr><br/>", column + "_contains");
                menu += type;
            }
            menu += "</table>";
            return menu;
        }
        //returns the html for the dropDown for DataType
        //called from getFilterMenu
        private static string getSelection(string selectedDataType, string columnName)
        {   //this function is only used by getFilteringMenu2 and onwards
            string select = string.Format("<select name=\"{0}\" class=\"dataType\" onchange=\"typeChanged(this)\" >", columnName + "_dataType");

            int index = 2;
            if (selectedDataType == "number")
            {
                index = 1;
            }
            else if (selectedDataType == "date")
            {
                index = 3;
            }
            string[] default_selected = new string[4];

            default_selected[index] = "selected=\"true\"";
            string text = string.Format("<option value=\"text\" {0} > Text </option>", default_selected[2]);
            string num = string.Format("<option value=\"number\" {0} > Number </option>", default_selected[1]);
            string date = string.Format("<option value=\"date\" {0} > Date </option>", default_selected[3]);

            select += text + num + date;
            select += "</select>";

            return select;
        }

        public static string getTableDataWhole(string tableName)
        {
            string sql = string.Format("select * from [{0}]", tableName);
            DataBaseTable db = new DataBaseTable();
            db.setTableName(tableName);
            string[] names = db.getColumnNames();
            int length = names.Length;
            SqlDataReader s = executeQuery(sql);
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
            return table;
        }
        public static string getFilteredTableData(List<string> selects, List<string[]> conditions, string tableName)
        {
            DataBaseTable db = new DataBaseTable();
            db.setTableName(tableName);
            string[] names = selects.ToArray();
            int length = names.Length;//selected names length
            string[] types = new string[length];//selected names datatypes

            string[] originalTableName = getNames(tableName);
            string[] originalTableTypes = getDataTypes(tableName);
            string temp = "";
            for (int i = 0; i < length; i++)
            {
                temp = names[i];
                for (int j = 0; j < originalTableName.Count(); j++)
                {
                    if (temp == originalTableName.ElementAt(j))
                    {
                        types[i] = originalTableTypes[j];
                        break;
                    }
                }
            }//matching the datatypes

            string select = " ";
            for (int i = 0; i < selects.Count; i++)
            {
                select += selects.ElementAt(i);
                if (i < selects.Count - 1)
                {
                    select += ", ";
                }
                else
                {
                    select += " ";
                }
            }
            string query = buildSql(select, conditions, tableName);

            SqlCommand Sqlcommand = new SqlCommand(query, con);
            length = conditions.Count();
            for (int i = 0; i < length; i++)
            {
                if (originalTableTypes[i] == "text")
                {
                    string equals = conditions.ElementAt(i)[0];
                    string contains = conditions.ElementAt(i)[1];
                    if (equals != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_equals", originalTableName[i]), string.Format("{0}", equals));
                    }
                    else if (contains != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_contains", originalTableName[i]), string.Format("%{0}%", contains));
                    }
                }//if text
                else if (originalTableTypes[i] == "number")
                {
                    string equals = conditions.ElementAt(i)[0];
                    string lessThen = conditions.ElementAt(i)[1];
                    string greaterThen = conditions.ElementAt(i)[2];
                    if (equals != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_equals", originalTableName[i]), equals);
                    }
                    if (lessThen != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_lessThen", originalTableName[i]), lessThen);
                    }
                    if (greaterThen != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_greaterThen", originalTableName[i]), greaterThen);
                    }
                }//number
                else if (originalTableTypes[i] == "date")
                {
                    string equals = conditions.ElementAt(i)[0];
                    string lessThen = conditions.ElementAt(i)[1];
                    string greaterThen = conditions.ElementAt(i)[2];
                    if (equals != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_equals", originalTableName[i]), string.Format("{0}", equals));
                    }
                    if (lessThen != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_lessThen", originalTableName[i]), string.Format("{0}", lessThen));
                    }
                    if (greaterThen != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_greaterThen", originalTableName[i]), string.Format("{0}", greaterThen));
                    }
                }//date

            }//for

            SqlDataReader s = null;
            try
            {
                Sqlcommand.Connection = con;
                con.Close();
                con.Open();
                s = Sqlcommand.ExecuteReader();
            }
            catch (Exception e)
            {
                con.Close();
            }
            length = names.Length;
            string table = "<div class=\"tableData\" > <table border=\"1\">";
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
            table += "</table></div>";
            if (s != null)
            {
                s.Close();
            }
            return table + query;
        }
        public static string getFilteredTableData(List<string[]> conditions, string tableName)
        {
            DataBaseTable db = new DataBaseTable();
            db.setTableName(tableName);
            string[] names = db.getColumnNames();
            string[] types = getDataTypes(tableName);
            int length = names.Length;

            string query = buildSql(conditions, tableName);

            SqlCommand Sqlcommand = new SqlCommand(query, con);

            for (int i = 0; i < length; i++)
            {
                if (types[i] == "text")
                {
                    string equals = conditions.ElementAt(i)[0];
                    string contains = conditions.ElementAt(i)[1];
                    if (equals != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_equals", names[i]), string.Format("{0}", equals));
                    }
                    else if (contains != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_contains", names[i]), string.Format("%{0}%", contains));
                    }
                }//if text
                else if (types[i] == "number")
                {
                    string equals = conditions.ElementAt(i)[0];
                    string lessThen = conditions.ElementAt(i)[1];
                    string greaterThen = conditions.ElementAt(i)[2];
                    if (equals != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_equals", names[i]), equals);
                    }
                    if (lessThen != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_lessThen", names[i]), lessThen);
                    }
                    if (greaterThen != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_greaterThen", names[i]), greaterThen);
                    }
                }//number
                else if (types[i] == "date")
                {
                    string equals = conditions.ElementAt(i)[0];
                    string lessThen = conditions.ElementAt(i)[1];
                    string greaterThen = conditions.ElementAt(i)[2];
                    if (equals != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_equals", names[i]), string.Format("{0}", equals));
                    }
                    if (lessThen != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_lessThen", names[i]), string.Format("{0}", lessThen));
                    }
                    if (greaterThen != "")
                    {
                        Sqlcommand.Parameters.AddWithValue(string.Format("@{0}_greaterThen", names[i]), string.Format("{0}", greaterThen));
                    }
                }//date

            }//for

            SqlDataReader s = null;
            try
            {
                Sqlcommand.Connection = con;
                con.Close();
                con.Open();
                s = Sqlcommand.ExecuteReader();
            }
            catch (Exception e)
            {
                con.Close();
            }

            string table = "<div class=\"tableData\" > <table border=\"1\">";
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
            table += "</table></div>";
            if (s != null)
            {
                s.Close();
            }
            return table + query;
        }
        //returns the general sql with @signs, to be used by getFilteredTableData2
        private static string buildSql(string selects, List<string[]> conditions, string tableName)
        {
            DataBaseTable db = new DataBaseTable();
            db.setTableName(tableName);
            string[] names = db.getColumnNames();
            string[] types = getDataTypes(tableName);
            int length = conditions.Count;//names.Length;
            string sql = string.Format("SELECT {0} FROM [{1}] ", selects, tableName);
            string text = "";
            string and = " ";
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                if (types[i] == "text")
                {
                    string equals = conditions.ElementAt(i)[0];
                    string contains = conditions.ElementAt(i)[1];
                    if (equals != "")
                    {
                        text += string.Format("{0} {1} like @{2}_equals ", and, names[i], names[i]);
                        count++;
                        and = " AND ";
                    }
                    else if (contains != "")
                    {
                        if (count < 1)
                        {
                            text += string.Format("{0} {1} like @{2}_contains ", and, names[i], names[i]);
                            and = " AND ";
                            count++;
                        }
                        else
                        {
                            text += string.Format("{0} {1} like @{2}_contains ", and, names[i], names[i]);
                        }
                    }
                }//if text
                else if (types[i] == "number")
                {
                    string equals = conditions.ElementAt(i)[0];
                    string lessThen = conditions.ElementAt(i)[1];
                    string greaterThen = conditions.ElementAt(i)[2];
                    if (equals != "")
                    {
                        text += string.Format(" {0} {1} = @{2}_equals ", and, names[i], names[i]);
                        and = " AND ";
                        count++;
                    }
                    if (lessThen != "")
                    {
                        if (count > 0)
                            text += string.Format("{0}  {1} < @{2}_lessThen ", and, names[i], names[i]);
                        else
                        {
                            text += string.Format(" {0}< @{1}_lessThen ", names[i], names[i]);
                            and = " AND ";
                            count++;
                        }
                    }
                    if (greaterThen != "")
                    {
                        if (count > 0)
                            text += string.Format("{0} {1} > @{2}_greaterThen ", and, names[i], names[i]);
                        else
                        {
                            text += string.Format(" {0} > @{1}_greaterThen ", names[i], names[i]);
                            and = " AND ";
                            count++;
                        }
                    }
                }//number
                else if (types[i] == "date")
                {
                    string equals = conditions.ElementAt(i)[0];
                    string lessThen = conditions.ElementAt(i)[1];
                    string greaterThen = conditions.ElementAt(i)[2];
                    if (equals != "")
                    {
                        text = string.Format(" {0} = @{1}_equals ", names[i], names[i]);
                        and = " AND ";
                        count++;
                    }
                    if (lessThen != "")
                    {
                        if (count > 0)
                            text += string.Format("{0}  {1} < @{2}_lessThen ", and, names[i], names[i]);
                        else
                        {
                            text += string.Format(" {0}< @{1}_lessThen ", names[i], names[i]);
                            and = " AND ";
                            count++;
                        }
                    }
                    if (greaterThen != "")
                    {
                        if (count > 0)
                            text += string.Format("{0} {1} > @{2}_greaterThen ", and, names[i], names[i]);
                        else
                        {
                            text += string.Format(" {0} > @{1}_greaterThen ", names[i], names[i]);
                            and = " AND ";
                            count++;
                        }
                    }
                }//date

            }//for
            if (text.Trim() != "")
            {
                sql += string.Format(" WHERE ({0}) ", text);
            }

            return sql;
        }//buildSql function
        private static string buildSql(List<string[]> conditions, string tableName)
        {
            DataBaseTable db = new DataBaseTable();
            db.setTableName(tableName);
            string[] names = db.getColumnNames();
            string[] types = getDataTypes(tableName);
            int length = conditions.Count;//names.Length;
            string sql = string.Format("SELECT * FROM [{0}] ", tableName);
            string text = "";
            string and = " ";
            int count = 0;
            for (int i = 0; i < length; i++)
            {
                if (types[i] == "text")
                {
                    string equals = conditions.ElementAt(i)[0];
                    string contains = conditions.ElementAt(i)[1];
                    if (equals != "")
                    {
                        text += string.Format("{0} {1} like @{2}_equals ", and, names[i], names[i]);
                        count++;
                        and = " AND ";
                    }
                    else if (contains != "")
                    {
                        if (count < 1)
                        {
                            text += string.Format("{0} {1} like @{2}_contains ", and, names[i], names[i]);
                            and = " AND ";
                            count++;
                        }
                        else
                        {
                            text += string.Format("{0} {1} like @{2}_contains ", and, names[i], names[i]);
                        }
                    }
                }//if text
                else if (types[i] == "number")
                {
                    string equals = conditions.ElementAt(i)[0];
                    string lessThen = conditions.ElementAt(i)[1];
                    string greaterThen = conditions.ElementAt(i)[2];
                    if (equals != "")
                    {
                        text += string.Format(" {0} {1} = @{2}_equals ", and, names[i], names[i]);
                        and = " AND ";
                        count++;
                    }
                    if (lessThen != "")
                    {
                        if (count > 0)
                            text += string.Format("{0}  {1} < @{2}_lessThen ", and, names[i], names[i]);
                        else
                        {
                            text += string.Format(" {0}< @{1}_lessThen ", names[i], names[i]);
                            and = " AND ";
                            count++;
                        }
                    }
                    if (greaterThen != "")
                    {
                        if (count > 0)
                            text += string.Format("{0} {1} > @{2}_greaterThen ", and, names[i], names[i]);
                        else
                        {
                            text += string.Format(" {0} > @{1}_greaterThen ", names[i], names[i]);
                            and = " AND ";
                            count++;
                        }
                    }
                }//number
                else if (types[i] == "date")
                {
                    string equals = conditions.ElementAt(i)[0];
                    string lessThen = conditions.ElementAt(i)[1];
                    string greaterThen = conditions.ElementAt(i)[2];
                    if (equals != "")
                    {
                        text = string.Format(" {0} = @{1}_equals ", names[i], names[i]);
                        and = " AND ";
                        count++;
                    }
                    if (lessThen != "")
                    {
                        if (count > 0)
                            text += string.Format("{0}  {1} < @{2}_lessThen ", and, names[i], names[i]);
                        else
                        {
                            text += string.Format(" {0}< @{1}_lessThen ", names[i], names[i]);
                            and = " AND ";
                            count++;
                        }
                    }
                    if (greaterThen != "")
                    {
                        if (count > 0)
                            text += string.Format("{0} {1} > @{2}_greaterThen ", and, names[i], names[i]);
                        else
                        {
                            text += string.Format(" {0} > @{1}_greaterThen ", names[i], names[i]);
                            and = " AND ";
                            count++;
                        }
                    }
                }//date

            }//for
            if (text.Trim() != "")
            {
                sql += string.Format(" WHERE ({0}) ", text);
            }

            return sql;
        }//buildSql function

        public static string queryToHtmlTable(string query)
        {
            //string html = "";
            SqlCommand Sqlcommand = new SqlCommand(query, con);
            SqlDataReader s = null;
            try
            {
                Sqlcommand.Connection = con;
                con.Close();
                con.Open();
                s = Sqlcommand.ExecuteReader();
            }
            catch (Exception e)
            {
                con.Close();
                return "Something is wrong with your query " + query + "<br/>" + e.Message;
            }
            int length = s.FieldCount;
            string table = "<div class=\"tableData\" > <table border=\"1\">";
            table += "<tr>";
            for (int i = 0; i < length; i++)
            {
                table += string.Format("<th>{0}</th>", s.GetName(i));
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
            table += "</table></div>";
            if (s != null)
            {
                s.Close();
            }
            return table;// +query;
            //return html;
        }
    }//class
}//namespace