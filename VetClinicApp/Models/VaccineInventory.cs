using System;

namespace VetClinicApp
{
    public class VaccineInventory
    {
        public int InventoryID { get; set; }
        public int ClinicID { get; set; }
        public string VaccineInventoryType { get; set; }
        public string BatchNumber { get; set; }
        public string SupplierName { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int QuantityAvailable { get; set; }
        public int ReorderThreshold { get; set; }
    }
}
