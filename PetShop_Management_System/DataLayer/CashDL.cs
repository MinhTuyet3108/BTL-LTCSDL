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
   public class CashDL : Dataprovider
    {
        public List<Cash> GetCash()
        {
            List<Cash> cashes = new List<Cash>();
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspGetCash", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string cashID = reader["CashID"].ToString();
                    string transno = reader["Transno"].ToString();
                    string pcode = reader["Pcode"].ToString();
                    string pname = reader["Pname"].ToString();
                    decimal price = Convert.ToDecimal(reader["Price"]);
                    decimal total = Convert.ToDecimal(reader["Total"]);
                    int qty = Convert.ToInt32(reader["Qty"]);
                    string cid = reader["Cid"].ToString();
                    string cashier = reader["Cashier"].ToString();

                    Cash cash = new Cash(cashID, transno, pcode, pname, qty, price, total, cid, cashier);
                    cashes.Add(cash);
                }
                reader.Close();
                return cashes;
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


        public bool Add(Cash cash)
        {

            // Kiểm tra dữ liệu đầu vào cơ bản
            if (cash == null)
                throw new ArgumentNullException(nameof(cash), "Đối tượng Cash không được null.");

            try
            {
                Connect(); // Sử dụng phương thức Connect() từ Dataprovider để mở kết nối
                using (SqlCommand cmd = new SqlCommand("uspAddCash", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số với kiểu dữ liệu rõ ràng
                    cmd.Parameters.Add(new SqlParameter("@Transno", SqlDbType.VarChar, 15) { Value = cash.Transno });
                    cmd.Parameters.Add(new SqlParameter("@Pcode", SqlDbType.VarChar, 15) { Value = (object)cash.Pcode ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Pname", SqlDbType.VarChar, 50) { Value = (object)cash.Pname ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Qty", SqlDbType.Int) { Value = (object)cash.Qty ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Price", SqlDbType.Decimal) { Value = cash.Price, Precision = 18, Scale = 2 });
                    cmd.Parameters.Add(new SqlParameter("@Total", SqlDbType.Decimal) { Value = (object)cash.Total ?? DBNull.Value, Precision = 18, Scale = 2 });
                    cmd.Parameters.Add(new SqlParameter("@Cid", SqlDbType.VarChar, 10) { Value = (object)cash.Cid ?? DBNull.Value });
                    cmd.Parameters.Add(new SqlParameter("@Cashier", SqlDbType.VarChar, 10) { Value = (object)cash.Cashier ?? DBNull.Value });

                    int result = cmd.ExecuteNonQuery();
                    return result > 0;
                }
            }
            catch (SqlException ex)
            {
                throw new Exception($"Lỗi cơ sở dữ liệu khi thêm giao dịch: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi không xác định khi thêm giao dịch: {ex.Message}", ex);
            }
            finally
            {
                Disconnect(); // Sử dụng phương thức Disconnect() từ Dataprovider để đóng kết nối
            }
        }



        public bool Delete(string id)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspDeleteCash", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CashID", id);

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
        public bool Update(Cash cash)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspUpdateCash", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // cmd.Parameters.AddWithValue("@CashID", cash.CashID);
                cmd.Parameters.AddWithValue("@Transno", cash.Transno);
                cmd.Parameters.AddWithValue("@Price", cash.Price);
                cmd.Parameters.AddWithValue("@Total", cash.Total);
                cmd.Parameters.AddWithValue("@Pcode", cash.Pcode);
                cmd.Parameters.AddWithValue("@Pname", cash.Pname);
                cmd.Parameters.AddWithValue("@Qty", cash.Qty);
                cmd.Parameters.AddWithValue("@Cid", cash.Cid);
                cmd.Parameters.AddWithValue("@Cashier", cash.Cashier);


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

        public List<Cash> Search(string keyword)
        {
            List<Cash> cashes = new List<Cash>();

            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspSearchCash", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string cashID = reader["CashID"].ToString();
                    string transno = reader["Transno"].ToString();
                    string pcode = reader["Pcode"].ToString();
                    string pname = reader["Pname"].ToString();
                    decimal price = Convert.ToDecimal(reader["Price"]);
                    decimal total = Convert.ToDecimal(reader["Total"]);
                    int qty = Convert.ToInt32(reader["Qty"]);
                    string cid = reader["Cid"].ToString();
                    string cashier = reader["Cashier"].ToString();

                    Cash cash = new Cash(cashID, transno, pcode, pname, qty, price, total, cid, cashier);
                    cashes.Add(cash);
                }
                reader.Close();
                return cashes;
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
