using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransObject
{
    public class PetReport
    {
        public int PetID { get; set; }
        public string PetName { get; set; }
        public string Type { get; set; }
        public int Age { get; set; }
        public decimal Price { get; set; }
        public string HealthStatus { get; set; }
        public string CustomerID { get; set; }
        public string Category { get; set; }
        public int Qty { get; set; }

        public PetReport() { }

        public PetReport(int petID, string petName, string type, int age, decimal price, string healthStatus, string customerID, string category, int qty)
        {
            PetID = petID;
            PetName = petName;
            Type = type;
            Age = age;
            Price = price;
            HealthStatus = healthStatus;
            CustomerID = customerID;
            Category = category;
            Qty = qty;
        }
    }
}
