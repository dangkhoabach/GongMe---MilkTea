namespace GongMe.Report
{
    partial class FrmInThongKeNhapHang
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInThongKeNhapHang));
            this.btnPrint = new Guna.UI2.WinForms.Guna2GradientButton();
            this.RptThongKeNhapHang = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnExit = new Guna.UI2.WinForms.Guna2Button();
            this.txtTimTKNhapHang = new Guna.UI2.WinForms.Guna2TextBox();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.btnPrint.BorderRadius = 10;
            this.btnPrint.BorderThickness = 1;
            this.btnPrint.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnPrint.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnPrint.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPrint.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnPrint.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnPrint.FillColor = System.Drawing.Color.White;
            this.btnPrint.FillColor2 = System.Drawing.Color.White;
            resources.ApplyResources(this.btnPrint, "btnPrint");
            this.btnPrint.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // RptThongKeNhapHang
            // 
            this.RptThongKeNhapHang.LocalReport.ReportEmbeddedResource = "GongMe.Report.RptThongKeNhapHang.rdlc";
            resources.ApplyResources(this.RptThongKeNhapHang, "RptThongKeNhapHang");
            this.RptThongKeNhapHang.Name = "RptThongKeNhapHang";
            this.RptThongKeNhapHang.ServerReport.BearerToken = null;
            // 
            // btnExit
            // 
            this.btnExit.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnExit.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnExit.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnExit.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnExit.FillColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.btnExit, "btnExit");
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Image = global::GongMe.Properties.Resources.close;
            this.btnExit.Name = "btnExit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtTimTKNhapHang
            // 
            this.txtTimTKNhapHang.Animated = true;
            this.txtTimTKNhapHang.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.txtTimTKNhapHang.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimTKNhapHang.DefaultText = "";
            this.txtTimTKNhapHang.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimTKNhapHang.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimTKNhapHang.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimTKNhapHang.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimTKNhapHang.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.txtTimTKNhapHang, "txtTimTKNhapHang");
            this.txtTimTKNhapHang.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimTKNhapHang.IconLeft = global::GongMe.Properties.Resources.kinhlup;
            this.txtTimTKNhapHang.Name = "txtTimTKNhapHang";
            this.txtTimTKNhapHang.PasswordChar = '\0';
            this.txtTimTKNhapHang.PlaceholderText = "Nhập tháng cần in";
            this.txtTimTKNhapHang.SelectedText = "";
            this.txtTimTKNhapHang.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.txtTimTKNhapHang.TextChanged += new System.EventHandler(this.txtTimTKNhapHang_TextChanged);
            // 
            // FrmInThongKeNhapHang
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtTimTKNhapHang);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.RptThongKeNhapHang);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "FrmInThongKeNhapHang";
            this.Load += new System.EventHandler(this.FrmInThongKeNhapHang_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnExit;
        private Guna.UI2.WinForms.Guna2TextBox txtTimTKNhapHang;
        private Guna.UI2.WinForms.Guna2GradientButton btnPrint;
        private Microsoft.Reporting.WinForms.ReportViewer RptThongKeNhapHang;
    }
}