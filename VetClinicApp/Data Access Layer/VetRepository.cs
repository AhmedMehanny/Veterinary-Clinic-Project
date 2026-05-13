using System;
using System.Data;
using System.Data.SqlClient;

namespace VetClinicApp
{
    public class VetRepository
    {
        public DataTable GetAll()
        {
            string sql = "SELECT * FROM VETERINARIAN ORDER BY VETLASTNAME";
            return DBHandler.ExecuteQuery(sql);
        }

        public int Insert(Veterinarian v)
        {
            string sql = @"INSERT INTO VETERINARIAN (VETFIRSTNAME, VETLASTNAME, SPECIALTY, LICENSENUMBER, VETPHONE)
                           VALUES (@fn, @ln, @sp, @lic, @ph)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@fn", (object)v.FirstName ?? DBNull.Value),
                new SqlParameter("@ln", (object)v.LastName ?? DBNull.Value),
                new SqlParameter("@sp", (object)v.Specialty ?? DBNull.Value),
                new SqlParameter("@lic", (object)v.LicenseNumber ?? DBNull.Value),
                new SqlParameter("@ph", (object)v.Phone ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }

        public int Update(Veterinarian v)
        {
            string sql = @"UPDATE VETERINARIAN SET VETFIRSTNAME = @fn, VETLASTNAME = @ln,
                           SPECIALTY = @sp, LICENSENUMBER = @lic, VETPHONE = @ph
                           WHERE VETID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@fn", (object)v.FirstName ?? DBNull.Value),
                new SqlParameter("@ln", (object)v.LastName ?? DBNull.Value),
                new SqlParameter("@sp", (object)v.Specialty ?? DBNull.Value),
                new SqlParameter("@lic", (object)v.LicenseNumber ?? DBNull.Value),
                new SqlParameter("@ph", (object)v.Phone ?? DBNull.Value),
                new SqlParameter("@id", v.VetID)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }

        public int Delete(int vetID)
        {
            string sql = "DELETE FROM VETERINARIAN WHERE VETID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", vetID)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }
    }
}
