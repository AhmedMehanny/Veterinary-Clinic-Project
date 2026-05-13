//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BusinessLogicLayer
//{
//    internal class PetManager
//    {
//    }
//}

using System;
using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class PetManager
    {
        private readonly PetRepository _petRepository;

        public PetManager()
        {
            _petRepository = new PetRepository();
        }

        public List<Pet> GetAllPets()
        {
            try
            {
                return _petRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllPets: {ex.Message}");
                throw;
            }
        }

        public Pet GetPetById(int petId)
        {
            if (petId <= 0)
                throw new ArgumentException("Invalid pet ID");

            try
            {
                return _petRepository.GetById(petId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetPetById: {ex.Message}");
                throw;
            }
        }

        public List<Pet> GetPetsByOwner(int ownerId)
        {
            if (ownerId <= 0)
                throw new ArgumentException("Invalid owner ID");

            try
            {
                return _petRepository.GetByOwnerId(ownerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetPetsByOwner: {ex.Message}");
                throw;
            }
        }

        public bool AddPet(Pet pet)
        {
            if (pet == null)
                throw new ArgumentNullException(nameof(pet));

            ValidatePet(pet);

            try
            {
                return _petRepository.Insert(pet) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddPet: {ex.Message}");
                throw;
            }
        }

        public bool UpdatePet(Pet pet)
        {
            if (pet == null)
                throw new ArgumentNullException(nameof(pet));
            if (pet.PetId <= 0)
                throw new ArgumentException("Pet ID must be positive");

            ValidatePet(pet);

            try
            {
                return _petRepository.Update(pet) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdatePet: {ex.Message}");
                throw;
            }
        }

        public bool DeletePet(int petId)
        {
            if (petId <= 0)
                throw new ArgumentException("Invalid pet ID");

            try
            {
                // Check if pet has visits/vaccinations before delete (business rule)
                // if (HasRelatedRecords(petId)) throw new Exception("Cannot delete pet with visit history");
                return _petRepository.Delete(petId) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeletePet: {ex.Message}");
                throw;
            }
        }

        private void ValidatePet(Pet pet)
        {
            if (string.IsNullOrWhiteSpace(pet.PetName))
                throw new ArgumentException("Pet name is required");
            if (string.IsNullOrWhiteSpace(pet.Species))
                throw new ArgumentException("Species is required");
            if (pet.OwnerId <= 0)
                throw new ArgumentException("Owner ID is required");
            if (pet.Age < 0)
                throw new ArgumentException("Age cannot be negative");
        }
    }
}