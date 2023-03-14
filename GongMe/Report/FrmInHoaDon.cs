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
    public partial class FrmInHoaDon : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DANGKHOABACH;Initial Catalog=QLTiemTraSua;Integrated Security=True");

        public FrmInHoaDon()
        {
            InitializeComponent();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            String sql = ("select *from View_All_Bill_Test where MaHoaDon = '" + txtTimHoaDon.Text + "'");
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            //DataTable tk = new DataTable();
            DSHoaDon ds = new DSHoaDon();
            da.Fill(ds, "DataTable_HoaDon");

            ReportDataSource dataSource = new ReportDataSource("DataSet_RptHoaDon", ds.Tables[0]);

            this.rptHoaDon.LocalReport.DataSources.Clear();
            this.rptHoaDon.LocalReport.DataSources.Add(dataSource);
            this.rptHoaDon.RefreshReport();
        }

        private void FrmInHoaDon_Load(object sender, EventArgs e)
        {
            this.rptHoaDon.RefreshReport();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
