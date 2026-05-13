using System;

namespace VetClinicApp
{
    public class Reminder
    {
        public int ReminderID { get; set; }
        public int OwnerID { get; set; }
        public int VaccinationID { get; set; }
        public DateTime ScheduledDate { get; set; }
        public string Channel { get; set; }
        public string ReminderStatus { get; set; }
        public DateTime? SentAt { get; set; }
    }
}
