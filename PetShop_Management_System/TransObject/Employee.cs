using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransObject
{
    public class Employee
    {
        public string EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }

        public Employee(string employeeID, string lastName, string firstName, string position, decimal salary, string phone, string username, string password)
        {
            EmployeeID = employeeID;
            LastName = lastName;
            FirstName = firstName;
            Position = position;
            Salary = salary;
            Phone = phone;
            Username = username;
            Password = password;
        }

        public Employee() { }
    }
}
