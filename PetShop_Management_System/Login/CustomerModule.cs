﻿using System;
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
using System.Xml.Linq;
using System.Security.AccessControl;

namespace Login
{
    public partial class CustomerModule : Form
    {
        private CustomerBL customerBL;
        public CustomerModule()
        {
            InitializeComponent();
            customerBL = new CustomerBL();
        }

        private void CustomerModule_Load(object sender, EventArgs e)
        {
            LoadCustomer();
        }
        public void LoadCustomer()
        {
            try
            {
                dgvCustomer.Rows.Clear(); // Xóa dữ liệu cũ trước khi load mới
                List<Customer> customers = customerBL.GetCustomers(); // Lấy danh sách từ lớp Business
                int i = 0;
                foreach (Customer c in customers)
                {
                    dgvCustomer.Rows.Add(
                      i++,
                        c.CustomerID,
                        c.LastName,
                        c.FirstName,
                        c.Gender,
                        c.Phone,
                        c.Address,
                        c.Email
                    );
                }

            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        private void btAdd_Click(object sender, EventArgs e)
        {
            Customer c = new Customer(
      txtID.Text,
      txtLname.Text,
      txtFname.Text,
      cbGender.Text,
      txtPhone.Text,
      txtAddress.Text,
      txtEmail.Text
  );
            customerBL.AddCustomer(c);
            LoadCustomer();
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void Clear()
        {
            txtID.Clear();
            txtLname.Clear();
            txtFname.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtEmail.Clear();

            txtID.Focus();
            btAdd.Enabled = true;
            btupdate.Enabled = false;
        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    if (dgvCustomer.Columns[e.ColumnIndex].Name == "Edit")
                    {
                        // Load dữ liệu lên textbox
                        txtID.Text = dgvCustomer.Rows[e.RowIndex].Cells["CustomerID"].Value.ToString();
                        txtLname.Text = dgvCustomer.Rows[e.RowIndex].Cells["LastName"].Value.ToString();
                        txtFname.Text = dgvCustomer.Rows[e.RowIndex].Cells["FirstName"].Value.ToString();
                        cbGender.Text = dgvCustomer.Rows[e.RowIndex].Cells["Gender"].Value.ToString();
                        txtPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells["Phone"].Value.ToString();
                        txtAddress.Text = dgvCustomer.Rows[e.RowIndex].Cells["Address"].Value.ToString();
                        txtEmail.Text = dgvCustomer.Rows[e.RowIndex].Cells["Email"].Value?.ToString();
                    }
                    else if (dgvCustomer.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        string id = dgvCustomer.Rows[e.RowIndex].Cells["CustomerID"].Value.ToString();
                        if (MessageBox.Show("Xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            customerBL.DeleteCustomer(id);
                            LoadCustomer();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {

                throw ex;
            }
           
        }

       
        private void btupdate_Click(object sender, EventArgs e)
        {
            Customer c = new Customer()
            {
                CustomerID = txtID.Text,
                LastName = txtLname.Text,
                FirstName = txtFname.Text,
                Gender = cbGender.Text,
                Phone = txtPhone.Text,
                Address = txtAddress.Text,
                Email = txtEmail.Text
            };


            if (customerBL.UpdateCustomer(c))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadCustomer();
            }
        }

        public void LoadSearchCustomer(List<Customer> customers)
        {
            dgvCustomer.Rows.Clear(); // Xóa dữ liệu cũ
            int i = 0;
            foreach (Customer c in customers)
            {
                dgvCustomer.Rows.Add(
                    i++,
                    c.CustomerID,
                    c.LastName,
                    c.FirstName,
                    c.Gender,
                    c.Phone,
                    c.Address,
                    c.Email
                );
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadCustomer(); // Trở lại danh sách gốc nếu không có từ khóa
            }
            else
            {
                List<Customer> result = customerBL.SearchCustomers(keyword);
                LoadSearchCustomer(result); // Dùng hàm riêng
            }
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}