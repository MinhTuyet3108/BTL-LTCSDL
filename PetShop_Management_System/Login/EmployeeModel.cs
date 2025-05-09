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
using TransObject;

namespace Login
{
    public partial class UserModule : Form
    {
        string title = "PetShop Management System";
        private EmployeeBL employeeBL;

        public UserModule()
        {
            InitializeComponent();
            employeeBL = new EmployeeBL();
        }

        public void LoadEmployee()
        {
            dgvEmployee.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //điều chỉnh kích thước vừa với khung

            // set data
            try
            {
                dgvEmployee.DataSource = employeeBL.GetEmployees();
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void dgvEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Employee employee = new Employee();
                if (e.RowIndex >= 0)
                {
                    if (dgvEmployee.Columns[e.ColumnIndex].Name == "Edit")
                    {
                        // Load dữ liệu lên textbox
                        txtID.Text = dgvEmployee.Rows[e.RowIndex].Cells["EmployeeID"].Value.ToString();
                        txtLName.Text = dgvEmployee.Rows[e.RowIndex].Cells["LastName"].Value.ToString();
                        txtFName.Text = dgvEmployee.Rows[e.RowIndex].Cells["FirstName"].Value.ToString();
                        cbPosition.Text = dgvEmployee.Rows[e.RowIndex].Cells["Position"].Value.ToString();
                        txtSalary.Text = dgvEmployee.Rows[e.RowIndex].Cells["Salary"].Value.ToString();
                        txtPhone.Text = dgvEmployee.Rows[e.RowIndex].Cells["Phone"].Value.ToString();
                        txtUsername.Text = dgvEmployee.Rows[e.RowIndex].Cells["Username"].Value.ToString();
                        txtPass.Text = dgvEmployee.Rows[e.RowIndex].Cells["Password"].Value?.ToString();

                    }
                    else if (dgvEmployee.Columns[e.ColumnIndex].Name == "Delete")
                    {
                            string id = dgvEmployee.Rows[e.RowIndex].Cells["EmployeeID"].Value.ToString();
                            if (MessageBox.Show("Delete this employee?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                employeeBL.DeleteEmployee(id);
                                LoadEmployee();
                            }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UserModule_Load(object sender, EventArgs e)
        {
            LoadEmployee();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtFName.Text) || string.IsNullOrWhiteSpace(txtLName.Text) ||
    string.IsNullOrWhiteSpace(cbPosition.Text) || string.IsNullOrWhiteSpace(txtSalary.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) ||
    string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Please fill in all information. ", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Do you want to save this employee information?", "New Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Employee employee = new Employee
                {
                    EmployeeID = txtID.Text,
                    FirstName = txtFName.Text,
                    LastName = txtLName.Text,
                    Position = cbPosition.Text,
                    Salary = Convert.ToDecimal(txtSalary.Text),
                    Phone = txtPhone.Text,
                    Username = txtUsername.Text,
                    Password = txtPass.Text
                };

                employeeBL.AddEmployee(employee);
                MessageBox.Show("Employee added successfully!", title);
                LoadEmployee();
                Clear();

            }
        }

        public void Clear()
        {
            txtID.Clear();
            txtLName.Clear();
            txtFName.Clear();
            cbPosition.SelectedIndex = -1; // hoặc cbPosition.Text = "";
            txtSalary.Clear();
            txtPhone.Clear();
            txtUsername.Clear();
            txtPass.Clear();

            txtID.Focus();
            btnSave.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text) || string.IsNullOrWhiteSpace(txtFName.Text) || string.IsNullOrWhiteSpace(txtLName.Text) ||
    string.IsNullOrWhiteSpace(cbPosition.Text) || string.IsNullOrWhiteSpace(txtSalary.Text) || string.IsNullOrWhiteSpace(txtPhone.Text) ||
    string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Please fill in all information. ", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
                {
                    if (MessageBox.Show("Do you want to update this employee information?", "Update Employee", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Employee emp = new Employee()
                        {
                            EmployeeID = txtID.Text,
                            LastName = txtLName.Text,
                            FirstName = txtFName.Text,
                            Position = cbPosition.Text,
                            Salary = decimal.Parse(txtSalary.Text),
                            Phone = txtPhone.Text,
                            Username = txtUsername.Text,
                            Password = txtPass.Text
                        };


                        if (employeeBL.UpdateEmployee(emp))
                        {
                            MessageBox.Show("Update successfully!");
                            LoadEmployee();
                            Clear();
                        }
                    }
                }
                catch (SqlException ex)
                {

                    throw ex;
                }
          
        }
        public void LoadSearchEmployee(List<Employee> employee)
        {
            dgvEmployee.DataSource = null; // reset lại
            dgvEmployee.DataSource = employee; // tự động binding dữ liệu
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadEmployee(); //load toàn bộ nhân viên
            }
            else
            {
                List<Employee> result = employeeBL.SearchEmployees(keyword);
                LoadSearchEmployee(result); //load danh sách nhân viên đã tìm kiếm
            }
        }
    }

}
