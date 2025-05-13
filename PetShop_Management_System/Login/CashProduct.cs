using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using Guna.UI2.WinForms;
using TransObject;

namespace Login
{
    public partial class CashProduct : Form
    {
        private ProductBL productBL;
        private CashProductBL cashProductBL;
        private CashForm cashForm;
        public Product SelectedProduct { get; private set; } // Thêm thuộc tính SelectedProduct
        public CashProduct(CashForm parentForm)
        {
            InitializeComponent();
            SqlCommand cm = new SqlCommand();
            productBL = new ProductBL();
            cashProductBL = new CashProductBL();
            cashForm = parentForm;
        }

     

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                DataGridViewRow row = dgvProduct.Rows[e.RowIndex];
                if (dgvProduct.Columns[e.ColumnIndex].Name == "Edit")
                {
                    Product product = new Product
                    {
                        ProductID = row.Cells["ProductID"].Value.ToString(),
                        PrName = row.Cells["PrName"].Value.ToString(),
                        Price = Convert.ToDecimal(row.Cells["Price"].Value),
                        Stock = Convert.ToInt32(row.Cells["Stock"].Value),
                        Category = row.Cells["Category"].Value.ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            LoadProduct();
        }
        public void LoadProduct()
        {
            dgvProduct.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //điều chỉnh kích thước vừa với khung

            //// set data
            try
            {
                dgvProduct.DataSource = productBL.GetProducts();
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            List<Cash> selectedItems = new List<Cash>();

            foreach (DataGridViewRow row in dgvProduct.Rows)
            {
                bool isChecked = Convert.ToBoolean(row.Cells["Select"].Value);

                if (isChecked)
                {
                    Cash cash = new Cash
                    {
                        Transno = DateTime.Now.ToString("yyyyMMddHHmmss"),
                        Pcode = row.Cells["ProductID"].Value.ToString(),
                        Pname = row.Cells["PrName"].Value.ToString(),
                        Qty = 1,
                        Price = Convert.ToDecimal(row.Cells["Price"].Value),
                        Stock = Convert.ToInt32(row.Cells["Stock"].Value),
                        Category = row.Cells["Category"].Value.ToString(),
                        Total = Convert.ToDecimal(row.Cells["Price"].Value)
                    };
                    selectedItems.Add(cash);
                }
            }

            if (selectedItems.Count > 0)
            {
                cashForm.AddSelectedItems(selectedItems); // Gửi sang CashForm
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void txtSearchCashProduct_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadProduct(); //load toàn bộ sản phẩm
            }
            else
            {
                List<Product> result = productBL.SearchProducts(keyword);
                //SearcProduct(result); //load danh sách sản phẩm đã tìm kiếm
            }
        }
        public void LoadSearchProduct(List<Product> products)
        {
            try
            {
                dgvProduct.DataSource = null;
                dgvProduct.Columns.Clear();

                if (products != null && products.Any())
                {
                    dgvProduct.DataSource = products;

                    // Gán lại header
                    dgvProduct.Columns["ProductID"].HeaderText = "Mã SP";
                    dgvProduct.Columns["PrName"].HeaderText = "Tên sản phẩm";
                    dgvProduct.Columns["Price"].HeaderText = "Giá";
                    dgvProduct.Columns["Stock"].HeaderText = "Tồn kho";
                    dgvProduct.Columns["Category"].HeaderText = "Danh mục";

                    // Thêm cột checkbox "Select"
                    if (!dgvProduct.Columns.Contains("Select"))
                    {
                        DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
                        chk.HeaderText = "Chọn";
                        chk.Name = "Select";
                        chk.Width = 50;
                        dgvProduct.Columns.Insert(0, chk);
                    }

                    // Thêm cột nút "Edit"
                    if (!dgvProduct.Columns.Contains("Edit"))
                    {
                        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                        btn.HeaderText = "Chỉnh sửa";
                        btn.Text = "Chỉnh sửa";
                        btn.Name = "Edit";
                        btn.UseColumnTextForButtonValue = true;
                        dgvProduct.Columns.Add(btn);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị kết quả tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchCashProduct_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearchCashProduct.Text.Trim(); // Sử dụng tên đúng của TextBox
                if (string.IsNullOrEmpty(keyword))
                {
                    LoadProduct();
                }
                else
                {
                    List<Product> result = cashProductBL.SearchProducts(keyword);
                    LoadSearchProduct(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

