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
            string sql = "SELECT * FROM Customer";
            string customerID, lastName, firstName, gender, phone, address, Email;
            List<Customer> customers = new List<Customer>();
            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader(sql, CommandType.Text);
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

        public bool AddCustomer(Customer customer)
        {
            string sql = "INSERT INTO Customer (CustomerID, LastName, FirstName, Gender, Phone, Address, Email) " +
                         "VALUES (@CustomerID, @LastName, @FirstName, @Gender, @Phone, @Address, @Email)";
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                cmd.Parameters.AddWithValue("@LastName", customer.LastName);
                cmd.Parameters.AddWithValue("@FirstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@Gender", customer.Gender);
                cmd.Parameters.AddWithValue("@Phone", customer.Phone);
                cmd.Parameters.AddWithValue("@Address", customer.Address);

                // Nếu Email rỗng thì thêm DBNull.Value
                if (string.IsNullOrEmpty(customer.Email))
                    cmd.Parameters.AddWithValue("@Email", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Email", customer.Email);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
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
        public bool Delete(string id)
        {
            string query = "DELETE FROM Customer WHERE CustomerID = @id";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@id", id);
            cn.Open();
            int result = cmd.ExecuteNonQuery();
            cn.Close();
            return result > 0;
        }

        public bool Update(Customer c)
        {
            string query = @"UPDATE Customer SET 
                         LastName=@LastName, FirstName=@FirstName, Gender=@Gender,
                         Phone=@Phone, Address=@Address, Email=@Email
                         WHERE CustomerID=@CustomerID";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@CustomerID", c.CustomerID);
            cmd.Parameters.AddWithValue("@LastName", c.LastName);
            cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
            cmd.Parameters.AddWithValue("@Gender", c.Gender);
            cmd.Parameters.AddWithValue("@Phone", c.Phone);
            cmd.Parameters.AddWithValue("@Address", c.Address);
            cmd.Parameters.AddWithValue("@Email", c.Email);
            cn.Open();
            int result = cmd.ExecuteNonQuery();
            cn.Close();
            return result > 0;
        }

        public List<Customer> SearchCustomers(string keyword)
        {
            string sql = @"SELECT * FROM Customer 
                   WHERE CONCAT(CustomerID, LastName, FirstName, Gender, Phone, Address, Email) 
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
                    string customerID = reader["CustomerID"].ToString();
                    string lastName = reader["LastName"].ToString();
                    string firstName = reader["FirstName"].ToString();
                    string gender = reader["Gender"].ToString();
                    string phone = reader["Phone"].ToString();
                    string address = reader["Address"].ToString();
                    string email = reader["Email"].ToString();

                    Customer customer = new Customer(customerID, lastName, firstName, gender, phone, address, email);
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