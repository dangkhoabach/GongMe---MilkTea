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
    public partial class FrmInThongKeXuatHang : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DANGKHOABACH;Initial Catalog=QLTiemTraSua;Integrated Security=True");

        public FrmInThongKeXuatHang()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmInThongKeXuatHang_Load(object sender, EventArgs e)
        {
            this.RptThongKeXuatHang.RefreshReport();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtTimTKXuatHang.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tháng cần in", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn in thống kê xuất hàng không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String sql = ("select *from View_All_PhieuXuat where Month(NgayXuat) = '" + txtTimTKXuatHang.Text + "'");
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    //DataTable tk = new DataTable();
                    DSPhieuXuat ds = new DSPhieuXuat();
                    da.Fill(ds, "DataTable_PhieuXuat");

                    ReportDataSource dataSource = new ReportDataSource("DataSet_RptThongKeXuatHang", ds.Tables[0]);

                    this.RptThongKeXuatHang.LocalReport.DataSources.Clear();
                    this.RptThongKeXuatHang.LocalReport.DataSources.Add(dataSource);
                    this.RptThongKeXuatHang.RefreshReport();
                }
            }
        }

        private void txtTimTKXuatHang_TextChanged_1(object sender, EventArgs e)
        {
            int box_int = 0;
            Int32.TryParse(txtTimTKXuatHang.Text, out box_int);
            if (box_int > 12)
            {
                txtTimTKXuatHang.Text = "12";
            }
            else
                if (box_int < 1)
            {
                txtTimTKXuatHang.Text = "1";
            }
        }
    }
}
