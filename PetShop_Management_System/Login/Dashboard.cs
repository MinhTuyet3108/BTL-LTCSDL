using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using System.Data.SqlClient;
using TransObject;

namespace Login
{
    public partial class Dashboard : Form
    {
        private PetReportBL petReportBL;
        public Dashboard()
        {
            InitializeComponent();
            petReportBL = new PetReportBL();

        }
        
        private void LoadPetCounts()
        {
            try
            {
                var counts = petReportBL.GetPetCounts();

                lblDog.Text = counts.ContainsKey("Chó") ? counts["Chó"].ToString() : "0";
                lbCat.Text = counts.ContainsKey("Mèo") ? counts["Mèo"].ToString() : "0";
                lblFish.Text = counts.ContainsKey("Cá") ? counts["Cá"].ToString() : "0";
                lbBird.Text = counts.ContainsKey("Chim") ? counts["Chim"].ToString() : "0";
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải số lượng thú cưng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadLowStockProducts()
        {
            try
            {
                List<string> products = petReportBL.GetLowStockProducts();
                lBxLowStock.Items.Clear();
                foreach (string item in products)
                {
                    lBxLowStock.Items.Add(item);
                }
            }
            catch (SqlException ex)
            {

                MessageBox.Show("Lỗi tải sản phẩm tồn kho thấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        //private void LoadTodayRevenue()
        //{
        //    try
        //    {
        //        decimal total = petReportBL.GetTodayRevenue();
        //        lbTotal.Text = total.ToString("N0") + " VND";
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show("Lỗi SQL khi tải doanh thu hôm nay: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}


        private void LoadUpcomingAppointments()
        {
            try
            {
                var list = petReportBL.GetUpcomingAppointments();
                lBxAppointment.Items.Clear();
                foreach (var appt in list)
                {
                    lBxAppointment.Items.Add($"KH: {appt.CustomerID} - {appt.AppointmentDate:g}");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi tải lịch hẹn: " + ex.Message);
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadPetCounts();
            LoadLowStockProducts();
            //LoadTodayRevenue();
            LoadUpcomingAppointments();
            dtpDay.Format = DateTimePickerFormat.Short;

            // Chọn tháng + năm (ẩn ngày)
            dtpMonth.Format = DateTimePickerFormat.Custom;
            dtpMonth.CustomFormat = "MM/yyyy";
            dtpMonth.ShowUpDown = true;

            // Chọn chỉ năm (ẩn ngày, tháng)
            dtpYear.Format = DateTimePickerFormat.Custom;
            dtpYear.CustomFormat = "yyyy";
            dtpYear.ShowUpDown = true;
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnRevenueByDate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = dtpDay.Value;
                decimal revenue = petReportBL.GetRevenueByDate(date);
                lbTotal.Text = revenue.ToString("N0");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnRevenueByMonth_Click(object sender, EventArgs e)
        {
            try
            {
                int selectmonth = dtpMonth.Value.Month;
                int selectyear = dtpMonth.Value.Year;
                decimal revenue = petReportBL.GetRevenueByMonth(selectmonth, selectyear);
                lbTotalmonth.Text = revenue.ToString("N0");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnRevenueByYear_Click(object sender, EventArgs e)
        {
            try
            {
                int year = dtpYear.Value.Year;
                decimal revenue = petReportBL.GetRevenueByYear(year);
                lbTotalyear.Text = revenue.ToString("N0");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
