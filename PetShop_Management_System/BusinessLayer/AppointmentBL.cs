using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using TransObject;

namespace BusinessLayer
{
    public class AppointmentBL
    {
        private AppointmentDL appointmentDL;
        public AppointmentBL()
        {
            appointmentDL = new AppointmentDL();
        }
        public List<Appointment> GetAppointments()
        {
            try
            {
                return appointmentDL.GetAppointments();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        public int AddAppointment(Appointment appointment)
        {
            try
            {
                return appointmentDL.Add(appointment);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }

        public bool DeleteAppointment(int id)
        {
            try
            {
                return appointmentDL.Delete(id);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }

        public bool UpdateAppointment(Appointment appointment)
        {
            try
            {
                return appointmentDL.Update(appointment);
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }


        public List<Appointment> SearchAppointments(string keyword)
        {
            try
            {
                return appointmentDL.Search(keyword);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

    }
}
