using System;
using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class VaccineInventoryManager
    {
        private readonly VaccineInventoryRepository _repo = new VaccineInventoryRepository();

        public List<VaccineInventory> GetAllInventory() => _repo.GetAll();
        public VaccineInventory GetInventoryById(int id) => _repo.GetById(id);
        public List<VaccineInventory> GetInventoryByClinic(int clinicId) => _repo.GetByClinicId(clinicId);
        public List<VaccineInventory> GetLowStock() => _repo.GetLowStock();
        public List<VaccineInventory> GetExpiredStock(DateTime asOfDate) => _repo.GetExpiredStock(asOfDate);
        public bool AddInventory(VaccineInventory inv) => _repo.Insert(inv) > 0;
        public bool UpdateInventory(VaccineInventory inv) => _repo.Update(inv) > 0;
        public bool UpdateQuantity(int id, int newQty) => _repo.UpdateQuantity(id, newQty) > 0;
        public bool DecreaseQuantity(int id, int amount) => _repo.DecreaseQuantity(id, amount) > 0;
        public bool DeleteInventory(int id) => _repo.Delete(id) > 0;
    }
}