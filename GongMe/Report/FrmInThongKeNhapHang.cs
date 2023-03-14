using Microsoft.Reporting.WinForms;
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

namespace GongMe.Report
{
    public partial class FrmInThongKeNhapHang : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DANGKHOABACH;Initial Catalog=QLTiemTraSua;Integrated Security=True");

        public FrmInThongKeNhapHang()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmInThongKeNhapHang_Load(object sender, EventArgs e)
        {
            this.RptThongKeNhapHang.RefreshReport();
        }

        private void txtTimTKNhapHang_TextChanged(object sender, EventArgs e)
        {
            int box_int = 0;
            Int32.TryParse(txtTimTKNhapHang.Text, out box_int);
            if (box_int > 12)
            {
                txtTimTKNhapHang.Text = "12";
            }
            else
                if (box_int < 1)
            {
                txtTimTKNhapHang.Text = "1";
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtTimTKNhapHang.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tháng cần in", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn in thống kê nhập hàng không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String sql = ("select *from View_All_PhieuNhap where Month(NgayNhap) = '" + txtTimTKNhapHang.Text + "'");
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    //DataTable tk = new DataTable();
                    DSPhieuNhap ds = new DSPhieuNhap();
                    da.Fill(ds, "DataTable_PhieuNhap");

                    ReportDataSource dataSource = new ReportDataSource("DataSet_RptThongKeNhapHang", ds.Tables[0]);

                    this.RptThongKeNhapHang.LocalReport.DataSources.Clear();
                    this.RptThongKeNhapHang.LocalReport.DataSources.Add(dataSource);
                    this.RptThongKeNhapHang.RefreshReport();
                }
            }
        }
    }
}
