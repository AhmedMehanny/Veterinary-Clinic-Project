using System;
using System.Data;

namespace VetClinicApp
{
    public class VaccinationManager
    {
        private readonly VaccinationRepository _repo = new VaccinationRepository();

        public DataTable GetAllVaccinations()
        {
            return _repo.GetAll();
        }

        public string AddVaccination(Vaccination v)
        {
            string validationError = Validate(v);
            if (validationError != null)
                return validationError;

            _repo.Insert(v);
            return "Vaccination recorded successfully.";
        }

        public string UpdateVaccination(Vaccination v)
        {
            string validationError = Validate(v);
            if (validationError != null)
                return validationError;

            _repo.Update(v);
            return "Vaccination updated successfully.";
        }

        public string DeleteVaccination(int id)
        {
            _repo.Delete(id);
            return "Vaccination deleted.";
        }

        private string Validate(Vaccination v)
        {
            if (v.VisitID <= 0)
                return "Please select a visit.";

            if (v.InventoryID <= 0)
                return "Please select a vaccine from inventory.";

            if (string.IsNullOrWhiteSpace(v.VaccineType))
                return "Vaccine type is required.";

            return null;
        }
    }
}
