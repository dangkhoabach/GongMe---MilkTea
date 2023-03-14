namespace GongMe.DataTier.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Luong")]
    public partial class Luong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNhanVien { get; set; }

        [StringLength(10)]
        public string MaChucVu { get; set; }

        [StringLength(50)]
        public string ThoiGian { get; set; }

        public long? LuongCoBan { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }
    }
}
