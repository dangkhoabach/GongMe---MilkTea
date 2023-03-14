using GongMe.DataTier;
using GongMe.DataTier.Models;
using GongMe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GongMe.BusinessTier
{
    internal class PhieuXuatBUS
    {
        private PhieuXuatDAL phieuXuatDAL;
        public PhieuXuatBUS()
        {
            phieuXuatDAL = new PhieuXuatDAL();
        }
        public IEnumerable<PhieuXuatViewModel> GetPhieuXuats()
        {
            return phieuXuatDAL.GetPhieuXuats();
        }

        public IEnumerable<PhieuXuatViewModel> GetPhieuXuatTheoMaPX(int maPX)
        {
            return phieuXuatDAL.GetPhieuXuatTheoMaPX(maPX);
        }
        internal PhieuXuat GetPhieuXuatTheoMaPhieuXuat(int maPX)
        {
            return phieuXuatDAL.GetPhieuXuatTheoMaPhieuXuat(maPX);
        }
        public bool SuaPhieuXuat(PhieuXuat px)
        {
            return phieuXuatDAL.SuaPhieuXuat(px);
        }
        public bool XoaPhieuXuat(PhieuXuat px)
        {
            return phieuXuatDAL.XoaPhieuXuat(px);
        }
    }
}