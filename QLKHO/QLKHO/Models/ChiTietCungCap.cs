namespace QLKHO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChiTietCungCap")]
    public partial class ChiTietCungCap
    {
        public int MaNCC { get; set; }

        public int MaSP { get; set; }

        [Column(TypeName = "money")]
        public decimal? GiaNhap { get; set; }

        public int id { get; set; }
        public object NhaCungCap { get; internal set; }
        public object SanPham { get; internal set; }
    }
}
