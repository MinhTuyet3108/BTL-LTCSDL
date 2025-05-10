namespace Login
{
    partial class Home
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Home));
            this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnClose = new Guna.UI2.WinForms.Guna2Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbUsername = new System.Windows.Forms.Label();
            this.guna2CirclePictureBox1 = new Guna.UI2.WinForms.Guna2CirclePictureBox();
            this.btnLogout = new Guna.UI2.WinForms.Guna2Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAppointment = new Guna.UI2.WinForms.Guna2Button();
            this.btnDashboard = new Guna.UI2.WinForms.Guna2Button();
            this.btUser = new Guna.UI2.WinForms.Guna2Button();
            this.btCash = new Guna.UI2.WinForms.Guna2Button();
            this.btPet = new Guna.UI2.WinForms.Guna2Button();
            this.btProduct = new Guna.UI2.WinForms.Guna2Button();
            this.btCustomer = new Guna.UI2.WinForms.Guna2Button();
            this.panelChild = new System.Windows.Forms.Panel();
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // guna2Elipse1
            // 
            this.guna2Elipse1.BorderRadius = 18;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Century", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1190, 42);
            this.lblTitle.TabIndex = 9;
            this.lblTitle.Text = "Welcome to Pet Shop Management System";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panel4.Controls.Add(this.lblTitle);
            this.panel4.Controls.Add(this.btnClose);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1226, 52);
            this.panel4.TabIndex = 9;
            // 
            // btnClose
            // 
            this.btnClose.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnClose.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnClose.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FillColor = System.Drawing.Color.Transparent;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.Location = new System.Drawing.Point(1190, 0);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 52);
            this.btnClose.TabIndex = 3;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lbUsername);
            this.panel3.Controls.Add(this.guna2CirclePictureBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(224, 147);
            this.panel3.TabIndex = 3;
            // 
            // lbUsername
            // 
            this.lbUsername.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsername.Location = new System.Drawing.Point(6, 73);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(191, 56);
            this.lbUsername.TabIndex = 5;
            this.lbUsername.Text = "Username";
            this.lbUsername.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2CirclePictureBox1
            // 
            this.guna2CirclePictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("guna2CirclePictureBox1.Image")));
            this.guna2CirclePictureBox1.ImageRotate = 0F;
            this.guna2CirclePictureBox1.Location = new System.Drawing.Point(48, 5);
            this.guna2CirclePictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.guna2CirclePictureBox1.Name = "guna2CirclePictureBox1";
            this.guna2CirclePictureBox1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CirclePictureBox1.Size = new System.Drawing.Size(72, 66);
            this.guna2CirclePictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.guna2CirclePictureBox1.TabIndex = 5;
            this.guna2CirclePictureBox1.TabStop = false;
            // 
            // btnLogout
            // 
            this.btnLogout.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLogout.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLogout.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FillColor = System.Drawing.Color.Transparent;
            this.btnLogout.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.DarkGray;
            this.btnLogout.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btnLogout.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btnLogout.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnLogout.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            this.btnLogout.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLogout.ImageOffset = new System.Drawing.Point(10, 0);
            this.btnLogout.Location = new System.Drawing.Point(0, 528);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(224, 46);
            this.btnLogout.TabIndex = 7;
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnLogout.TextOffset = new System.Drawing.Point(10, 0);
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAppointment);
            this.panel2.Controls.Add(this.btnDashboard);
            this.panel2.Controls.Add(this.btUser);
            this.panel2.Controls.Add(this.btCash);
            this.panel2.Controls.Add(this.btnLogout);
            this.panel2.Controls.Add(this.btPet);
            this.panel2.Controls.Add(this.btProduct);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.btCustomer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 52);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(224, 574);
            this.panel2.TabIndex = 10;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btnAppointment
            // 
            this.btnAppointment.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAppointment.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAppointment.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAppointment.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAppointment.FillColor = System.Drawing.Color.Transparent;
            this.btnAppointment.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAppointment.ForeColor = System.Drawing.Color.DarkGray;
            this.btnAppointment.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btnAppointment.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btnAppointment.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnAppointment.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image2")));
            this.btnAppointment.Image = ((System.Drawing.Image)(resources.GetObject("btnAppointment.Image")));
            this.btnAppointment.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnAppointment.ImageOffset = new System.Drawing.Point(10, 0);
            this.btnAppointment.Location = new System.Drawing.Point(2, 439);
            this.btnAppointment.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAppointment.Name = "btnAppointment";
            this.btnAppointment.Size = new System.Drawing.Size(222, 36);
            this.btnAppointment.TabIndex = 13;
            this.btnAppointment.Text = "Appointment";
            this.btnAppointment.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnAppointment.TextOffset = new System.Drawing.Point(10, 0);
            this.btnAppointment.Click += new System.EventHandler(this.btnAppointment_Click);
            // 
            // btnDashboard
            // 
            this.btnDashboard.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDashboard.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDashboard.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDashboard.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDashboard.FillColor = System.Drawing.Color.Transparent;
            this.btnDashboard.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.ForeColor = System.Drawing.Color.DarkGray;
            this.btnDashboard.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btnDashboard.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btnDashboard.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image3")));
            this.btnDashboard.Image = ((System.Drawing.Image)(resources.GetObject("btnDashboard.Image")));
            this.btnDashboard.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDashboard.ImageOffset = new System.Drawing.Point(10, 0);
            this.btnDashboard.Location = new System.Drawing.Point(9, 177);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(191, 36);
            this.btnDashboard.TabIndex = 12;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnDashboard.TextOffset = new System.Drawing.Point(10, 0);
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btUser
            // 
            this.btUser.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btUser.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btUser.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btUser.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btUser.FillColor = System.Drawing.Color.Transparent;
            this.btUser.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btUser.ForeColor = System.Drawing.Color.DarkGray;
            this.btUser.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btUser.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btUser.HoverState.ForeColor = System.Drawing.Color.White;
            this.btUser.HoverState.Image = global::Login.Properties.Resources.us;
            this.btUser.Image = ((System.Drawing.Image)(resources.GetObject("btUser.Image")));
            this.btUser.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btUser.ImageOffset = new System.Drawing.Point(10, 0);
            this.btUser.Location = new System.Drawing.Point(6, 282);
            this.btUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btUser.Name = "btUser";
            this.btUser.Size = new System.Drawing.Size(194, 36);
            this.btUser.TabIndex = 12;
            this.btUser.Text = "User";
            this.btUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btUser.TextOffset = new System.Drawing.Point(10, 0);
            this.btUser.Click += new System.EventHandler(this.btUser_Click);
            // 
            // btCash
            // 
            this.btCash.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btCash.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btCash.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btCash.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btCash.FillColor = System.Drawing.Color.Transparent;
            this.btCash.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCash.ForeColor = System.Drawing.Color.DarkGray;
            this.btCash.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btCash.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btCash.HoverState.ForeColor = System.Drawing.Color.White;
            this.btCash.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image4")));
            this.btCash.Image = ((System.Drawing.Image)(resources.GetObject("btCash.Image")));
            this.btCash.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btCash.ImageOffset = new System.Drawing.Point(10, 0);
            this.btCash.Location = new System.Drawing.Point(2, 488);
            this.btCash.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btCash.Name = "btCash";
            this.btCash.Size = new System.Drawing.Size(195, 36);
            this.btCash.TabIndex = 11;
            this.btCash.Text = "Cash";
            this.btCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btCash.TextOffset = new System.Drawing.Point(10, 0);
            this.btCash.Click += new System.EventHandler(this.btCash_Click);
            // 
            // btPet
            // 
            this.btPet.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btPet.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btPet.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btPet.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btPet.FillColor = System.Drawing.Color.Transparent;
            this.btPet.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.btPet.ForeColor = System.Drawing.Color.DarkGray;
            this.btPet.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btPet.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btPet.HoverState.ForeColor = System.Drawing.Color.White;
            this.btPet.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image5")));
            this.btPet.Image = ((System.Drawing.Image)(resources.GetObject("btPet.Image")));
            this.btPet.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btPet.ImageOffset = new System.Drawing.Point(10, 0);
            this.btPet.Location = new System.Drawing.Point(6, 388);
            this.btPet.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btPet.Name = "btPet";
            this.btPet.Size = new System.Drawing.Size(195, 36);
            this.btPet.TabIndex = 10;
            this.btPet.Text = "Pet";
            this.btPet.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btPet.TextOffset = new System.Drawing.Point(10, 0);
            this.btPet.Click += new System.EventHandler(this.btPet_Click);
            // 
            // btProduct
            // 
            this.btProduct.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btProduct.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btProduct.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btProduct.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btProduct.FillColor = System.Drawing.Color.Transparent;
            this.btProduct.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btProduct.ForeColor = System.Drawing.Color.DarkGray;
            this.btProduct.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btProduct.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btProduct.HoverState.ForeColor = System.Drawing.Color.White;
            this.btProduct.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image6")));
            this.btProduct.Image = ((System.Drawing.Image)(resources.GetObject("btProduct.Image")));
            this.btProduct.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btProduct.ImageOffset = new System.Drawing.Point(10, 0);
            this.btProduct.Location = new System.Drawing.Point(6, 335);
            this.btProduct.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btProduct.Name = "btProduct";
            this.btProduct.Size = new System.Drawing.Size(195, 36);
            this.btProduct.TabIndex = 10;
            this.btProduct.Text = "Product";
            this.btProduct.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btProduct.TextOffset = new System.Drawing.Point(10, 0);
            this.btProduct.Click += new System.EventHandler(this.btProduct_Click);
            // 
            // btCustomer
            // 
            this.btCustomer.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btCustomer.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btCustomer.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btCustomer.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btCustomer.FillColor = System.Drawing.Color.Transparent;
            this.btCustomer.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCustomer.ForeColor = System.Drawing.Color.DarkGray;
            this.btCustomer.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btCustomer.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(172)))), ((int)(((byte)(220)))));
            this.btCustomer.HoverState.ForeColor = System.Drawing.Color.White;
            this.btCustomer.HoverState.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image7")));
            this.btCustomer.Image = ((System.Drawing.Image)(resources.GetObject("btCustomer.Image")));
            this.btCustomer.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btCustomer.ImageOffset = new System.Drawing.Point(10, 0);
            this.btCustomer.Location = new System.Drawing.Point(6, 230);
            this.btCustomer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btCustomer.Name = "btCustomer";
            this.btCustomer.Size = new System.Drawing.Size(195, 36);
            this.btCustomer.TabIndex = 8;
            this.btCustomer.Text = "Customers";
            this.btCustomer.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btCustomer.TextOffset = new System.Drawing.Point(10, 0);
            this.btCustomer.Click += new System.EventHandler(this.btCustomer_Click);
            // 
            // panelChild
            // 
            this.panelChild.BackColor = System.Drawing.Color.White;
            this.panelChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChild.Location = new System.Drawing.Point(224, 52);
            this.panelChild.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelChild.Name = "panelChild";
            this.panelChild.Size = new System.Drawing.Size(1002, 574);
            this.panelChild.TabIndex = 11;
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // guna2Elipse2
            // 
            this.guna2Elipse2.BorderRadius = 40;
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1226, 626);
            this.Controls.Add(this.panelChild);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Home";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Home";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2CirclePictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Button btnClose;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Label lbUsername;
        private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox1;
        private Guna.UI2.WinForms.Guna2Button btnLogout;
        private System.Windows.Forms.Panel panel2;
        private Guna.UI2.WinForms.Guna2Button btCash;
        private Guna.UI2.WinForms.Guna2Button btPet;
        private Guna.UI2.WinForms.Guna2Button btProduct;
        private Guna.UI2.WinForms.Guna2Button btCustomer;
        private Guna.UI2.WinForms.Guna2Button btUser;
        private System.Windows.Forms.Panel panelChild;
        private Guna.UI2.WinForms.Guna2BorderlessForm guna2BorderlessForm1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse2;
        private Guna.UI2.WinForms.Guna2Button btnDashboard;
        private Guna.UI2.WinForms.Guna2Button btnAppointment;
    }
}