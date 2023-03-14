namespace GongMe.PresentionTier
{
    partial class FrmThongKeTonKho
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
            this.lvsTonKho = new System.Windows.Forms.ListView();
            this.colMaMathang = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTenMatHang = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSoLuongTon = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNgayNhap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnExcel = new Guna.UI2.WinForms.Guna2GradientButton();
            this.btnCheck = new Guna.UI2.WinForms.Guna2GradientButton();
            this.lbTimKiem = new System.Windows.Forms.Label();
            this.txtTim = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvsTonKho
            // 
            this.lvsTonKho.BackColor = System.Drawing.Color.SkyBlue;
            this.lvsTonKho.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colMaMathang,
            this.colTenMatHang,
            this.colSoLuongTon,
            this.colNgayNhap});
            this.lvsTonKho.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lvsTonKho.ForeColor = System.Drawing.Color.Black;
            this.lvsTonKho.HideSelection = false;
            this.lvsTonKho.Location = new System.Drawing.Point(0, 0);
            this.lvsTonKho.Margin = new System.Windows.Forms.Padding(2);
            this.lvsTonKho.Name = "lvsTonKho";
            this.lvsTonKho.Size = new System.Drawing.Size(1100, 530);
            this.lvsTonKho.TabIndex = 1;
            this.lvsTonKho.UseCompatibleStateImageBehavior = false;
            this.lvsTonKho.View = System.Windows.Forms.View.Details;
            // 
            // colMaMathang
            // 
            this.colMaMathang.Text = "Mã mặt hàng";
            this.colMaMathang.Width = 250;
            // 
            // colTenMatHang
            // 
            this.colTenMatHang.Text = "Tên mặt hàng";
            this.colTenMatHang.Width = 250;
            // 
            // colSoLuongTon
            // 
            this.colSoLuongTon.Text = "Số lượng tồn";
            this.colSoLuongTon.Width = 250;
            // 
            // colNgayNhap
            // 
            this.colNgayNhap.Text = "Ngày nhập";
            this.colNgayNhap.Width = 250;
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.Controls.Add(this.lvsTonKho);
            this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.guna2GroupBox1.Location = new System.Drawing.Point(10, 10);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Size = new System.Drawing.Size(1100, 530);
            this.guna2GroupBox1.TabIndex = 29;
            this.guna2GroupBox1.Text = "guna2GroupBox1";
            // 
            // btnExcel
            // 
            this.btnExcel.AutoRoundedCorners = true;
            this.btnExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnExcel.BorderRadius = 23;
            this.btnExcel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExcel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExcel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExcel.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExcel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExcel.FillColor = System.Drawing.Color.DeepSkyBlue;
            this.btnExcel.FillColor2 = System.Drawing.Color.RoyalBlue;
            this.btnExcel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnExcel.ForeColor = System.Drawing.Color.White;
            this.btnExcel.Location = new System.Drawing.Point(956, 554);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(2);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(153, 49);
            this.btnExcel.TabIndex = 2;
            this.btnExcel.Text = "Xuất Excel";
            this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnCheck.BorderRadius = 10;
            this.btnCheck.BorderThickness = 1;
            this.btnCheck.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCheck.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCheck.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCheck.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCheck.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCheck.FillColor = System.Drawing.Color.White;
            this.btnCheck.FillColor2 = System.Drawing.Color.White;
            this.btnCheck.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnCheck.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnCheck.Location = new System.Drawing.Point(423, 559);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(112, 40);
            this.btnCheck.TabIndex = 3;
            this.btnCheck.Text = "Kiểm tra";
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // lbTimKiem
            // 
            this.lbTimKiem.AutoSize = true;
            this.lbTimKiem.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.lbTimKiem.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lbTimKiem.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lbTimKiem.Location = new System.Drawing.Point(12, 572);
            this.lbTimKiem.Name = "lbTimKiem";
            this.lbTimKiem.Size = new System.Drawing.Size(77, 19);
            this.lbTimKiem.TabIndex = 38;
            this.lbTimKiem.Text = "Tìm kiếm";
            // 
            // txtTim
            // 
            this.txtTim.Animated = true;
            this.txtTim.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.txtTim.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTim.DefaultText = "";
            this.txtTim.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTim.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTim.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTim.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTim.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTim.Font = new System.Drawing.Font("Arial", 9F);
            this.txtTim.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTim.IconLeft = global::GongMe.Properties.Resources.kinhlup;
            this.txtTim.Location = new System.Drawing.Point(104, 563);
            this.txtTim.Name = "txtTim";
            this.txtTim.PasswordChar = '\0';
            this.txtTim.PlaceholderText = "Tìm kiếm";
            this.txtTim.SelectedText = "";
            this.txtTim.Size = new System.Drawing.Size(292, 36);
            this.txtTim.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.txtTim.TabIndex = 37;
            this.txtTim.TextChanged += new System.EventHandler(this.txtTim_TextChanged);
            // 
            // FrmThongKeTonKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1120, 615);
            this.Controls.Add(this.lbTimKiem);
            this.Controls.Add(this.txtTim);
            this.Controls.Add(this.guna2GroupBox1);
            this.Controls.Add(this.btnExcel);
            this.Controls.Add(this.btnCheck);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "FrmThongKeTonKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê tồn kho";
            this.guna2GroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvsTonKho;
        private System.Windows.Forms.ColumnHeader colMaMathang;
        private System.Windows.Forms.ColumnHeader colTenMatHang;
        private System.Windows.Forms.ColumnHeader colSoLuongTon;
        private System.Windows.Forms.ColumnHeader colNgayNhap;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private Guna.UI2.WinForms.Guna2GradientButton btnExcel;
        private Guna.UI2.WinForms.Guna2GradientButton btnCheck;
        private System.Windows.Forms.Label lbTimKiem;
        private Guna.UI2.WinForms.Guna2TextBox txtTim;
    }
}