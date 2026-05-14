using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; // أضف هذا

namespace DataAccessLayer
{
    public class ClinicRepository
    {
        public List<Clinic> GetAll()
        {
            var list = new List<Clinic>();
            string query = "SELECT CLINICID, CLINICNAME, LOCATION, HASEMERGENCYFACILITY, CLINICPHONE FROM CLINIC";
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Clinic
                {
                    ClinicId = Convert.ToInt32(row["CLINICID"]),      // ✅ ClinicId وليس ClinicId
                    ClinicName = row["CLINICNAME"].ToString(),
                    Location = row["LOCATION"]?.ToString(),
                    HasEmergencyFacility = row["HASEMERGENCYFACILITY"] != DBNull.Value && (bool)row["HASEMERGENCYFACILITY"],
                    Phone = row["CLINICPHONE"]?.ToString()
                });
            }
            return list;
        }

        // دالة إضافية: الحصول على عيادة بواسطة المعرف
        public Clinic GetById(int clinicId)
        {
            string query = "SELECT CLINICID, CLINICNAME, LOCATION, HASEMERGENCYFACILITY, CLINICPHONE FROM CLINIC WHERE CLINICID = @Id";
            var dt = DBHandler.ExecuteQuery(query, CommandType.Text, new[] { new SqlParameter("@Id", clinicId) });
            if (dt.Rows.Count == 0) return null;
            var row = dt.Rows[0];
            return new Clinic
            {
                ClinicId = Convert.ToInt32(row["CLINICID"]),
                ClinicName = row["CLINICNAME"].ToString(),
                Location = row["LOCATION"]?.ToString(),
                HasEmergencyFacility = (bool)row["HASEMERGENCYFACILITY"],
                Phone = row["CLINICPHONE"]?.ToString()
            };
        }

        // يمكن إضافة Insert, Update, Delete حسب الحاجة
        public int Insert(Clinic clinic)
        {
            string query = @"
                INSERT INTO CLINIC (CLINICNAME, LOCATION, HASEMERGENCYFACILITY, CLINICPHONE)
                VALUES (@Name, @Location, @HasEmergency, @Phone);
                SELECT SCOPE_IDENTITY();";
            var p = new[]
            {
                new SqlParameter("@Name", clinic.ClinicName),
                new SqlParameter("@Location", (object)clinic.Location ?? DBNull.Value),
                new SqlParameter("@HasEmergency", clinic.HasEmergencyFacility),
                new SqlParameter("@Phone", (object)clinic.Phone ?? DBNull.Value)
            };
            return Convert.ToInt32(DBHandler.ExecuteScalar(query, CommandType.Text, p));
        }

        public int Update(Clinic clinic)
        {
            string query = @"
                UPDATE CLINIC
                SET CLINICNAME = @Name, LOCATION = @Location, HASEMERGENCYFACILITY = @HasEmergency, CLINICPHONE = @Phone
                WHERE CLINICID = @Id";
            var p = new[]
            {
                new SqlParameter("@Id", clinic.ClinicId),
                new SqlParameter("@Name", clinic.ClinicName),
                new SqlParameter("@Location", (object)clinic.Location ?? DBNull.Value),
                new SqlParameter("@HasEmergency", clinic.HasEmergencyFacility),
                new SqlParameter("@Phone", (object)clinic.Phone ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(query, CommandType.Text, p);
        }

        public int Delete(int clinicId)
        {
            return DBHandler.ExecuteNonQuery("DELETE FROM CLINIC WHERE CLINICID = @Id", CommandType.Text, new[] { new SqlParameter("@Id", clinicId) });
        }
    }
}