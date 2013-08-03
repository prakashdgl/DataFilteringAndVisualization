using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace DataVisualization.CodeBehind.Database
{
    public class DatabaseClass
    {
        private static SqlConnection connection;
        private static SqlDataAdapter datatableadapter;
        private static DataSet dsData{get;set;}
        private static DataTable resultTable;
        private static SqlDataReader reader;
        private static String connectionstring;
        //Creates a connection
        public static void createconnection()
        {
            //String connectionstring = "Data Source=.\\SQLEXPRESS;AttachDbFilename=|DataDirectory|\\ASPNETDB.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True";
            try
            {
                connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                connection = new SqlConnection(connectionstring);
                connection.Open();
            }
            catch(HttpException e) 
            {
                int errorcode =  e.ErrorCode;
            }
        }

         
        //Closes the connection
        public static void closeConnection()
        {
            connection.Close();
        }

        public static SqlDataReader ExequteReaderQuery(String s)
        {
            //String connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            SqlCommand cmd = new SqlCommand(s, connection);
            reader = cmd.ExecuteReader();
            return reader;
        
        }
        public static void closeReaderConnection()
        {
            reader.Close();
        }
             

        //It executes SELECT statements and returns the result in the form of DataTable
        public static DataTable ExecuteQuery(String s)
        {
            //Execute a query and return result in the form of DataTable
            DatabaseClass.createconnection();
            SqlDataAdapter adp = new SqlDataAdapter(s, DatabaseClass.connection);
            DataSet ds = new DataSet();
            adp.Fill(ds, "resultTable");
            resultTable = ds.Tables["resultTable"];
            DatabaseClass.closeConnection();
            return resultTable;
        }

        //It executes non queries like: INSERT AND UPDATE statements
        public static void ExecuteNonQuery(String query)
        {
            DatabaseClass.createconnection();
            SqlDataAdapter adp = new SqlDataAdapter(query, DatabaseClass.connection);
            SqlCommand dataCommand = DatabaseClass.connection.CreateCommand();
            dataCommand.CommandText = query;
            dataCommand.ExecuteNonQuery();
            DatabaseClass.closeConnection();
        }

        //Inserts a row and returns the output value (i.e. primary key of the newly inserted row)
        public static int ExecuteNonQueryAndGetInt(String query)
        {
            DatabaseClass.createconnection();
            SqlDataAdapter adp = new SqlDataAdapter(query, DatabaseClass.connection);
            SqlCommand dataCommand = DatabaseClass.connection.CreateCommand();
            dataCommand.CommandText = query;
            int id = (int)(dataCommand.ExecuteScalar());
            DatabaseClass.closeConnection();
            return id;
        }


        //It returns all the data inside the given table
        public static DataSet getTableDatafromTable(String tableName)
        {
            DatabaseClass.createconnection();
            String sqlquerydata = "SELECT * FROM ["+tableName+" ];";
            datatableadapter = new SqlDataAdapter(sqlquerydata, DatabaseClass.connection);
            DatabaseClass.closeConnection();
            dsData = new DataSet();
            datatableadapter.Fill(dsData, "datatableall");
            return dsData;
        }


    //Setters and getters
        public static void setDsData(DataSet dsData)
        {
            DatabaseClass.dsData = dsData;
        }

        public static DataSet getDsData()
        {
            return DatabaseClass.dsData;
        }

        public static SqlConnection getConnection()
        {
            return DatabaseClass.connection;
                
        }
    }
}