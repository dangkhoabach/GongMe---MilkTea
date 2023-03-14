namespace GongMe.DataTier.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_All_Bill_Test
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHoaDon { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MaMon { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(10)]
        public string MaBan { get; set; }

        public DateTime? NgayXuat { get; set; }

        public long? TongTien { get; set; }

        [StringLength(50)]
        public string TenMon { get; set; }

        public long? DonGia { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }
    }
}
