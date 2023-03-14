namespace GongMe.DataTier.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class View_All_PhieuXuat
    {
        public DateTime? NgayXuat { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaMatHang { get; set; }

        public int? SoLuong { get; set; }

        [StringLength(50)]
        public string TenHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaPhieuXuat { get; set; }
    }
}
