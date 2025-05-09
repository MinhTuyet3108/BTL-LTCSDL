using BusinessLayer;
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
using TransObject;
using BusinessLayer;

namespace Login
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }

             private void btLogin_Click(object sender, EventArgs e)
        {
            Account account = new Account(txtUsername.Text, txtPass.Text)
            {
                Username = txtUsername.Text,
                Password = txtPass.Text
            };
            LoginBL bus = new LoginBL();

            if (bus.Login(account))
            {
                Home main = new Home(account.Username);
                this.Hide();
                main.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


     private void btForget_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ quản trị viên để lấy lại mật khẩu.", "Quên mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Exit Application?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

       
    }
}
