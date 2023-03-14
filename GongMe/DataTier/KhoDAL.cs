using GongMe.BusinessTier;
using GongMe.DataTier.Models;
using GongMe.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GongMe.DataTier
{
    internal class KhoDAL
    {
        private TiemTraSuaModel tiemTraSuaModel;
        public KhoDAL()
        {
            tiemTraSuaModel = new TiemTraSuaModel();
        }
        public IEnumerable<KhoViewModel> GetKhos()
        {
            var DanhSach = tiemTraSuaModel.TonKho
                .Select(p => new KhoViewModel
                {
                    MaMatHang = p.MaMatHang,
                    TenMatHang = p.MatHang.TenHang,
                    SoLuongTon = p.SoLuongTon,
                    NgayNhap = p.NgayNhap,
                }).ToList();
            return DanhSach;
        }

        internal bool CapNhatKho(CTPX cTPX, TonKho tk)
        {
            TonKho tonKho = tiemTraSuaModel.TonKho.Where(p => p.MaMatHang == tk.MaMatHang).FirstOrDefault(p => p.NgayNhap == tk.NgayNhap);
            try
            {
                if (tonKho == null)
                {
                    MessageBox.Show("Lỗi!");
                    return false;
                }
                tonKho.SoLuongTon -= cTPX.SoLuong;
                tiemTraSuaModel.TonKho.AddOrUpdate(tonKho);
                tiemTraSuaModel.SaveChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
        public bool XoaTonKho(TonKho tk)
        {
            TonKho tonKho = tiemTraSuaModel.TonKho.Where(p => p.MaMatHang == tk.MaMatHang).FirstOrDefault(p => p.NgayNhap == tk.NgayNhap);
            try
            {
                if (tonKho == null)
                {
                    MessageBox.Show("Lỗi!");
                    return false;
                }
                else
                {
                    tiemTraSuaModel.TonKho.Remove(tk);
                    tiemTraSuaModel.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex) { throw ex; }
        }
        public TonKho GetHangTheoMa(string maMatHang)
        {
            return tiemTraSuaModel.TonKho.Where(p => p.MaMatHang == maMatHang).FirstOrDefault();
        }

        public IEnumerable<KhoViewModel> GetHangTheoMaLoai(string maLoai)
        {
            var DanhSach = tiemTraSuaModel.TonKho
                .Where(p => p.MatHang.MaLoai == maLoai)
                .Select(p => new KhoViewModel
                {
                    MaMatHang = p.MaMatHang,
                    TenMatHang = p.MatHang.TenHang,
                    SoLuongTon = p.SoLuongTon,
                    NgayNhap = p.NgayNhap,
                }).ToList();
            return DanhSach;
        }

        public IEnumerable<KhoViewModel> GetHangTheoMaHang(string maHang)
        {
            var DanhSach = tiemTraSuaModel.TonKho
                .Where(p => p.MaMatHang == maHang)
                .Select(p => new KhoViewModel
                {
                    MaMatHang = p.MaMatHang,
                    TenMatHang = p.MatHang.TenHang,
                    SoLuongTon = p.SoLuongTon,
                    NgayNhap = p.NgayNhap,
                }).ToList();
            return DanhSach;
        }

        public List<TonKho> GetListHangTheoMa(string maHang)
        {
            return tiemTraSuaModel.TonKho.Where(p => p.MaMatHang == maHang).ToList();
        }
    }
}