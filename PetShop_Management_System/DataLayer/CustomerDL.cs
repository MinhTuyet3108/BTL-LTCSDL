using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using TransObject;
using System.Runtime.Remoting.Messaging;


namespace DataLayer
{
    public class CustomerDL : Dataprovider
    {
        public List<Customer> GetCustomers()
        {
            string sql = "uspGetAllCustomers";
            string customerID, lastName, firstName, gender, phone, address, Email;
            List<Customer> customers = new List<Customer>();
            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.StoredProcedure);
                while (reader.Read())
                {
                    customerID = reader["CustomerID"].ToString();
                    lastName = reader["LastName"].ToString();
                    firstName = reader["FirstName"].ToString();
                    gender = reader["Gender"].ToString();
                    phone = reader["Phone"].ToString();
                    address = reader["Address"].ToString();
                    Email = reader["Email"].ToString();

                    Customer customer = new Customer(customerID, lastName, firstName, gender, phone, address, Email);
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
        public List<Customer> SearchCustomers(string keyword)
        {

            List<Customer> customers = new List<Customer>();
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@Keyword","%"+keyword+"%")
            };
            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader("uspSearchCustomers", CommandType.StoredProcedure, parameters);
                while (reader.Read())
                {
                    Customer customer = new Customer(
                       reader["CustomerID"].ToString(),
                       reader["LastName"].ToString(),
                       reader["FirstName"].ToString(),
                       reader["Gender"].ToString(),
                       reader["Phone"].ToString(),
                       reader["Address"].ToString(),
                       reader["Email"].ToString()
                       );
                    customers.Add(customer);

                };

                reader.Close();
                return customers;
            }
            catch
            {
                throw;
            }
            finally
            {
                Disconnect();
            }
        }
        public bool AddCustomer(Customer customer)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@CustomerID", customer.CustomerID),
                new SqlParameter("@LastName", customer.LastName),
                new SqlParameter("@FirstName", customer.FirstName),
                new SqlParameter("@Gender", customer.Gender),
                new SqlParameter("@Phone", customer.Phone),
                new SqlParameter("@Address", customer.Address),
                new SqlParameter("@Email", customer.Email)
            };

            int result = MyExecuteNonQuery("uspAddCustomer", CommandType.StoredProcedure, parameters);
            return result > 0;
        }
        public bool Delete(string id)
        {
            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@CustomerID", id)
            };

            int result = MyExecuteNonQuery("uspDeleteCustomer", CommandType.StoredProcedure, parameters);
            return result > 0;

        }

        public bool Update(Customer customer)
        {

            List<SqlParameter> parameters = new List<SqlParameter>()
            {
                new SqlParameter("@CustomerID", customer.CustomerID),
                new SqlParameter("@LastName", customer.LastName),
                new SqlParameter("@FirstName", customer.FirstName),
                new SqlParameter("@Gender", customer.Gender),
                new SqlParameter("@Phone", customer.Phone),
                new SqlParameter("@Address", customer.Address),
                new SqlParameter("@Email", customer.Email)
            };

            int result = MyExecuteNonQuery("uspUpdateCustomer", CommandType.StoredProcedure, parameters);
            return result > 0;

        }

    }

}