using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransObject;
using DataLayer;
using System.Data.SqlClient;
namespace BusinessLayer
{
    public class CashCustomerBL
    {
        private CustomerDL customerDL;

        public CashCustomerBL()
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


        public object SearchCustomer(string keyword)
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

