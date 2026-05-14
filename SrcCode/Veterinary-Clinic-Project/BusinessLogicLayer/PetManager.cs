using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class PetManager
    {
        private readonly PetRepository _repo = new PetRepository();

        public List<Pet> GetAllPets() => _repo.GetAll();
        public Pet GetPetById(int petId) => _repo.GetById(petId);
        public List<Pet> GetPetsByOwner(int ownerId) => _repo.GetByOwnerId(ownerId);
        public bool AddPet(Pet pet) => _repo.Insert(pet) > 0;
        public bool UpdatePet(Pet pet) => _repo.Update(pet) > 0;
        public bool DeletePet(int petId) => _repo.Delete(petId) > 0;
    }
}