using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using TransObject;

namespace DataLayer
{
    public class EmployeeDL : Dataprovider
    {
        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspGetEmployees", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string employeeID = reader["EmployeeID"].ToString();
                    string lastName = reader["LastName"].ToString();
                    string firstName = reader["FirstName"].ToString();
                    string position = reader["Position"].ToString();
                    decimal salary = Convert.ToDecimal(reader["Salary"]);
                    string phone = reader["Phone"].ToString();
                    string username = reader["Username"].ToString();
                    string password = reader["Password"].ToString();


                    Employee employee = new Employee(employeeID, lastName, firstName, position, salary, phone, username, password);
                    employees.Add(employee);
                }
                reader.Close();
                return employees;
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


        public bool AddEmployee(Employee employee)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspAddEmployee", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@employeeID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@lastName", employee.LastName);
                cmd.Parameters.AddWithValue("@firstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@position", employee.Position);
                cmd.Parameters.AddWithValue("@salary", employee.Salary);
                cmd.Parameters.AddWithValue("@phone", employee.Phone);
                cmd.Parameters.AddWithValue("@username", employee.Username);
                cmd.Parameters.AddWithValue("@password", employee.Password);

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
        public bool DeleteEmployee(string id)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspDeleteEmployee", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@employeeID", id);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
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
        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspUpdateEmployee", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@Position", employee.Position);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                cmd.Parameters.AddWithValue("@Username", employee.Username);
                cmd.Parameters.AddWithValue("@Password", employee.Password);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
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

        public List<Employee> SearchEmployees(string keyword)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspSearchEmployee", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string employeeID = reader["EmployeeID"].ToString();
                    string lastName = reader["LastName"].ToString();
                    string firstName = reader["FirstName"].ToString();
                    string position = reader["Position"].ToString();
                    decimal salary = Convert.ToDecimal(reader["Salary"]);
                    string phone = reader["Phone"].ToString();
                    string username = reader["Username"].ToString();
                    string password = reader["Password"].ToString();

                    Employee emp = new Employee(employeeID, lastName, firstName, position, salary, phone, username, password);
                    employees.Add(emp);
                }
                reader.Close();
                return employees;
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
