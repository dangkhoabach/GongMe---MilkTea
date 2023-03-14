using GongMe.BusinessTier;
using GongMe.DataTier.Models;
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
    public partial class FrmXuatHang : Form
    {
        private NhanVienBUS nhanVienBUS;
        private LoaiHangBUS loaiHangBUS;
        private MatHangBUS matHangBUS;
        private PhieuXuatBUS phieuXuatBUS;
        private CTPXBUS cTPXBUS;
        private KhoBUS khoBUS;
        private int maPX = -1;
        private int slTon = 0;

        public FrmXuatHang()
        {
            InitializeComponent();
            nhanVienBUS = new NhanVienBUS();
            loaiHangBUS = new LoaiHangBUS();
            matHangBUS = new MatHangBUS();
            phieuXuatBUS = new PhieuXuatBUS();
            cTPXBUS = new CTPXBUS();
            khoBUS = new KhoBUS();
            CaiDatDieuKhien();
        }

        private void FrmXuatHang_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            LoadLoaiHang();
            LoadPhieuXuat();
            LoadKho();
            btnSuaCT.Enabled = false;
            btnXoaCT.Enabled = false;
            btnXuat.Enabled = false;
        }

        private void CaiDatDieuKhien()
        {
            cbxNhanvien.DisplayMember = "HoTen";
            cbxNhanvien.ValueMember = "MaNhanVien";

            cbxLoaiHang.DisplayMember = "TenLoai";
            cbxLoaiHang.ValueMember = "MaLoai";

            cbxHangHoa.DisplayMember = "TenHang";
            cbxHangHoa.ValueMember = "MaHang";

            dtpNgayXuat.Format = DateTimePickerFormat.Custom;
            dtpNgayXuat.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            dtpNgayXuat.MaxDate = DateTime.Now;
            dtpNgayXuat.Value = DateTime.Today;
        }

        private void LoadNhanVien()
        {
            cbxNhanvien.DataSource = nhanVienBUS.GetCBXNhanViens();
        }

        private void LoadLoaiHang()
        {
            cbxLoaiHang.DataSource = loaiHangBUS.GetLoaiHangs();
        }

        private void LoadPhieuXuat()
        {
            dgvPhieuXuat.DataSource = phieuXuatBUS.GetPhieuXuats();
        }

        private void LoadCTPX(int maPX)
        {
            dgvCTPhieuXuat.DataSource = cTPXBUS.GetCTPXs(maPX);
        }

        private void LoadCTnull()
        {
            dgvCTPhieuXuat.DataSource = null;
            maPX = -1;
        }

        private void LoadPhieuXuatNUll()
        {
            dgvPhieuXuat.DataSource = null;
        }

        private void LoadKho()
        {
            dgvKho.DataSource = khoBUS.GetKhos();
        }

        private void dgvPhieuXuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rows = e.RowIndex;
                maPX = int.Parse(dgvPhieuXuat.Rows[rows].Cells[0].Value.ToString());
                cbxNhanvien.SelectedValue = Convert.ToInt32(dgvPhieuXuat.Rows[rows].Cells[1].Value.ToString());
                NhanVien nhanVien = cbxNhanvien.SelectedItem as NhanVien;
                cbxNhanvien.Text = nhanVien.HoTen;
                dtpNgayXuat.Text = dgvPhieuXuat.Rows[rows].Cells[2].Value.ToString();

                LoadCTPX(maPX);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnThemPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                if (maPX == -1)
                {
                    MessageBox.Show("Vui lòng chọn phiếu xuất");
                    return;
                }
                PhieuXuat phieuXuat = phieuXuatBUS.GetPhieuXuatTheoMaPhieuXuat(maPX);
                if(phieuXuat.TrangThai == "Lưu")
                {
                    MessageBox.Show("Phiếu đã lưu", "Thông báo");
                    return;
                }
                phieuXuat.TrangThai = "Lưu";
                List<CTPX> phieuXuatCTPXs = cTPXBUS.GetListCTPXs(maPX);
                foreach (CTPX chitiet in phieuXuatCTPXs)
                {
                    List<TonKho> listTonKho = khoBUS.GetListHangTheoMa(chitiet.MaMatHang);
                    int? soLuongTon = listTonKho.Sum(p => p.SoLuongTon).Value;
                    if (chitiet.SoLuong > soLuongTon)
                    {
                        MessageBox.Show(chitiet.MatHang.TenHang + " không đủ", "Thông báo ");
                        return;
                    }
                    foreach (TonKho tonKho in listTonKho)
                    {
                        if (tonKho.SoLuongTon <= chitiet.SoLuong)
                        {
                            khoBUS.XoaTonKho(tonKho);
                            chitiet.SoLuong -= tonKho.SoLuongTon;
                        }
                        else
                        {
                            khoBUS.CapNhatKho(chitiet, tonKho);
                            break;
                        }
                    }
                }
                phieuXuatBUS.SuaPhieuXuat(phieuXuat);
                MessageBox.Show("Lưu phiếu xuất thành công!");
                LoadPhieuXuat();
                LoadKho();
            }
            catch (Exception ex) { throw ex; }
        }

        private void btnSuaPhieu_Click(object sender, EventArgs e)
        {
            if (maPX == -1)
            {
                MessageBox.Show("Vui lòng chọn phiếu xuất");
                return;
            }

            PhieuXuat px = phieuXuatBUS.GetPhieuXuatTheoMaPhieuXuat(maPX);
            if (px.TrangThai == "Lưu")
            {
                MessageBox.Show("Không chỉnh sửa được phiếu xuất đã lưu", "Thông báo");
                return;
            }
            PhieuXuat phieuXuat = new PhieuXuat();
            phieuXuat.MaPhieuXuat = maPX;
            NhanVien nhanVien = cbxNhanvien.SelectedItem as NhanVien;
            phieuXuat.MaNhanVien = nhanVien.MaNhanVien;
            phieuXuat.NgayXuat = DateTime.Parse(dtpNgayXuat.Value.ToString());

            try
            {
                phieuXuatBUS.SuaPhieuXuat(phieuXuat);
                MessageBox.Show("Sửa thành công", "Thông báo");
                LoadPhieuXuat();
                LoadCTnull();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaPhieu_Click(object sender, EventArgs e)
        {
            if (maPX == -1)
            {
                MessageBox.Show("Vui lòng chọn phiếu xuất");
                return;
            }

            PhieuXuat px = phieuXuatBUS.GetPhieuXuatTheoMaPhieuXuat(maPX);
            if (px.TrangThai == "Lưu")
            {
                MessageBox.Show("Không xóa được phiếu xuất đã lưu", "Thông báo");
                return;
            }
            PhieuXuat phieuXuat = new PhieuXuat();
            phieuXuat.MaPhieuXuat = maPX;
            NhanVien nhanVien = cbxNhanvien.SelectedItem as NhanVien;
            phieuXuat.MaNhanVien = nhanVien.MaNhanVien;
            phieuXuat.NgayXuat = DateTime.Parse(dtpNgayXuat.Value.ToString());
            foreach (var item in phieuXuatBUS.GetPhieuXuatTheoMaPhieuXuat(maPX).CTPX)
            {
                phieuXuat.CTPX.Remove(item);
            }
            try
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu xuất này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    phieuXuatBUS.XoaPhieuXuat(phieuXuat);
                    MessageBox.Show("Xóa thành công", "Thông báo");
                    LoadPhieuXuat();
                    LoadCTnull();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rows = e.RowIndex;
                string maHang = dgvKho.Rows[rows].Cells[0].Value.ToString();
                string maLoai = matHangBUS.GetMaLoaiTheoMaMatHang(maHang);
                cbxLoaiHang.SelectedValue = maLoai;
                string tenHang = dgvKho.Rows[rows].Cells[1].Value.ToString();
                cbxHangHoa.Text = tenHang;
                nudSoLuong.Value = slTon = int.Parse(dgvKho.Rows[rows].Cells[2].Value.ToString());

                btnXuat.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            if (maPX == -1)
            {
                MessageBox.Show("Chọn phiếu xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            PhieuXuat px = phieuXuatBUS.GetPhieuXuatTheoMaPhieuXuat(maPX);
            if (px.TrangThai == "Lưu")
            {
                MessageBox.Show("Không chỉnh sửa phiếu xuất đã lưu", "Thông báo");
                return;
            }
            if (nudSoLuong.Value <= 0 || nudSoLuong.Value > slTon)
            {
                MessageBox.Show("Số lượng không hợp yêu cầu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }
            CTPX cTPX = new CTPX();
            cTPX.MaPhieuXuat = maPX;
            MatHang matHang = cbxHangHoa.SelectedItem as MatHang;
            cTPX.MaMatHang = matHang.MaMatHang;
            cTPX.SoLuong = int.Parse(nudSoLuong.Value.ToString());
            try
            {
                cTPXBUS.CapNhatPhieuXuat(cTPX);
                LoadCTPX(maPX);
                btnXuat.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvCTPhieuXuat_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int rows = e.RowIndex;
                string maHang = dgvCTPhieuXuat.Rows[rows].Cells[1].Value.ToString();
                string maLoai = matHangBUS.GetMaLoaiTheoMaMatHang(maHang);
                cbxLoaiHang.SelectedValue = maLoai;
                string tenHang = dgvCTPhieuXuat.Rows[rows].Cells[2].Value.ToString();
                cbxHangHoa.Text = tenHang;
                nudSoLuong.Value = int.Parse(dgvCTPhieuXuat.Rows[rows].Cells[3].Value.ToString());
                btnSuaCT.Enabled = true;
                btnXoaCT.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSuaCT_Click(object sender, EventArgs e)
        {
            PhieuXuat px = phieuXuatBUS.GetPhieuXuatTheoMaPhieuXuat(maPX);
            if (px.TrangThai == "Lưu")
            {
                MessageBox.Show("Không chỉnh sửa phiếu xuất đã lưu", "Thông báo");
                return;
            }
            CTPX cTPX = new CTPX();
            cTPX.MaPhieuXuat = maPX;
            MatHang matHang = cbxHangHoa.SelectedItem as MatHang;
            cTPX.MaMatHang = matHang.MaMatHang;
            cTPX.SoLuong = int.Parse(nudSoLuong.Value.ToString());
            try
            {
                cTPXBUS.SuaPhieuXuat(cTPX);
                LoadCTPX(maPX);
                btnSuaCT.Enabled = false;
                btnXoaCT.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            PhieuXuat px = phieuXuatBUS.GetPhieuXuatTheoMaPhieuXuat(maPX);
            if (px.TrangThai == "Lưu")
            {
                MessageBox.Show("Không chỉnh sửa phiếu xuất đã lưu", "Thông báo");
                return;
            }
            CTPX cTPX = new CTPX();
            cTPX.MaPhieuXuat = maPX;
            MatHang matHang = cbxHangHoa.SelectedItem as MatHang;
            cTPX.MaMatHang = matHang.MaMatHang;
            cTPX.SoLuong = int.Parse(nudSoLuong.Value.ToString());
            try
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu xuất này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    cTPXBUS.XoaPhieuXuat(cTPX);
                    MessageBox.Show("Xóa chi tiết phiếu xuất thành công", "Thông báo");
                    LoadCTPX(maPX);
                }
                btnSuaCT.Enabled = false;
                btnXoaCT.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnSuaCT.Enabled = false;
            btnXoaCT.Enabled = false;
            btnXuat.Enabled = false;
            LoadKho();
            LoadCTnull();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string maLoai = cbxLoaiHang.SelectedValue.ToString();
            dgvKho.DataSource = khoBUS.GetHangTheoMaLoai(maLoai);
            /*string maHang = cbxHangHoa.SelectedValue.ToString();
            dgvKho.DataSource = khoBUS.GetHangTheoMaHang(maHang);*/
        }

        private void cbxHangHoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MatHang mathang = cbxHangHoa.SelectedItem as MatHang;
            //dgvKho.DataSource = khoBUS.GetHangTheoMaHang(mathang.MaMatHang);
        }

        private void btnLuuNhap_Click(object sender, EventArgs e)
        {
            PhieuXuat phieuXuat = new PhieuXuat();
            NhanVien nhanVien = cbxNhanvien.SelectedItem as NhanVien;
            phieuXuat.MaNhanVien = nhanVien.MaNhanVien;
            phieuXuat.NgayXuat = DateTime.Parse(dtpNgayXuat.Value.ToString());
            phieuXuat.TrangThai = "Nháp";
            try
            {
                phieuXuatBUS.SuaPhieuXuat(phieuXuat);
                LoadPhieuXuat();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnInPhieuXuat_Click(object sender, EventArgs e)
        {
            new Report.FrmInPhieuXuat().ShowDialog();
        }

        private void txtTimPhieuXuat_TextChanged(object sender, EventArgs e)
        {
            if (txtTimPhieuXuat.Text == "")
            {
                LoadPhieuXuat();
                LoadCTnull();
            }
            else
            {
                try
                {
                    int timPX = int.Parse(txtTimPhieuXuat.Text.ToString());
                    foreach (var phieuXuat in phieuXuatBUS.GetPhieuXuats())
                    {
                        if (phieuXuat.MaPhieuXuat == timPX)
                        {
                            dgvPhieuXuat.DataSource = phieuXuatBUS.GetPhieuXuatTheoMaPX(timPX);
                            LoadCTPX(timPX);
                            return;
                        }

                    }
                    LoadPhieuXuatNUll();
                    LoadCTnull();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void txtTimPhieuXuat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void cbxLoaiHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbx = sender as ComboBox;
            string maLoai = cbx.SelectedValue.ToString();
            cbxHangHoa.DataSource = matHangBUS.GetMatHangTheoMaLoaiHang(maLoai);
        }
    }
}