using GongMe.DataTier.Models;
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
    public partial class FrmInPhieuXuat : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DANGKHOABACH;Initial Catalog=QLTiemTraSua;Integrated Security=True");

        public FrmInPhieuXuat()
        {
            InitializeComponent();
        }

        private void FrmInPhieuXuat_Load(object sender, EventArgs e)
        {
            this.rptPhieuXuat.RefreshReport();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtTimPhieuXuat.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã phiếu cần in", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn in phiếu xuất hàng không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String sql = ("select *from View_All_PhieuXuat where MaPhieuXuat = '" + txtTimPhieuXuat.Text + "'");
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    //DataTable tk = new DataTable();
                    DSPhieuXuat ds = new DSPhieuXuat();
                    da.Fill(ds, "DataTable_PhieuXuat");

                    ReportDataSource dataSource = new ReportDataSource("DataSet_PhieuXuat", ds.Tables[0]);

                    this.rptPhieuXuat.LocalReport.DataSources.Clear();
                    this.rptPhieuXuat.LocalReport.DataSources.Add(dataSource);
                    this.rptPhieuXuat.RefreshReport();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
