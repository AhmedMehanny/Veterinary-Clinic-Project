using System.Configuration;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public static class DBConnection
    {
        public static string ConnectionString => ConfigurationManager.ConnectionStrings["VetClinic"].ConnectionString; //دخلت عملت refernce system.configration

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}