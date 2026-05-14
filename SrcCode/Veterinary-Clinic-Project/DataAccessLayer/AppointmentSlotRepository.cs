using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DataAccessLayer
{
    public class AppointmentSlotRepository
    {
        // الحصول على جميع فترات المواعيد
        public List<AppointmentSlot> GetAll()
        {
            var list = new List<AppointmentSlot>();
            string query = @"
                SELECT SLOTID, ATTRIBUTE_70 AS VetClinicId, VISITID, SLOTDATETIME, DURATIONMINUTES, STATUS
                FROM APPOINTMENT_SLOT
                ORDER BY SLOTDATETIME";
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // الحصول على موعد بواسطة المعرف
        public AppointmentSlot GetById(int slotId)
        {
            string query = @"
                SELECT SLOTID, ATTRIBUTE_70, VISITID, SLOTDATETIME, DURATIONMINUTES, STATUS
                FROM APPOINTMENT_SLOT
                WHERE SLOTID = @SlotId";
            var dt = DBHandler.ExecuteQuery(query, CommandType.Text,
                new[] { new SqlParameter("@SlotId", slotId) });
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        // الحصول على جميع المواعيد لعيادة معينة (VetClinicId)
        public List<AppointmentSlot> GetByVetClinicId(int vetClinicId)
        {
            var list = new List<AppointmentSlot>();
            string query = @"
                SELECT SLOTID, ATTRIBUTE_70, VISITID, SLOTDATETIME, DURATIONMINUTES, STATUS
                FROM APPOINTMENT_SLOT
                WHERE ATTRIBUTE_70 = @VetClinicId
                ORDER BY SLOTDATETIME";
            var dt = DBHandler.ExecuteQuery(query, CommandType.Text,
                new[] { new SqlParameter("@VetClinicId", vetClinicId) });
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // الحصول على المواعيد المتاحة (STATUS = 'Available' أو 'Free')
        public List<AppointmentSlot> GetAvailableSlots()
        {
            var list = new List<AppointmentSlot>();
            string query = @"
                SELECT SLOTID, ATTRIBUTE_70, VISITID, SLOTDATETIME, DURATIONMINUTES, STATUS
                FROM APPOINTMENT_SLOT
                WHERE STATUS = 'Available'
                ORDER BY SLOTDATETIME";
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // الحصول على المواعيد المحجوزة (STATUS = 'Booked')
        public List<AppointmentSlot> GetBookedSlots()
        {
            var list = new List<AppointmentSlot>();
            string query = @"
                SELECT SLOTID, ATTRIBUTE_70, VISITID, SLOTDATETIME, DURATIONMINUTES, STATUS
                FROM APPOINTMENT_SLOT
                WHERE STATUS = 'Booked'
                ORDER BY SLOTDATETIME";
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // إضافة موعد جديد
        public int Insert(AppointmentSlot slot)
        {
            string query = @"
                INSERT INTO APPOINTMENT_SLOT (ATTRIBUTE_70, VISITID, SLOTDATETIME, DURATIONMINUTES, STATUS)
                VALUES (@VetClinicId, @VisitId, @SlotDateTime, @DurationMinutes, @Status);
                SELECT SCOPE_IDENTITY();";
            var p = new[]
            {
                new SqlParameter("@VetClinicId", slot.VetClinicId),
                new SqlParameter("@VisitId", slot.VisitId),
                new SqlParameter("@SlotDateTime", slot.SlotDateTime),
                new SqlParameter("@DurationMinutes", (object)slot.DurationMinutes ?? DBNull.Value),
                new SqlParameter("@Status", (object)slot.Status ?? DBNull.Value)
            };
            return Convert.ToInt32(DBHandler.ExecuteScalar(query, CommandType.Text, p));
        }

        // تحديث موعد
        public int Update(AppointmentSlot slot)
        {
            string query = @"
                UPDATE APPOINTMENT_SLOT
                SET ATTRIBUTE_70 = @VetClinicId,
                    VISITID = @VisitId,
                    SLOTDATETIME = @SlotDateTime,
                    DURATIONMINUTES = @DurationMinutes,
                    STATUS = @Status
                WHERE SLOTID = @SlotId";
            var p = new[]
            {
                new SqlParameter("@SlotId", slot.SlotId),
                new SqlParameter("@VetClinicId", slot.VetClinicId),
                new SqlParameter("@VisitId", slot.VisitId),
                new SqlParameter("@SlotDateTime", slot.SlotDateTime),
                new SqlParameter("@DurationMinutes", (object)slot.DurationMinutes ?? DBNull.Value),
                new SqlParameter("@Status", (object)slot.Status ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(query, CommandType.Text, p);
        }

        // حذف موعد
        public int Delete(int slotId)
        {
            string query = "DELETE FROM APPOINTMENT_SLOT WHERE SLOTID = @SlotId";
            return DBHandler.ExecuteNonQuery(query, CommandType.Text,
                new[] { new SqlParameter("@SlotId", slotId) });
        }

        // تحويل DataRow إلى كائن AppointmentSlot
        private AppointmentSlot Map(DataRow row)
        {
            return new AppointmentSlot
            {
                SlotId = Convert.ToInt32(row["SLOTID"]),
                VetClinicId = Convert.ToInt32(row["ATTRIBUTE_70"]),
                VisitId = row["VISITID"] == DBNull.Value ? 0 : Convert.ToInt32(row["VISITID"]),
                SlotDateTime = Convert.ToDateTime(row["SLOTDATETIME"]),
                DurationMinutes = row["DURATIONMINUTES"] == DBNull.Value ? null : (int?)Convert.ToInt32(row["DURATIONMINUTES"]),
                Status = row["STATUS"].ToString()
            };
        }
    }
}