//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BusinessLogicLayer
//{
//    internal class VisitManager
//    {
//    }
//}

using System;
using System.Collections.Generic;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class VisitManager
    {
        private readonly VisitRepository _visitRepository;

        public VisitManager()
        {
            _visitRepository = new VisitRepository();
        }

        public List<Visit> GetAllVisits()
        {
            try
            {
                return _visitRepository.GetAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllVisits: {ex.Message}");
                throw;
            }
        }

        public Visit GetVisitById(int visitId)
        {
            if (visitId <= 0)
                throw new ArgumentException("Invalid visit ID");

            try
            {
                return _visitRepository.GetById(visitId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetVisitById: {ex.Message}");
                throw;
            }
        }

        public List<Visit> GetVisitsByPet(int petId)
        {
            if (petId <= 0)
                throw new ArgumentException("Invalid pet ID");

            try
            {
                return _visitRepository.GetByPetId(petId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetVisitsByPet: {ex.Message}");
                throw;
            }
        }

        public bool ScheduleVisit(Visit visit)
        {
            if (visit == null)
                throw new ArgumentNullException(nameof(visit));

            ValidateVisit(visit);

            try
            {
                // Ensure appointment slot is available (call to AppointmentSlotManager)
                // For brevity, assume repository handles concurrency
                return _visitRepository.Insert(visit) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ScheduleVisit: {ex.Message}");
                throw;
            }
        }

        public bool UpdateVisit(Visit visit)
        {
            if (visit == null)
                throw new ArgumentNullException(nameof(visit));
            if (visit.VisitId <= 0)
                throw new ArgumentException("Visit ID must be positive");

            ValidateVisit(visit);

            try
            {
                return _visitRepository.Update(visit) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateVisit: {ex.Message}");
                throw;
            }
        }

        public bool CancelVisit(int visitId)
        {
            if (visitId <= 0)
                throw new ArgumentException("Invalid visit ID");

            try
            {
                var visit = GetVisitById(visitId);
                if (visit == null)
                    throw new Exception("Visit not found");

                visit.VisitStatus = "Cancelled";
                return _visitRepository.Update(visit) > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CancelVisit: {ex.Message}");
                throw;
            }
        }

        private void ValidateVisit(Visit visit)
        {
            if (visit.PetId <= 0)
                throw new ArgumentException("Pet ID is required");
            if (visit.SlotId <= 0)
                throw new ArgumentException("Appointment slot is required");
            if (visit.VisitDate == DateTime.MinValue)
                throw new ArgumentException("Visit date is required");
            if (string.IsNullOrWhiteSpace(visit.VisitStatus))
                visit.VisitStatus = "Scheduled";
        }
    }
}