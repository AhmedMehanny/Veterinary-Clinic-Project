using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DataAccessLayer
{
    public class VaccineInventoryRepository
    {
        // الحصول على جميع سجلات المخزون
        public List<VaccineInventory> GetAll()
        {
            var list = new List<VaccineInventory>();
            string query = @"
                SELECT INVENTORYID, CLINICID, VACCINEINVENTORYTYPE, BATCHNUMBER, 
                       SUPPLIERNAME, EXPIRYDATE, QUANTITYAVAILABLE, REORDERTHRESHOLD
                FROM VACCINE_INVENTORY
                ORDER BY EXPIRYDATE";
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // الحصول على سجل مخزون بواسطة المعرف
        public VaccineInventory GetById(int inventoryId)
        {
            string query = @"
                SELECT INVENTORYID, CLINICID, VACCINEINVENTORYTYPE, BATCHNUMBER, 
                       SUPPLIERNAME, EXPIRYDATE, QUANTITYAVAILABLE, REORDERTHRESHOLD
                FROM VACCINE_INVENTORY
                WHERE INVENTORYID = @InventoryId";
            var dt = DBHandler.ExecuteQuery(query, CommandType.Text,
                new[] { new SqlParameter("@InventoryId", inventoryId) });
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        // الحصول على سجلات المخزون الخاصة بعيادة معينة
        public List<VaccineInventory> GetByClinicId(int clinicId)
        {
            var list = new List<VaccineInventory>();
            string query = @"
                SELECT INVENTORYID, CLINICID, VACCINEINVENTORYTYPE, BATCHNUMBER, 
                       SUPPLIERNAME, EXPIRYDATE, QUANTITYAVAILABLE, REORDERTHRESHOLD
                FROM VACCINE_INVENTORY
                WHERE CLINICID = @ClinicId
                ORDER BY EXPIRYDATE";
            var dt = DBHandler.ExecuteQuery(query, CommandType.Text,
                new[] { new SqlParameter("@ClinicId", clinicId) });
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // الحصول على مخزون منخفض (الكمية <= الحد الأدنى لإعادة الطلب)
        public List<VaccineInventory> GetLowStock()
        {
            var list = new List<VaccineInventory>();
            string query = @"
                SELECT INVENTORYID, CLINICID, VACCINEINVENTORYTYPE, BATCHNUMBER, 
                       SUPPLIERNAME, EXPIRYDATE, QUANTITYAVAILABLE, REORDERTHRESHOLD
                FROM VACCINE_INVENTORY
                WHERE QUANTITYAVAILABLE <= REORDERTHRESHOLD
                ORDER BY QUANTITYAVAILABLE";
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // الحصول على المخزون المنتهي الصلاحية (تاريخ الصلاحية <= اليوم)
        public List<VaccineInventory> GetExpiredStock(DateTime asOfDate)
        {
            var list = new List<VaccineInventory>();
            string query = @"
                SELECT INVENTORYID, CLINICID, VACCINEINVENTORYTYPE, BATCHNUMBER, 
                       SUPPLIERNAME, EXPIRYDATE, QUANTITYAVAILABLE, REORDERTHRESHOLD
                FROM VACCINE_INVENTORY
                WHERE EXPIRYDATE IS NOT NULL AND EXPIRYDATE <= @AsOfDate
                ORDER BY EXPIRYDATE";
            var dt = DBHandler.ExecuteQuery(query, CommandType.Text,
                new[] { new SqlParameter("@AsOfDate", asOfDate) });
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // إضافة سجل مخزون جديد
        public int Insert(VaccineInventory inventory)
        {
            string query = @"
                INSERT INTO VACCINE_INVENTORY (CLINICID, VACCINEINVENTORYTYPE, BATCHNUMBER, 
                                               SUPPLIERNAME, EXPIRYDATE, QUANTITYAVAILABLE, REORDERTHRESHOLD)
                VALUES (@ClinicId, @VaccineType, @BatchNumber, @SupplierName, @ExpiryDate, @Quantity, @Threshold);
                SELECT SCOPE_IDENTITY();";
            var p = new[]
            {
                new SqlParameter("@ClinicId", inventory.ClinicId),
                new SqlParameter("@VaccineType", inventory.VaccineInventoryType),
                new SqlParameter("@BatchNumber", inventory.BatchNumber),
                new SqlParameter("@SupplierName", (object)inventory.SupplierName ?? DBNull.Value),
                new SqlParameter("@ExpiryDate", (object)inventory.ExpiryDate ?? DBNull.Value),
                new SqlParameter("@Quantity", (object)inventory.QuantityAvailable ?? DBNull.Value),
                new SqlParameter("@Threshold", (object)inventory.ReorderThreshold ?? DBNull.Value)
            };
            return Convert.ToInt32(DBHandler.ExecuteScalar(query, CommandType.Text, p));
        }

        // تحديث سجل مخزون
        public int Update(VaccineInventory inventory)
        {
            string query = @"
                UPDATE VACCINE_INVENTORY
                SET CLINICID = @ClinicId,
                    VACCINEINVENTORYTYPE = @VaccineType,
                    BATCHNUMBER = @BatchNumber,
                    SUPPLIERNAME = @SupplierName,
                    EXPIRYDATE = @ExpiryDate,
                    QUANTITYAVAILABLE = @Quantity,
                    REORDERTHRESHOLD = @Threshold
                WHERE INVENTORYID = @InventoryId";
            var p = new[]
            {
                new SqlParameter("@InventoryId", inventory.InventoryId),
                new SqlParameter("@ClinicId", inventory.ClinicId),
                new SqlParameter("@VaccineType", inventory.VaccineInventoryType),
                new SqlParameter("@BatchNumber", inventory.BatchNumber),
                new SqlParameter("@SupplierName", (object)inventory.SupplierName ?? DBNull.Value),
                new SqlParameter("@ExpiryDate", (object)inventory.ExpiryDate ?? DBNull.Value),
                new SqlParameter("@Quantity", (object)inventory.QuantityAvailable ?? DBNull.Value),
                new SqlParameter("@Threshold", (object)inventory.ReorderThreshold ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(query, CommandType.Text, p);
        }

        // تحديث الكمية المتاحة فقط (مثلاً عند استخدام جرعة)
        public int UpdateQuantity(int inventoryId, int newQuantity)
        {
            string query = "UPDATE VACCINE_INVENTORY SET QUANTITYAVAILABLE = @Quantity WHERE INVENTORYID = @InventoryId";
            var p = new[]
            {
                new SqlParameter("@InventoryId", inventoryId),
                new SqlParameter("@Quantity", newQuantity)
            };
            return DBHandler.ExecuteNonQuery(query, CommandType.Text, p);
        }

        // تقليل الكمية بمقدار معين (مثلاً عند إعطاء تطعيم)
        public int DecreaseQuantity(int inventoryId, int amount)
        {
            string query = @"
                UPDATE VACCINE_INVENTORY 
                SET QUANTITYAVAILABLE = QUANTITYAVAILABLE - @Amount
                WHERE INVENTORYID = @InventoryId AND QUANTITYAVAILABLE >= @Amount";
            var p = new[]
            {
                new SqlParameter("@InventoryId", inventoryId),
                new SqlParameter("@Amount", amount)
            };
            return DBHandler.ExecuteNonQuery(query, CommandType.Text, p);
        }

        // حذف سجل مخزون
        public int Delete(int inventoryId)
        {
            string query = "DELETE FROM VACCINE_INVENTORY WHERE INVENTORYID = @InventoryId";
            return DBHandler.ExecuteNonQuery(query, CommandType.Text,
                new[] { new SqlParameter("@InventoryId", inventoryId) });
        }

        // تحويل DataRow إلى كائن VaccineInventory
        private VaccineInventory Map(DataRow row)
        {
            return new VaccineInventory
            {
                InventoryId = Convert.ToInt32(row["INVENTORYID"]),
                ClinicId = Convert.ToInt32(row["CLINICID"]),
                VaccineInventoryType = row["VACCINEINVENTORYTYPE"].ToString(),
                BatchNumber = row["BATCHNUMBER"].ToString(),
                SupplierName = row["SUPPLIERNAME"]?.ToString(),
                ExpiryDate = row["EXPIRYDATE"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["EXPIRYDATE"]),
                QuantityAvailable = row["QUANTITYAVAILABLE"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["QUANTITYAVAILABLE"]),
                ReorderThreshold = row["REORDERTHRESHOLD"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["REORDERTHRESHOLD"])
            };
        }
    }
}