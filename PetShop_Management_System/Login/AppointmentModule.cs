using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BusinessLayer;
using TransObject;


namespace Login
{
    public partial class AppointmentModule : Form
    {

        string title = "PetShop Management System";
        private AppointmentBL appointmentBL;
        public AppointmentModule()
        {
            InitializeComponent();
            appointmentBL = new AppointmentBL();
        }
        public void LoadAppointment()
        {
            dgvAppointment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //điều chỉnh kích thước vừa với khung

            // set data
            try
            {
                dgvAppointment.DataSource = appointmentBL.GetAppointments();
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Appointment_Load(object sender, EventArgs e)
        {
            LoadAppointment();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Do you want to save this appointment information?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Appointment appointment = new Appointment
                    {
                        CustomerID = txtCusID.Text,
                        AppointmentDate = dateTimePickerAppointment.Value,

                    };

                    int result = appointmentBL.AddAppointment(appointment);

                    if (result == 0)
                    {
                        MessageBox.Show("Thêm lịch hẹn thành công.", "Thông báo");
                        Clear(); // Hàm xóa nội dung nhập
                    }
                    else if (result == -1)
                    {
                        MessageBox.Show("Khách hàng này không tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    Clear();
                    LoadAppointment();

                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Do you want to update this appointment information?", "Update Appointment", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Appointment appointment = new Appointment()
                    {
                        AppointmentID = Convert.ToInt32(dgvAppointment.CurrentRow.Cells["AppointmentID"].Value),
                        CustomerID = txtCusID.Text,
                        AppointmentDate = dateTimePickerAppointment.Value,
                    };

                    appointmentBL.UpdateAppointment(appointment);
                    MessageBox.Show("Update successfully!");
                    LoadAppointment();
                    Clear();


                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Error: " + ex.Message, "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Clear()
        {
            txtCusID.Clear();
            dateTimePickerAppointment.Value = DateTime.Now;

            btnSave.Enabled = true;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LoadSearchAppointment(List<Appointment> appointments)
        {
            dgvAppointment.DataSource = null; // reset lại
            dgvAppointment.DataSource = appointments; // tự động binding dữ liệu
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadAppointment(); //load toàn bộ lịch hẹn

            }
            else
            {
                List<Appointment> result = appointmentBL.SearchAppointments(keyword);
                LoadSearchAppointment(result); //load danh sách lịch hẹn đã tìm kiếm
            }
        }

        private void dgvAppointment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Appointment appointment = new Appointment();
                if (e.RowIndex >= 0)
                {
                    if (dgvAppointment.Columns[e.ColumnIndex].Name == "Edit")
                    {
                        // Load dữ liệu lên textbox
                        txtCusID.Text = dgvAppointment.Rows[e.RowIndex].Cells["CustomerID"].Value.ToString();
                        dateTimePickerAppointment.Value = Convert.ToDateTime(dgvAppointment.Rows[e.RowIndex].Cells["AppointmentDate"].Value);

                    }
                    else if (dgvAppointment.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        int id = Convert.ToInt32(dgvAppointment.Rows[e.RowIndex].Cells["AppointmentID"].Value);
                        if (MessageBox.Show("Delete this appointment?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            appointmentBL.DeleteAppointment(id);
                            LoadAppointment();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
