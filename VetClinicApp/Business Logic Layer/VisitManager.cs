using System;
using System.Data;

namespace VetClinicApp
{
    public class VisitManager
    {
        private readonly VisitRepository _repo = new VisitRepository();

        public DataTable GetAllVisits()
        {
            return _repo.GetAll();
        }

        public DataTable GetVisitsWithDetails()
        {
            return _repo.GetVisitsWithDetails();
        }

        public string AddVisit(Visit v)
        {
            string validationError = Validate(v);
            if (validationError != null)
                return validationError;

            _repo.Insert(v);
            return "Visit added successfully.";
        }

        public string UpdateVisit(Visit v)
        {
            string validationError = Validate(v);
            if (validationError != null)
                return validationError;

            _repo.Update(v);
            return "Visit updated successfully.";
        }

        public string UpdateVisitStatus(int id, string status)
        {
            if (string.IsNullOrWhiteSpace(status))
                return "Please select a status.";

            _repo.UpdateStatus(id, status);
            return "Visit status updated successfully.";
        }

        public string DeleteVisit(int id)
        {
            _repo.Delete(id);
            return "Visit deleted.";
        }

        private string Validate(Visit v)
        {
            if (v.PetID <= 0)
                return "Please select a pet.";

            if (v.SlotID <= 0)
                return "Please select an appointment slot.";

            if (v.VisitDate == default(DateTime))
                return "Visit date is required.";

            if (string.IsNullOrWhiteSpace(v.VisitStatus))
                return "Please select a status.";

            return null;
        }
    }
}
