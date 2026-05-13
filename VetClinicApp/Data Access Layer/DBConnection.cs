using System.Data.SqlClient;

namespace VetClinicApp
{
    public static class DBConnection
    {
        public static readonly string ConnectionString =
            "Data Source=(local);Initial Catalog=VetClinic;Integrated Security=True;";

        public static SqlConnection GetConnection()
        {
            var conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
