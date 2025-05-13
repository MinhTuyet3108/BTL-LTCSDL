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
    public partial class ProductModule : Form
    {
        string title = "PetShop Management System";
        private ProductBL productBL;

        public ProductModule()
        {
            InitializeComponent();
            productBL = new ProductBL();
        }

        public void LoadProduct()
        {
            dgvProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //điều chỉnh kích thước vừa với khung

            // set data
            try
            {
                dgvProduct.DataSource = productBL.GetProducts();
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ProductModule_Load(object sender, EventArgs e)
        {
            LoadProduct();
        }

        public void Clear()
        {
            txtID.Clear();
            txtPrice.Clear();
            txtPrName.Clear();
            cbCategory.SelectedIndex = -1; // hoặc cbPosition.Text = "";
            txtStock.Clear();

             btnSave.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtPrName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) ||
    string.IsNullOrWhiteSpace(txtStock.Text) || string.IsNullOrWhiteSpace(cbCategory.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin. ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (MessageBox.Show("Cập nhật thông tin sản phẩm này?", "Cập nhật", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Product product = new Product()
                    {
                        ProductID = txtID.Text,
                        PrName = txtPrName.Text,
                        Price = Convert.ToDecimal(txtPrice.Text),
                        Stock = Convert.ToInt32(txtStock.Text),
                        Category = cbCategory.Text,
                    };


                    if (productBL.UpdateProduct(product))
                    {
                        MessageBox.Show("Cập nhật thành công!");
                        LoadProduct();
                        Clear();
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtPrName.Text) || string.IsNullOrWhiteSpace(txtPrice.Text) ||
    string.IsNullOrWhiteSpace(txtStock.Text) || string.IsNullOrWhiteSpace(cbCategory.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin. ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {

                if (MessageBox.Show("Lưu thông tin sản phẩm này?", "Thêm sản phẩm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Product product = new Product
                    {
                        ProductID = txtID.Text,
                        PrName = txtPrName.Text,
                        Price = Convert.ToDecimal(txtPrice.Text),
                        Stock = Convert.ToInt32(txtStock.Text),
                        Category = cbCategory.Text,
                    };

                    productBL.AddProduct(product);
                    MessageBox.Show("Thêm sản phẩm thành công!", title);
                    LoadProduct();
                    Clear();

                }
            }
            catch(SqlException ex) 
            {
                throw ex;
            }
        }

        public void LoadSearchProduct(List<Product> products)
        {
            dgvProduct.DataSource = null; // reset lại
            dgvProduct.DataSource = products; // tự động binding dữ liệu
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadProduct(); //load toàn bộ sản phẩm
            }
            else
            {
                List<Product> result = productBL.SearchProducts(keyword);
                LoadSearchProduct(result); //load danh sách sản phẩm đã tìm kiếm
            }
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Product product = new Product();
                if (e.RowIndex >= 0)
                {
                    if (dgvProduct.Columns[e.ColumnIndex].Name == "Edit")
                    {
                        // Load dữ liệu lên textbox
                        txtID.Text = dgvProduct.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
                        txtPrName.Text = dgvProduct.Rows[e.RowIndex].Cells["PrName"].Value.ToString();
                        txtPrice.Text = dgvProduct.Rows[e.RowIndex].Cells["Price"].Value.ToString();
                        cbCategory.Text = dgvProduct.Rows[e.RowIndex].Cells["Category"].Value.ToString();
                        txtStock.Text = dgvProduct.Rows[e.RowIndex].Cells["Stock"].Value.ToString();
                        
                    }
                    else if (dgvProduct.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        string id = dgvProduct.Rows[e.RowIndex].Cells["ProductID"].Value.ToString();
                        if (MessageBox.Show("Xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            productBL.DeleteProduct(id);
                            LoadProduct();
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
               