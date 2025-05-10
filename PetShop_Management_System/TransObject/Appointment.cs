using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransObject
{
    public class Appointment
    {
        public int AppointmentID { get; set; }
        public string CustomerID { get; set; }
        public DateTime AppointmentDate { get; set; }

        public Appointment(int appointmentID, string customerID, DateTime appointmentDate)
        {
            AppointmentID = appointmentID;
            CustomerID = customerID;
            AppointmentDate = appointmentDate;
        }

        public Appointment() { }
    }
}
