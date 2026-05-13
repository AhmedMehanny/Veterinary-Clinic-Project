//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace BusinessLogicLayer
//{
//    internal class ReportManager
//    {
//    }
//}

using System;
using System.Collections.Generic;
using System.Data;
using DataAccessLayer;
using Models;

namespace BusinessLogicLayer
{
    public class ReportManager
    {
        private readonly ReportRepository _reportRepository;

        public ReportManager()
        {
            _reportRepository = new ReportRepository();
        }

        // 1. Vaccinations expiring/overdue (booster due soon)
        public List<Vaccination> GetBoosterDueReport(int daysAhead = 30)
        {
            try
            {
                DateTime fromDate = DateTime.Today;
                DateTime toDate = DateTime.Today.AddDays(daysAhead);
                return _reportRepository.GetBoosterDueBetween(fromDate, toDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetBoosterDueReport: {ex.Message}");
                throw;
            }
        }

        // 2. Clinic visit statistics grouped by month/clinic
        public DataTable GetClinicVisitStatistics(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("Start date must be before end date");

            try
            {
                return _reportRepository.GetVisitStatistics(startDate, endDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetClinicVisitStatistics: {ex.Message}");
                throw;
            }
        }

        // 3. Inventory low stock report
        public List<VaccineInventory> GetLowStockReport()
        {
            try
            {
                return _reportRepository.GetLowStockInventory();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetLowStockReport: {ex.Message}");
                throw;
            }
        }

        // 4. Owner pet summary (list of owners with their pets)
        public DataTable GetOwnerPetSummary()
        {
            try
            {
                return _reportRepository.GetOwnerPetSummary();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetOwnerPetSummary: {ex.Message}");
                throw;
            }
        }

        // 5. Revenue / visit count per clinic (if billing data available)
        public DataTable GetClinicRevenueReport(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("Invalid date range");

            try
            {
                return _reportRepository.GetClinicRevenue(startDate, endDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetClinicRevenueReport: {ex.Message}");
                throw;
            }
        }
    }
}
