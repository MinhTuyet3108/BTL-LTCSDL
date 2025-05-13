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
using System.Drawing.Printing;
using System.Web.Configuration;

namespace Login
{
    public partial class CashForm : Form
    {
        private string _currentTransno = null; // Lưu Transno để tái sử dụng trong cùng giao dịch
        private PrintDocument printDocument = new PrintDocument();
        private PrintDialog printDialog = new PrintDialog();
        private List<Cash> cart = new List<Cash>();
        private CashCustomer cashCustomerForm;
        private CashProduct cashProductForm;
        private CashBL cashBL;
        private Customer selectedCustomer;
        private string cashId;
        private static string _lastDate = "";
        private static int _sequence = 0;

        public object CustomerName { get; private set; }

        public CashForm()
        {
            InitializeComponent();
            cashBL = new CashBL();
            LoadCash();
            _currentTransno = getTransno(); // Gán giá trị ban đầu
            lblTransno.Text = _currentTransno;
            //dgvCash.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCash_CellContentClick);
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage); // Gán sự kiện in

            // Đảm bảo cột Date được định dạng
            if (dgvCash.Columns.Contains("Date"))
            {
                dgvCash.Columns["Date"].DefaultCellStyle.Format = "dd/MM/yyyy";
            }
        }

        private void LoadCash()
        {
            try
            {
                dgvCash.DataSource = null;
                if (cart != null)
                {
                    dgvCash.DataSource = cart.ToList();
                    for (int i = 0; i < cart.Count; i++)
                    {
                        cart[i].No = i + 1; // Gán số thứ tự
                    }
                }
                UpdateTotalAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string GenerateCashID()
        {
            return $"CASH{DateTime.Now.Ticks % 1000:D3}";
        }
        private string getTransno()
        {
            return $"{DateTime.Now:yyyyMMddHHmmss}";

        }

        private decimal CalculateTotal()
        {
            return cart.Sum(cash => (cash.Qty ?? 0) * cash.Price);
        }

        private void dgvCash_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // Kiểm tra chỉ số hợp lệ
                if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= dgvCash.Rows.Count || e.ColumnIndex >= dgvCash.Columns.Count)
                {
                    return;
                }

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
                    int qty = Convert.ToInt32(qtyValue);

                    if (columnName == "Increase")
                    {
                        qty++;
                        dgvCash.Rows[e.RowIndex].Cells["Qty"].Value = qty;
                        cart[e.RowIndex].Qty = qty;
                        UpdateTotalInRow(e.RowIndex);
                    }
                    else if (columnName == "Decrease")
                    {
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
                    UpdateTotalAmount();
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
            try
            {
                if (rowIndex < 0 || rowIndex >= dgvCash.Rows.Count)
                {
                    return; // Thoát nếu chỉ số không hợp lệ
                }
                var priceVal = dgvCash.Rows[rowIndex].Cells["Price"].Value;
                var qtyVal = dgvCash.Rows[rowIndex].Cells["Qty"].Value;

                decimal price = priceVal != null ? Convert.ToDecimal(priceVal) : 0;
                int qty = 0;
                if (qtyVal != null && int.TryParse(qtyVal.ToString(), out int parsedQty))
                    qty = parsedQty;

                decimal total = price * qty;
                dgvCash.Rows[rowIndex].Cells["Total"].Value = total;
                cart[rowIndex].Total = total;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public void AddSelectedItems(List<Cash> items)
        {

            try
            {
                foreach (var item in items)
                {

                    if (string.IsNullOrEmpty(item.CashID))
                        item.CashID = GenerateCashID();
                    if (string.IsNullOrEmpty(item.Cashier))
                        item.Cashier = "Nhân viên bán hàng";
                    if (item.Date == default(DateTime)) // Kiểm tra nếu Date chưa được gán
                        item.Date = DateTime.Now; // Gán ngày giờ hiện tại

                    cart.Add(item);
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
                    lblTransno.Text = getTransno();
                    foreach (var cash in cart)
                    {
                        cash.Cid = selectedCustomer.CustomerID;
                        cash.CustomerName = $"{selectedCustomer.FirstName} {selectedCustomer.LastName}";
                        cash.Transno = lblTransno.Text;
                    }
                    LoadCash();
                    UpdateTotalAmount();   // cập nhật tổng tiền
                    lblTotal.Text = CalculateTotal().ToString("F2");
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
                    CustomerName = CustomerName.ToString(); //Cập nhật giao diện
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
                var item = new Cash
                {
                    Pcode = productID,
                    Pname = productName,
                    Qty = 1, // Giá trị mặc định
                    Price = price,
                    Total = price, // Tổng ban đầu bằng giá
                    Date = DateTime.Now


                };

                if (string.IsNullOrEmpty(item.CashID))
                    item.CashID = GenerateCashID();
                if (string.IsNullOrEmpty(item.Cashier))
                    item.Cashier = "Employee";

                cart.Add(item);
                LoadCash();
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
                var totalValue = row.Cells["Total"].Value;
                if (totalValue != null && decimal.TryParse(totalValue.ToString(), out decimal rowTotal))
                {
                    totalAmount += rowTotal;
                }
            }
            lblTotal.Text = $"Tổng tiền: {totalAmount:C}";
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            if (selectedCustomer == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cart == null || !cart.Any())
            {
                MessageBox.Show("Giỏ hàng trống, không thể thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn thanh toán?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                cashBL.SaveTransaction(cart, lblTransno.Text);
                MessageBox.Show("Thanh toán thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // In hóa đơn trước khi xóa cart
                if (cart.Count > 0)
                {
                    try
                    {
                        Console.WriteLine($"_currentTransno trước khi in: {_currentTransno}"); // Debug
                        printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", 315, 1000); // Khổ giấy 80mm
                        printDocument.DefaultPageSettings.Margins = new Margins(10, 10, 10, 10);
                        printDocument.Print();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi in hóa đơn: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Không có giao dịch để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                // Xóa cart sau khi in
                cart.Clear();
                selectedCustomer = null;
                LoadCash();
                _currentTransno = getTransno(); // Cập nhật Transno mới
                lblTransno.Text = _currentTransno;

            }
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {

        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font font = new Font("Arial", 12);
            Font boldFont = new Font("Arial", 12, FontStyle.Bold);
            float fontHeight = font.GetHeight();
            int startX = 10;
            int startY = 10;
            int offsetY = 0;

            // Tiêu đề hóa đơn
            g.DrawString("HÓA ĐƠN BÁN HÀNG", boldFont, Brushes.Black, startX, startY + offsetY);
            offsetY += (int)fontHeight + 25;

            // Thông tin giao dịch
            g.DrawString($"Mã giao dịch: {_currentTransno}", font, Brushes.Black, startX, startY + offsetY);
            offsetY += (int)fontHeight + 10;
            g.DrawString($"Ngày: {DateTime.Now:dd/MM/yyyy HH:mm:ss}", font, Brushes.Black, startX, startY + offsetY);
            offsetY += (int)fontHeight + 10;
            g.DrawString($"Khách hàng: {(selectedCustomer != null ? $" {selectedCustomer.LastName} {selectedCustomer.FirstName}" : "Không có")}", font, Brushes.Black, startX, startY + offsetY);
            offsetY += (int)fontHeight + 10;
            g.DrawString($"Nhân viên: {(cart.Any() ? cart[0].Cashier : "Employee")}", font, Brushes.Black, startX, startY + offsetY);
            offsetY += (int)fontHeight + 25;

            // Tiêu đề bảng sản phẩm
            g.DrawString("Tên sản phẩm \tSố lượng \tĐơn giá \tThành tiền", boldFont, Brushes.Black, startX, startY + offsetY);
            offsetY += (int)fontHeight + 10;
            g.DrawString("---------------------------------------------------------", font, Brushes.Black, startX, startY + offsetY);
            offsetY += (int)fontHeight + 10;

            // Danh sách sản phẩm
            foreach (var item in cart)
            {
                g.DrawString($"{item.Pname} \t{item.Qty} \t{item.Price:F2} \t{item.Total:F2}", font, Brushes.Black, startX, startY + offsetY);
                offsetY += (int)fontHeight + 10;
            }

            // Tổng tiền
            g.DrawString("----------------------------------------------------------", font, Brushes.Black, startX, startY + offsetY);
            offsetY += (int)fontHeight + 10;
            g.DrawString($"Tổng tiền: {lblTotal.Text} VND", boldFont, Brushes.Black, startX, startY + offsetY);
            offsetY += (int)fontHeight + 25;

            // Lời cảm ơn
            g.DrawString("Cảm ơn quý khách!", font, Brushes.Black, startX, startY + offsetY);
        }
    }
}
