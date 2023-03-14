namespace GongMe.DataTier.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TonKho")]
    public partial class TonKho
    {
        [Key]
        public int MaTonKho { get; set; }

        [Required]
        [StringLength(10)]
        public string MaMatHang { get; set; }

        [StringLength(50)]
        public string TenHang { get; set; }

        public int? SoLuongTon { get; set; }

        public DateTime? NgayNhap { get; set; }

        public virtual MatHang MatHang { get; set; }
    }
}
