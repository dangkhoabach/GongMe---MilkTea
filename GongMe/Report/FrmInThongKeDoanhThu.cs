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
    public partial class FrmInThongKeDoanhThu : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DANGKHOABACH;Initial Catalog=QLTiemTraSua;Integrated Security=True");

        public FrmInThongKeDoanhThu()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmInThongKeDoanhThu_Load(object sender, EventArgs e)
        {
            this.RptThongKeDoanhThu.RefreshReport();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtTimDoanhThu.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tháng cần in", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn in thống kê doanh thu không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String sql = "select *from View_All_DoanhThu  where (Month(NgayXuat) = '" + txtTimDoanhThu.Text + "' AND TrangThai = N'" + "Đã thanh toán" + "')";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    //DataTable tk = new DataTable();
                    DSDoanhThu ds = new DSDoanhThu();
                    da.Fill(ds, "DataTable_DoanhThu");

                    ReportDataSource dataSource = new ReportDataSource("DataSet_RptDoanhThu", ds.Tables[0]);

                    this.RptThongKeDoanhThu.LocalReport.DataSources.Clear();
                    this.RptThongKeDoanhThu.LocalReport.DataSources.Add(dataSource);
                    this.RptThongKeDoanhThu.RefreshReport();
                }
            }
        }

        private void txtTimDoanhThu_TextChanged_1(object sender, EventArgs e)
        {
            int box_int = 0;
            Int32.TryParse(txtTimDoanhThu.Text, out box_int);
            if (box_int > 12)
            {
                txtTimDoanhThu.Text = "12";
            }
            else
                if (box_int < 1)
            {
                txtTimDoanhThu.Text = "1";
            }
        }
    }
}
