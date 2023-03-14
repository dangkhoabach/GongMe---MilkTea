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
    internal class KhoBUS
    {
        private KhoDAL khoDAL;
        public KhoBUS()
        {
            khoDAL = new KhoDAL();
        }
        public IEnumerable<KhoViewModel> GetKhos()
        {
            return khoDAL.GetKhos();
        }

        public bool CapNhatKho(CTPX cTPX, TonKho tonKho)
        {
            return khoDAL.CapNhatKho(cTPX, tonKho);
        }

        public TonKho GetHangTheoMa(string maMatHang)
        {
            return khoDAL.GetHangTheoMa(maMatHang);
        }

        public IEnumerable<KhoViewModel> GetHangTheoMaLoai(string maLoai)
        {
            return khoDAL.GetHangTheoMaLoai(maLoai);
        }
        public IEnumerable<KhoViewModel> GetHangTheoMaHang(string maHang)
        {
            return khoDAL.GetHangTheoMaHang(maHang);
        }

        public List<TonKho> GetListHangTheoMa(string maMatHang)
        {
            return khoDAL.GetListHangTheoMa(maMatHang);
        }

        internal bool XoaTonKho(TonKho tonKho)
        {
            return khoDAL.XoaTonKho(tonKho);
        }
    }
}