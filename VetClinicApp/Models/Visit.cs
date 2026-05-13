using System;

namespace VetClinicApp
{
    public class Visit
    {
        public int VisitID { get; set; }
        public int NoteID { get; set; }
        public int PetID { get; set; }
        public int SlotID { get; set; }
        public DateTime VisitDate { get; set; }
        public string VisitStatus { get; set; }
    }
}
