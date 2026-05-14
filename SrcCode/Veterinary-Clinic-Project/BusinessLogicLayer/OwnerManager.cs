using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class OwnerManager
    {
        private readonly OwnerRepository _repo = new OwnerRepository();

        public List<Owner> GetAllOwners() => _repo.GetAll();
        public Owner GetOwnerById(int ownerId) => _repo.GetById(ownerId);   // ✅ تم التصحيح
        public bool AddOwner(Owner owner) => _repo.Insert(owner) > 0;
        public bool UpdateOwner(Owner owner) => _repo.Update(owner) > 0;
        public bool DeleteOwner(int ownerId) => _repo.Delete(ownerId) > 0;
    }
}