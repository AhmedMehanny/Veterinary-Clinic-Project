using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DataAccessLayer
{
    public class ReportRepository
    {
        // 1. التطعيمات التي تحتاج جرعة تنشيطية خلال فترة معينة
        public List<Vaccination> GetBoosterDueBetween(DateTime from, DateTime to)
        {
            var list = new List<Vaccination>();
            string query = @"
                SELECT v.VACCINATIONID, v.VISITID, v.INVENTORYID, v.VACCINETYPE, v.ADMINISTEREDDATE, v.NEXTBOOSTERDUE,
                       p.PETID, p.PETNAME, p.SPECIES,
                       o.OFRISTNAME AS OwnerFirstName, o.OLASTNAME AS OwnerLastName, o.OPHONE AS OwnerPhone, o.OEMAIL AS OwnerEmail,
                       mv.VISITDATE, mv.VISITSTATUS,
                       c.CLINICNAME, c.LOCATION AS ClinicLocation,
                       vi.BATCHNUMBER, vi.SUPPLIERNAME
                FROM VACCINATION v
                INNER JOIN MEDICAL_VISIT mv ON v.VISITID = mv.VISITID
                INNER JOIN PET p ON mv.PETID = p.PETID
                INNER JOIN OWNER o ON p.OWNERID = o.OWNERID
                LEFT JOIN APPOINTMENT_SLOT a ON mv.SLOTID = a.SLOTID
                LEFT JOIN CLINIC c ON a.ATTRIBUTE_70 = c.CLINICID
                LEFT JOIN VACCINE_INVENTORY vi ON v.INVENTORYID = vi.INVENTORYID
                WHERE v.NEXTBOOSTERDUE BETWEEN @From AND @To";
            var parameters = new[] { new SqlParameter("@From", from), new SqlParameter("@To", to) };
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text, parameters);
            foreach (DataRow row in dt.Rows)
                list.Add(MapVaccination(row));
            return list;
        }

        // 2. إحصائيات الزيارات لكل عيادة (عدد الزيارات حسب الشهر والسنة)
        public DataTable GetVisitStatistics(DateTime start, DateTime end)
        {
            string query = @"
                SELECT c.CLINICNAME, COUNT(v.VISITID) AS VisitCount, 
                       YEAR(v.VISITDATE) AS Year, MONTH(v.VISITDATE) AS Month
                FROM MEDICAL_VISIT v
                INNER JOIN APPOINTMENT_SLOT a ON v.SLOTID = a.SLOTID
                INNER JOIN CLINIC c ON a.ATTRIBUTE_70 = c.CLINICID
                WHERE v.VISITDATE BETWEEN @Start AND @End
                GROUP BY c.CLINICNAME, YEAR(v.VISITDATE), MONTH(v.VISITDATE)
                ORDER BY Year, Month";
            var parameters = new[] { new SqlParameter("@Start", start), new SqlParameter("@End", end) };
            return DBHandler.ExecuteQuery(query, CommandType.Text, parameters);
        }

        // 3. المخزون المنخفض (الكمية <= الحد الأدنى)
        public List<VaccineInventory> GetLowStockInventory()
        {
            var list = new List<VaccineInventory>();
            string query = @"
                SELECT INVENTORYID, CLINICID, VACCINEINVENTORYTYPE, BATCHNUMBER, 
                       SUPPLIERNAME, EXPIRYDATE, QUANTITYAVAILABLE, REORDERTHRESHOLD
                FROM VACCINE_INVENTORY
                WHERE QUANTITYAVAILABLE <= REORDERTHRESHOLD";
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(MapVaccineInventory(row));
            return list;
        }

        // 4. ملخص المالكين وحيواناتهم
        public DataTable GetOwnerPetSummary()
        {
            string query = @"
                SELECT o.OWNERID, o.OFRISTNAME + ' ' + o.OLASTNAME AS OwnerName, 
                       COUNT(p.PETID) AS PetCount,
                       STRING_AGG(p.PETNAME, ', ') AS PetNames
                FROM OWNER o
                LEFT JOIN PET p ON o.OWNERID = p.OWNERID
                GROUP BY o.OWNERID, o.OFRISTNAME, o.OLASTNAME";
            return DBHandler.ExecuteQuery(query, CommandType.Text);
        }

        // 5. تقرير الإيرادات المقدرة للعيادات (مثال: 50 وحدة نقدية لكل زيارة)
        public DataTable GetClinicRevenue(DateTime start, DateTime end)
        {
            string query = @"
                SELECT c.CLINICNAME, COUNT(v.VISITID) * 50 AS EstimatedRevenue
                FROM MEDICAL_VISIT v
                INNER JOIN APPOINTMENT_SLOT a ON v.SLOTID = a.SLOTID
                INNER JOIN CLINIC c ON a.ATTRIBUTE_70 = c.CLINICID
                WHERE v.VISITDATE BETWEEN @Start AND @End
                GROUP BY c.CLINICNAME";
            var parameters = new[] { new SqlParameter("@Start", start), new SqlParameter("@End", end) };
            return DBHandler.ExecuteQuery(query, CommandType.Text, parameters);
        }

        // ============================================================
        // دوال التحويل (Mapping) الخاصة
        // ============================================================

        private Vaccination MapVaccination(DataRow row)
        {
            return new Vaccination
            {
                VaccinationId = Convert.ToInt32(row["VACCINATIONID"]),
                VisitId = Convert.ToInt32(row["VISITID"]),
                InventoryId = Convert.ToInt32(row["INVENTORYID"]),
                VaccineType = row["VACCINETYPE"].ToString(),
                AdministeredDate = Convert.ToDateTime(row["ADMINISTEREDDATE"]),
                NextBoosterDue = row["NEXTBOOSTERDUE"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["NEXTBOOSTERDUE"]),
                PetId = Convert.ToInt32(row["PETID"]),
                PetName = row["PETNAME"].ToString(),
                Species = row["SPECIES"].ToString(),
                OwnerFirstName = row["OwnerFirstName"]?.ToString(),
                OwnerLastName = row["OwnerLastName"]?.ToString(),
                OwnerPhone = row["OwnerPhone"]?.ToString(),
                OwnerEmail = row["OwnerEmail"]?.ToString(),
                VisitDate = Convert.ToDateTime(row["VISITDATE"]),
                VisitStatus = row["VISITSTATUS"].ToString(),
                ClinicName = row["CLINICNAME"]?.ToString(),
                ClinicLocation = row["ClinicLocation"]?.ToString(),
                BatchNumber = row["BATCHNUMBER"]?.ToString(),
                SupplierName = row["SUPPLIERNAME"]?.ToString()
            };
        }

        private VaccineInventory MapVaccineInventory(DataRow row)
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