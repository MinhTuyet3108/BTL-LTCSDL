using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransObject
{
    public class CashProduct
    {
        public int CashID { get; set; }
        public string ProductCode { get; set; }
        public int PrName { get; set; }
        public decimal Price { get; set; }
        public decimal Type { get; set; }
        public string Category { get; set; }
    }
}
