using GongMe.DataTier.Models;
using GongMe.DataTier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GongMe.BusinessTier
{
    internal class MatHangBUS
    {
        private MatHangDAL matHangDAL;
        public MatHangBUS()
        {
            matHangDAL = new MatHangDAL();
        }
        public IEnumerable<MatHang> GetMatHangs()
        {
            return matHangDAL.GetMatHangs();
        }
        public IEnumerable<MatHang> GetMatHangTheoMaLoaiHang(string maLoai)
        {
            return matHangDAL.GetMatHangTheoMaLoaiHang(maLoai);
        }
        internal string GetMaLoaiTheoMaMatHang(string maMatHang)
        {
            return matHangDAL.GetMaLoaiTheoMaMatHang(maMatHang);
        }
    }
}