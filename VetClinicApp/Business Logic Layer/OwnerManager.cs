using System;
using System.Data;
using System.Text.RegularExpressions;

namespace VetClinicApp
{
    public class OwnerManager
    {
        private readonly OwnerRepository _repo = new OwnerRepository();

        public DataTable GetAllOwners()
        {
            return _repo.GetAll();
        }

        public DataTable SearchOwners(string phone, string email)
        {
            return _repo.Search(phone, email);
        }

        public string AddOwner(Owner o)
        {
            string validationError = Validate(o);
            if (validationError != null)
                return validationError;

            _repo.Insert(o);
            return "Owner added successfully.";
        }

        public string UpdateOwner(Owner o)
        {
            string validationError = Validate(o);
            if (validationError != null)
                return validationError;

            _repo.Update(o);
            return "Owner updated successfully.";
        }

        public string DeleteOwner(int id)
        {
            _repo.Delete(id);
            return "Owner deleted.";
        }

        private string Validate(Owner o)
        {
            if (string.IsNullOrWhiteSpace(o.FirstName))
                return "First name is required.";

            if (string.IsNullOrWhiteSpace(o.LastName))
                return "Last name is required.";

            if (string.IsNullOrWhiteSpace(o.Phone) ||
                !Regex.IsMatch(o.Phone, @"^\d{7,15}$"))
                return "Phone must be 7–15 digits.";

            if (string.IsNullOrWhiteSpace(o.Email) ||
                !o.Email.Contains("@") || !o.Email.Contains("."))
                return "Invalid email format.";

            return null;
        }
    }
}
