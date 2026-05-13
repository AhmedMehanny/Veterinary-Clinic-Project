//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BusinessLogicLayer
//{
//    internal class VaccinationManager
//    {
//    }
//}

using System;
using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class VaccinationManager
    {
        private readonly VaccinationRepository _vaccinationRepository;

        public VaccinationManager()
        {
            _vaccinationRepository = new VaccinationRepository();
        }

        public List<Vaccination> GetAllVaccinations()
        {
            try
            {
                return _vaccinationRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllVaccinations: {ex.Message}");
                throw;
            }
        }

        public Vaccination GetVaccinationById(int vaccinationId)
        {
            if (vaccinationId <= 0)
                throw new ArgumentException("Invalid vaccination ID");

            try
            {
                return _vaccinationRepository.GetById(vaccinationId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetVaccinationById: {ex.Message}");
                throw;
            }
        }

        public List<Vaccination> GetVaccinationsByPet(int petId)
        {
            if (petId <= 0)
                throw new ArgumentException("Invalid pet ID");

            try
            {
                return _vaccinationRepository.GetByPetId(petId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetVaccinationsByPet: {ex.Message}");
                throw;
            }
        }

        public List<Vaccination> GetOverdueBoosters()
        {
            try
            {
                return _vaccinationRepository.GetOverdueBoosters(DateTime.Today);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetOverdueBoosters: {ex.Message}");
                throw;
            }
        }

        public bool AddVaccination(Vaccination vaccination)
        {
            if (vaccination == null)
                throw new ArgumentNullException(nameof(vaccination));

            ValidateVaccination(vaccination);

            try
            {
                // Decrease inventory quantity
                // var invManager = new VaccineInventoryManager();
                // invManager.DecrementQuantity(vaccination.InventoryId, 1);
                return _vaccinationRepository.Insert(vaccination) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddVaccination: {ex.Message}");
                throw;
            }
        }

        public bool UpdateVaccination(Vaccination vaccination)
        {
            if (vaccination == null)
                throw new ArgumentNullException(nameof(vaccination));
            if (vaccination.VaccinationId <= 0)
                throw new ArgumentException("Vaccination ID must be positive");

            ValidateVaccination(vaccination);

            try
            {
                return _vaccinationRepository.Update(vaccination) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVaccination: {ex.Message}");
                throw;
            }
        }

        private void ValidateVaccination(Vaccination vaccination)
        {
            if (vaccination.VisitId <= 0)
                throw new ArgumentException("Visit ID is required");
            if (vaccination.InventoryId <= 0)
                throw new ArgumentException("Inventory ID is required");
            if (string.IsNullOrWhiteSpace(vaccination.VaccineType))
                throw new ArgumentException("Vaccine type is required");
            if (vaccination.AdministeredDate == DateTime.MinValue)
                throw new ArgumentException("Administered date is required");
        }
    }
}