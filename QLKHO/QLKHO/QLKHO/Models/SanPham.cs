﻿namespace QLKHO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
<<<<<<< HEAD:QLKHO/QLKHO/QLKHO/Models/SanPham.cs
        public int MaSP { get; set; }

        [Display(Name = "Mã Đơn Vị Tính")]
        public int? MaDVT { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(100, ErrorMessage = "Tên sản phẩm không được vượt quá 100 ký tự")]
=======
        public int MaSP { get; set; }     
        public int? MaDVT { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage ="Tên sản phẩm không được để trống")]
>>>>>>> e63bec154b0ad6905191a88f87d4449a3f379879:QLKHO/QLKHO/Models/SanPham.cs
        public string TenSP { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage = "Giá bán không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá bán không hợp lệ")]
        public decimal? GiaBan { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không hợp lệ")]
<<<<<<< HEAD:QLKHO/QLKHO/QLKHO/Models/SanPham.cs
        public int? SoLuongCon { get; set; }

        [Required(ErrorMessage = "Chưa upload file ảnh")]
        [StringLength(100, ErrorMessage = "Đường dẫn ảnh không được vượt quá 100 ký tự")]
=======

        public int? SoLuongCon { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Chưa upload file ảnh")]
       
>>>>>>> e63bec154b0ad6905191a88f87d4449a3f379879:QLKHO/QLKHO/Models/SanPham.cs
        public string Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietCungCap> ChiTietCungCap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuat { get; set; }

        public virtual DonViTinh DonViTinh { get; set; }
    }
}
