using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransObject
{
    public class Customer
    {
        private string cid;

        public string CustomerID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

        public Customer(string customerID, string lastName, string firstName, string gender, string phone, string address, string email)
        {
            CustomerID = customerID;
            LastName = lastName;
            FirstName = firstName;
            Gender = gender;
            Phone = phone;
            Address = address;
            Email = email;
        }

        public Customer() { }

        public Customer(string cid, string lastName, string firstName, string phone)
        {
            this.cid = cid;
            LastName = lastName;
            FirstName = firstName;
            Phone = phone;
        }
    }
}
