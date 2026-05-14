using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DataAccessLayer
{
    public class ClinicalNoteRepository
    {
        // الحصول على جميع الملاحظات السريرية
        public List<ClinicalNote> GetAll()
        {
            var list = new List<ClinicalNote>();
            string query = @"
                SELECT NOTEID, VISITID, WEIGHTKG, DIAGNOSIS, TREATMENTPLAN, GENERALOBSERVATIONS, RECORDEDAT
                FROM CLINICAL_NOTE
                ORDER BY RECORDEDAT DESC";
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // الحصول على ملاحظة بواسطة المعرف
        public ClinicalNote GetById(int noteId)
        {
            string query = @"
                SELECT NOTEID, VISITID, WEIGHTKG, DIAGNOSIS, TREATMENTPLAN, GENERALOBSERVATIONS, RECORDEDAT
                FROM CLINICAL_NOTE
                WHERE NOTEID = @NoteId";
            var dt = DBHandler.ExecuteQuery(query, CommandType.Text,
                new[] { new SqlParameter("@NoteId", noteId) });
            if (dt.Rows.Count == 0) return null;
            return Map(dt.Rows[0]);
        }

        // الحصول على الملاحظات الخاصة بزيارة معينة
        public List<ClinicalNote> GetByVisitId(int visitId)
        {
            var list = new List<ClinicalNote>();
            string query = @"
                SELECT NOTEID, VISITID, WEIGHTKG, DIAGNOSIS, TREATMENTPLAN, GENERALOBSERVATIONS, RECORDEDAT
                FROM CLINICAL_NOTE
                WHERE VISITID = @VisitId
                ORDER BY RECORDEDAT DESC";
            var dt = DBHandler.ExecuteQuery(query, CommandType.Text,
                new[] { new SqlParameter("@VisitId", visitId) });
            foreach (DataRow row in dt.Rows)
                list.Add(Map(row));
            return list;
        }

        // إضافة ملاحظة سريرية جديدة
        public int Insert(ClinicalNote note)
        {
            string query = @"
                INSERT INTO CLINICAL_NOTE (VISITID, WEIGHTKG, DIAGNOSIS, TREATMENTPLAN, GENERALOBSERVATIONS, RECORDEDAT)
                VALUES (@VisitId, @WeightKg, @Diagnosis, @TreatmentPlan, @GeneralObservations, @RecordedAt);
                SELECT SCOPE_IDENTITY();";
            var p = new[]
            {
                new SqlParameter("@VisitId", note.VisitId),
                new SqlParameter("@WeightKg", (object)note.WeightKg ?? DBNull.Value),
                new SqlParameter("@Diagnosis", (object)note.Diagnosis ?? DBNull.Value),
                new SqlParameter("@TreatmentPlan", (object)note.TreatmentPlan ?? DBNull.Value),
                new SqlParameter("@GeneralObservations", (object)note.GeneralObservations ?? DBNull.Value),
                new SqlParameter("@RecordedAt", (object)note.RecordedAt ?? DBNull.Value)
            };
            return Convert.ToInt32(DBHandler.ExecuteScalar(query, CommandType.Text, p));
        }

        // تحديث ملاحظة سريرية
        public int Update(ClinicalNote note)
        {
            string query = @"
                UPDATE CLINICAL_NOTE
                SET VISITID = @VisitId,
                    WEIGHTKG = @WeightKg,
                    DIAGNOSIS = @Diagnosis,
                    TREATMENTPLAN = @TreatmentPlan,
                    GENERALOBSERVATIONS = @GeneralObservations,
                    RECORDEDAT = @RecordedAt
                WHERE NOTEID = @NoteId";
            var p = new[]
            {
                new SqlParameter("@NoteId", note.NoteId),
                new SqlParameter("@VisitId", note.VisitId),
                new SqlParameter("@WeightKg", (object)note.WeightKg ?? DBNull.Value),
                new SqlParameter("@Diagnosis", (object)note.Diagnosis ?? DBNull.Value),
                new SqlParameter("@TreatmentPlan", (object)note.TreatmentPlan ?? DBNull.Value),
                new SqlParameter("@GeneralObservations", (object)note.GeneralObservations ?? DBNull.Value),
                new SqlParameter("@RecordedAt", (object)note.RecordedAt ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(query, CommandType.Text, p);
        }

        // حذف ملاحظة سريرية
        public int Delete(int noteId)
        {
            string query = "DELETE FROM CLINICAL_NOTE WHERE NOTEID = @NoteId";
            return DBHandler.ExecuteNonQuery(query, CommandType.Text,
                new[] { new SqlParameter("@NoteId", noteId) });
        }

        // تحويل DataRow إلى كائن ClinicalNote
        private ClinicalNote Map(DataRow row)
        {
            return new ClinicalNote
            {
                NoteId = Convert.ToInt32(row["NOTEID"]),
                VisitId = Convert.ToInt32(row["VISITID"]),
                WeightKg = row["WEIGHTKG"] == DBNull.Value ? null : (decimal?)Convert.ToDecimal(row["WEIGHTKG"]),
                Diagnosis = row["DIAGNOSIS"]?.ToString(),
                TreatmentPlan = row["TREATMENTPLAN"]?.ToString(),
                GeneralObservations = row["GENERALOBSERVATIONS"]?.ToString(),
                RecordedAt = row["RECORDEDAT"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(row["RECORDEDAT"])
            };
        }
    }
}