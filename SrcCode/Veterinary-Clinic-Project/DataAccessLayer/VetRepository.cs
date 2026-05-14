using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DataAccessLayer
{
    public class VetRepository
    {
        // ============================================================
        // PRIVATE MAPPING METHODS
        // ============================================================

        private Veterinarian MapVet(DataRow row)
        {
            return new Veterinarian
            {
                VetId = Convert.ToInt32(row["VETID"]),
                FirstName = row["VETFIRSTNAME"].ToString().Trim(),
                LastName = row["VETLASTNAME"].ToString().Trim(),
                Specialty = row["SPECIALTY"] == DBNull.Value ? null : row["SPECIALTY"].ToString().Trim(),
                LicenseNumber = row["LICENSENUMBER"] == DBNull.Value ? null : row["LICENSENUMBER"].ToString().Trim(),
                Phone = row["VETPHONE"] == DBNull.Value ? null : row["VETPHONE"].ToString().Trim()
            };
        }

        private Veterinarian MapVetWithClinic(DataRow row)
        {
            var vet = MapVet(row);
            if (row["CLINICID"] != DBNull.Value)
            {
                vet.ClinicId = Convert.ToInt32(row["CLINICID"]);
                vet.ClinicName = row["CLINICNAME"]?.ToString().Trim();
                vet.ClinicLocation = row["LOCATION"]?.ToString().Trim();
                vet.IsPrimaryAtClinic = row["ISPRIMARY"] != DBNull.Value && Convert.ToBoolean(row["ISPRIMARY"]);
                vet.JoinDate = row["JOINDATE"] as DateTime?;
            }
            return vet;
        }

        // ============================================================
        // INSERT (CREATE)
        // ============================================================

        public int Insert(Veterinarian vet)
        {
            string sql = @"INSERT INTO VETERINARIAN (VETFIRSTNAME, VETLASTNAME, SPECIALTY, LICENSENUMBER, VETPHONE) 
                           VALUES (@FirstName, @LastName, @Specialty, @LicenseNumber, @Phone)";
            var p = new[]
            {
                new SqlParameter("@FirstName", vet.FirstName),
                new SqlParameter("@LastName", vet.LastName),
                new SqlParameter("@Specialty", (object)vet.Specialty ?? DBNull.Value),
                new SqlParameter("@LicenseNumber", (object)vet.LicenseNumber ?? DBNull.Value),
                new SqlParameter("@Phone", (object)vet.Phone ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(sql, CommandType.Text, p);
        }

        public int InsertAndGetId(Veterinarian vet)
        {
            string sql = @"INSERT INTO VETERINARIAN (VETFIRSTNAME, VETLASTNAME, SPECIALTY, LICENSENUMBER, VETPHONE) 
                           VALUES (@FirstName, @LastName, @Specialty, @LicenseNumber, @Phone);
                           SELECT SCOPE_IDENTITY();";
            var p = new[]
            {
                new SqlParameter("@FirstName", vet.FirstName),
                new SqlParameter("@LastName", vet.LastName),
                new SqlParameter("@Specialty", (object)vet.Specialty ?? DBNull.Value),
                new SqlParameter("@LicenseNumber", (object)vet.LicenseNumber ?? DBNull.Value),
                new SqlParameter("@Phone", (object)vet.Phone ?? DBNull.Value)
            };
            object result = DBHandler.ExecuteScalar(sql, CommandType.Text, p);
            return Convert.ToInt32(result);
        }

        // ============================================================
        // SELECT (READ)
        // ============================================================

        public List<Veterinarian> GetAll()
        {
            var list = new List<Veterinarian>();
            string sql = "SELECT * FROM VETERINARIAN ORDER BY VETLASTNAME, VETFIRSTNAME";
            DataTable dt = DBHandler.ExecuteQuery(sql, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(MapVet(row));
            return list;
        }

        public List<Veterinarian> GetAllWithClinics()
        {
            var list = new List<Veterinarian>();
            string sql = @"SELECT 
                           v.VETID, v.VETFIRSTNAME, v.VETLASTNAME, v.SPECIALTY, v.LICENSENUMBER, v.VETPHONE,
                           vc.CLINICID, c.CLINICNAME, c.LOCATION, vc.ISPRIMARY, vc.JOINDATE
                           FROM VETERINARIAN v
                           LEFT JOIN VET_CLINIC vc ON v.VETID = vc.VETID
                           LEFT JOIN CLINIC c ON vc.CLINICID = c.CLINICID
                           ORDER BY v.VETLASTNAME, v.VETFIRSTNAME";
            DataTable dt = DBHandler.ExecuteQuery(sql, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(MapVetWithClinic(row));
            return list;
        }

        public Veterinarian GetById(int vetId)
        {
            string sql = "SELECT * FROM VETERINARIAN WHERE VETID = @Id";
            var p = new[] { new SqlParameter("@Id", vetId) };
            DataTable dt = DBHandler.ExecuteQuery(sql, CommandType.Text, p);
            if (dt.Rows.Count == 0) return null;
            return MapVet(dt.Rows[0]);
        }

        public Veterinarian GetByIdWithClinic(int vetId)
        {
            string sql = @"SELECT 
                           v.VETID, v.VETFIRSTNAME, v.VETLASTNAME, v.SPECIALTY, v.LICENSENUMBER, v.VETPHONE,
                           vc.CLINICID, c.CLINICNAME, c.LOCATION, vc.ISPRIMARY, vc.JOINDATE
                           FROM VETERINARIAN v
                           LEFT JOIN VET_CLINIC vc ON v.VETID = vc.VETID
                           LEFT JOIN CLINIC c ON vc.CLINICID = c.CLINICID
                           WHERE v.VETID = @Id";
            var p = new[] { new SqlParameter("@Id", vetId) };
            DataTable dt = DBHandler.ExecuteQuery(sql, CommandType.Text, p);
            if (dt.Rows.Count == 0) return null;
            return MapVetWithClinic(dt.Rows[0]);
        }

        public List<Veterinarian> GetBySpecialty(string specialty)
        {
            var list = new List<Veterinarian>();
            string sql = "SELECT * FROM VETERINARIAN WHERE SPECIALTY = @Specialty ORDER BY VETLASTNAME";
            var p = new[] { new SqlParameter("@Specialty", specialty) };
            DataTable dt = DBHandler.ExecuteQuery(sql, CommandType.Text, p);
            foreach (DataRow row in dt.Rows)
                list.Add(MapVet(row));
            return list;
        }

        public List<Veterinarian> SearchByName(string searchTerm)
        {
            var list = new List<Veterinarian>();
            string sql = @"SELECT * FROM VETERINARIAN 
                           WHERE VETFIRSTNAME LIKE @Search OR VETLASTNAME LIKE @Search
                           ORDER BY VETLASTNAME";
            var p = new[] { new SqlParameter("@Search", "%" + searchTerm + "%") };
            DataTable dt = DBHandler.ExecuteQuery(sql, CommandType.Text, p);
            foreach (DataRow row in dt.Rows)
                list.Add(MapVet(row));
            return list;
        }

        public List<Veterinarian> GetByClinicId(int clinicId)
        {
            var list = new List<Veterinarian>();
            string sql = @"SELECT v.* 
                           FROM VETERINARIAN v
                           INNER JOIN VET_CLINIC vc ON v.VETID = vc.VETID
                           WHERE vc.CLINICID = @ClinicId
                           ORDER BY v.VETLASTNAME";
            var p = new[] { new SqlParameter("@ClinicId", clinicId) };
            DataTable dt = DBHandler.ExecuteQuery(sql, CommandType.Text, p);
            foreach (DataRow row in dt.Rows)
                list.Add(MapVet(row));
            return list;
        }

        public List<Veterinarian> GetByClinicIdWithDetails(int clinicId)
        {
            var list = new List<Veterinarian>();
            string sql = @"SELECT 
                           v.VETID, v.VETFIRSTNAME, v.VETLASTNAME, v.SPECIALTY, v.LICENSENUMBER, v.VETPHONE,
                           vc.CLINICID, c.CLINICNAME, c.LOCATION, vc.ISPRIMARY, vc.JOINDATE
                           FROM VETERINARIAN v
                           INNER JOIN VET_CLINIC vc ON v.VETID = vc.VETID
                           INNER JOIN CLINIC c ON vc.CLINICID = c.CLINICID
                           WHERE vc.CLINICID = @ClinicId
                           ORDER BY v.VETLASTNAME";
            var p = new[] { new SqlParameter("@ClinicId", clinicId) };
            DataTable dt = DBHandler.ExecuteQuery(sql, CommandType.Text, p);
            foreach (DataRow row in dt.Rows)
                list.Add(MapVetWithClinic(row));
            return list;
        }

        public Veterinarian GetPrimaryVetByClinicId(int clinicId)
        {
            string sql = @"SELECT v.* 
                           FROM VETERINARIAN v
                           INNER JOIN VET_CLINIC vc ON v.VETID = vc.VETID
                           WHERE vc.CLINICID = @ClinicId AND vc.ISPRIMARY = 1";
            var p = new[] { new SqlParameter("@ClinicId", clinicId) };
            DataTable dt = DBHandler.ExecuteQuery(sql, CommandType.Text, p);
            if (dt.Rows.Count == 0) return null;
            return MapVet(dt.Rows[0]);
        }

        public List<string> GetAllSpecialties()
        {
            var list = new List<string>();
            string sql = "SELECT DISTINCT SPECIALTY FROM VETERINARIAN WHERE SPECIALTY IS NOT NULL ORDER BY SPECIALTY";
            DataTable dt = DBHandler.ExecuteQuery(sql, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(row["SPECIALTY"].ToString().Trim());
            return list;
        }

        public List<Veterinarian> GetVetsWithoutClinic()
        {
            var list = new List<Veterinarian>();
            string sql = @"SELECT v.* 
                           FROM VETERINARIAN v
                           LEFT JOIN VET_CLINIC vc ON v.VETID = vc.VETID
                           WHERE vc.VETID IS NULL
                           ORDER BY v.VETLASTNAME";
            DataTable dt = DBHandler.ExecuteQuery(sql, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(MapVet(row));
            return list;
        }

        // ============================================================
        // UPDATE
        // ============================================================

        public int Update(Veterinarian vet)
        {
            string sql = @"UPDATE VETERINARIAN SET 
                           VETFIRSTNAME = @FirstName, 
                           VETLASTNAME = @LastName, 
                           SPECIALTY = @Specialty, 
                           LICENSENUMBER = @LicenseNumber, 
                           VETPHONE = @Phone 
                           WHERE VETID = @Id";
            var p = new[]
            {
                new SqlParameter("@Id", vet.VetId),
                new SqlParameter("@FirstName", vet.FirstName),
                new SqlParameter("@LastName", vet.LastName),
                new SqlParameter("@Specialty", (object)vet.Specialty ?? DBNull.Value),
                new SqlParameter("@LicenseNumber", (object)vet.LicenseNumber ?? DBNull.Value),
                new SqlParameter("@Phone", (object)vet.Phone ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(sql, CommandType.Text, p);
        }

        public int UpdateSpecialty(int vetId, string newSpecialty)
        {
            string sql = "UPDATE VETERINARIAN SET SPECIALTY = @Specialty WHERE VETID = @Id";
            var p = new[]
            {
                new SqlParameter("@Id", vetId),
                new SqlParameter("@Specialty", (object)newSpecialty ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(sql, CommandType.Text, p);
        }

        public int UpdatePhone(int vetId, string newPhone)
        {
            string sql = "UPDATE VETERINARIAN SET VETPHONE = @Phone WHERE VETID = @Id";
            var p = new[]
            {
                new SqlParameter("@Id", vetId),
                new SqlParameter("@Phone", (object)newPhone ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(sql, CommandType.Text, p);
        }

        public int UpdateLicenseNumber(int vetId, string newLicense)
        {
            string sql = "UPDATE VETERINARIAN SET LICENSENUMBER = @License WHERE VETID = @Id";
            var p = new[]
            {
                new SqlParameter("@Id", vetId),
                new SqlParameter("@License", (object)newLicense ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(sql, CommandType.Text, p);
        }

        // ============================================================
        // DELETE
        // ============================================================

        public int Delete(int vetId)
        {
            string sql = "DELETE FROM VETERINARIAN WHERE VETID = @Id";
            return DBHandler.ExecuteNonQuery(sql, CommandType.Text, new[] { new SqlParameter("@Id", vetId) });
        }

        public int DeleteBySpecialty(string specialty)
        {
            string sql = "DELETE FROM VETERINARIAN WHERE SPECIALTY = @Specialty";
            return DBHandler.ExecuteNonQuery(sql, CommandType.Text, new[] { new SqlParameter("@Specialty", specialty) });
        }

        // ============================================================
        // HELPER / EXISTENCE
        // ============================================================

        public bool Exists(int vetId)
        {
            string sql = "SELECT COUNT(1) FROM VETERINARIAN WHERE VETID = @Id";
            var result = DBHandler.ExecuteScalar(sql, CommandType.Text, new[] { new SqlParameter("@Id", vetId) });
            return Convert.ToInt32(result) > 0;
        }

        public bool LicenseNumberExists(string licenseNumber, int excludeVetId = 0)
        {
            string sql = "SELECT COUNT(1) FROM VETERINARIAN WHERE LICENSENUMBER = @License AND VETID != @ExcludeId";
            var p = new[]
            {
                new SqlParameter("@License", licenseNumber),
                new SqlParameter("@ExcludeId", excludeVetId)
            };
            var result = DBHandler.ExecuteScalar(sql, CommandType.Text, p);
            return Convert.ToInt32(result) > 0;
        }

        public bool VetWorksAtClinic(int vetId, int clinicId)
        {
            string sql = "SELECT COUNT(1) FROM VET_CLINIC WHERE VETID = @VetId AND CLINICID = @ClinicId";
            var p = new[]
            {
                new SqlParameter("@VetId", vetId),
                new SqlParameter("@ClinicId", clinicId)
            };
            var result = DBHandler.ExecuteScalar(sql, CommandType.Text, p);
            return Convert.ToInt32(result) > 0;
        }

        public int GetCount()
        {
            string sql = "SELECT COUNT(*) FROM VETERINARIAN";
            return Convert.ToInt32(DBHandler.ExecuteScalar(sql, CommandType.Text));
        }

        public Dictionary<string, int> GetCountBySpecialty()
        {
            var dict = new Dictionary<string, int>();
            string sql = "SELECT SPECIALTY, COUNT(*) AS Count FROM VETERINARIAN GROUP BY SPECIALTY ORDER BY SPECIALTY";
            DataTable dt = DBHandler.ExecuteQuery(sql, CommandType.Text);
            foreach (DataRow row in dt.Rows)
            {
                string specialty = row["SPECIALTY"] == DBNull.Value ? "Not Specified" : row["SPECIALTY"].ToString().Trim();
                int count = Convert.ToInt32(row["Count"]);
                dict[specialty] = count;
            }
            return dict;
        }

        public Dictionary<string, int> GetCountPerClinic()
        {
            var dict = new Dictionary<string, int>();
            string sql = @"SELECT c.CLINICNAME, COUNT(vc.VETID) AS VetCount
                           FROM CLINIC c
                           LEFT JOIN VET_CLINIC vc ON c.CLINICID = vc.CLINICID
                           GROUP BY c.CLINICID, c.CLINICNAME
                           ORDER BY VetCount DESC";
            DataTable dt = DBHandler.ExecuteQuery(sql, CommandType.Text);
            foreach (DataRow row in dt.Rows)
            {
                string clinicName = row["CLINICNAME"].ToString().Trim();
                int count = Convert.ToInt32(row["VetCount"]);
                dict[clinicName] = count;
            }
            return dict;
        }

        public double GetAverageClinicsPerVet()
        {
            string sql = @"SELECT AVG(ClinicCount) 
                           FROM (SELECT v.VETID, COUNT(vc.CLINICID) AS ClinicCount
                                 FROM VETERINARIAN v
                                 LEFT JOIN VET_CLINIC vc ON v.VETID = vc.VETID
                                 GROUP BY v.VETID) AS Subquery";
            object result = DBHandler.ExecuteScalar(sql, CommandType.Text);
            return result == DBNull.Value ? 0 : Convert.ToDouble(result);
        }
    }
}