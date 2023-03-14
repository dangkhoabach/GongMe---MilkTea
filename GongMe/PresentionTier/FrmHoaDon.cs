using GongMe.BusinessTier;
using GongMe.DataTier.Models;
using GongMe.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = GongMe.DataTier.Models.Menu;

namespace GongMe
{
    public partial class FrmHoaDon : Form
    {
        TiemTraSuaModel context = new TiemTraSuaModel();
        private readonly HoaDonBUS hoaDonBUS;
        private DanhMucBUS danhMucBUS;
        private CTHDBUS cTHDBUS;
        private MenuBUS menuBUS;
        private int maHoaDon = -1;
        private long? tongTien = 0;
        private long? tongdoanhthu = 0;

        string MACHUCVU = "";

        System.Globalization.CultureInfo fVND = new System.Globalization.CultureInfo("vi-VN");

        public FrmHoaDon(string MACHUCVU)
        {
            InitializeComponent();
            hoaDonBUS = new HoaDonBUS();
            danhMucBUS = new DanhMucBUS();
            menuBUS = new MenuBUS();
            cTHDBUS = new CTHDBUS();
            CaiDatDieuKhien();
            this.Load += FrmHoaDon_Load;

            this.MACHUCVU = MACHUCVU;
        }

        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            LoadBan();
            LoadHoaDon();
            LoadDanhMuc();
            LoadDoanhThu();
            nupSoluong.Value = 1;
            btnSua_CT.Enabled = false;
            btnXoaCT.Enabled = false;
        }

        private void LoadBan()
        {
            cbxTenban.DataSource = context.Ban.ToList();
        }
        private void LoadNhanVien()
        {
            cbxNhanvien.DataSource = context.NhanVien.ToList();
        }
        private void LoadDanhMuc()
        {
            cbxDanhMuc.DataSource = danhMucBUS.GetDANHMUCs();
        }
        private void LoadHoaDon()
        {
            dgvHoaDon.DataSource = hoaDonBUS.GetHoaDons();
        }
        private void LoadDoanhThu()
        {
            tongdoanhthu = hoaDonBUS.GetHoaDonDaThanhToan().Sum(p => p.TongTien).Value;
            txtTongTien.Text = String.Format(fVND, "{0:C0}", (tongdoanhthu));
        }
        private void LoadCTHD(int maHoaDon)
        {
            dgvCTHD.DataSource = cTHDBUS.GetCTHDs(maHoaDon);
        }
        private void LoaddgvCTHD()
        {
            dgvCTHD.DataSource = null;
            maHoaDon = -1;
        }

        private void LoaddgvHD()
        {
            dgvHoaDon.DataSource = null;
        }

        private void CaiDatDieuKhien()
        {
            cbxTenban.DisplayMember = "TenBan";
            cbxTenban.ValueMember = "MaBan";

            cbxNhanvien.DisplayMember = "HoTen";
            cbxNhanvien.ValueMember = "MaNhanVien";

            cbxDanhMuc.DisplayMember = "TenDanhMuc";
            cbxDanhMuc.ValueMember = "MaDanhMuc";

            dtpNgayban.Format = DateTimePickerFormat.Custom;
            dtpNgayban.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            dtpNgayban.MaxDate = DateTime.Now;
            dtpNgayban.Value = DateTime.Today;

            cbxTenMon.DisplayMember = "TenMon";
            cbxTenMon.ValueMember = "MaMon";
        }

        private void TaoHoaDonMoi(HoaDon hd)
        {
            Ban ban = cbxTenban.SelectedItem as Ban;
            hd.MaBan = ban.MaBan;
            hd.NgayXuat = DateTime.Parse(dtpNgayban.Value.ToString());
            NhanVien nhanVien = cbxNhanvien.SelectedItem as NhanVien;
            hd.MaNhanVien = nhanVien.MaNhanVien;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (maHoaDon == -1)
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn", "Thông báo");
                    return;
                }
                HoaDon hd2 = hoaDonBUS.GetHoaDonTheoMa(maHoaDon);
                hd2.TrangThai = "Đã thanh toán";
                tongdoanhthu += hd2.TongTien;
                txtTongTien.Text = String.Format(fVND, "{0:C0}", (tongdoanhthu));
                hoaDonBUS.SuaHoaDon(hd2);
                MessageBox.Show("Lưu hóa đơn thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadHoaDon();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (maHoaDon == -1)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn", "Thông báo");
                return;
            }
            HoaDon hoaDon = hoaDonBUS.GetHoaDonTheoMa(maHoaDon);
            if (hoaDon.TrangThai == "Đã thanh toán")
            {
                MessageBox.Show("Không chỉnh sửa được hóa đơn đã lưu", "Thông báo");
                return;
            }
            List<HoaDon> hoaDonChuaThanhToan = hoaDonBUS.GetHoaDonChuaThanhToan().ToList();
            for (int i = 0; i < hoaDonChuaThanhToan.Count; i++)
            {
                if (hoaDonChuaThanhToan[i].MaBan == cbxTenban.SelectedValue.ToString())
                {
                    MessageBox.Show("Bàn đang có khách!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            HoaDon hd = new HoaDon();
            TaoHoaDonMoi(hd);
            hd.MaHoaDon = maHoaDon;
            try
            {
                hoaDonBUS.SuaHoaDon(hd);
                MessageBox.Show("Sửa hóa đơn thành công", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadHoaDon();
                LoaddgvCTHD();
                btnThem.Enabled = true;
                btnThemCT.Enabled = true;
                cbxDanhMuc.Enabled = true;
                cbxTenMon.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbxDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            string maDanhMuc = cbx.SelectedValue.ToString();
            cbxTenMon.DataSource = menuBUS.GetMonTheoMaDanhMuc(maDanhMuc);
        }

        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rows = e.RowIndex;
                maHoaDon = Convert.ToInt32(dgvHoaDon.Rows[rows].Cells[0].Value.ToString());
                cbxNhanvien.SelectedValue = Convert.ToInt32(dgvHoaDon.Rows[rows].Cells[1].Value.ToString());
                NhanVien nhanVien = cbxNhanvien.SelectedItem as NhanVien;
                cbxNhanvien.Text = nhanVien.HoTen;
                cbxTenban.SelectedValue = dgvHoaDon.Rows[rows].Cells[2].Value;
                Ban ban = cbxTenban.SelectedItem as Ban;
                cbxTenban.Text = ban.TenBan;
                dtpNgayban.Text = dgvHoaDon.Rows[rows].Cells[3].Value.ToString();

                LoadCTHD(maHoaDon);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (maHoaDon == -1)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn", "Thông báo");
                return;
            }
            HoaDon hoaDon = hoaDonBUS.GetHoaDonTheoMa(maHoaDon);
            if (hoaDon.TrangThai == "Đã thanh toán")
            {
                MessageBox.Show("Không chỉnh sửa được hóa đơn đã lưu", "Thông báo");
                return;
            }
            HoaDon hd = new HoaDon();
            TaoHoaDonMoi(hd);
            hd.MaHoaDon = maHoaDon;
            foreach (var item in hoaDonBUS.GetHoaDonTheoMa(maHoaDon).CTHD)
            {
                hd.CTHD.Remove(item);
            }
            try
            {
                hoaDonBUS.XoaHoaDon(hd);
                MessageBox.Show("Xóa hóa đơn thành công!!", "Thông báo");
                LoadHoaDon();
                LoaddgvCTHD();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThemCT_Click(object sender, EventArgs e)
        {
            if (maHoaDon == -1)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn muốn thêm chi tiết!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            HoaDon hoaDon = hoaDonBUS.GetHoaDonTheoMa(maHoaDon);
            if (hoaDon.TrangThai == "Đã thanh toán")
            {
                MessageBox.Show("Không chỉnh sửa được hóa đơn đã lưu", "Thông báo");
                return;
            }
            if (nupSoluong.Value <= 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // lưu cthd cần thêm
            CTHD cTHD = new CTHD();
            cTHD.MaHoaDon = maHoaDon;
            Menu meNu = cbxTenMon.SelectedItem as Menu;
            cTHD.MaMon = meNu.MaMon;
            cTHD.SoLuong = int.Parse(nupSoluong.Value.ToString());
            try
            {
                tongTien = hoaDon.TongTien + meNu.DonGia * cTHD.SoLuong;
                hoaDon.TongTien = tongTien;
                tongTien = 0;
                hoaDonBUS.SuaHoaDon(hoaDon);
                cTHDBUS.CapNhatMon(cTHD);
                MessageBox.Show("Thêm món thành công!!", "Thông báo");
                LoadHoaDon();
                LoadCTHD(maHoaDon);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvCTHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rows = e.RowIndex;
                maHoaDon = Convert.ToInt32(dgvCTHD.Rows[rows].Cells[0].Value.ToString());
                string maMon = dgvCTHD.Rows[rows].Cells[1].Value.ToString();
                string maDanhMuc = menuBUS.GetMaDanhMucTheoMaMon(maMon);
                cbxDanhMuc.SelectedValue = maDanhMuc;
                cbxTenMon.SelectedValue = maMon;
                nupSoluong.Value = int.Parse(dgvCTHD.Rows[rows].Cells[2].Value.ToString());
                btnXoaCT.Enabled = true;
                btnSua_CT.Enabled = true;
                btnThemCT.Enabled = false;
                cbxDanhMuc.Enabled = false;
                cbxTenMon.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            if (maHoaDon == -1)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn muốn thêm chi tiết!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            HoaDon hoaDon = hoaDonBUS.GetHoaDonTheoMa(maHoaDon);
            if (hoaDon.TrangThai == "Đã thanh toán")
            {
                MessageBox.Show("Không chỉnh sửa được hóa đơn đã lưu", "Thông báo");
                return;
            }
            CTHD cTHD = new CTHD();
            cTHD.MaHoaDon = maHoaDon;
            Menu meNu = cbxTenMon.SelectedItem as Menu;
            cTHD.MaMon = meNu.MaMon;
            cTHD.SoLuong = int.Parse(nupSoluong.Value.ToString());
            try
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết hóa đơn này?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cTHDBUS.XoaCTHD(cTHD);
                    tongTien = meNu.DonGia * cTHD.SoLuong;
                    hoaDon.TongTien = hoaDon.TongTien - tongTien;
                    tongTien = 0;
                    LoadCTHD(maHoaDon);
                    MessageBox.Show("Xóa món thành công!!", "Thông báo");
                    hoaDonBUS.SuaHoaDon(hoaDon);
                    LoadHoaDon();
                    cbxDanhMuc.Enabled = true;
                    cbxTenMon.Enabled = true;
                    btnThemCT.Enabled = true;
                    btnSua_CT.Enabled = false;
                    btnXoaCT.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_CT_Click(object sender, EventArgs e)
        {
            if (maHoaDon == -1)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn muốn thêm chi tiết!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            HoaDon hoaDon = hoaDonBUS.GetHoaDonTheoMa(maHoaDon);
            if (hoaDon.TrangThai == "Đã thanh toán")
            {
                MessageBox.Show("Không chỉnh sửa được hóa đơn đã lưu", "Thông báo");
                return;
            }
            if (nupSoluong.Value <= 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CTHD cTHD = new CTHD();
            cTHD.MaHoaDon = maHoaDon;
            Menu meNu = cbxTenMon.SelectedItem as Menu;
            cTHD.MaMon = meNu.MaMon;
            cTHD.SoLuong = int.Parse(nupSoluong.Value.ToString());
            CTHD chitiet = cTHDBUS.getCTHDTheoMa(maHoaDon);
            List<CTHD> listCTHD = cTHDBUS.getListCTHDTheoMa(maHoaDon);
            try
            {
                for (int i = 0; i < listCTHD.Count; i++)
                {
                    if (listCTHD[i].MaMon == cTHD.MaMon)
                    {
                        chitiet = listCTHD[i];
                        tongTien = meNu.DonGia * cTHD.SoLuong - meNu.DonGia * chitiet.SoLuong;
                        hoaDon.TongTien += tongTien;
                        tongTien = 0;
                    }

                    hoaDonBUS.SuaHoaDon(hoaDon);
                    cTHDBUS.SuaCTHD(cTHD);
                    MessageBox.Show("Sửa món thành công!!", "Thông báo");
                    LoadHoaDon();
                    LoadCTHD(maHoaDon);
                    cbxDanhMuc.Enabled = true;
                    cbxTenMon.Enabled = true;
                    btnThemCT.Enabled = true;
                    btnSua_CT.Enabled = false;
                    btnXoaCT.Enabled = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtTimHoaDon_TextChanged(object sender, EventArgs e)
        {
            if (txtTimHoaDon.Text == "")
            {
                LoadHoaDon();
                LoaddgvCTHD();
            }
            else
            {
                try
                {
                    int timhd = int.Parse(txtTimHoaDon.Text.ToString());
                    foreach (var hd in hoaDonBUS.GetHoaDons())
                    {
                        if (hd.MaHD == timhd)
                        {
                            dgvHoaDon.DataSource = hoaDonBUS.GetHoaDonMaHD(hd.MaHD);
                            LoadCTHD(hd.MaHD);
                            return;
                        }
                    }
                    foreach (var hd in hoaDonBUS.GetHoaDons())
                    {
                        if (hd.MaNV == timhd)
                        {
                            dgvHoaDon.DataSource = hoaDonBUS.GetHoaDonMaNV(int.Parse(timhd.ToString()));
                            LoadCTHD(hd.MaHD);
                            return;
                        }
                    }
                    LoaddgvHD();
                    LoaddgvCTHD();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnLuuNhap_Click(object sender, EventArgs e)
        {
            try
            {
                List<HoaDon> hoaDonChuaThanhToan = hoaDonBUS.GetHoaDonChuaThanhToan().ToList();
                for (int i = 0; i < hoaDonChuaThanhToan.Count; i++)
                {
                    if (hoaDonChuaThanhToan[i].MaBan == cbxTenban.SelectedValue.ToString())
                    {
                        MessageBox.Show("Bàn đang có khách!!!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                HoaDon hd = new HoaDon();
                TaoHoaDonMoi(hd);
                hd.TrangThai = "Chưa thanh toán";
                hd.TongTien = tongTien;
                hoaDonBUS.LuuHoaDon(hd);
                MessageBox.Show("Thêm hóa đơn thành công!!", "Thông báo");
                LoadHoaDon();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            maHoaDon = -1;
            btnSua_CT.Enabled = false;
            btnXoaCT.Enabled = false;
            cbxDanhMuc.Enabled = true;
            cbxTenMon.Enabled = true;
            btnThemCT.Enabled = true;
            rdoChuaThanhToan.Checked = false;
            rdoDaThanhToan.Checked = false;
            LoadHoaDon();
            LoaddgvCTHD();
        }

        private void txtTimHoaDon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            new Report.FrmInHoaDon().ShowDialog();
        }

        private void rdoDaThanhToan_CheckedChanged(object sender, EventArgs e)
        {
            dgvHoaDon.DataSource = hoaDonBUS.GetHoaDonTrangThai1();
        }

        private void rdoChuaThanhToan_CheckedChanged(object sender, EventArgs e)
        {
            dgvHoaDon.DataSource = hoaDonBUS.GetHoaDonTrangThai2();
        }

        private void FrmHoaDon_Load_1(object sender, EventArgs e)
        {

        }
    }
}