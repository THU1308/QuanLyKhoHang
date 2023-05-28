namespace QLKHO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NhaCungCap")]
    public partial class NhaCungCap
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhaCungCap()
        {
            ChiTietCungCap = new HashSet<ChiTietCungCap>();
            PhieuNhap = new HashSet<PhieuNhap>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNCC { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage ="Tên Nhà Cung Cấp không được để trống")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Tên Nhà Cung Cấp không được chứa số")]
        public string TenNCC { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Email không được để trống")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Định dạng email không hợp lệ")]
        public string Email { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage ="Số Fax không được để trống")]
        [RegularExpression(@"^[0-9]{10,}$", ErrorMessage = "Số Fax không hợp lệ.")]
        public string Fax { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [RegularExpression(@"^[0-9]{10,11}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietCungCap> ChiTietCungCap { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuNhap> PhieuNhap { get; set; }
    }
}
