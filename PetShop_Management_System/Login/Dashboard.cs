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
        public Dashboard()
        {
            InitializeComponent();
         
        }
        PetReportBL petReportBL = new PetReportBL();


        private void LoadPetCounts()
        {
            var counts = petReportBL.GetPetCounts();

            lblDog.Text = counts.ContainsKey("chó") ? counts["chó"].ToString() : "0";
            lbCat.Text = counts.ContainsKey("mèo") ? counts["mèo"].ToString() : "0";
            lblFish.Text = counts.ContainsKey("cá") ? counts["cá"].ToString() : "0";
            lbBird.Text = counts.ContainsKey("chim") ? counts["chim"].ToString() : "0";
        }
        private void LoadLowStockProducts()
        {
            PetReportBL reportBL = new PetReportBL();
            List<string> products = reportBL.GetLowStockProducts();
            lBxLowStock.Items.Clear();
            foreach (string item in products)
            {
                lBxLowStock.Items.Add(item);
            }
        }
        private void LoadTodayRevenue()
        {
            PetReportBL revenueBL = new PetReportBL();
            decimal total = revenueBL.GetTodayRevenue();
            lbTotal.Text = total.ToString("N0") + " VND";
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
            LoadTodayRevenue();
        }
    }
}
