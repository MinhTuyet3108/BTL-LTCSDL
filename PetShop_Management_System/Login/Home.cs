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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace Login
{
    public partial class Home : Form
    {
        private readonly string position;              // Biến lưu chức vụ
        private readonly PermissionService permission; // Đối tượng phân quyền
        public Home(string username, string position)
        {
            InitializeComponent();
            lbUsername.Text = "Xin chào, " + username;
            this.position = position.Trim();
            this.permission = new PermissionService(this.position);
        }
        private Form activeForm = null;


    private void OpenChildForm(Form childForm)
    {
        if (activeForm != null)
            activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChild.Controls.Clear();
            panelChild.Controls.Add(childForm);
            panelChild.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
    }

       
        private void btCustomer_Click(object sender, EventArgs e)
        {
            if (!permission.CanAccessCustomer())
            {
                MessageBox.Show(permission.ShowAccessDenied(), "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new CustomerModule());
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void btUser_Click(object sender, EventArgs e)
        {
            if (!permission.CanAccessEmployee())
            {
                MessageBox.Show(permission.ShowAccessDenied(), "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new UserModule());
            
        }

        private void btProduct_Click(object sender, EventArgs e)
        {
            if (!permission.CanAccessProduct())
            {
                MessageBox.Show(permission.ShowAccessDenied(), "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new ProductModule());

        }

        private void btCash_Click(object sender, EventArgs e)
        {
            if (!permission.CanAccessCash())
            {
                MessageBox.Show(permission.ShowAccessDenied(), "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                OpenChildForm(new CashForm());
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            if (!permission.CanAccessDashboard())
            {
                MessageBox.Show(permission.ShowAccessDenied(), "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new Dashboard());

        }

        private void btPet_Click(object sender, EventArgs e)
        {
            if (!permission.CanAccessPet())
            {
                MessageBox.Show(permission.ShowAccessDenied(), "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new PetModule());
            
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
                if (MessageBox.Show("Bạn có chắc muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Hide();
                LogForm loginForm = new LogForm();
                    loginForm.ShowDialog();
                    this.Close();
                }

        }

        private void btnAppointment_Click(object sender, EventArgs e)
        {
            if (!permission.CanAccessAppointment())
            {
                MessageBox.Show(permission.ShowAccessDenied(), "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenChildForm(new AppointmentModule());
        }
    }
}
