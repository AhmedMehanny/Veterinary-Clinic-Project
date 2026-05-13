using System;
using System.Data;
using System.Data.SqlClient;

namespace VetClinicApp
{
    public class VaccinationRepository
    {
        public DataTable GetAll()
        {
            string sql = @"SELECT va.*, p.PETNAME, vi.VACCINEINVENTORYTYPE
                           FROM VACCINATION va
                           JOIN MEDICAL_VISIT mv ON va.VISITID = mv.VISITID
                           JOIN PET p ON mv.PETID = p.PETID
                           JOIN VACCINE_INVENTORY vi ON va.INVENTORYID = vi.INVENTORYID";
            return DBHandler.ExecuteQuery(sql);
        }

        public DataTable GetLowStockInventory()
        {
            string sql = @"SELECT vi.*, c.CLINICNAME
                           FROM VACCINE_INVENTORY vi
                           JOIN CLINIC c ON vi.CLINICID = c.CLINICID
                           WHERE vi.QUANTITYAVAILABLE <= vi.REORDERTHRESHOLD
                           ORDER BY vi.QUANTITYAVAILABLE ASC";
            return DBHandler.ExecuteQuery(sql);
        }

        public DataTable GetUpcomingReminders()
        {
            string sql = @"SELECT r.REMINDERID, r.SCHEDULEDDATE, r.CHANNEL, r.REMINDESTATUS,
                                  o.OFRISTNAME + ' ' + o.OLASTNAME AS OwnerName,
                                  o.OPHONE, o.OEMAIL,
                                  va.VACCINETYPE, va.NEXTBOOSTERDUE
                           FROM REMINDER r
                           JOIN OWNER o ON r.OWNERID = o.OWNERID
                           JOIN VACCINATION va ON r.VACCINATIONID = va.VACCINATIONID
                           WHERE r.SCHEDULEDDATE >= GETDATE()
                           ORDER BY r.SCHEDULEDDATE ASC";
            return DBHandler.ExecuteQuery(sql);
        }

        public int Insert(Vaccination v)
        {
            string sql = @"INSERT INTO VACCINATION (VISITID, INVENTORYID, VACCINETYPE, ADMINISTEREDDATE, NEXTBOOSTERDUE)
                           VALUES (@vid, @iid, @vt, @ad, @nb)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@vid", v.VisitID),
                new SqlParameter("@iid", v.InventoryID),
                new SqlParameter("@vt", (object)v.VaccineType ?? DBNull.Value),
                new SqlParameter("@ad", v.AdministeredDate),
                new SqlParameter("@nb", v.NextBoosterDue.HasValue ? (object)v.NextBoosterDue.Value : DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }

        public int Update(Vaccination v)
        {
            string sql = @"UPDATE VACCINATION SET VISITID = @vid, INVENTORYID = @iid, VACCINETYPE = @vt,
                           ADMINISTEREDDATE = @ad, NEXTBOOSTERDUE = @nb
                           WHERE VACCINATIONID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@vid", v.VisitID),
                new SqlParameter("@iid", v.InventoryID),
                new SqlParameter("@vt", (object)v.VaccineType ?? DBNull.Value),
                new SqlParameter("@ad", v.AdministeredDate),
                new SqlParameter("@nb", v.NextBoosterDue.HasValue ? (object)v.NextBoosterDue.Value : DBNull.Value),
                new SqlParameter("@id", v.VaccinationID)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }

        public int Delete(int vaccinationID)
        {
            string sql = "DELETE FROM VACCINATION WHERE VACCINATIONID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", vaccinationID)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }
    }
}
