using System;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DBConnection
    {
        private string GetconnectionString()
        {
            return "Data Source=DESKTOP-187HKL2;Initial Catalog=VetClinic;Integrated Security=True;";
            
        }
        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(GetconnectionString());
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {
                throw new Exception("Database connection failed: " + ex.Message, ex);
            }
        }
    }
}
