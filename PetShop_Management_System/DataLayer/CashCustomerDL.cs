using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransObject;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer
{
    public class CashCustomerDL : Dataprovider
    {
        public List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspGetCustomers", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string cid = reader["CustomerID"].ToString();
                    string lastName = reader["LastName"].ToString();
                    string firstName = reader["FirstName"].ToString();
                    string phone = reader["Phone"].ToString();




                    Customer customer = new Customer(cid, lastName, firstName, phone);
                    customers.Add(customer);
                }
                reader.Close();
                return customers;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }


        public bool Add(Customer customer)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspAddCustomer", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Cid", customer.CustomerID);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@Phone", (object)customer.Phone ?? DBNull.Value);



                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (SqlException ex)
            {
                throw;
            }
            finally
            {
                Disconnect();
            }

        }
        public List<Customer> SearchCustomers(string keyword)
        {
            string sql = @"SELECT * FROM Customer 
                         WHERE CONCAT(CustomerID, LastName, FirstName,  Phone) 
                         LIKE @Keyword";
            List<Customer> customers = new List<Customer>();

            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string cid = reader["Cid"].ToString();
                    string lastName = reader["LastName"].ToString();
                    string firstName = reader["FirstName"].ToString();
                    string phone = reader["Phone"].ToString();


                    Customer customer = new Customer(cid, lastName, firstName, phone);
                    customers.Add(customer);
                }
                reader.Close();
                return customers;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }
    }
}
