using GongMe.DataTier.Models;
using GongMe.DataTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GongMe.BusinessTier
{
    internal class LoaiHangBUS
    {
        private LoaiHangDAL loaiHangDAL;
        public LoaiHangBUS()
        {
            loaiHangDAL = new LoaiHangDAL();
        }
        public IEnumerable<LoaiHang> GetLoaiHangs()
        {
            return loaiHangDAL.GetLoaiHangs();
        }
        public IEnumerable<LoaiHang> GetLoaiHangTheoHangs(string maLoai)
        {
            return loaiHangDAL.GetLoaiHangTheoHangs(maLoai);
        }
    }
}