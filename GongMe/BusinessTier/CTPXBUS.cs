using GongMe.DataTier.Models;
using GongMe.DataTier;
using GongMe.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GongMe.BusinessTier
{
    internal class CTPXBUS
    {
        private CTPXDAL cTPXDAL;
        public CTPXBUS()
        {
            cTPXDAL = new CTPXDAL();
        }
        public IEnumerable<CTPXViewModel> GetCTPXs(int maPX)
        {
            return cTPXDAL.GetCTPXs(maPX);
        }
        public bool CapNhatPhieuXuat(CTPX cTPX)
        {
            return cTPXDAL.CapNhatPhieuXuat(cTPX);
        }

        internal bool SuaPhieuXuat(CTPX cTPX)
        {
            return cTPXDAL.SuaPhieuXuat(cTPX);
        }

        internal bool XoaPhieuXuat(CTPX cTPX)
        {
            try
            {
                return cTPXDAL.XoaPhieuXuat(cTPX);
            }
            catch (Exception ex) { throw ex; }
        }

        internal List<CTPX> GetListCTPXs(int maPX)
        {
            try
            {
                return cTPXDAL.GetListCTPXs(maPX);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}