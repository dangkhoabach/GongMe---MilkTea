using GongMe.BusinessTier;
using GongMe.DataTier.Models;
using GongMe.PresentionTier;
using GongMe.ViewModel;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Menu = GongMe.DataTier.Models.Menu;

namespace GongMe
{
    public partial class FrmQuanLyBan : Form
    {
        private int MaNhanVienHoatDong = -1;
        private const int W = 75;
        private const int H = 75;
        private const int dis = 85;
        private FrmTraSuaGongMe frmTraSuaGongMe;
        private const int col = 5;
        private BanTraSuaBUS banTraSuaBUS;
        private DanhMucBUS danhMucBUS;
        private MenuBUS menuBUS;
        private CTHDBUS chitietBUS;
        private HoaDonBUS hoaDonBUS;
        private Button banChon = null;
        public int maHD = -1;
        private long? tongTien = 0;
        System.Globalization.CultureInfo fVND = new System.Globalization.CultureInfo("vi-VN");
        List<HoaDon> listHoaDonTrangThai;

        public FrmQuanLyBan(string maNhanVienHoatDong)
        {
            InitializeComponent();
            CaidatDieuKhien();
            frmTraSuaGongMe = new FrmTraSuaGongMe();
            banTraSuaBUS = new BanTraSuaBUS();
            danhMucBUS = new DanhMucBUS();
            menuBUS = new MenuBUS();
            hoaDonBUS = new HoaDonBUS();
            chitietBUS = new CTHDBUS();
            listHoaDonTrangThai = hoaDonBUS.GetHoaDonChuaThanhToan();
            this.MaNhanVienHoatDong = int.Parse(maNhanVienHoatDong);
        }
        private void CaidatDieuKhien()
        {
            cbxDanhMuc.DisplayMember = "TenDanhMuc";
            cbxDanhMuc.ValueMember = "MaDanhMuc";

            cbxDoiBan.DisplayMember = "TenBan";
            cbxDoiBan.ValueMember = "MaBan";

            cbxMon.DisplayMember = "TenMon";
            cbxMon.ValueMember = "MaMon";
        }

        private void LoadDanhSachMon(int maHoaDon)
        {
            if (maHoaDon == -1)
            {
                dgvDanhSachMon.DataSource = null;
                return;
            }
            dgvDanhSachMon.DataSource = chitietBUS.GetCTHDMaBan(maHoaDon);
        }

        private void FrmQuanLyBan_Load(object sender, EventArgs e)
        {
            KhoiTaoSoLuongBan();
            LoadDanhSachBan();
            LoadDanhMuc();
            nudSoLuong.Value = 1;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
        }
        private void KhoiTaoSoLuongBan()
        {
            int x = 20;
            int y = 30;
            int i = 0;
            foreach (Ban ban in banTraSuaBUS.GetBANs())
            {
                TaoBan(x, y, ban);
                i++;
                if (i % col == 0)
                {
                    y += dis;
                    x = 20;
                    continue;
                }
                x += dis;
            }
        }

        private void TaoBan(int x, int y, Ban ban)
        {
            Button btn = new Button();
            btn.Location = new Point(x, y);
            btn.Text = ban.TenBan;
            btn.Tag = ban.MaBan;
            btn.Size = new Size(W, H);
            btn.BackColor = Color.White;
            btn.Click += Btn_Click;
            btn.Font = new Font("Arial", 10);
            btn.ForeColor = Color.Black;
            foreach (var item in hoaDonBUS.GetHoaDonTrangThai2())
            {
                if (item.MaBan == ban.MaBan)
                {
                    btn.Image = Image.FromFile("../../Resources/lytrasua.png");
                }
            }
            pnlBan.Controls.Add(btn);
        }

        private void LoadDanhMuc()
        {
            cbxDanhMuc.DataSource = danhMucBUS.GetDANHMUCs();
        }

        private void LoadDanhSachBan()
        {
            cbxDoiBan.DataSource = banTraSuaBUS.GetBANs();
        }

        private void cbxDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            string maDanhMuc = cbx.SelectedValue.ToString();
            cbxMon.DataSource = menuBUS.GetMonTheoMaDanhMuc(maDanhMuc);
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            Button ban = sender as Button;
            if (banChon == null)
            {
                banChon = ban;
                banChon.BackColor = Color.DeepSkyBlue;
            }
            else if (banChon == ban)
            {
                banChon.BackColor = Color.White;
                banChon = null;
                maHD = -1;
                LoadDanhSachMon(maHD);
                tongTien = 0;
                txtTongTien.Text = "";
                return;
            }
            else
            {
                banChon.BackColor = Color.White;
                banChon = ban;
                ban.BackColor = Color.DeepSkyBlue;
            }
            string maSoBanChon = banChon.Tag.ToString();
            foreach (var item in hoaDonBUS.GetHoaDonTrangThai2())
            {
                if (item.MaBan == maSoBanChon)
                {
                    maHD = item.MaHD;
                    LoadDanhSachMon(maHD);
                    tongTien = item.TongTien;
                    txtTongTien.Text = String.Format(fVND, "{0:C0}", tongTien);
                    return;
                }
            }
            maHD = -1;
            dgvDanhSachMon.DataSource = null;
            tongTien = 0;
            txtTongTien.Text = "";
        }
       
        
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (banChon == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (nudSoLuong.Value <= 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng món!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string maSoBanChon = banChon.Tag.ToString();
            CTHD chitiet = new CTHD();
            Menu m = cbxMon.SelectedItem as Menu;
            chitiet.MaMon = m.MaMon;
            chitiet.SoLuong = int.Parse(nudSoLuong.Value.ToString());
            HoaDon hoaDon = hoaDonBUS.GetHoaDonTheoTrangThaiVaBan(maSoBanChon);
            if (hoaDon != null)
            {
                chitiet.MaHoaDon = hoaDon.MaHoaDon;
                hoaDon.TongTien += chitiet.SoLuong * m.DonGia;
                hoaDonBUS.SuaHoaDon(hoaDon);
                MessageBox.Show("Thêm món thành công!", "Thông Báo");
            }
            else
            {
                hoaDon = new HoaDon();
                hoaDon.MaBan = maSoBanChon;
                hoaDon.MaNhanVien = MaNhanVienHoatDong;
                hoaDon.NgayXuat = DateTime.Now;
                hoaDon.TrangThai = "Chưa thanh toán";
                banChon.Image = Image.FromFile("../../Resources/lytrasua.png");
                hoaDon.TongTien = chitiet.SoLuong * m.DonGia;
                hoaDonBUS.LuuHoaDon(hoaDon);
                MessageBox.Show("Thêm hóa đơn thành công!", "Thông Báo");
            }
            maHD = hoaDon.MaHoaDon;
            chitiet.MaHoaDon = hoaDon.MaHoaDon;
            chitietBUS.CapNhatMon(chitiet);
            LoadDanhSachMon(maHD);
            tongTien = hoaDon.TongTien;
            txtTongTien.Text = String.Format(fVND, "{0:C0}", tongTien);
        }

        private void btnDoiBan_Click(object sender, EventArgs e)
        {
            Button banDich = null;
            string maSoBanChuyen, maSoBanDich;
            if (banChon == null)
            {
                MessageBox.Show("Vui lòng chọn bàn cần chuyển!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            maSoBanChuyen = banChon.Tag.ToString();
            maSoBanDich = cbxDoiBan.SelectedValue.ToString();
            if (maSoBanDich == null)
            {
                MessageBox.Show("Vui lòng chọn bàn muốn chuyển đến", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            HoaDon hoaDonChuyen = hoaDonBUS.GetHoaDonTheoTrangThaiVaBan(maSoBanChuyen);
            HoaDon hoaDonDich = hoaDonBUS.GetHoaDonTheoTrangThaiVaBan(maSoBanDich);
            banDich = pnlBan.Controls.OfType<Button>().Where(x => x.Tag.ToString() == maSoBanDich.ToString()).FirstOrDefault();
            if (hoaDonChuyen == null)
            {
                MessageBox.Show("Không thể chuyển bàn trống", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (hoaDonDich == null)
            {
                hoaDonDich = hoaDonChuyen;
                hoaDonDich.MaBan = maSoBanDich;
                banDich.Image = Image.FromFile("../../Resources/lytrasua.png");
                hoaDonBUS.SuaHoaDon(hoaDonDich);
                maHD = hoaDonDich.MaHoaDon;
                LoadDanhSachMon(maHD);
                tongTien = hoaDonDich.TongTien;
                txtTongTien.Text = String.Format(fVND, "{0:C0}", tongTien);
                MessageBox.Show("Chuyển bàn thành công!", "Thông Báo");
                banChon.Image = null;
                banChon.BackColor = Color.White;
                banChon = banDich;
                banChon.BackColor = Color.DeepSkyBlue;
                return;
            }
            List<CTHD> listCTHD = chitietBUS.getListCTHDTheoMa(hoaDonChuyen.MaHoaDon);
            foreach (CTHD chitiet in listCTHD)
            {
                CTHD ct = new CTHD();
                ct.MaHoaDon = hoaDonDich.MaHoaDon;
                ct.MaMon = chitiet.MaMon;
                Menu menu = menuBUS.GetMonTheoMaMon(chitiet.MaMon);
                ct.SoLuong = chitiet.SoLuong;
                hoaDonDich.TongTien += chitiet.SoLuong * menu.DonGia;
                chitietBUS.CapNhatMon(ct);
                hoaDonBUS.SuaHoaDon(hoaDonDich);
                chitietBUS.XoaCTHD(chitiet);
            }
            hoaDonBUS.XoaHoaDon(hoaDonChuyen);
            maHD = hoaDonDich.MaHoaDon;
            LoadDanhSachMon(maHD);
            tongTien = hoaDonDich.TongTien;
            txtTongTien.Text = String.Format(fVND, "{0:C0}", tongTien);
            MessageBox.Show("Gộp bàn thành công", "Thông Báo");
            banChon.Image = null;
            banChon.BackColor = Color.White;
            banChon = banDich;
            banChon.BackColor = Color.DeepSkyBlue;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (maHD == -1)
            {
                MessageBox.Show("Chọn bàn cần thanh toán", "Thông Báo");
                return;
            }
            FrmThanhToan f = new FrmThanhToan(maHD);
            f.ShowDialog();
            HoaDon hoaDon = hoaDonBUS.GetHoaDonDaThanhToanTheoMa(maHD);
            if (hoaDon != null)
            {
                banChon.Image = null;
                banChon.BackColor = Color.White;
                banChon = null;
                dgvDanhSachMon.DataSource = null;
                return;
            }
        }

        private void dgvDanhSachMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rows = e.RowIndex;
                string tenMon = dgvDanhSachMon.Rows[rows].Cells[0].Value.ToString();
                string maDanhMuc = menuBUS.GetMaDanhMucTheoTenMon(tenMon);
                cbxDanhMuc.SelectedValue = maDanhMuc;
                cbxMon.Text = tenMon;
                nudSoLuong.Value = int.Parse(dgvDanhSachMon.Rows[rows].Cells[1].Value.ToString());
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                cbxDanhMuc.Enabled = false;
                cbxMon.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (banChon == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (nudSoLuong.Value <= 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string maSoBanChon = banChon.Tag.ToString();
            CTHD chitiet = new CTHD();
            chitiet.MaHoaDon = maHD;
            Menu m = (Menu)cbxMon.SelectedItem;
            chitiet.MaMon = m.MaMon;
            chitiet.SoLuong = int.Parse(nudSoLuong.Value.ToString());
            CTHD cTHD = chitietBUS.getCTHDTheoMaVaMon(maHD, chitiet.MaMon);
            HoaDon hoaDon = hoaDonBUS.GetHoaDonTheoTrangThaiVaBan(maSoBanChon);
            try
            {
                hoaDon.TongTien = hoaDon.TongTien - cTHD.SoLuong * m.DonGia + chitiet.SoLuong * m.DonGia;
                chitietBUS.SuaCTHD(chitiet);
                hoaDonBUS.SuaHoaDon(hoaDon);
                MessageBox.Show("Sửa món thành công!", "Thông Báo");
                LoadDanhSachMon(maHD);
                tongTien = hoaDon.TongTien;
                txtTongTien.Text = String.Format(fVND, "{0:C0}", tongTien);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            cbxDanhMuc.Enabled = true;
            cbxMon.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            cbxDanhMuc.Enabled = true;
            cbxMon.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (banChon == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string maSoBanChon = banChon.Tag.ToString();
            CTHD chitiet = new CTHD();
            chitiet.MaHoaDon = maHD;
            Menu m = (Menu)cbxMon.SelectedItem;
            chitiet.MaMon = m.MaMon;
            chitiet.SoLuong = int.Parse(nudSoLuong.Value.ToString());
            CTHD cTHD = chitietBUS.getCTHDTheoMaVaMon(maHD, chitiet.MaMon);
            HoaDon hoaDon = hoaDonBUS.GetHoaDonTheoTrangThaiVaBan(maSoBanChon);
            try
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa món này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    hoaDon.TongTien -= m.DonGia * chitiet.SoLuong;
                    chitietBUS.XoaCTHD(chitiet);
                    hoaDonBUS.SuaHoaDon(hoaDon);
                    LoadDanhSachMon(maHD);
                    tongTien = hoaDon.TongTien;
                    txtTongTien.Text = String.Format(fVND, "{0:C0}", tongTien);
                }
                cbxDanhMuc.Enabled = true;
                cbxMon.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}