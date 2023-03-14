namespace GongMe.DataTier.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChamCong")]
    public partial class ChamCong
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNhanVien { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NgayChamCong { get; set; }

        public TimeSpan? CheckIn { get; set; }

        public TimeSpan? CheckOut { get; set; }

        [StringLength(50)]
        public string TrangThai { get; set; }

        public double? SoGioLam { get; set; }

        public virtual NhanVien NhanVien { get; set; }
    }
}
