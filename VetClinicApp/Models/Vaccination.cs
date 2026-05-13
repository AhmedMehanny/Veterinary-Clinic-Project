using System;

namespace VetClinicApp
{
    public class Vaccination
    {
        public int VaccinationID { get; set; }
        public int VisitID { get; set; }
        public int InventoryID { get; set; }
        public string VaccineType { get; set; }
        public DateTime AdministeredDate { get; set; }
        public DateTime? NextBoosterDue { get; set; }
    }
}
