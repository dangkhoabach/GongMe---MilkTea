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
    public partial class FrmInPhieuNhap : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DANGKHOABACH;Initial Catalog=QLTiemTraSua;Integrated Security=True");

        public FrmInPhieuNhap()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmInPhieuNhap_Load(object sender, EventArgs e)
        {
            this.rptPhieuNhap.RefreshReport();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (txtTimPhieuNhap.Text == "")
            {
                MessageBox.Show("Vui lòng nhập mã phiếu cần in", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn in phiếu nhập hàng không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    String sql = ("select *from View_All_PhieuNhap where MaPhieuNhap = '" + txtTimPhieuNhap.Text + "'");
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    //DataTable tk = new DataTable();
                    DSPhieuNhap ds = new DSPhieuNhap();
                    da.Fill(ds, "DataTable_PhieuNhap");

                    ReportDataSource dataSource = new ReportDataSource("DataSet_RptPhieuNhap", ds.Tables[0]);

                    this.rptPhieuNhap.LocalReport.DataSources.Clear();
                    this.rptPhieuNhap.LocalReport.DataSources.Add(dataSource);
                    this.rptPhieuNhap.RefreshReport();
                }
            }
        }
    }
}
