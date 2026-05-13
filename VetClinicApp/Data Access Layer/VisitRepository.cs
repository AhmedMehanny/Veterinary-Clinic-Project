using System;
using System.Data;
using System.Data.SqlClient;

namespace VetClinicApp
{
    public class VisitRepository
    {
        public DataTable GetAll()
        {
            string sql = "SELECT * FROM MEDICAL_VISIT ORDER BY VISITDATE DESC";
            return DBHandler.ExecuteQuery(sql);
        }

        public DataTable GetVisitsWithDetails()
        {
            string sql = @"SELECT mv.VISITID, mv.VISITDATE, mv.VISITSTATUS,
                                  p.PETNAME, p.SPECIES,
                                  v.VETFIRSTNAME + ' ' + v.VETLASTNAME AS VetName,
                                  c.CLINICNAME,
                                  a.SLOTDATETIME, a.DURATIONMINUTES
                           FROM MEDICAL_VISIT mv
                           INNER JOIN PET p ON mv.PETID = p.PETID
                           INNER JOIN APPOINTMENT_SLOT a ON mv.SLOTID = a.SLOTID
                           INNER JOIN VET_CLINIC vc ON a.ATTRIBUTE_70 = vc.ATTRIBUTE_70
                           INNER JOIN VETERINARIAN v ON vc.VETID = v.VETID
                           INNER JOIN CLINIC c ON vc.CLINICID = c.CLINICID
                           ORDER BY mv.VISITDATE DESC";
            return DBHandler.ExecuteQuery(sql);
        }

        public DataTable GetVaccinationCountPerPet()
        {
            string sql = @"SELECT p.PETNAME, p.SPECIES,
                                  COUNT(va.VACCINATIONID) AS TotalVaccinations
                           FROM PET p
                           LEFT JOIN MEDICAL_VISIT mv ON p.PETID = mv.PETID
                           LEFT JOIN VACCINATION va ON mv.VISITID = va.VISITID
                           GROUP BY p.PETID, p.PETNAME, p.SPECIES
                           ORDER BY TotalVaccinations DESC";
            return DBHandler.ExecuteQuery(sql);
        }

        public DataTable GetVisitsPerClinic()
        {
            string sql = @"SELECT c.CLINICNAME, c.LOCATION,
                                  COUNT(mv.VISITID) AS TotalVisits
                           FROM CLINIC c
                           LEFT JOIN VET_CLINIC vc ON c.CLINICID = vc.CLINICID
                           LEFT JOIN APPOINTMENT_SLOT a ON vc.ATTRIBUTE_70 = a.ATTRIBUTE_70
                           LEFT JOIN MEDICAL_VISIT mv ON a.SLOTID = mv.SLOTID
                           GROUP BY c.CLINICID, c.CLINICNAME, c.LOCATION
                           ORDER BY TotalVisits DESC";
            return DBHandler.ExecuteQuery(sql);
        }

        public int Insert(Visit v)
        {
            string sql = @"INSERT INTO MEDICAL_VISIT (NOTEID, PETID, SLOTID, VISITDATE, VISITSTATUS)
                           VALUES (@nid, @pid, @sid, @vd, @vs)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@nid", v.NoteID == 0 ? (object)DBNull.Value : v.NoteID),
                new SqlParameter("@pid", v.PetID),
                new SqlParameter("@sid", v.SlotID),
                new SqlParameter("@vd", v.VisitDate),
                new SqlParameter("@vs", (object)v.VisitStatus ?? DBNull.Value)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }

        public int Update(Visit v)
        {
            string sql = @"UPDATE MEDICAL_VISIT SET PETID = @pid, SLOTID = @sid,
                           VISITDATE = @vd, VISITSTATUS = @vs
                           WHERE VISITID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@pid", v.PetID),
                new SqlParameter("@sid", v.SlotID),
                new SqlParameter("@vd", v.VisitDate),
                new SqlParameter("@vs", (object)v.VisitStatus ?? DBNull.Value),
                new SqlParameter("@id", v.VisitID)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }

        public int UpdateStatus(int visitID, string status)
        {
            string sql = "UPDATE MEDICAL_VISIT SET VISITSTATUS = @status WHERE VISITID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@status", (object)status ?? DBNull.Value),
                new SqlParameter("@id", visitID)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }

        public int Delete(int visitID)
        {
            string sql = "DELETE FROM MEDICAL_VISIT WHERE VISITID = @id";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@id", visitID)
            };
            return DBHandler.ExecuteNonQuery(sql, parameters);
        }
    }
}
