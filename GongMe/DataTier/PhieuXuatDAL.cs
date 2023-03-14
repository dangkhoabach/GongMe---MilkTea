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
    internal class PhieuXuatDAL
    {
        private TiemTraSuaModel tiemTraSuaModel;
        public PhieuXuatDAL()
        {
            tiemTraSuaModel = new TiemTraSuaModel();
        }
        public IEnumerable<PhieuXuatViewModel> GetPhieuXuats()
        {
            var Danhsach = tiemTraSuaModel.PhieuXuat
                .Select(p => new PhieuXuatViewModel
                {
                    MaPhieuXuat = p.MaPhieuXuat,
                    MaNhanVien = p.MaNhanVien,
                    NgayXuat = p.NgayXuat,
                    TrangThai = p.TrangThai,
                }).ToList();
            return Danhsach;
        }
        public IEnumerable<PhieuXuatViewModel> GetPhieuXuatTheoMaPX(int maPX)
        {
            var Danhsach = tiemTraSuaModel.PhieuXuat
                .Where(p => p.MaPhieuXuat == maPX)
                .Select(p => new PhieuXuatViewModel
                {
                    MaPhieuXuat = p.MaPhieuXuat,
                    MaNhanVien = p.MaNhanVien,
                    NgayXuat = p.NgayXuat,
                    TrangThai = p.TrangThai,
                }).ToList();
            return Danhsach;
        }
        public PhieuXuat GetPhieuXuatTheoMaPhieuXuat(int maPX)
        {
            return tiemTraSuaModel.PhieuXuat.Where(p => p.MaPhieuXuat == maPX).FirstOrDefault();
        }
        public bool SuaPhieuXuat(PhieuXuat px)
        {
            try
            {
                PhieuXuat phieuXuat = tiemTraSuaModel.PhieuXuat.Where(p => p.MaPhieuXuat == px.MaPhieuXuat).FirstOrDefault();
                if (phieuXuat == null)
                {
                    tiemTraSuaModel.PhieuXuat.Add(px);
                    tiemTraSuaModel.SaveChanges();
                }
                else
                {
                    phieuXuat.MaNhanVien = px.MaNhanVien;
                    phieuXuat.NgayXuat = px.NgayXuat;
                    tiemTraSuaModel.PhieuXuat.AddOrUpdate(phieuXuat);
                    tiemTraSuaModel.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
        public bool XoaPhieuXuat(PhieuXuat px)
        {
            try
            {
                PhieuXuat phieuXuat = tiemTraSuaModel.PhieuXuat.Where(p => p.MaPhieuXuat == px.MaPhieuXuat).FirstOrDefault();
                tiemTraSuaModel.PhieuXuat.Remove(phieuXuat);
                tiemTraSuaModel.SaveChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}