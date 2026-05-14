using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DataAccessLayer
{
    public class ReminderRepository
    {
        // الحصول على جميع التذكيرات
        public List<Reminder> GetAll()
        {
            var list = new List<Reminder>();
            string query = @"
                SELECT REMINDERID, OWNERID, VACCINATIONID, SCHEDULEDDATE, CHANNEL, REMINDESTATUS, SENTAT
                FROM REMINDER
                ORDER BY SCHEDULEDDATE DESC";
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // الحصول على تذكير بواسطة المعرف
        public Reminder GetById(int reminderId)
        {
            string query = @"
                SELECT REMINDERID, OWNERID, VACCINATIONID, SCHEDULEDDATE, CHANNEL, REMINDESTATUS, SENTAT
                FROM REMINDER
                WHERE REMINDERID = @ReminderId";
            var dt = DBHandler.ExecuteQuery(query, CommandType.Text,
                new[] { new SqlParameter("@ReminderId", reminderId) });
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        // الحصول على تذكيرات مالك معين
        public List<Reminder> GetByOwnerId(int ownerId)
        {
            var list = new List<Reminder>();
            string query = @"
                SELECT REMINDERID, OWNERID, VACCINATIONID, SCHEDULEDDATE, CHANNEL, REMINDESTATUS, SENTAT
                FROM REMINDER
                WHERE OWNERID = @OwnerId
                ORDER BY SCHEDULEDDATE DESC";
            var dt = DBHandler.ExecuteQuery(query, CommandType.Text,
                new[] { new SqlParameter("@OwnerId", ownerId) });
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // الحصول على تذكيرات متعلقة بتطعيم معين
        public List<Reminder> GetByVaccinationId(int vaccinationId)
        {
            var list = new List<Reminder>();
            string query = @"
                SELECT REMINDERID, OWNERID, VACCINATIONID, SCHEDULEDDATE, CHANNEL, REMINDESTATUS, SENTAT
                FROM REMINDER
                WHERE VACCINATIONID = @VaccinationId";
            var dt = DBHandler.ExecuteQuery(query, CommandType.Text,
                new[] { new SqlParameter("@VaccinationId", vaccinationId) });
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // الحصول على التذكيرات التي لم تُرسل بعد (ReminderStatus = 'Pending')
        public List<Reminder> GetPendingReminders()
        {
            var list = new List<Reminder>();
            string query = @"
                SELECT REMINDERID, OWNERID, VACCINATIONID, SCHEDULEDDATE, CHANNEL, REMINDESTATUS, SENTAT
                FROM REMINDER
                WHERE REMINDESTATUS = 'Pending' AND SCHEDULEDDATE <= GETDATE()
                ORDER BY SCHEDULEDDATE";
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // إضافة تذكير جديد
        public int Insert(Reminder reminder)
        {
            string query = @"
                INSERT INTO REMINDER (OWNERID, VACCINATIONID, SCHEDULEDDATE, CHANNEL, REMINDESTATUS, SENTAT)
                VALUES (@OwnerId, @VaccinationId, @ScheduledDate, @Channel, @ReminderStatus, @SentAt);
                SELECT SCOPE_IDENTITY();";
            var p = new[]
            {
                new SqlParameter("@OwnerId", reminder.OwnerId),
                new SqlParameter("@VaccinationId", reminder.VaccinationId),
                new SqlParameter("@ScheduledDate", reminder.ScheduledDate),
                new SqlParameter("@Channel", (object)reminder.Channel ?? DBNull.Value),
                new SqlParameter("@ReminderStatus", (object)reminder.ReminderStatus ?? DBNull.Value),
                new SqlParameter("@SentAt", (object)reminder.SentAt ?? DBNull.Value)
            };
            return Convert.ToInt32(DBHandler.ExecuteScalar(query, CommandType.Text, p));
        }

        // تحديث تذكير
        public int Update(Reminder reminder)
        {
            string query = @"
                UPDATE REMINDER
                SET OWNERID = @OwnerId,
                    VACCINATIONID = @VaccinationId,
                    SCHEDULEDDATE = @ScheduledDate,
                    CHANNEL = @Channel,
                    REMINDESTATUS = @ReminderStatus,
                    SENTAT = @SentAt
                WHERE REMINDERID = @ReminderId";
            var p = new[]
            {
                new SqlParameter("@ReminderId", reminder.ReminderId),
                new SqlParameter("@OwnerId", reminder.OwnerId),
                new SqlParameter("@VaccinationId", reminder.VaccinationId),
                new SqlParameter("@ScheduledDate", reminder.ScheduledDate),
                new SqlParameter("@Channel", (object)reminder.Channel ?? DBNull.Value),
                new SqlParameter("@ReminderStatus", (object)reminder.ReminderStatus ?? DBNull.Value),
                new SqlParameter("@SentAt", (object)reminder.SentAt ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(query, CommandType.Text, p);
        }

        // تحديث حالة التذكير فقط (مثلاً بعد الإرسال)
        public int UpdateStatus(int reminderId, string status, DateTime? sentAt = null)
        {
            string query = @"
                UPDATE REMINDER
                SET REMINDESTATUS = @Status,
                    SENTAT = @SentAt
                WHERE REMINDERID = @ReminderId";
            var p = new[]
            {
                new SqlParameter("@ReminderId", reminderId),
                new SqlParameter("@Status", status),
                new SqlParameter("@SentAt", (object)sentAt ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(query, CommandType.Text, p);
        }

        // حذف تذكير
        public int Delete(int reminderId)
        {
            string query = "DELETE FROM REMINDER WHERE REMINDERID = @ReminderId";
            return DBHandler.ExecuteNonQuery(query, CommandType.Text,
                new[] { new SqlParameter("@ReminderId", reminderId) });
        }

        // تحويل DataRow إلى كائن Reminder
        private Reminder Map(DataRow row)
        {
            return new Reminder
            {
                ReminderId = Convert.ToInt32(row["REMINDERID"]),
                OwnerId = Convert.ToInt32(row["OWNERID"]),
                VaccinationId = Convert.ToInt32(row["VACCINATIONID"]),
                ScheduledDate = Convert.ToDateTime(row["SCHEDULEDDATE"]),
                Channel = row["CHANNEL"]?.ToString(),
                ReminderStatus = row["REMINDESTATUS"]?.ToString(),
                SentAt = row["SENTAT"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["SENTAT"])
            };
        }
    }
}