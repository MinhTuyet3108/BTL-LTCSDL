using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransObject;
using BusinessLayer;
using System.Data.SqlClient;

namespace Login
{
    public partial class PetModule : Form
    {
        string title = "PetShop Management System";
        private PetBL petBL;
        public PetModule()
        {
            InitializeComponent();
            petBL = new PetBL();
        }
        public void LoadPet()
        {
            dgvPet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //điều chỉnh kích thước vừa với khung

            // set data
            try
            {
                dgvPet.DataSource = petBL.GetPets();
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            LoadPet();
        }

        public void Clear()
        {
            txtPetID.Clear();
            txtName.Clear();
            txtPrice.Clear();
            txtType.Clear();

            txtHealth.Clear();

            btnSave.Enabled = true;
        }
        public void LoadSearchPet(List<Pet> pets)
        {
            dgvPet.DataSource = null; // reset lại
            dgvPet.DataSource = pets; // tự động binding dữ liệu
        }
        private void dgvPet_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                if (dgvPet.Columns[e.ColumnIndex].Name == "Edit")
                {
                    DataGridViewRow row = dgvPet.Rows[e.RowIndex];
                    txtPetID.Text = row.Cells["PetID"].Value?.ToString() ?? "";
                    txtName.Text = row.Cells["PetName"].Value?.ToString() ?? "";
                    txtType.Text = row.Cells["Type"].Value?.ToString() ?? "";
                    txtPrice.Text = row.Cells["Price"].Value?.ToString() ?? "";
                    txtHealth.Text = row.Cells["HealthStatus"].Value?.ToString() ?? "";
                    btnSave.Enabled = false;
                }
                else if (dgvPet.Columns[e.ColumnIndex].Name == "Delete")
                {
                    int id = (int)dgvPet.Rows[e.RowIndex].Cells["PetID"].Value;
                    if (MessageBox.Show("Delete this product?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        petBL.DeletePet(id);
                        LoadPet();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {
            LoadPet();
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtType.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) || string.IsNullOrWhiteSpace(txtHealth.Text))
                {
                    MessageBox.Show("Please fill in all information. ", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                try
                {

                    if (MessageBox.Show("Do you want to save this product information?", "New Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Pet pet = new Pet
                        {

                            PetName = txtName.Text,
                            Price = Convert.ToDecimal(txtPrice.Text),
                            Type = txtType.Text,
                            HealthStatus = txtHealth.Text,

                        };

                        petBL.AddPet(pet);
                        MessageBox.Show("Product added successfully!", title);
                        LoadPet();
                        Clear();

                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtType.Text) ||
       string.IsNullOrWhiteSpace(txtPrice.Text) || string.IsNullOrWhiteSpace(txtHealth.Text) ||
       string.IsNullOrWhiteSpace(txtPetID.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin, bao gồm Mã thú cưng!", "Xác nhận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (!int.TryParse(txtPetID.Text, out int petId) || petId <= 0)
                {
                    MessageBox.Show("Mã thú cưng phải là số nguyên dương hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (MessageBox.Show("Bạn có muốn cập nhật thông tin thú cưng này không?", "Cập nhật thú cưng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Pet pet = new Pet
                    {
                        PetID = petId,
                        PetName = txtName.Text,
                        Type = txtType.Text,
                        Price = Convert.ToDecimal(txtPrice.Text),
                        HealthStatus = txtHealth.Text,
                        CustomerID = null // Để null nếu chưa bán
                    };

                    if (petBL.UpdatePet(pet))
                    {
                        MessageBox.Show("Cập nhật thành công!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadPet();
                        Clear();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thú cưng để cập nhật!", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật thú cưng: {ex.Message}", title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadPet(); //load toàn bộ sản phẩm
            }
            else
            {
                List<Pet> result = petBL.SearchPets(keyword).OfType<Pet>().ToList();
                LoadSearchPet(result);
            }
        }
    }
}
