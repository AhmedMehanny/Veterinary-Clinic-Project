using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class AppointmentSlotManager
    {
        private readonly AppointmentSlotRepository _repo = new AppointmentSlotRepository();

        public List<AppointmentSlot> GetAllSlots() => _repo.GetAll();
        public AppointmentSlot GetSlotById(int slotId) => _repo.GetById(slotId);
        public List<AppointmentSlot> GetAvailableSlots() => _repo.GetAvailableSlots();
        public List<AppointmentSlot> GetSlotsByVetClinicId(int vetClinicId) => _repo.GetByVetClinicId(vetClinicId);
        public bool AddSlot(AppointmentSlot slot) => _repo.Insert(slot) > 0;
        public bool UpdateSlot(AppointmentSlot slot) => _repo.Update(slot) > 0;
        public bool DeleteSlot(int slotId) => _repo.Delete(slotId) > 0;
    }
}