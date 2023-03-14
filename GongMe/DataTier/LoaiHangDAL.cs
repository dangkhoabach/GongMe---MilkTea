using GongMe.DataTier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GongMe.DataTier
{
    internal class LoaiHangDAL
    {
        private TiemTraSuaModel tiemTraSuaModel;
        public LoaiHangDAL()
        {
            tiemTraSuaModel = new TiemTraSuaModel();
        }
        public IEnumerable<LoaiHang> GetLoaiHangs()
        {
            return tiemTraSuaModel.LoaiHang.ToList();
        }
        public IEnumerable<LoaiHang> GetLoaiHangTheoHangs(string maLoai)
        {
            return tiemTraSuaModel.LoaiHang.Where(p => p.MaLoai == maLoai).ToList();
        }
    }
}