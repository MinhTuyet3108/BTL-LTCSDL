
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


            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader("uspGetPetTypeQT", CommandType.StoredProcedure);
                while (reader.Read())
                {
                    string category = reader["LoaiThuCung"].ToString();
                    int qty = Convert.ToInt32(reader["SoLuong"]);
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

            try
            {
                Connect();
                SqlDataReader reader = MyExcuteReader("uspGetLowStockProducts", CommandType.StoredProcedure);


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

        //public decimal GetTodayRevenue()
        //{
        //    decimal total = 0;

        //    try
        //    {
        //        Connect();
        //        object result = MyExcuteScalar("uspGetTodayRevenue", CommandType.StoredProcedure);

        //        if (result != DBNull.Value)
        //            total = Convert.ToDecimal(result);

        //        return total;
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        Disconnect();
        //    }
        //}

        public decimal GetRevenueBytDate(DateTime date)
        {
            try
            {
                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@Date", date.Date)
        };

                var result = MyExcuteScalar("uspGetRevenueByDate", CommandType.StoredProcedure, parameters);
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
            catch (SqlException ex)
            {
                // Ghi log lỗi nếu cần: Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public decimal GetRevenueByMonth(int month, int year)
        {
            try
            {
                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@Month", month),
            new SqlParameter("@Year", year)
        };

                var result = MyExcuteScalar("uspGetRevenueByMonth", CommandType.StoredProcedure, parameters);
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
            catch (SqlException ex)
            {
                // Ghi log lỗi nếu cần: Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public decimal GetRevenueByYear(int year)
        {
            try
            {
                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@Year", year)
        };

                var result = MyExcuteScalar("uspGetRevenueByYear", CommandType.StoredProcedure, parameters);
                return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
            catch (SqlException ex)
            {
                // Ghi log lỗi nếu cần: Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public List<Appointment> GetUpcomingAppointments()
        {
            List<Appointment> list = new List<Appointment>();
            Connect();
            List<SqlParameter> parameters = null;
            SqlDataReader reader = MyExcuteReader("uspGetUpcomingAppointments", CommandType.StoredProcedure, parameters);
            while (reader.Read())
            {
                list.Add(new Appointment
                {
                    AppointmentID = Convert.ToInt32(reader["AppointmentID"]),
                    CustomerID = reader["CustomerID"].ToString(),
                    AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"])
                });
            }
            reader.Close();
            Disconnect();
            return list;
        }

    }
}
