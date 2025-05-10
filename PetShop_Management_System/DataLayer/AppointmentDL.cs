using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransObject;

namespace DataLayer
{
    public class AppointmentDL : Dataprovider
    {
        public List<Appointment> GetAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspGetAppointments", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int appointmentID = Convert.ToInt32(reader["AppointmentID"]);
                    string cusID = reader["CustomerID"].ToString();
                    DateTime datetime = Convert.ToDateTime(reader["AppointmentDate"]);

                    Appointment appointment = new Appointment(appointmentID, cusID, datetime);
                    appointments.Add(appointment);
                }
                reader.Close();
                return appointments;
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


        public int Add(Appointment appointment)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspAddAppointment", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@CustomerID", appointment.CustomerID);
                cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);


                // Nhận giá trị RETURN từ stored procedure
                SqlParameter returnValue = new SqlParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);

                cmd.ExecuteNonQuery();

                int result = (int)returnValue.Value;
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
        public bool Delete(int id)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspDeleteAppointment", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AppointmentID", id);

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
        public bool Update(Appointment appointment)
        {
            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspUpdateAppointment", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@AppointmentID", appointment.AppointmentID);
                cmd.Parameters.AddWithValue("@CustomerID", appointment.CustomerID);
                cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);


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

        public List<Appointment> Search(string keyword)
        {
            List<Appointment> appointments = new List<Appointment>();

            try
            {
                Connect();
                SqlCommand cmd = new SqlCommand("uspSearchAppointment", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int appointmentID = Convert.ToInt32(reader["AppointmentID"]);
                    string cusID = reader["CustomerID"].ToString();
                    DateTime appointmentdate = Convert.ToDateTime(reader["AppointmentDate"]);


                    Appointment appointment = new Appointment(appointmentID, cusID, appointmentdate);
                    appointments.Add(appointment);
                }
                reader.Close();
                return appointments;
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
