using GongMe.BusinessTier;
using GongMe.DataTier.Models;
using GongMe.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GongMe.PresentionTier
{
    public partial class FrmThanhToan : Form
    {
        private HoaDonBUS hoaDonBUS;
        private CTHDBUS cTHDBUS;
        public int MaHD;
        long? thanhTien = 0;
        long? tienKhachTra = 0;
        long? tienThua = 0;
        System.Globalization.CultureInfo fVND = new System.Globalization.CultureInfo("vi-VN");

        public FrmThanhToan(int MaHD)
        {
            InitializeComponent();
            hoaDonBUS = new HoaDonBUS();
            cTHDBUS = new CTHDBUS();
            this.MaHD = MaHD;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            HoaDon hd = hoaDonBUS.GetHoaDonTheoMa(MaHD);
            if (hd.TrangThai == "Chưa thanh toán")
            {
                DialogResult result = MessageBox.Show("Chưa thanh toán!!. \n Bạn chắc chắn muốn thoát??", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Close();
                    return;
                }
            }
            this.Close();
        }

        private void txtTienKhachTra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FrmThanhToan_Load(object sender, EventArgs e)
        {
            HoaDon hd = hoaDonBUS.GetHoaDonTheoMa(MaHD);
            thanhTien = hd.TongTien;
            txtThanhTien.Text = String.Format(fVND, "{0:C0}", thanhTien);
            lbMaHD.Text = MaHD.ToString();
        }

        private void txtTienKhachTra_TextChanged(object sender, EventArgs e)
        {
            if (txtTienKhachTra.Text == "")
            {
                txtTienThua.Text = "";
            }
            else
            {
                tienKhachTra = long.Parse(txtTienKhachTra.Text);
                tienThua = tienKhachTra - thanhTien;
                txtTienThua.Text = String.Format(fVND, "{0:C0}", tienThua);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            HoaDon hoaDon = hoaDonBUS.GetHoaDonTheoMa(MaHD);
            hoaDon.TrangThai = "Đã thanh toán";
            hoaDonBUS.SuaHoaDon(hoaDon);
            MessageBox.Show("Thanh toán hóa đơn thành công", "Thông báo", MessageBoxButtons.OK);
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            new Report.FrmInHoaDon().ShowDialog();
        }
    }
}
