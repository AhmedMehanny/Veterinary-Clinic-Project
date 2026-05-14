using System;
using System.Collections.Generic;
using System.Data;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class ReportManager
    {
        private readonly ReportRepository _repo = new ReportRepository();

        public List<Vaccination> GetBoosterDueReport(int daysAhead = 30)
            => _repo.GetBoosterDueBetween(DateTime.Today, DateTime.Today.AddDays(daysAhead));
        public DataTable GetClinicVisitStatistics(DateTime start, DateTime end)
            => _repo.GetVisitStatistics(start, end);
        public List<VaccineInventory> GetLowStockReport()
            => _repo.GetLowStockInventory();
        public DataTable GetOwnerPetSummary()
            => _repo.GetOwnerPetSummary();
        public DataTable GetClinicRevenueReport(DateTime start, DateTime end)
            => _repo.GetClinicRevenue(start, end);
    }
}