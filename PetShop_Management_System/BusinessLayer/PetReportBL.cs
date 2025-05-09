using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataLayer;
using TransObject;

namespace BusinessLayer
{
    public class PetReportBL
    {
        private PetReportDL petReportDL;

        public PetReportBL()
        {
            petReportDL = new PetReportDL();
        }
        public Dictionary<string, int> GetPetCounts()
        {
            try
            {
                return petReportDL.GetPetTypeQuantities();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        //Low Stock

        public List<string> GetLowStockProducts()
        {
            try
            {
                return petReportDL.GetLowStockProducts();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        //public decimal GetTodayRevenue()
        //{
        //    try
        //    {
        //        return petReportDL.GetTodayRevenue();
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw ex;
        //    }
        //}

        public decimal GetRevenueByDate(DateTime date)
        {
            
            try
            {
                return petReportDL.GetRevenueBytDate(date);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public decimal GetRevenueByMonth(int month, int year)
        {
            
            try
            {
                return petReportDL.GetRevenueByMonth(month, year);
            }
            catch (SqlException ex)
            {
                throw ex;
            }

        }

        public decimal GetRevenueByYear(int year)
        {
            
            try
            {
                return petReportDL.GetRevenueByYear(year);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<Appointment> GetUpcomingAppointments()
        {
            try
            {
                return petReportDL.GetUpcomingAppointments();

            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
    }
}
