using System;
using System.Data;
using System.Data.SqlClient;

namespace VetClinicApp
{
    public class PetRepository
    {
        public DataTable GetAll()
        {
            string sql = "SELECT * FROM PET ORDER BY PETNAME";
            return DBHandler.ExecuteQuery(sql);
        }

        public DataTable GetByOwner(int ownerID)
        {
            string sql = "SELECT * FROM PET WHERE OWNERID = @ownerID";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@ownerID", ownerID)
            };
            return DBHandler.ExecuteQuery(sql, parameters);
        }

        public DataTable GetPetsWithOwners()
        {
            string sql = @"SELECT p.PETID, p.PETNAME, p.SPECIES, p.BREED, p.AGE,
                                  o.OWNERID, o.OFRISTNAME + ' ' + o.OLASTNAME AS OwnerName
                           FROM PET p
                           INNER JOIN OWNER o ON p.OWNERID = o.OWNERID
                           ORDER BY p.PETNAME";
            return DBHandler.ExecuteQuery(sql);
        }

        public DataTable GetPetsNoVisit6Months()
        {
            string sql = @"SELECT p.PETID, p.PETNAME, p.SPECIES,
                                  o.OFRISTNAME + ' ' + o.OLASTNAME AS OwnerName,
                                  MAX(mv.VISITDATE) AS LastVisit
                           FROM PET p
                           INNER JOIN OWNER o ON p.OWNERID = o.OWNERID
                           LEFT JOIN MEDICAL_VISIT mv ON p.PETID = mv.PETID
                           GROUP BY p.PETID, p.PETNAME, p.SPECIES, o.OFRISTNAME, o.OLASTNAME
                           HAVING MAX(mv.VISITDATE) < DATEADD(MONTH, -6, GETDATE())
                              OR MAX(mv.VISITDATE) IS NULL";
            return DBHandler.ExecuteQuery(sql);
        }

        public int Insert(Pet p)
        {
            string sql = @"INSERT INTO PET (OWNERID, PETNAME, SPECIES, BREED, AGE)
                           VALUES (@oid, @pn, @sp, @br, @ag)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@oid", p.OwnerID),
                new SqlParameter("@pn", (object)p.PetName ?? DBNull.Value),
                new SqlParameter("@sp", (object)p.Species ?? DBNull.Value),
                new SqlParameter("@br", (object)p.Breed ?? DBNull.Value),
                new SqlParameter("@ag", p.Age)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }

        public int Update(Pet p)
        {
            string sql = @"UPDATE PET SET OWNERID = @oid, PETNAME = @pn, SPECIES = @sp,
                           BREED = @br, AGE = @ag
                           WHERE PETID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@oid", p.OwnerID),
                new SqlParameter("@pn", (object)p.PetName ?? DBNull.Value),
                new SqlParameter("@sp", (object)p.Species ?? DBNull.Value),
                new SqlParameter("@br", (object)p.Breed ?? DBNull.Value),
                new SqlParameter("@ag", p.Age),
                new SqlParameter("@id", p.PetID)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }

        public int Delete(int petID)
        {
            string sql = "DELETE FROM PET WHERE PETID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", petID)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }
    }
}
