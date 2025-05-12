using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransObject
{
    public class Pet
    {
        private string customerID;

        public int PetID { get; set; }
        public string PetName { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string HealthStatus { get; set; }
        public string CustomerID { get; set; }



        public Pet(int petID, string petName, string type, decimal price, string healthStatus, string customerID)
        {
            PetID = petID;
            PetName = petName;
            Type = type;
            Price = price;
            HealthStatus = healthStatus;
            CustomerID = customerID;
        }


        public Pet()
        {
        }

        public Pet(string petName, string type, decimal price, string healthStatus, string customerID)
        {
            PetName = petName;
            Type = type;
            Price = price;
            HealthStatus = healthStatus;
            this.customerID = customerID;
        }
    }
}

