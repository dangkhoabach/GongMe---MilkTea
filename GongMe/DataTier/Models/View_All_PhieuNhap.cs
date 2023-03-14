namespace GongMe.DataTier.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_All_PhieuNhap
    {
        [StringLength(50)]
        public string TenNhaCC { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        public long? TongTien { get; set; }

        public DateTime? NgayNhap { get; set; }

        [StringLength(50)]
        public string TenHang { get; set; }

        public long? DonGia { get; set; }

        public int? Expr1 { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPhieuNhap { get; set; }
    }
}
