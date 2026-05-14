using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DataAccessLayer
{
    public class VisitRepository
    {
        // الحصول على جميع الزيارات مع بيانات الحيوان والمالك والطبيب والعيادة
        public List<Visit> GetAll()
        {
            var list = new List<Visit>();
            string query = @"
        SELECT v.VISITID, v.PETID, v.SLOTID, v.NOTEID, v.VISITDATE, v.VISITSTATUS,
               p.PETNAME, p.SPECIES,
               o.OFRISTNAME AS OwnerFirstName, o.OLASTNAME AS OwnerLastName, o.OPHONE AS OwnerPhone,
               vet.VETFIRSTNAME, vet.VETLASTNAME, vet.SPECIALTY AS VetSpecialty,
               c.CLINICNAME, c.LOCATION AS ClinicLocation,
               cn.DIAGNOSIS, cn.TREATMENTPLAN
        FROM MEDICAL_VISIT v
        INNER JOIN PET p ON v.PETID = p.PETID
        INNER JOIN OWNER o ON p.OWNERID = o.OWNERID
        LEFT JOIN APPOINTMENT_SLOT a ON v.SLOTID = a.SLOTID
        LEFT JOIN VET_CLINIC vc ON a.ATTRIBUTE_70 = vc.ATTRIBUTE_70
        LEFT JOIN VETERINARIAN vet ON vc.VETID = vet.VETID   -- ✅ تم التصحيح من Vet إلى VETERINARIAN
        LEFT JOIN CLINIC c ON vc.CLINICID = c.CLINICID
        LEFT JOIN CLINICAL_NOTE cn ON v.NOTEID = cn.NOTEID";
            DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
            foreach (DataRow row in dt.Rows)
                list.Add(MapVisit(row));
            return list;
        }

        // الحصول على زيارة بواسطة معرفها
        public Visit GetById(int visitId)
        {
            string query = @"
                SELECT v.VISITID, v.PETID, v.SLOTID, v.NOTEID, v.VISITDATE, v.VISITSTATUS,
                       p.PETNAME, p.SPECIES,
                       o.OFRISTNAME, o.OLASTNAME, o.OPHONE,
                       vet.VETFIRSTNAME, vet.VETLASTNAME, vet.SPECIALTY,
                       c.CLINICNAME, c.LOCATION,
                       cn.DIAGNOSIS, cn.TREATMENTPLAN
                FROM MEDICAL_VISIT v
                INNER JOIN PET p ON v.PETID = p.PETID
                INNER JOIN OWNER o ON p.OWNERID = o.OWNERID
                LEFT JOIN APPOINTMENT_SLOT a ON v.SLOTID = a.SLOTID
                LEFT JOIN VET_CLINIC vc ON a.ATTRIBUTE_70 = vc.ATTRIBUTE_70
                LEFT JOIN VETERINARIAN vet ON vc.VETID = vet.VETID
                LEFT JOIN CLINIC c ON vc.CLINICID = c.CLINICID
                LEFT JOIN CLINICAL_NOTE cn ON v.NOTEID = cn.NOTEID
                WHERE v.VISITID = @VisitId";
            var dt = DBHandler.ExecuteQuery(query, CommandType.Text, new[] { new SqlParameter("@VisitId", visitId) });
            if (dt.Rows.Count == 0) return null;
            return MapVisit(dt.Rows[0]);
        }

        // الحصول على جميع الزيارات لحيوان معين
        public List<Visit> GetByPetId(int petId)
        {
            var list = new List<Visit>();
            string query = @"
                SELECT v.VISITID, v.PETID, v.SLOTID, v.NOTEID, v.VISITDATE, v.VISITSTATUS,
                       p.PETNAME, p.SPECIES,
                       o.OFRISTNAME, o.OLASTNAME, o.OPHONE,
                       vet.VETFIRSTNAME, vet.VETLASTNAME, vet.SPECIALTY,
                       c.CLINICNAME, c.LOCATION,
                       cn.DIAGNOSIS, cn.TREATMENTPLAN
                FROM MEDICAL_VISIT v
                INNER JOIN PET p ON v.PETID = p.PETID
                INNER JOIN OWNER o ON p.OWNERID = o.OWNERID
                LEFT JOIN APPOINTMENT_SLOT a ON v.SLOTID = a.SLOTID
                LEFT JOIN VET_CLINIC vc ON a.ATTRIBUTE_70 = vc.ATTRIBUTE_70
                LEFT JOIN VETERINARIAN vet ON vc.VETID = vet.VETID
                LEFT JOIN CLINIC c ON vc.CLINICID = c.CLINICID
                LEFT JOIN CLINICAL_NOTE cn ON v.NOTEID = cn.NOTEID
                WHERE v.PETID = @PetId";
            var dt = DBHandler.ExecuteQuery(query, CommandType.Text, new[] { new SqlParameter("@PetId", petId) });
            foreach (DataRow row in dt.Rows)
                list.Add(MapVisit(row));
            return list;
        }

        // إضافة زيارة جديدة
        public int Insert(Visit visit)
        {
            string query = @"
                INSERT INTO MEDICAL_VISIT (NOTEID, PETID, SLOTID, VISITDATE, VISITSTATUS)
                VALUES (@NoteId, @PetId, @SlotId, @VisitDate, @VisitStatus);
                SELECT SCOPE_IDENTITY();";
            var p = new[]
            {
                new SqlParameter("@NoteId", visit.NoteId),
                new SqlParameter("@PetId", visit.PetId),
                new SqlParameter("@SlotId", visit.SlotId),
                new SqlParameter("@VisitDate", visit.VisitDate),
                new SqlParameter("@VisitStatus", visit.VisitStatus)
            };
            return Convert.ToInt32(DBHandler.ExecuteScalar(query, CommandType.Text, p));
        }

        // تحديث زيارة موجودة
        public int Update(Visit visit)
        {
            string query = @"
                UPDATE MEDICAL_VISIT
                SET NOTEID = @NoteId, PETID = @PetId, SLOTID = @SlotId,
                    VISITDATE = @VisitDate, VISITSTATUS = @VisitStatus
                WHERE VISITID = @VisitId";
            var p = new[]
            {
                new SqlParameter("@VisitId", visit.VisitId),
                new SqlParameter("@NoteId", visit.NoteId),
                new SqlParameter("@PetId", visit.PetId),
                new SqlParameter("@SlotId", visit.SlotId),
                new SqlParameter("@VisitDate", visit.VisitDate),
                new SqlParameter("@VisitStatus", visit.VisitStatus)
            };
            return DBHandler.ExecuteNonQuery(query, CommandType.Text, p);
        }

        // حذف زيارة (قد لا تستخدم حسب قواعد العمل)
        public int Delete(int visitId)
        {
            string query = "DELETE FROM MEDICAL_VISIT WHERE VISITID = @VisitId";
            return DBHandler.ExecuteNonQuery(query, CommandType.Text, new[] { new SqlParameter("@VisitId", visitId) });
        }

        // تحويل DataRow إلى كائن Visit
        private Visit MapVisit(DataRow row)
        {
            return new Visit
            {
                VisitId = Convert.ToInt32(row["VISITID"]),
                PetId = Convert.ToInt32(row["PETID"]),
                SlotId = row["SLOTID"] != DBNull.Value ? Convert.ToInt32(row["SLOTID"]) : 0,
                NoteId = row["NOTEID"] != DBNull.Value ? Convert.ToInt32(row["NOTEID"]) : 0,
                VisitDate = Convert.ToDateTime(row["VISITDATE"]),
                VisitStatus = row["VISITSTATUS"].ToString(),
                PetName = row["PETNAME"].ToString(),
                Species = row["SPECIES"].ToString(),
                OwnerFirstName = row["OwnerFirstName"]?.ToString(),
                OwnerLastName = row["OwnerLastName"]?.ToString(),
                OwnerPhone = row["OwnerPhone"]?.ToString(),
                VetFirstName = row["VETFIRSTNAME"]?.ToString(),
                VetLastName = row["VETLASTNAME"]?.ToString(),
                VetSpecialty = row["VetSpecialty"]?.ToString(),
                ClinicName = row["CLINICNAME"]?.ToString(),
                ClinicLocation = row["ClinicLocation"]?.ToString(),
                Diagnosis = row["DIAGNOSIS"]?.ToString(),
                TreatmentPlan = row["TREATMENTPLAN"]?.ToString()
            };
        }
    }
}