namespace QLKHO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DonViTinh")]
    public partial class DonViTinh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDVT { get; set; }

<<<<<<< HEAD:QLKHO/QLKHO/QLKHO/Models/DonViTinh.cs
        [Required(ErrorMessage = "Tên Đơn Vị Tính không được để trống")]
        [StringLength(100, ErrorMessage = "Tên Đơn Vị Tính không được vượt quá 100 ký tự")]
=======
        [StringLength(100)]
        [Required(ErrorMessage = "Tên Đơn Vị Tính không được để trống")]
>>>>>>> e63bec154b0ad6905191a88f87d4449a3f379879:QLKHO/QLKHO/Models/DonViTinh.cs
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Tên Đơn Vị Tính không được chứa số")]
        public string TenDVT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SanPham> SanPham { get; set; }
    }
}
