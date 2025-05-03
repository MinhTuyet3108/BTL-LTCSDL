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
    public class EmployeeBL
    {
        private EmployeeDL employeeDL;
        public EmployeeBL()
        {
            employeeDL = new EmployeeDL();
        }
        public List<Employee> GetEmployees()
        {
            try
            {
                return employeeDL.GetEmployees();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                employeeDL.AddEmployee(employee);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }
        public bool DeleteEmployee(string id)
        {
            try
            {
                return employeeDL.DeleteEmployee(id);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }

        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                return employeeDL.UpdateEmployee(employee);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }


        public List<Employee> SearchEmployees(string keyword)
        {
            try
            {
                return employeeDL.SearchEmployees(keyword);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
