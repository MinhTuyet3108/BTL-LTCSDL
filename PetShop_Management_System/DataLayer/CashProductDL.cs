﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransObject;
using System.Data.SqlClient;
using System.Data;

namespace DataLayer
{
    public class CashProductDL : Dataprovider
    {
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspGetProducts", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string productID = reader["ProductID"].ToString();
                    string prName = reader["PrName"].ToString();
                    decimal price = Convert.ToDecimal(reader["Price"]);
                    int stock = Convert.ToInt32(reader["Stock"]);
                    string category = reader["Category"].ToString();

                    Product product = new Product(productID, prName, price, stock, category);
                    products.Add(product);
                }
                reader.Close();
                return products;
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


        public bool Add(Product product)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspAddProduct", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                cmd.Parameters.AddWithValue("@PrName", product.PrName);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                cmd.Parameters.AddWithValue("@Stock", product.Stock);
                cmd.Parameters.AddWithValue("@Category", product.Category);


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
        public List<Product> SearchProducts(string keyword) // Đổi tên từ Search thành SearchProducts
        {
            List<Product> products = new List<Product>();

            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspSearchProduct", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string productID = reader["ProductID"].ToString();
                    string prName = reader["PrName"].ToString();
                    decimal price = Convert.ToDecimal(reader["Price"]);
                    int stock = Convert.ToInt32(reader["Stock"]);
                    string category = reader["Category"].ToString();

                    Product product = new Product(productID, prName, price, stock, category);
                    products.Add(product);
                }
                reader.Close();
                return products;
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
