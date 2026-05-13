using System;
using System.Data;
using System.Data.SqlClient;

namespace VetClinicApp
{
    public class OwnerRepository
    {
        public DataTable GetAll()
        {
            string sql = "SELECT * FROM OWNER ORDER BY OLASTNAME";
            return DBHandler.ExecuteQuery(sql);
        }

        public DataTable GetByID(int ownerID)
        {
            string sql = "SELECT * FROM OWNER WHERE OWNERID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", ownerID)
            };
            return DBHandler.ExecuteQuery(sql, parameters);
        }

        public DataTable Search(string phone, string email)
        {
            string sql = "SELECT * FROM OWNER WHERE OPHONE = @phone OR OEMAIL = @email";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@phone", (object)phone ?? DBNull.Value),
                new SqlParameter("@email", (object)email ?? DBNull.Value)
            };
            return DBHandler.ExecuteQuery(sql, parameters);
        }

        public int Insert(Owner o)
        {
            string sql = @"INSERT INTO OWNER (OFRISTNAME, OLASTNAME, OPHONE, OEMAIL, BILLINGADDRESS, EMERGENCYCONTACT)
                           VALUES (@fn, @ln, @ph, @em, @ba, @ec)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@fn", (object)o.FirstName ?? DBNull.Value),
                new SqlParameter("@ln", (object)o.LastName ?? DBNull.Value),
                new SqlParameter("@ph", (object)o.Phone ?? DBNull.Value),
                new SqlParameter("@em", (object)o.Email ?? DBNull.Value),
                new SqlParameter("@ba", (object)o.BillingAddress ?? DBNull.Value),
                new SqlParameter("@ec", (object)o.EmergencyContact ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }

        public int Update(Owner o)
        {
            string sql = @"UPDATE OWNER SET OFRISTNAME = @fn, OLASTNAME = @ln, OPHONE = @ph,
                           OEMAIL = @em, BILLINGADDRESS = @ba, EMERGENCYCONTACT = @ec
                           WHERE OWNERID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@fn", (object)o.FirstName ?? DBNull.Value),
                new SqlParameter("@ln", (object)o.LastName ?? DBNull.Value),
                new SqlParameter("@ph", (object)o.Phone ?? DBNull.Value),
                new SqlParameter("@em", (object)o.Email ?? DBNull.Value),
                new SqlParameter("@ba", (object)o.BillingAddress ?? DBNull.Value),
                new SqlParameter("@ec", (object)o.EmergencyContact ?? DBNull.Value),
                new SqlParameter("@id", o.OwnerID)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }

        public int Delete(int ownerID)
        {
            string sql = "DELETE FROM OWNER WHERE OWNERID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", ownerID)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }
    }
}
