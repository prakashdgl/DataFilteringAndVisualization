using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using manualcodes;

namespace manualCodes
{
    public class FileTable :FileTabular
    {
        //convention
        public static int number = 1;
        public static int text = 2;
        public static int date = 3;
        public static int gps = 4;
        public static int boolean = 0;

        private CSVFileValidator abc=new CSVFileValidator();
        private List<string[]> innerdata;
        private int[] dataType;

        //the CONSTRUCTOR which analyses the file and throws an exception
        //if the file is not valid
        public FileTable(string filePath)
        {
           /*Determines the actual list of data if the file it valid format, or
            * throws an Table.Exception exception which needs to be handled by the calling module
            * */

            if (abc.isValid(filePath))//fileParserCsv();
            {
                innerdata = abc.getData();
            }
            else
            {
                Error e = new Error();
                e.setMessage(abc.getErrorMessage());
                throw e;
            }
        }
        
        //returns the count of the inner data
        public int getDataSize()
        {
            
            return this.innerdata.Count();
        }

        //returns the number of fields in the table
        public int getColumnCount()
        {
            return this.innerdata.ElementAt(0).Count();
        }

        //returns the name of the table column
        public string getFieldName(int columnIndex)
        {
            return this.innerdata.ElementAt(0).ElementAt(columnIndex);
        }

        //return the data in the row and column intersection
        public string getData(int row, int column)
        {
            return this.innerdata.ElementAt(row).ElementAt(column);
        }

        //return the datatype of the column in the table
        public int getType(int column)
        {//just used for assigning default value, Not used anymore
            return text;
        }

        //returns another instance of Table object with
        //required columns only
        public FileTable getSubData(int[] columns)
        {
            throw new NotImplementedException();
        }
        private void checkDataType()
        {//used to assign default value
            int count=innerdata.ElementAt(0).Count();
            dataType=new int[count];
            for (int i = 0; i < count; i++)
            {
                dataType[i] = 2;
            }
        }

        /*store all the records in the requried table names
         * the userInfoTable, created a table for the file and dumps all data
         * should store datatypes in a table;
         * */
        public void storeInTable(string userId, string tableName)
        {//This method is used
            manualCodes.DataTypeFinder df = new manualCodes.DataTypeFinder();
            df.setData(this.innerdata);
            df.operate();
            this.dataType = df.getDataType2();

            string actualTableName = tableName;
            manualCodes.DatabaseAccessLayer.insertUserInfoTable(userId,actualTableName);
            manualCodes.DatabaseAccessLayer.createAndFillData(actualTableName,this.innerdata,dataType);
            string[] types2=getDataTypes(this.dataType);//solves the mapping error from team work
            manualCodes.DatabaseAccessLayer.insertTableDataTypes(actualTableName, types2);
            manualCodes.DatabaseAccessLayer.insertUserTableIdTableNameMap(userId,actualTableName,actualTableName);
        }

        //converts int[] of datatypes to string[]
        private string[] getDataTypes(int [] a)
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

        //return the html table of the data and return the string
        //JUST FOR DISPLAY PURPOSE
        public string getHtmlTable(int count)
        {
            string html = "<table>\n";
            int i = count < innerdata.Count() ? count : innerdata.Count();
            if(count==0)//ie whole data
                i=innerdata.Count();
            int col=innerdata.ElementAt(0).Count();

            for (int j = 0; j < i; j++)
            {
                html+="\t<tr>";
                for (int k = 0; k < col; k++)
                {
                    html += "\t\t<td>" + innerdata.ElementAt(j).ElementAt(k) + "</td>\n";
                }
                html+="\t</tr>\n";
            }
            html += "</table>";

            return html;
        }
    }

    public class Error : SystemException
    {//ERROR CLASS FOR HANDELING AND LOGGING EXCEPTION
        private string errorMessage;

        public void setMessage(string message)
        {
            errorMessage = message;
        }
        public string getMessage()
        {
            return errorMessage;
        }
    }
}