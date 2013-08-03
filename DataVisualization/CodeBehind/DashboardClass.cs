using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataVisualization.CodeBehind.Database;
using System.Data;
namespace DataVisualization.CodeBehind
{

    public class DashboardClass
    {
        public int dashboardId { set; get; }
        public String userId { set; get; }
        public String title { set; get; }
        public String templateFileName { set; get; }
        public DataTable dataTable { set; get; }

        public DashboardClass() { }
        public int insert()
        {
            String insertQuery = "INSERT INTO dvs_Dashboard(UserId, Title, TemplateFileName) OUTPUT INSERTED.dashboardId VALUES('" + userId + "', '" + title + "', '" + templateFileName + "')";
            dashboardId = DatabaseClass.ExecuteNonQueryAndGetInt(insertQuery);
            return dashboardId;
        }


        public void update()
        {
            String query = "UPDATE dvs_Dashboard SET Title='"+title+"' WHERE DashboardId="+dashboardId;
            DatabaseClass.ExecuteNonQuery(query);
        }

        public void retrieve()
        {
            String query = "SELECT * FROM dvs_Dashboard WHERE DashboardId=" + dashboardId;
            dataTable = DatabaseClass.ExecuteQuery(query);
            userId = dataTable.Rows[0][1].ToString();
            title = dataTable.Rows[0][2].ToString();
            templateFileName = dataTable.Rows[0][3].ToString();
        }
    }
}