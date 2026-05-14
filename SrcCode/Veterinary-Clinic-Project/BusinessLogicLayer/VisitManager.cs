using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class VisitManager
    {
        private readonly VisitRepository _repo = new VisitRepository();

        public List<Visit> GetAllVisits() => _repo.GetAll();
        public Visit GetVisitById(int visitId) => _repo.GetById(visitId);
        public List<Visit> GetVisitsByPet(int petId) => _repo.GetByPetId(petId);
        public bool AddVisit(Visit visit) => _repo.Insert(visit) > 0;
        public bool UpdateVisit(Visit visit) => _repo.Update(visit) > 0;
        public bool CancelVisit(int visitId)
        {
            var visit = _repo.GetById(visitId);
            if (visit == null) return false;
            visit.VisitStatus = "Cancelled";
            return _repo.Update(visit) > 0;
        }
    }
}