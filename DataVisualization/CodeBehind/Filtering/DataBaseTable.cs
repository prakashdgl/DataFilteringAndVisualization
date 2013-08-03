using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace manualCodes
{
    public class DataBaseTable
    {
        /// <summary>
        /// This class is not used Much, Because table specific information requires database access
        /// and so far possible, all database related code are placed in one file (DatabaseAccessLayer.cs)
        /// All functions done by this class can be done there
        /// </summary>
        private string tableName;
        private string[] columnNames;
        private List<string[]> conditions;
        private int[] datatype;

        //setters and getters for the parameters
        public void setTableName(string tableName)
        {
            this.tableName = tableName;
            int length=this.tableName.Length;
            columnNames = DatabaseAccessLayer.getNames(tableName);
            datatype = new int[length];//should be assigned according to its datatype
            for (int i = 0; i < length; i++)
            {
                datatype[i] = 2;
            }
        }
        public string getTableName()
        {
            return this.tableName;
        }
        public string[] getColumnNames()
        {
            return this.columnNames;
        }

        //for working with the list of conditions in a new way to avoid sql injection
        public void setConditions2(List<string[]> conditions)
        {
            this.conditions = conditions;
        }
    }
}