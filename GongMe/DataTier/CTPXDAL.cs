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
    internal class CTPXDAL
    {
        private TiemTraSuaModel tiemTraSuaModel;
        public CTPXDAL()
        {
            tiemTraSuaModel = new TiemTraSuaModel();
        }
        public IEnumerable<CTPXViewModel> GetCTPXs(int maPX)
        {
            var Danhsach = tiemTraSuaModel.CTPX
                .Where(p => p.MaPhieuXuat == maPX)
                .Select(p => new CTPXViewModel
                {
                    MaPhieuXuat = p.MaPhieuXuat,
                    MaMatHang = p.MaMatHang,
                    TenMatHang = p.MatHang.TenHang,
                    Soluong = p.SoLuong,
                }).ToList();
            return Danhsach;
        }
        public bool CapNhatPhieuXuat(CTPX cTPX)
        {
            KhoBUS khoBUS = new KhoBUS();
            PhieuXuatBUS phieuXuatBUS = new PhieuXuatBUS();
            CTPX chitiet = tiemTraSuaModel.CTPX.Where(p => p.MaPhieuXuat == cTPX.MaPhieuXuat).FirstOrDefault();
            List<TonKho> tonKhos = tiemTraSuaModel.TonKho.Where(p => p.MaMatHang == cTPX.MaMatHang).ToList();
            int? SLtonKho = tonKhos.Sum(p => p.SoLuongTon).Value;
            try
            {
                if (chitiet == null)
                {
                    MessageBox.Show("Thêm chi tiết phiếu xuất thành công", "Thông báo");
                    tiemTraSuaModel.CTPX.Add(cTPX);
                    tiemTraSuaModel.SaveChanges();
                    return true;
                }
                else
                {
                    foreach (var item in phieuXuatBUS.GetPhieuXuatTheoMaPhieuXuat(cTPX.MaPhieuXuat).CTPX)
                    {
                        if (item.MaMatHang == cTPX.MaMatHang)
                        {
                            chitiet = item;
                            chitiet.SoLuong += cTPX.SoLuong;
                            if (chitiet.SoLuong > SLtonKho)
                            {
                                MessageBox.Show("Lượng hàng không đủ", "Thông báo");
                                return false;
                            }
                            else
                            {
                                MessageBox.Show("Thêm chi tiết phiếu xuất thành công", "Thông báo");
                                tiemTraSuaModel.CTPX.AddOrUpdate(chitiet);
                                tiemTraSuaModel.SaveChanges();
                                return true;
                            }
                        }
                    }
                    tiemTraSuaModel.CTPX.Add(cTPX);
                    tiemTraSuaModel.SaveChanges();
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        internal bool SuaPhieuXuat(CTPX cTPX)
        {
            KhoBUS khoBUS = new KhoBUS();
            PhieuXuatBUS phieuXuatBUS = new PhieuXuatBUS();
            CTPX chitiet = tiemTraSuaModel.CTPX.Where(p => p.MaPhieuXuat == cTPX.MaPhieuXuat).FirstOrDefault();
            List<TonKho> tonKhos = tiemTraSuaModel.TonKho.Where(p => p.MaMatHang == cTPX.MaMatHang).ToList();
            int? SLtonKho = tonKhos.Sum(p => p.SoLuongTon).Value;
            try
            {
                if (chitiet == null)
                {
                    MessageBox.Show("Lỗi", "Thông báo");
                    return false;
                }
                else
                {
                    foreach (var item in phieuXuatBUS.GetPhieuXuatTheoMaPhieuXuat(cTPX.MaPhieuXuat).CTPX)
                    {
                        if (item.MaMatHang == cTPX.MaMatHang)
                        {
                            chitiet = item;
                            chitiet.SoLuong = cTPX.SoLuong;
                            if (chitiet.SoLuong > SLtonKho)
                            {
                                MessageBox.Show("Lượng hàng không đủ", "Thông báo");
                                return false;
                            }
                            else
                            {
                                MessageBox.Show("Sửa chi tiết phiếu xuất thành công", "Thông báo");
                                tiemTraSuaModel.CTPX.AddOrUpdate(chitiet);
                                tiemTraSuaModel.SaveChanges();
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        internal bool XoaPhieuXuat(CTPX cTPX)
        {
            PhieuXuatBUS phieuXuatBUS = new PhieuXuatBUS();
            CTPX chitiet = tiemTraSuaModel.CTPX.Where(p => p.MaPhieuXuat == cTPX.MaPhieuXuat).FirstOrDefault();
            List<CTPX> listCTPX = tiemTraSuaModel.CTPX.Where(p => p.MaPhieuXuat == cTPX.MaPhieuXuat).ToList();
            try
            {
                if (chitiet == null)
                {
                    MessageBox.Show("Lỗi", "Thông báo");
                    return false;
                }
                else
                {
                    for (int i = 0; i < listCTPX.Count; i++)
                    {
                        if (listCTPX[i].MaMatHang == cTPX.MaMatHang)
                        {
                            chitiet = listCTPX[i];
                            tiemTraSuaModel.CTPX.Remove(chitiet);
                            tiemTraSuaModel.SaveChanges();
                            return true;

                        }
                    }
                    return false;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        internal List<CTPX> GetListCTPXs(int maPX)
        {
            return tiemTraSuaModel.CTPX.Where(p => p.MaPhieuXuat == maPX).ToList();
        }
    }
}