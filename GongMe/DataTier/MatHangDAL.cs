using GongMe.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GongMe.DataTier
{
    internal class MatHangDAL
    {
        private TiemTraSuaModel tiemTraSuaModel;
        public MatHangDAL()
        {
            tiemTraSuaModel = new TiemTraSuaModel();
        }
        public IEnumerable<MatHang> GetMatHangs()
        {
            return tiemTraSuaModel.MatHang.ToList();
        }
        public IEnumerable<MatHang> GetMatHangTheoMaLoaiHang(string maLoaiHang)
        {
            return tiemTraSuaModel.MatHang.Where(p => p.MaLoai == maLoaiHang).ToList();
        }
        internal string GetMaLoaiTheoMaMatHang(string maMatHang)
        {
            return tiemTraSuaModel.MatHang.Where(p => p.MaMatHang == maMatHang).FirstOrDefault().MaLoai;
        }
    }
}