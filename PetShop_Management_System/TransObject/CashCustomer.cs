using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransObject
{
   public class CashCustomer
    {
        public int CashID { get; set; }
        public string CustomerLName { get; set; }
        public string CustomerFName { get; set; }
        public string PhoneNumber { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
