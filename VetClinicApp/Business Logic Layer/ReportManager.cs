using System;
using System.Data;

namespace VetClinicApp
{
    public class ReportManager
    {
        private readonly PetRepository         _petRepo  = new PetRepository();
        private readonly VisitRepository       _visRepo  = new VisitRepository();
        private readonly VaccinationRepository _vacRepo  = new VaccinationRepository();
        private readonly OwnerRepository       _ownRepo  = new OwnerRepository();

        public DataTable Report1_PetsWithOwners()
        {
            return _petRepo.GetPetsWithOwners();
        }

        public DataTable Report2_VisitsWithDetails()
        {
            return _visRepo.GetVisitsWithDetails();
        }

        public DataTable Report3_VaccinationsPerPet()
        {
            return _visRepo.GetVaccinationCountPerPet();
        }

        public DataTable Report4_PetsNoVisit6Months()
        {
            return _petRepo.GetPetsNoVisit6Months();
        }

        public DataTable Report5_SearchOwners(string phone, string email)
        {
            return _ownRepo.Search(phone, email);
        }

        public DataTable Report6_UpcomingReminders()
        {
            return _vacRepo.GetUpcomingReminders();
        }

        public DataTable Report7_LowStockInventory()
        {
            return _vacRepo.GetLowStockInventory();
        }

        public DataTable Report8_VisitsPerClinic()
        {
            return _visRepo.GetVisitsPerClinic();
        }
    }
}
