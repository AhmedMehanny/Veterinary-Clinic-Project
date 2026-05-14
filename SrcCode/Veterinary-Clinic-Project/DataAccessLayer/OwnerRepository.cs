using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DataAccessLayer
{
    public class OwnerRepository
    {
        // الحصول على جميع المالكين
        public List<Owner> GetAll()
        {
            var list = new List<Owner>();
            string query = @"
                SELECT OwnerId, OFRISTNAME AS FirstName, OLASTNAME AS LastName, 
                       OPHONE AS Phone, OEMAIL AS Email, BILLINGADDRESS, EMERGENCYCONTACT
                FROM OWNER";
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(MapOwner(row));
            return list;
        }

        // الحصول على مالك بواسطة المعرف (Primary Key)
        public Owner GetById(int ownerId)   // ✅ هذه هي الدالة المطلوبة
        {
            string query = @"
                SELECT OwnerId, OFRISTNAME, OLASTNAME, OPHONE, OEMAIL, BILLINGADDRESS, EMERGENCYCONTACT
                FROM OWNER WHERE OwnerId = @Id";
            var param = new SqlParameter("@Id", ownerId);
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text, new[] { param });
            if (dt.Rows.Count == 0) return null;
            return MapOwner(dt.Rows[0]);
        }

        // إضافة مالك جديد
        public int Insert(Owner owner)
        {
            string query = @"
                INSERT INTO OWNER (OFRISTNAME, OLASTNAME, OPHONE, OEMAIL, BILLINGADDRESS, EMERGENCYCONTACT)
                VALUES (@FirstName, @LastName, @Phone, @Email, @BillingAddress, @EmergencyContact);
                SELECT SCOPE_IDENTITY();";
            var parameters = new[]
            {
                new SqlParameter("@FirstName", owner.FirstName),
                new SqlParameter("@LastName", owner.LastName),
                new SqlParameter("@Phone", (object)owner.Phone ?? DBNull.Value),
                new SqlParameter("@Email", (object)owner.Email ?? DBNull.Value),
                new SqlParameter("@BillingAddress", (object)owner.BillingAddress ?? DBNull.Value),
                new SqlParameter("@EmergencyContact", (object)owner.EmergencyContact ?? DBNull.Value)
            };
            object result = DBHandler.ExecuteScalar(query, CommandType.Text, parameters);
            return Convert.ToInt32(result);
        }

        // تحديث بيانات مالك
        public int Update(Owner owner)
        {
            string query = @"
                UPDATE OWNER 
                SET OFRISTNAME = @FirstName, OLASTNAME = @LastName, 
                    OPHONE = @Phone, OEMAIL = @Email, 
                    BILLINGADDRESS = @BillingAddress, EMERGENCYCONTACT = @EmergencyContact
                WHERE OwnerId = @Id";
            var parameters = new[]
            {
                new SqlParameter("@Id", owner.OwnerId),
                new SqlParameter("@FirstName", owner.FirstName),
                new SqlParameter("@LastName", owner.LastName),
                new SqlParameter("@Phone", (object)owner.Phone ?? DBNull.Value),
                new SqlParameter("@Email", (object)owner.Email ?? DBNull.Value),
                new SqlParameter("@BillingAddress", (object)owner.BillingAddress ?? DBNull.Value),
                new SqlParameter("@EmergencyContact", (object)owner.EmergencyContact ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(query, CommandType.Text, parameters);
        }

        // حذف مالك
        public int Delete(int ownerId)
        {
            string query = "DELETE FROM OWNER WHERE OwnerId = @Id";
            return DBHandler.ExecuteNonQuery(query, CommandType.Text, new[] { new SqlParameter("@Id", ownerId) });
        }

        // دالة تحويل DataRow إلى كائن Owner
        private Owner MapOwner(DataRow row)
        {
            return new Owner
            {
                OwnerId = Convert.ToInt32(row["OwnerId"]),
                FirstName = row["FirstName"].ToString(),
                LastName = row["LastName"].ToString(),
                Phone = row["Phone"]?.ToString(),
                Email = row["Email"]?.ToString(),
                BillingAddress = row["BILLINGADDRESS"]?.ToString(),
                EmergencyContact = row["EMERGENCYCONTACT"]?.ToString()
            };
        }
    }
}