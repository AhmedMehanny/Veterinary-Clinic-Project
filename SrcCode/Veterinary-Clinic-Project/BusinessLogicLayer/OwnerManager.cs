//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BusinessLogicLayer
//{
//    internal class OwnerManager
//    {
//    }
//}

using System;
using System.Collections.Generic;
using System.Data;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class OwnerManager
    {
        private readonly OwnerRepository _ownerRepository;

        public OwnerManager()
        {
            _ownerRepository = new OwnerRepository();
        }

        public List<Owner> GetAllOwners()
        {
            try
            {
                return _ownerRepository.GetAll();
            }
            catch (Exception ex)
            {
                // Log exception (integrate with your logging framework)
                Console.WriteLine($"Error in GetAllOwners: {ex.Message}");
                throw;
            }
        }

        public Owner GetOwnerById(int ownerId)
        {
            if (ownerId <= 0)
                throw new ArgumentException("Invalid owner ID");

            try
            {
                return _ownerRepository.GetById(ownerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetOwnerById: {ex.Message}");
                throw;
            }
        }

        public bool AddOwner(Owner owner)
        {
            if (owner == null)
                throw new ArgumentNullException(nameof(owner));

            ValidateOwner(owner);

            try
            {
                return _ownerRepository.Insert(owner) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddOwner: {ex.Message}");
                throw;
            }
        }

        public bool UpdateOwner(Owner owner)
        {
            if (owner == null)
                throw new ArgumentNullException(nameof(owner));
            if (owner.OwnerId <= 0)
                throw new ArgumentException("Owner ID must be positive");

            ValidateOwner(owner);

            try
            {
                return _ownerRepository.Update(owner) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateOwner: {ex.Message}");
                throw;
            }
        }

        public bool DeleteOwner(int ownerId)
        {
            if (ownerId <= 0)
                throw new ArgumentException("Invalid owner ID");

            try
            {
                // Optional: check if owner has pets before deletion
                // if (HasAssociatedPets(ownerId)) throw new Exception("Owner has pets");
                return _ownerRepository.Delete(ownerId) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteOwner: {ex.Message}");
                throw;
            }
        }

        private void ValidateOwner(Owner owner)
        {
            if (string.IsNullOrWhiteSpace(owner.FirstName))
                throw new ArgumentException("First name is required");
            if (string.IsNullOrWhiteSpace(owner.LastName))
                throw new ArgumentException("Last name is required");
        }
    }
}
