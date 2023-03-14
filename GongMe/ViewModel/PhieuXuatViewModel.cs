using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GongMe.ViewModel
{
    internal class PhieuXuatViewModel
    {
        public int MaPhieuXuat { get; set; }
        public int? MaNhanVien { get; set; }
        public DateTime? NgayXuat { get; set; }
        public string TrangThai { get; set; }
    }
}