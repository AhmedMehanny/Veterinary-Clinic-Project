using System;

namespace VetClinicApp
{
    public class AppointmentSlot
    {
        public int SlotID { get; set; }
        public int VetClinicID { get; set; }
        public int VisitID { get; set; }
        public DateTime SlotDateTime { get; set; }
        public int DurationMinutes { get; set; }
        public string Status { get; set; }
    }
}
