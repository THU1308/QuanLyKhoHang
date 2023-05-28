namespace QLKHO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTietCungCap = new HashSet<ChiTietCungCap>();
            ChiTietPhieuNhap = new HashSet<ChiTietPhieuNhap>();
            ChiTietPhieuXuat = new HashSet<ChiTietPhieuXuat>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaSP { get; set; }     
        public int? MaDVT { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage ="Tên sản phẩm không được để trống")]
        public string TenSP { get; set; }

        [Column(TypeName = "money")]
        [Required(ErrorMessage = "Giá bán không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá bán không hợp lệ")]
        public decimal? GiaBan { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không hợp lệ")]

        public int? SoLuongCon { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Chưa upload file ảnh")]
       
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
