namespace GongMe.Report
{
    partial class FrmInPhieuXuat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmInPhieuXuat));
            this.btnExit = new Guna.UI2.WinForms.Guna2Button();
            this.txtTimPhieuXuat = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnPrint = new Guna.UI2.WinForms.Guna2GradientButton();
            this.rptPhieuXuat = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
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
            // txtTimPhieuXuat
            // 
            this.txtTimPhieuXuat.Animated = true;
            this.txtTimPhieuXuat.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.txtTimPhieuXuat.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimPhieuXuat.DefaultText = "";
            this.txtTimPhieuXuat.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimPhieuXuat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimPhieuXuat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimPhieuXuat.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimPhieuXuat.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            resources.ApplyResources(this.txtTimPhieuXuat, "txtTimPhieuXuat");
            this.txtTimPhieuXuat.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimPhieuXuat.IconLeft = global::GongMe.Properties.Resources.kinhlup;
            this.txtTimPhieuXuat.Name = "txtTimPhieuXuat";
            this.txtTimPhieuXuat.PasswordChar = '\0';
            this.txtTimPhieuXuat.PlaceholderText = "Nhập mã phiếu nhập cần in";
            this.txtTimPhieuXuat.SelectedText = "";
            this.txtTimPhieuXuat.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
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
            // rptPhieuXuat
            // 
            this.rptPhieuXuat.LocalReport.ReportEmbeddedResource = "GongMe.Report.RptPhieuXuat.rdlc";
            resources.ApplyResources(this.rptPhieuXuat, "rptPhieuXuat");
            this.rptPhieuXuat.Name = "rptPhieuXuat";
            this.rptPhieuXuat.ServerReport.BearerToken = null;
            // 
            // FrmInPhieuXuat
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtTimPhieuXuat);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.rptPhieuXuat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "FrmInPhieuXuat";
            this.Load += new System.EventHandler(this.FrmInPhieuXuat_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnExit;
        private Guna.UI2.WinForms.Guna2TextBox txtTimPhieuXuat;
        private Guna.UI2.WinForms.Guna2GradientButton btnPrint;
        private Microsoft.Reporting.WinForms.ReportViewer rptPhieuXuat;
    }
}