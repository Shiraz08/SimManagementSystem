
using System.Data.SqlClient;
using System.Web.Configuration;

namespace SimManagementSystem.DBContext 
{
    public static class DBConnect
    {
        static string _DataSource = WebConfigurationManager.AppSettings["DataSource"];
        static string _InitialCatalog = WebConfigurationManager.AppSettings["InitialCatalog"];
        static string _UserID = WebConfigurationManager.AppSettings["UserID"];
        static string _Password = WebConfigurationManager.AppSettings["Password"];

        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder()
            {
                DataSource = _DataSource,
                InitialCatalog = _InitialCatalog,
                UserID = _UserID,
                Password = _Password,
            };

            return sqlString.ToString();
        }
    }
}
