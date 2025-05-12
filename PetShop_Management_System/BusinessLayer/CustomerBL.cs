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
    public class CustomerBL
    {
        private CustomerDL customerDL;
        public CustomerBL()
        {
            customerDL = new CustomerDL();
        }
        public List<Customer> GetCustomers()
        {
            try
            {
                return customerDL.GetCustomers();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        public void AddCustomer(Customer c)
        {
            try
            {
                customerDL.AddCustomer(c);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }
        public bool DeleteCustomer(string id)
        {
            try
            {
                return customerDL.Delete(id);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }

        public bool UpdateCustomer(Customer c)
        {
            try
            {
                return customerDL.Update(c);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }


        public List<Customer> SearchCustomers(string keyword)
        {
            try
            {
                return customerDL.SearchCustomers(keyword);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
