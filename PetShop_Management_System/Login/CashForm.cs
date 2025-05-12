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

namespace Login
{
    public partial class CashForm : Form
    {
        private List<Cash> cart = new List<Cash>();
        private CashCustomer cashCustomerForm;
        private CashProduct cashProductForm;
        private CashBL cashBL;
        private Customer selectedCustomer;
        private string cashId;
        public object CustomerName { get; private set; }
        public CashForm()
        {
            InitializeComponent();
            cashBL = new CashBL();
            LoadCash();
            lblTransno.Text = getTransno();
            dgvCash.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCash_CellContentClick);
        }
        private void LoadCash()
        {
            dgvCash.DataSource = null;
            dgvCash.DataSource = cart;
            for (int i = 0; i < cart.Count; i++)
            {
                cart[i].No = i + 1; // Gán số thứ tự
            }
            lblTotal.Text = CalculateTotal().ToString("F2");
        }


        private string GenerateCashID()
        {
            return $"CASH{DateTime.Now.Ticks % 1000:D3}";
        }
        private string getTransno()
        {
            return $"TRANS{DateTime.Now:yyyyMMddHHmmss}";
        }

        private decimal CalculateTotal()
        {
            return cart.Sum(cash => (cash.Qty ?? 0) * cash.Price);
        }

        private void dgvCash_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra chỉ số hợp lệ
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= dgvCash.Rows.Count)
            {
                return;
            }

            try
            {
                // Đảm bảo cart đã được khởi tạo
                if (cart == null)
                {
                    cart = new List<Cash>();
                    LoadCash();
                    return;
                }

                // Kiểm tra đồng bộ
                if (e.RowIndex >= cart.Count)
                {
                    MessageBox.Show("Dữ liệu không đồng bộ, đang làm mới...", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    LoadCash();
                    return;
                }

                string columnName = dgvCash.Columns[e.ColumnIndex].Name;

                if (columnName == "Increase" || columnName == "Decrease" || columnName == "Delete")
                {
                    // Lấy giá trị Qty và kiểm tra null
                    object qtyValue = dgvCash.Rows[e.RowIndex].Cells["Qty"].Value;
                    if (qtyValue == null || string.IsNullOrWhiteSpace(qtyValue.ToString()))
                    {
                        MessageBox.Show("Số lượng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (columnName == "Increase")
                    {
                        int qty = Convert.ToInt32(qtyValue);
                        qty++;
                        dgvCash.Rows[e.RowIndex].Cells["Qty"].Value = qty;
                        cart[e.RowIndex].Qty = qty;
                        UpdateTotalInRow(e.RowIndex);
                    }
                    else if (columnName == "Decrease")
                    {
                        int qty = Convert.ToInt32(qtyValue);
                        if (qty > 1)
                        {
                            qty--;
                            dgvCash.Rows[e.RowIndex].Cells["Qty"].Value = qty;
                            cart[e.RowIndex].Qty = qty;
                            UpdateTotalInRow(e.RowIndex);
                        }
                    }
                    else if (columnName == "Delete")
                    {
                        cart.RemoveAt(e.RowIndex);
                        LoadCash();
                    }

                    lblTotal.Text = CalculateTotal().ToString("F2");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Số lượng phải là một số nguyên hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}\nStackTrace: {ex.StackTrace}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void UpdateTotalInRow(int rowIndex)
        {
            var priceVal = dgvCash.Rows[rowIndex].Cells["Price"].Value;
            var qtyVal = dgvCash.Rows[rowIndex].Cells["Qty"].Value;

            decimal price = priceVal != null ? Convert.ToDecimal(priceVal) : 0;
            int qty = 0;
            if (qtyVal != null && int.TryParse(qtyVal.ToString(), out int parsedQty))
                qty = parsedQty;

            dgvCash.Rows[rowIndex].Cells["Total"].Value = price * qty;
        }

        public void AddSelectedItems(List<Cash> items)
        {
            try
            {
                foreach (var item in items)
                {
                    if (string.IsNullOrEmpty(item.Transno))
                        item.Transno = lblTransno.Text;
                    if (string.IsNullOrEmpty(item.CashID))
                        item.CashID = GenerateCashID();
                   // if (string.IsNullOrEmpty(item.Cashier))
                     //   item.Cashier = "DEFAULT_CASHIER";

                    cart.Add(item);             // Quan trọng ❗
                    cashBL.AddCash(item);       // Tuỳ bạn có muốn lưu DB không
                }

                LoadCash(); // Hiển thị lại DataGridView
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm các mục được chọn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCash_Click(object sender, EventArgs e)
        {
            using (var customerForm = new CashCustomer())
            {
                if (customerForm.ShowDialog() == DialogResult.OK && customerForm.SelectedCustomer != null)
                {
                    selectedCustomer = customerForm.SelectedCustomer;

                    // ✅ In ra kiểm tra tên
                    Console.WriteLine($"Chọn khách hàng: {selectedCustomer.FirstName} {selectedCustomer.LastName}");

                    foreach (var cash in cart)
                    {
                        cash.Cid = selectedCustomer.CustomerID;
                        cash.CustomerName = $"{selectedCustomer.FirstName} {selectedCustomer.LastName}";
                        cashBL.AddCash(cash);
                    }

                    if (MessageBox.Show("Bạn có chắc muốn thanh toán?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cashBL.SaveTransaction(cart, lblTransno.Text);
                        MessageBox.Show("Thanh toán thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cart.Clear();
                        LoadCash();
                        lblTransno.Text = getTransno();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (var cashProductForm = new CashProduct(this))
            {
                var result = cashProductForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Dữ liệu đã được thêm vào giỏ từ CashProduct qua cashForm.AddSelectedItems()
                    LoadCash();
                }
            }
        }

        private void SelectCustomer()
        {
            using (var customerForm = new CashCustomer())
            {
                if (customerForm.ShowDialog() == DialogResult.OK && customerForm.SelectedCustomer != null)
                {
                    selectedCustomer = customerForm.SelectedCustomer;
                    CustomerName = $"{selectedCustomer.FirstName} {selectedCustomer.LastName}";
                    CustomerName = CustomerName.ToString(); // ✅ Cập nhật giao diện
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        public void AddProductToCash(string productID, string productName, decimal price)
        {
            try
            {
                foreach (var item in cart)
                {
                    // Thêm từng sản phẩm vào DataGridView
                    var row = new DataGridViewRow();
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Pcode });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Pname });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Qty });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Price });
                    row.Cells.Add(new DataGridViewTextBoxCell { Value = item.Total });

                    dgvCash.Rows.Add(row);
                }

                // Cập nhật lại thông tin tổng giá trị
                UpdateTotalAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm sản phẩm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateTotalAmount()
        {
            decimal totalAmount = 0;

            foreach (DataGridViewRow row in dgvCash.Rows)
            {
                decimal rowTotal = Convert.ToDecimal(row.Cells["Total"].Value);
                totalAmount += rowTotal;
            }

            lblTotal.Text = $"Tổng tiền: {totalAmount:C}";
        }

    }
}
