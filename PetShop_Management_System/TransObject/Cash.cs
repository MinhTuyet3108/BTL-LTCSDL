using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransObject
{
    public class Cash
    {
        public string CashID { get; set; }
        public string Transno { get; set; }
        public string Pcode { get; set; }
        public string Pname { get; set; }
        public int? Qty { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string Cid { get; set; }
        public string Cashier { get; set; }

        public DateTime Date { get; set; }

        public int No { get; set; }
        public string CustomerName { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }

        public Cash() { }
        public Cash(string cashID, string transno, string pcode, string pname, int qty, decimal price, decimal total, string cid, string cashier, DateTime date)
        {
            CashID = cashID;
            Transno = transno;
            Pcode = pcode;
            Pname = pname;
            Qty = qty;
            Price = price;
            Total = total;
            Cid = cid;
            Cashier = cashier;
            Date = date;


        }

        public Cash(string pcode, string pname, int qty, decimal price, decimal total, string cid, string cashier)
        {
            Pcode = pcode;
            Pname = pname;
            Qty = qty;
            Price = price;
            Total = total;
            Cid = cid;
            Cashier = cashier;
        }
    }
}
