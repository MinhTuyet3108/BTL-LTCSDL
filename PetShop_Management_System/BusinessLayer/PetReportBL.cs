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
     private PetReportDL petdal=new PetReportDL();


        public Dictionary<string, int> GetPetCounts()
        {
            return petdal.GetPetTypeQuantities();
        }

        //Low Stock
        private PetReportDL dl = new PetReportDL();

        public List<string> GetLowStockProducts()
        {
            return dl.GetLowStockProducts();
        }

        private PetReportDL invdl = new PetReportDL();

        public decimal GetTodayRevenue()
        {
            return invdl.GetTodayRevenue();
        }
    }
}
