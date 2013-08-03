using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using DataVisualization.CodeBehind.Database;

namespace DataVisualization.CodeBehind.Database
{
    public class TableSchemaGenerator
    {
        private static SqlDataAdapter adp;
        private static String sqlquery;

        private static DataSet ds;
        protected static DataTable datatable;
        public static DataTable GetSchemaNameOfTable(String tableName)
        {
            DatabaseClass.createconnection();
            sqlquery = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME =  '" + tableName + "';";
            adp = new SqlDataAdapter(sqlquery, DatabaseClass.getConnection());
            ds = new DataSet();
            adp.Fill(ds, "datatableall");
            datatable = ds.Tables["datatableall"];
            DatabaseClass.closeConnection();
            return datatable;
        }
        public SqlDataAdapter Getadp()
        {
            return adp;
        }
    }
}