using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DataAccessLayer
{
	public class VaccinationRepository
	{
		// الحصول على جميع التطعيمات مع بيانات الزيارة والحيوان والمالك والعيادة والمخزون
		public List<Vaccination> GetAll()
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
                LEFT JOIN VACCINE_INVENTORY vi ON v.INVENTORYID = vi.INVENTORYID";
			DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
			foreach (DataRow row in dt.Rows)
				list.Add(MapVaccination(row));
			return list;
		}

		// الحصول على تطعيم بواسطة معرفه
		public Vaccination GetById(int vaccinationId)
		{
			string query = @"
                SELECT v.VACCINATIONID, v.VISITID, v.INVENTORYID, v.VACCINETYPE, v.ADMINISTEREDDATE, v.NEXTBOOSTERDUE,
                       p.PETID, p.PETNAME, p.SPECIES,
                       o.OFRISTNAME, o.OLASTNAME, o.OPHONE, o.OEMAIL,
                       mv.VISITDATE, mv.VISITSTATUS,
                       c.CLINICNAME, c.LOCATION,
                       vi.BATCHNUMBER, vi.SUPPLIERNAME
                FROM VACCINATION v
                INNER JOIN MEDICAL_VISIT mv ON v.VISITID = mv.VISITID
                INNER JOIN PET p ON mv.PETID = p.PETID
                INNER JOIN OWNER o ON p.OWNERID = o.OWNERID
                LEFT JOIN APPOINTMENT_SLOT a ON mv.SLOTID = a.SLOTID
                LEFT JOIN CLINIC c ON a.ATTRIBUTE_70 = c.CLINICID
                LEFT JOIN VACCINE_INVENTORY vi ON v.INVENTORYID = vi.INVENTORYID
                WHERE v.VACCINATIONID = @Id";
			var dt = DBHandler.ExecuteQuery(query, CommandType.Text, new[] { new SqlParameter("@Id", vaccinationId) });
			if (dt.Rows.Count == 0) return null;
			return MapVaccination(dt.Rows[0]);
		}

		// الحصول على جميع التطعيمات لحيوان معين (عبر PETID)
		public List<Vaccination> GetByPetId(int petId)
		{
			var list = new List<Vaccination>();
			string query = @"
                SELECT v.VACCINATIONID, v.VISITID, v.INVENTORYID, v.VACCINETYPE, v.ADMINISTEREDDATE, v.NEXTBOOSTERDUE,
                       p.PETID, p.PETNAME, p.SPECIES,
                       o.OFRISTNAME, o.OLASTNAME, o.OPHONE, o.OEMAIL,
                       mv.VISITDATE, mv.VISITSTATUS,
                       c.CLINICNAME, c.LOCATION,
                       vi.BATCHNUMBER, vi.SUPPLIERNAME
                FROM VACCINATION v
                INNER JOIN MEDICAL_VISIT mv ON v.VISITID = mv.VISITID
                INNER JOIN PET p ON mv.PETID = p.PETID
                INNER JOIN OWNER o ON p.OWNERID = o.OWNERID
                LEFT JOIN APPOINTMENT_SLOT a ON mv.SLOTID = a.SLOTID
                LEFT JOIN CLINIC c ON a.ATTRIBUTE_70 = c.CLINICID
                LEFT JOIN VACCINE_INVENTORY vi ON v.INVENTORYID = vi.INVENTORYID
                WHERE p.PETID = @PetId";
			var dt = DBHandler.ExecuteQuery(query, CommandType.Text, new[] { new SqlParameter("@PetId", petId) });
			foreach (DataRow row in dt.Rows)
				list.Add(MapVaccination(row));
			return list;
		}

		// الحصول على التطعيمات التي فات موعد جرعتها التنشيطية
		public List<Vaccination> GetOverdueBoosters(DateTime asOfDate)
		{
			var list = new List<Vaccination>();
			string query = @"
                SELECT v.VACCINATIONID, v.VISITID, v.INVENTORYID, v.VACCINETYPE, v.ADMINISTEREDDATE, v.NEXTBOOSTERDUE,
                       p.PETID, p.PETNAME, p.SPECIES,
                       o.OFRISTNAME, o.OLASTNAME, o.OPHONE, o.OEMAIL,
                       mv.VISITDATE, mv.VISITSTATUS,
                       c.CLINICNAME, c.LOCATION,
                       vi.BATCHNUMBER, vi.SUPPLIERNAME
                FROM VACCINATION v
                INNER JOIN MEDICAL_VISIT mv ON v.VISITID = mv.VISITID
                INNER JOIN PET p ON mv.PETID = p.PETID
                INNER JOIN OWNER o ON p.OWNERID = o.OWNERID
                LEFT JOIN APPOINTMENT_SLOT a ON mv.SLOTID = a.SLOTID
                LEFT JOIN CLINIC c ON a.ATTRIBUTE_70 = c.CLINICID
                LEFT JOIN VACCINE_INVENTORY vi ON v.INVENTORYID = vi.INVENTORYID
                WHERE v.NEXTBOOSTERDUE IS NOT NULL AND v.NEXTBOOSTERDUE <= @AsOfDate";
			var p = new[] { new SqlParameter("@AsOfDate", asOfDate) };
			DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text, p);
			foreach (DataRow row in dt.Rows)
				list.Add(MapVaccination(row));
			return list;
		}

		// إضافة تطعيم جديد
		public int Insert(Vaccination vacc)
		{
			string query = @"
                INSERT INTO VACCINATION (VISITID, INVENTORYID, VACCINETYPE, ADMINISTEREDDATE, NEXTBOOSTERDUE)
                VALUES (@VisitId, @InventoryId, @VaccineType, @AdministeredDate, @NextBoosterDue);
                SELECT SCOPE_IDENTITY();";
			var p = new[]
			{
				new SqlParameter("@VisitId", vacc.VisitId),
				new SqlParameter("@InventoryId", vacc.InventoryId),
				new SqlParameter("@VaccineType", vacc.VaccineType),
				new SqlParameter("@AdministeredDate", vacc.AdministeredDate),
				new SqlParameter("@NextBoosterDue", (object)vacc.NextBoosterDue ?? DBNull.Value)
			};
			return Convert.ToInt32(DBHandler.ExecuteScalar(query, CommandType.Text, p));
		}

		// تحديث تطعيم
		public int Update(Vaccination vacc)
		{
			string query = @"
                UPDATE VACCINATION
                SET VISITID = @VisitId, INVENTORYID = @InventoryId, VACCINETYPE = @VaccineType,
                    ADMINISTEREDDATE = @AdministeredDate, NEXTBOOSTERDUE = @NextBoosterDue
                WHERE VACCINATIONID = @Id";
			var p = new[]
			{
				new SqlParameter("@Id", vacc.VaccinationId),
				new SqlParameter("@VisitId", vacc.VisitId),
				new SqlParameter("@InventoryId", vacc.InventoryId),
				new SqlParameter("@VaccineType", vacc.VaccineType),
				new SqlParameter("@AdministeredDate", vacc.AdministeredDate),
				new SqlParameter("@NextBoosterDue", (object)vacc.NextBoosterDue ?? DBNull.Value)
			};
			return DBHandler.ExecuteNonQuery(query, CommandType.Text, p);
		}

		// حذف تطعيم (حسب الحاجة)
		public int Delete(int vaccinationId)
		{
			string query = "DELETE FROM VACCINATION WHERE VACCINATIONID = @Id";
			return DBHandler.ExecuteNonQuery(query, CommandType.Text, new[] { new SqlParameter("@Id", vaccinationId) });
		}

		// تحويل DataRow إلى كائن Vaccination
		private Vaccination MapVaccination(DataRow row)
		{
			return new Vaccination
			{
				VaccinationId = Convert.ToInt32(row["VACCINATIONID"]),
				VisitId = Convert.ToInt32(row["VISITID"]),
				InventoryId = Convert.ToInt32(row["INVENTORYID"]),
				VaccineType = row["VACCINETYPE"].ToString(),
				AdministeredDate = Convert.ToDateTime(row["ADMINISTEREDDATE"]),
				NextBoosterDue = row["NEXTBOOSTERDUE"] as DateTime?,
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
	}
}