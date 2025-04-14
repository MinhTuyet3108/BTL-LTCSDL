using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TransObject;

namespace DataLayer
{
    public class PetReportDL : Dataprovider
    {
        public Dictionary<string, int> GetPetTypeQuantities()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            string sql = @"SELECT LOWER(LTRIM(RTRIM(Category))) AS Category, SUM(Qty) AS TotalQty 
    FROM Pet 
    WHERE CustomerID IS NULL AND Qty > 0
    GROUP BY LOWER(LTRIM(RTRIM(Category)))";

            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string category = reader["Category"].ToString();
                    int qty = Convert.ToInt32(reader["TotalQty"]);
                    result[category] = qty;
                }

                reader.Close();
                return result;
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

        public List<string> GetLowStockProducts()
        {
            List<string> list = new List<string>();
            string sql = "SELECT PrName FROM Product WHERE Stock <= 5";

            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand(sql, cn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(reader["PrName"].ToString());
                }

                reader.Close();
                return list;
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

        public decimal GetTodayRevenue()
        {
            decimal total = 0;
            string sql = @"
            SELECT SUM(TotalAmount) AS Revenue 
            FROM Invoice 
            WHERE CONVERT(date, InvoiceDate) = CONVERT(date, GETDATE())";

            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand(sql, cn);
                object result = cmd.ExecuteScalar();

                if (result != DBNull.Value)
                    total = Convert.ToDecimal(result);

                return total;
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
