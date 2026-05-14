using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class VetManager
    {
        private readonly VetRepository _repo = new VetRepository();

        public List<Veterinarian> GetAllVets() => _repo.GetAll();
        public List<Veterinarian> GetAllVetsWithClinics() => _repo.GetAllWithClinics();
        public Veterinarian GetVetById(int vetId) => _repo.GetById(vetId);
        public bool AddVet(Veterinarian vet) => _repo.Insert(vet) > 0;
        public bool UpdateVet(Veterinarian vet) => _repo.Update(vet) > 0;
        public bool DeleteVet(int vetId) => _repo.Delete(vetId) > 0;
        public List<Veterinarian> GetVetsByClinic(int clinicId) => _repo.GetByClinicId(clinicId);
    }
}