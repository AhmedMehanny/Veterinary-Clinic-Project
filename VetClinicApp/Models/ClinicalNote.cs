using System;

namespace VetClinicApp
{
    public class ClinicalNote
    {
        public int NoteID { get; set; }
        public int VisitID { get; set; }
        public decimal WeightKg { get; set; }
        public string Diagnosis { get; set; }
        public string TreatmentPlan { get; set; }
        public string GeneralObservations { get; set; }
        public DateTime RecordedAt { get; set; }
    }
}
