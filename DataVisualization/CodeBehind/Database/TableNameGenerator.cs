using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace DataVisualization.CodeBehind.Database
{
    public class TableNameGenerator
    {
        private static DataTable tablelist;
        public static DataTable getTableName()
        {
            DatabaseClass.createconnection();
            tablelist = DatabaseClass.getConnection().GetSchema("Tables");
            DatabaseClass.closeConnection();
            return tablelist;
        }
    }
}