using System;
using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class VaccinationManager
    {
        private readonly VaccinationRepository _repo = new VaccinationRepository();

        public List<Vaccination> GetAllVaccinations() => _repo.GetAll();
        public Vaccination GetVaccinationById(int id) => _repo.GetById(id);
        public List<Vaccination> GetVaccinationsByPet(int petId) => _repo.GetByPetId(petId);
        public List<Vaccination> GetOverdueBoosters() => _repo.GetOverdueBoosters(DateTime.Today);
        public bool AddVaccination(Vaccination vacc) => _repo.Insert(vacc) > 0;
        public bool UpdateVaccination(Vaccination vacc) => _repo.Update(vacc) > 0;
    }
}