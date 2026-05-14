using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public static class DBHandler
    { // insert, update, delete
        public static int ExecuteNonQuery(string cmdText, CommandType cmdType, SqlParameter[] parameters = null)
        {
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(cmdText, conn))
            {
                cmd.CommandType = cmdType;
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        public static DataTable ExecuteQuery(string cmdText, CommandType cmdType, SqlParameter[] parameters = null)
        {//Select, return
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(cmdText, conn))
            {
                cmd.CommandType = cmdType;
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                using (var da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static object ExecuteScalar(string cmdText, CommandType cmdType, SqlParameter[] parameters = null)
        {//count, scope ownerIdentity
            using (var conn = DBConnection.GetConnection())
            using (var cmd = new SqlCommand(cmdText, conn))
            {
                cmd.CommandType = cmdType;
                if (parameters != null) cmd.Parameters.AddRange(parameters);
                conn.Open();
                return cmd.ExecuteScalar();
            }
        }
    }
}