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
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace GongMe.PresentionTier
{
    public partial class FrmThongKeTonKho : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DANGKHOABACH;Initial Catalog=QLTiemTraSua;Integrated Security=True");

        public FrmThongKeTonKho()
        {
            InitializeComponent();
            Database db;
            db = new Database();
        }

        public void LayDSHangTonKho()
        {
            String sql = ("select MaMatHang, TenHang, SoLuongTon, NgayNhap from TonKho where Month(NgayNhap) = '" + txtTim.Text + "'");
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable tk = new DataTable();
            da.Fill(tk);

            lvsTonKho.Items.Clear();

            for (int i = 0; i < tk.Rows.Count; i++)
            {
                ListViewItem lvi =
                lvsTonKho.Items.Add(tk.Rows[i][0].ToString());
                lvi.SubItems.Add(tk.Rows[i][1].ToString());
                lvi.SubItems.Add(tk.Rows[i][2].ToString());
                lvi.SubItems.Add(tk.Rows[i][3].ToString());
            }
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            int box_int = 0;
            Int32.TryParse(txtTim.Text, out box_int);
            if (box_int > 12)
            {
                txtTim.Text = "12";
            }
            else
                if (box_int < 1)
            {
                txtTim.Text = "1";
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog()
            {
                Filter = "Excel Workbook|* .xls",
                ValidateNames = true
            })
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application xla = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = xla.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)xla.ActiveSheet;
                    xla.Visible = false;
                    ws.Cells[1, 1] = "Mã mặt hàng";
                    ws.Cells[1, 2] = "Tên mặt hàng";
                    ws.Cells[1, 3] = "Số lượng tồn";
                    ws.Cells[1, 4] = "Ngày nhập";
                    int i = 2;
                    foreach (ListViewItem comp in lvsTonKho.Items)
                    {
                        ws.Cells[i, 1] = comp.SubItems[0].Text;
                        ws.Cells[i, 2] = comp.SubItems[1].Text;
                        ws.Cells[i, 3] = comp.SubItems[2].Text;
                        ws.Cells[i, 4] = comp.SubItems[3].Text;
                        i++;
                    }
                    wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    xla.Quit();
                    MessageBox.Show("Bạn đã xuất dữ liệu thành công", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            LayDSHangTonKho();
        }
    }
}
