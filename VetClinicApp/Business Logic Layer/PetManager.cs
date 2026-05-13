using System;
using System.Data;

namespace VetClinicApp
{
    public class PetManager
    {
        private readonly PetRepository _repo = new PetRepository();

        public DataTable GetAllPets()
        {
            return _repo.GetAll();
        }

        public DataTable GetPetsWithOwners()
        {
            return _repo.GetPetsWithOwners();
        }

        public string AddPet(Pet p)
        {
            string validationError = Validate(p);
            if (validationError != null)
                return validationError;

            _repo.Insert(p);
            return "Pet added successfully.";
        }

        public string UpdatePet(Pet p)
        {
            string validationError = Validate(p);
            if (validationError != null)
                return validationError;

            _repo.Update(p);
            return "Pet updated successfully.";
        }

        public string DeletePet(int id)
        {
            _repo.Delete(id);
            return "Pet deleted.";
        }

        private string Validate(Pet p)
        {
            if (string.IsNullOrWhiteSpace(p.PetName))
                return "Pet name is required.";

            if (string.IsNullOrWhiteSpace(p.Species))
                return "Species is required.";

            if (p.Age < 0 || p.Age > 50)
                return "Age must be between 0 and 50.";

            if (p.OwnerID <= 0)
                return "Please select an owner.";

            return null;
        }
    }
}
