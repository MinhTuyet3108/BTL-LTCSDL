using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransObject;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class PetDL : Dataprovider
    {
        private List<Pet> pets;

        public List<Pet> GetPets()
        {
            List<Pet> pets = new List<Pet>();
            try
            {
                Connect();
                using (SqlCommand cmd = new SqlCommand("uspGetPet", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Pet pet = new Pet
                            {
                                PetID = reader.GetInt32(reader.GetOrdinal("PetID")), // Ánh xạ chính xác PetID
                                PetName = reader["PetName"]?.ToString(),
                                Type = reader["Type"]?.ToString(),
                                Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0.0m,
                                HealthStatus = reader["HealthStatus"]?.ToString(),
                                CustomerID = reader["CustomerID"]?.ToString()
                            };
                            pets.Add(pet);
                            // Debug để kiểm tra giá trị PetID
                            System.Diagnostics.Debug.WriteLine($"PetID loaded: {pet.PetID}, Name: {pet.PetName}");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách thú cưng: {ex.Message}");
            }
            finally
            {
                Disconnect();
            }
            return pets;
        }


        public bool Add(Pet pet)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspAddPet", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.AddWithValue("@PetID", pet.PetID);
                cmd.Parameters.AddWithValue("@PetName", pet.PetName);
                cmd.Parameters.AddWithValue("@Type", pet.Type);
                cmd.Parameters.AddWithValue("@Price", pet.Price);
                cmd.Parameters.AddWithValue("@HealthStatus", pet.HealthStatus);
                cmd.Parameters.AddWithValue("@CustomerID", string.IsNullOrEmpty(pet.CustomerID) ? (object)DBNull.Value : pet.CustomerID);

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
        public bool Delete(int id)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspDeletePet", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PetID", id);

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
        public bool Update(Pet pet)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspUpdatePet", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PetID", pet.PetID);
                cmd.Parameters.AddWithValue("@PetName", pet.PetName);
                cmd.Parameters.AddWithValue("@Type", pet.Type);
                cmd.Parameters.AddWithValue("@Price", pet.Price);
                cmd.Parameters.AddWithValue("@HealthStatus", pet.HealthStatus);
                cmd.Parameters.AddWithValue("@CustomerID", pet.CustomerID);


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

        public List<Pet> Search(string keyword)
        {
            List<Pet> pets = new List<Pet>();

            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspSearchPet", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string petName = reader["PetName"].ToString();
                    string type = reader["Type"].ToString();
                    decimal price = Convert.ToDecimal(reader["Price"]);
                    string healthStatus = reader["HealthStatus"].ToString();
                    string customerID = reader["CustomerID"].ToString();

                    Pet pet = new Pet(petName, type, price, healthStatus, customerID);
                    pets.Add(pet);
                }
                reader.Close();
                return pets;
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

