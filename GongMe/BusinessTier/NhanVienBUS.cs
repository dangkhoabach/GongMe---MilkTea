using GongMe.DataTier.Models;
using GongMe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GongMe.BusinessTier
{
    internal class NhanVienBUS
    {
        private NhanVienDAL nhanVienDAL;
        public NhanVienBUS()
        {
            nhanVienDAL = new NhanVienDAL();
        }
        public IEnumerable<NhanVienViewModel> GetNhanViens()
        {
            return nhanVienDAL.GetNHANVIENs();
        }
        public IEnumerable<NhanVien> GetCBXNhanViens()
        {
            return nhanVienDAL.GetCBXNhanViens();
        }
        public bool ThemNhanVien(NhanVien nv)
        {
            try
            {
                return nhanVienDAL.ThemNhanVien(nv);
            }
            catch (Exception ex) { throw ex; }

        }
        public bool SuaNhanVien(NhanVien nv)
        {
            try
            {
                return nhanVienDAL.SuaNhanVien(nv);
            }
            catch (Exception ex) { throw ex; }

        }
        public bool XoaNhanVien(NhanVien nv)
        {
            try
            {
                return nhanVienDAL.XoaNhanVien(nv);
            }
            catch (Exception ex) { throw ex; }

        }

        public IEnumerable<NhanVienViewModel> GetNhanVienSDT(string sdt)
        {
            return nhanVienDAL.GetNhanVienSDT(sdt);
        }

        internal NhanVien GetNhanVienTheoMa()
        {
            throw new NotImplementedException();
        }

        internal NhanVien GetNhanVienTheoSDT(string sdt)
        {
            return nhanVienDAL.GetNhanVienTheoSDT(sdt);
        }
    }
}