using System;
using System.Data;
using System.Data.SqlClient;

namespace VetClinicApp
{
    public class ClinicRepository
    {
        public DataTable GetAll()
        {
            string sql = "SELECT * FROM CLINIC ORDER BY CLINICNAME";
            return DBHandler.ExecuteQuery(sql);
        }

        public int Insert(Clinic c)
        {
            string sql = @"INSERT INTO CLINIC (CLINICNAME, LOCATION, HASEMERGENCYFACILITY, CLINICPHONE)
                           VALUES (@cn, @loc, @emg, @ph)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@cn", (object)c.ClinicName ?? DBNull.Value),
                new SqlParameter("@loc", (object)c.Location ?? DBNull.Value),
                new SqlParameter("@emg", c.HasEmergencyFacility),
                new SqlParameter("@ph", (object)c.Phone ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }

        public int Update(Clinic c)
        {
            string sql = @"UPDATE CLINIC SET CLINICNAME = @cn, LOCATION = @loc,
                           HASEMERGENCYFACILITY = @emg, CLINICPHONE = @ph
                           WHERE CLINICID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@cn", (object)c.ClinicName ?? DBNull.Value),
                new SqlParameter("@loc", (object)c.Location ?? DBNull.Value),
                new SqlParameter("@emg", c.HasEmergencyFacility),
                new SqlParameter("@ph", (object)c.Phone ?? DBNull.Value),
                new SqlParameter("@id", c.ClinicID)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }

        public int Delete(int id)
        {
            string sql = "DELETE FROM CLINIC WHERE CLINICID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", id)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }
    }
}
