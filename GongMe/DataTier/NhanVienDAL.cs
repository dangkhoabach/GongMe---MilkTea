using GongMe.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GongMe.DataTier.Models
{
    internal class NhanVienDAL
    {
        private TiemTraSuaModel tiemTraSuaModel;
        public NhanVienDAL()
        {
            tiemTraSuaModel = new TiemTraSuaModel();
        }
        public IEnumerable<NhanVienViewModel> GetNHANVIENs()
        {
            var danhSach = tiemTraSuaModel.NhanVien
                .Select(p => new NhanVienViewModel
                {
                    MaNV = p.MaNhanVien,
                    MaCV = p.MaChucVu,
                    HoTen = p.HoTen.Trim(),
                    SDT = p.Sdt.Trim(),
                    NamSinh = p.NamSinh,
                    GioiTinh = p.GioiTinh,
                    Mail = p.Mail.Trim(),
                    MatKhau = p.MatKhau.Trim(),
                }
                ).ToList();
            return danhSach;
        }
        public bool ThemNhanVien(NhanVien nv)
        {
            try
            {
                tiemTraSuaModel.NhanVien.Add(nv);
                tiemTraSuaModel.SaveChanges();
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
        public bool SuaNhanVien(NhanVien nhanVien)
        {
            try
            {
                NhanVien nv = tiemTraSuaModel.NhanVien.Where(p => p.MaNhanVien == nhanVien.MaNhanVien).FirstOrDefault();
                nv.HoTen = nhanVien.HoTen;
                nv.NamSinh = nhanVien.NamSinh;
                nv.Sdt = nhanVien.Sdt;
                nv.MaChucVu = nhanVien.MaChucVu;
                nv.Mail = nhanVien.Mail;
                nv.MatKhau = nhanVien.MatKhau;
                nv.GioiTinh = nhanVien.GioiTinh;
                tiemTraSuaModel.NhanVien.AddOrUpdate(nhanVien);
                tiemTraSuaModel.SaveChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
        public bool XoaNhanVien(NhanVien nhanVien)
        {
            try
            {
                NhanVien nv = tiemTraSuaModel.NhanVien.Where(p => p.MaNhanVien == nhanVien.MaNhanVien).FirstOrDefault();
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    tiemTraSuaModel.NhanVien.Remove(nv);
                    tiemTraSuaModel.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }

        public IEnumerable<NhanVienViewModel> GetNhanVienSDT(string sdt)
        {
            var danhSach = tiemTraSuaModel.NhanVien
                .Where(p => p.Sdt == sdt)
                .Select(p => new NhanVienViewModel
                {
                    MaNV = p.MaNhanVien,
                    MaCV = p.MaChucVu,
                    HoTen = p.HoTen,
                    SDT = p.Sdt,
                    NamSinh = p.NamSinh,
                    GioiTinh = p.GioiTinh,
                    Mail = p.Mail,
                    MatKhau = p.MatKhau,
                }
                ).ToList();
            return danhSach;
        }
        public IEnumerable<NhanVien> GetCBXNhanViens()
        {
            return tiemTraSuaModel.NhanVien.ToList();
        }

        internal NhanVien GetNhanVienTheoSDT(string sdt)
        {
            return tiemTraSuaModel.NhanVien.Where(p => p.Sdt == sdt).FirstOrDefault();
        }
    }
}