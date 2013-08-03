using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace manualCodes
{
    public class DataBaseClass
    {

        public static void createConnection()
        {
            string Connection_String = "Data Source=.\\sqlexpress;Initial Catalog=rajan;Integrated Security=True;Pooling=False";
            SqlConnection con = new SqlConnection(Connection_String);
            SqlCommand cmd = new SqlCommand();
            con.Open();
            cmd.CommandText = "SELECT * from [Table1];";
            cmd.Connection = con;
            SqlDataReader DR1 = cmd.ExecuteReader();
            if (DR1.Read())
            {
                Console.WriteLine(DR1.GetValue(0).ToString());
            }
            con.Close();

        }

        public static void main(string[] args)
        {
            createConnection();
        }
    }
}