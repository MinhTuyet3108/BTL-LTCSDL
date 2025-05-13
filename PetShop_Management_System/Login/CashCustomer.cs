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
using TransObject;
using System.Data.SqlClient;

namespace Login
{
    public partial class CashCustomer : Form
    {
        private CustomerBL customerBL;
        public Customer SelectedCustomer { get; private set; } // Thêm thuộc tính SelectedProduct
        public string CustomerName { get; private set; }

        public CashCustomer()
        {
            InitializeComponent();
            SqlCommand cm = new SqlCommand();
            customerBL = new CustomerBL();
        }

        private void InitializeDataGridView()
        {
            dgvCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (!dgvCustomer.Columns.Contains("Edit"))
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn
                {
                    HeaderText = "Chọn",
                    Text = "Chọn",
                    Name = "Edit",
                    UseColumnTextForButtonValue = true
                };
                dgvCustomer.Columns.Add(btn);
            }
            throw new NotImplementedException();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            LoadCustomers();
            GenerateTransactionNo();
            // Thêm cột nút "Chọn" nếu chưa có

        }
        private void LoadCustomers()
        {
            try
            {
                dgvCustomer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                var customers = customerBL.GetCustomers();
                if (customers != null)
                {
                    dgvCustomer.Columns.Clear(); // Xoá toàn bộ cột cũ nếu có
                    dgvCustomer.DataSource = customers;

                    // Gán lại header
                    dgvCustomer.Columns["CustomerID"].HeaderText = "Mã KH";
                    dgvCustomer.Columns["FirstName"].HeaderText = "Tên";
                    dgvCustomer.Columns["LastName"].HeaderText = "Họ";
                    dgvCustomer.Columns["Phone"].HeaderText = "SĐT";

                    // Thêm nút chọn lại
                    if (!dgvCustomer.Columns.Contains("Edit"))
                    {
                        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                        btn.HeaderText = "Chọn";
                        btn.Text = "Chọn";
                        btn.Name = "Edit";
                        btn.UseColumnTextForButtonValue = true;
                        dgvCustomer.Columns.Add(btn);
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void GenerateTransactionNo()
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                string transno = $"{sdate}001";
                TextBox txtTransNo = this.Controls.Find("txtTransNo", true).FirstOrDefault() as TextBox;
                if (txtTransNo != null)
                    txtTransNo.Text = transno;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo mã giao dịch: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                if (dgvCustomer.Columns[e.ColumnIndex].Name == "Edit")
                {
                    DataGridViewRow row = dgvCustomer.Rows[e.RowIndex];
                    SelectedCustomer = new Customer
                    {
                        CustomerID = row.Cells["CustomerID"].Value.ToString(), // Loại bỏ khoảng trắng
                        FirstName = row.Cells["FirstName"].Value.ToString(),
                        LastName = row.Cells["LastName"].Value.ToString(),
                        Phone = row.Cells["Phone"].Value.ToString()
                    };
                    this.DialogResult = DialogResult.OK; // Đặt kết quả để đóng form
                    this.Close(); // Đóng form sau khi chọn
                }
            }


            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadSearchCustomer(List<Customer> customers)
        {
            try
            {
                dgvCustomer.DataSource = null; // Xóa dữ liệu cũ
                dgvCustomer.Columns.Clear(); // Xóa cột cũ

                if (customers != null && customers.Any())
                {
                    dgvCustomer.DataSource = customers;

                    // Gán lại header
                    dgvCustomer.Columns["CustomerID"].HeaderText = "Mã KH";
                    dgvCustomer.Columns["FirstName"].HeaderText = "Tên";
                    dgvCustomer.Columns["LastName"].HeaderText = "Họ";
                    dgvCustomer.Columns["Phone"].HeaderText = "SĐT";

                    // Thêm cột nút "Chọn"
                    if (!dgvCustomer.Columns.Contains("Edit"))
                    {
                        DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                        btn.HeaderText = "Chọn";
                        btn.Text = "Chọn";
                        btn.Name = "Edit";
                        btn.UseColumnTextForButtonValue = true;
                        dgvCustomer.Columns.Add(btn);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCustomers(); // Tải lại danh sách đầy đủ nếu không tìm thấy
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi hiển thị kết quả tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(keyword))
                {
                    LoadCustomers(); // Tải lại danh sách đầy đủ nếu từ khóa rỗng
                    return;
                }

                // Lấy danh sách khách hàng từ CustomerBL
                var allCustomers = customerBL.GetCustomers();
                if (allCustomers == null || !allCustomers.Any())
                {
                    MessageBox.Show("Không có dữ liệu khách hàng để tìm kiếm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lọc khách hàng theo từ khóa (FirstName hoặc LastName)
                var filteredCustomers = allCustomers
                    .Where(c => (c.FirstName != null && c.FirstName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0) ||
                                (c.LastName != null && c.LastName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0))
                    .ToList();

                LoadSearchCustomer(filteredCustomers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CashCustomer_Load(object sender, EventArgs e)
        {

        }
    }
}

