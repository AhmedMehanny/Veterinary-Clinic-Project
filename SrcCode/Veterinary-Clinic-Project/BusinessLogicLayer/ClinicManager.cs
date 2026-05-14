using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class ClinicManager
    {
        private readonly ClinicRepository _repo = new ClinicRepository();

        public List<Clinic> GetAllClinics() => _repo.GetAll();
        public Clinic GetClinicById(int id) => _repo.GetById(id);
        public bool AddClinic(Clinic clinic) => _repo.Insert(clinic) > 0;
        public bool UpdateClinic(Clinic clinic) => _repo.Update(clinic) > 0;
        public bool DeleteClinic(int id) => _repo.Delete(id) > 0;
    }
}