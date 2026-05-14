using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class ReminderManager
    {
        private readonly ReminderRepository _repo = new ReminderRepository();

        public List<Reminder> GetAllReminders() => _repo.GetAll();
        public Reminder GetReminderById(int id) => _repo.GetById(id);
        public List<Reminder> GetRemindersByOwner(int ownerId) => _repo.GetByOwnerId(ownerId);
        public List<Reminder> GetPendingReminders() => _repo.GetPendingReminders();
        public bool AddReminder(Reminder reminder) => _repo.Insert(reminder) > 0;
        public bool UpdateReminder(Reminder reminder) => _repo.Update(reminder) > 0;
        public bool UpdateReminderStatus(int id, string status) => _repo.UpdateStatus(id, status) > 0;
        public bool DeleteReminder(int id) => _repo.Delete(id) > 0;
    }
}