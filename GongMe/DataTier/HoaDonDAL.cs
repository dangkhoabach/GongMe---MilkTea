﻿using GongMe.DataTier.Models;
using GongMe.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Windows.Forms;

namespace GongMe.DataTier
{
    internal class HoaDonDAL
    {
        private TiemTraSuaModel tiemTraSuaModel;
        public HoaDonDAL()
        {
            tiemTraSuaModel = new TiemTraSuaModel();
        }
        public IEnumerable<HoaDonViewModel> GetHoaDons()
        {
            var danhSach = tiemTraSuaModel.HoaDon
                .Select(p => new HoaDonViewModel
                {
                    MaBan = p.MaBan,
                    MaHD = p.MaHoaDon,
                    MaNV = p.MaNhanVien,
                    NgayBan = p.NgayXuat,
                    TongTien = p.TongTien,
                    TrangThai = p.TrangThai,
                }).ToList();
            return danhSach;
        }
        public bool LuuHoaDon(HoaDon hd)
        {
            try
            {
                tiemTraSuaModel.HoaDon.Add(hd);
                tiemTraSuaModel.SaveChanges();
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
        public bool SuaHoaDon(HoaDon hd)
        {
            try
            {
                HoaDon hoaDon = tiemTraSuaModel.HoaDon.Where(p => p.MaHoaDon == hd.MaHoaDon).FirstOrDefault();
                if (hoaDon == null)
                {
                    tiemTraSuaModel.HoaDon.Add(hd);
                    tiemTraSuaModel.SaveChanges();
                }
                else
                {
                    hoaDon.MaNhanVien = hd.MaNhanVien;
                    hoaDon.NgayXuat = hd.NgayXuat;
                    hoaDon.MaBan = hd.MaBan;
                    tiemTraSuaModel.HoaDon.AddOrUpdate(hoaDon);
                    tiemTraSuaModel.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
        public bool XoaHoaDon(HoaDon hd)
        {
            try
            {
                HoaDon hoaDon = tiemTraSuaModel.HoaDon.Where(p => p.MaHoaDon == hd.MaHoaDon).FirstOrDefault();
                if (hoaDon == null)
                {
                    throw new Exception("Lỗi");
                }
                else
                {
                    /*DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {*/
                    tiemTraSuaModel.HoaDon.Remove(hoaDon);
                    tiemTraSuaModel.SaveChanges();
                    //}
                    return true;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public HoaDon GetHoaDonTheoMa(int maHoaDon)
        {
            return tiemTraSuaModel.HoaDon.Where(x => x.MaHoaDon == maHoaDon).FirstOrDefault();
        }
        public List<HoaDon> GetHoaDonDaThanhToan()
        {
            return tiemTraSuaModel.HoaDon.Where(p => p.TrangThai == "Đã thanh toán").ToList();
        }
        public IEnumerable<HoaDonViewModel> GetHoaDonMaNV(int maNV)
        {
            var danhSach = tiemTraSuaModel.HoaDon
                .Where(p => p.MaNhanVien == maNV)
                .Select(p => new HoaDonViewModel
                {
                    MaBan = p.MaBan,
                    MaHD = p.MaHoaDon,
                    MaNV = p.MaNhanVien,
                    NgayBan = p.NgayXuat,
                    TongTien = p.TongTien,
                    TrangThai = p.TrangThai,
                }).ToList();
            return danhSach;
        }

        public IEnumerable<HoaDonViewModel> GetHoaDonMaHD(int maHoaDon)
        {
            var danhSach = tiemTraSuaModel.HoaDon
                .Where(p => p.MaHoaDon == maHoaDon)
                .Select(p => new HoaDonViewModel
                {
                    MaBan = p.MaBan,
                    MaHD = p.MaHoaDon,
                    MaNV = p.MaNhanVien,
                    NgayBan = p.NgayXuat,
                    TongTien = p.TongTien,
                    TrangThai = p.TrangThai,
                }).ToList();
            return danhSach;
        }
        public List<HoaDon> GetHoaDonChuaThanhToan()
        {
            return tiemTraSuaModel.HoaDon.Where(p => p.TrangThai == "Chưa thanh toán").ToList();
        }
        public HoaDon GetHoaDonTheoTrangThaiVaBan(string maBan)
        {
            return tiemTraSuaModel.HoaDon.Where(p => p.TrangThai == "Chưa thanh toán").FirstOrDefault(p => p.MaBan == maBan);
        }
        public IEnumerable<HoaDonViewModel> GetHoaDonTrangThai2()
        {
            var danhSach = tiemTraSuaModel.HoaDon
                .Where(p => p.TrangThai == "Chưa thanh toán")
                .Select(p => new HoaDonViewModel
                {
                    MaBan = p.MaBan,
                    MaHD = p.MaHoaDon,
                    MaNV = p.MaNhanVien,
                    NgayBan = p.NgayXuat,
                    TongTien = p.TongTien,
                    TrangThai = p.TrangThai,
                }).ToList();
            return danhSach;
        }

        internal HoaDon GetHoaDonDaThanhToanTheoMa(int maHD)
        {
            return tiemTraSuaModel.HoaDon.Where(p => p.TrangThai == "Đã thanh toán").FirstOrDefault(p => p.MaHoaDon == maHD);
        }

        public IEnumerable<HoaDonViewModel> GetHoaDonTrangThai1()
        {
            var danhSach = tiemTraSuaModel.HoaDon
                .Where(p => p.TrangThai == "Đã thanh toán")
                .Select(p => new HoaDonViewModel
                {
                    MaBan = p.MaBan,
                    MaHD = p.MaHoaDon,
                    MaNV = p.MaNhanVien,
                    NgayBan = p.NgayXuat,
                    TongTien = p.TongTien,
                    TrangThai = p.TrangThai,
                }).ToList();
            return danhSach;
        }
    }
}
