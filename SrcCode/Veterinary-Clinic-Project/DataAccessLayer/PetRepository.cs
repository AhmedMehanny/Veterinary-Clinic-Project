using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Models;

namespace DataAccessLayer
{
	public class PetRepository
	{
		// الحصول على جميع الحيوانات الأليفة مع بيانات مالكها
		public List<Pet> GetAll()
		{
			var list = new List<Pet>();
			string query = @"
                SELECT p.PETID, p.OWNERID, p.PETNAME, p.SPECIES, p.BREED, p.AGE,
                       o.OFRISTNAME AS OwnerFirstName, o.OLASTNAME AS OwnerLastName, o.OPHONE AS OwnerPhone
                FROM PET p
                INNER JOIN OWNER o ON p.OWNERID = o.OWNERID";
			DataTable dt = DBHandler.ExecuteQuery(query, CommandType.Text);
			foreach (DataRow row in dt.Rows)
				list.Add(MapPet(row));
			return list;
		}

		// الحصول على حيوان بواسطة المعرف الخاص به
		public Pet GetById(int petId)
		{
			string query = @"
                SELECT p.PETID, p.OWNERID, p.PETNAME, p.SPECIES, p.BREED, p.AGE,
                       o.OFRISTNAME, o.OLASTNAME, o.OPHONE
                FROM PET p
                INNER JOIN OWNER o ON p.OWNERID = o.OWNERID
                WHERE p.PETID = @PetId";
			var dt = DBHandler.ExecuteQuery(query, CommandType.Text, new[] { new SqlParameter("@PetId", petId) });
			if (dt.Rows.Count == 0) return null;
			return MapPet(dt.Rows[0]);
		}

		// الحصول على جميع الحيوانات التي تتبع مالكاً معيناً
		public List<Pet> GetByOwnerId(int ownerId)
		{
			var list = new List<Pet>();
			string query = @"
                SELECT p.PETID, p.OWNERID, p.PETNAME, p.SPECIES, p.BREED, p.AGE,
                       o.OFRISTNAME, o.OLASTNAME, o.OPHONE
                FROM PET p
                INNER JOIN OWNER o ON p.OWNERID = o.OWNERID
                WHERE p.OWNERID = @OwnerId";
			var dt = DBHandler.ExecuteQuery(query, CommandType.Text, new[] { new SqlParameter("@OwnerId", ownerId) });
			foreach (DataRow row in dt.Rows)
				list.Add(MapPet(row));
			return list;
		}

		// إضافة حيوان جديد
		public int Insert(Pet pet)
		{
			string query = @"
                INSERT INTO PET (OWNERID, PETNAME, SPECIES, BREED, AGE)
                VALUES (@OwnerId, @PetName, @Species, @Breed, @Age);
                SELECT SCOPE_IDENTITY();";
			var p = new[]
			{
				new SqlParameter("@OwnerId", pet.OwnerId),
				new SqlParameter("@PetName", pet.PetName),
				new SqlParameter("@Species", pet.Species),
				new SqlParameter("@Breed", (object)pet.Breed ?? DBNull.Value),
				new SqlParameter("@Age", pet.Age)
			};
			return Convert.ToInt32(DBHandler.ExecuteScalar(query, CommandType.Text, p));
		}

		// تحديث بيانات حيوان
		public int Update(Pet pet)
		{
			string query = @"
                UPDATE PET
                SET OWNERID = @OwnerId, PETNAME = @PetName, SPECIES = @Species, BREED = @Breed, AGE = @Age
                WHERE PETID = @PetId";
			var p = new[]
			{
				new SqlParameter("@PetId", pet.PetId),
				new SqlParameter("@OwnerId", pet.OwnerId),
				new SqlParameter("@PetName", pet.PetName),
				new SqlParameter("@Species", pet.Species),
				new SqlParameter("@Breed", (object)pet.Breed ?? DBNull.Value),
				new SqlParameter("@Age", pet.Age)
			};
			return DBHandler.ExecuteNonQuery(query, CommandType.Text, p);
		}

		// حذف حيوان
		public int Delete(int petId)
		{
			string query = "DELETE FROM PET WHERE PETID = @PetId";
			return DBHandler.ExecuteNonQuery(query, CommandType.Text, new[] { new SqlParameter("@PetId", petId) });
		}

		// تحويل صف DataRow إلى كائن Pet
		private Pet MapPet(DataRow row)
		{
			return new Pet
			{
				PetId = Convert.ToInt32(row["PETID"]),
				OwnerId = Convert.ToInt32(row["OWNERID"]),
				PetName = row["PETNAME"].ToString(),
				Species = row["SPECIES"].ToString(),
				Breed = row["BREED"]?.ToString(),
				Age = row["AGE"] != DBNull.Value ? Convert.ToInt32(row["AGE"]) : 0,
				OwnerFirstName = row["OwnerFirstName"]?.ToString(),
				OwnerLastName = row["OwnerLastName"]?.ToString(),
				OwnerPhone = row["OwnerPhone"]?.ToString()
			};
		}
	}
}