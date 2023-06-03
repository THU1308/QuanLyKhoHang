<<<<<<< HEAD:QLKHO/QLKHO/QLKHO/Models/KhachHang.cs
﻿namespace QLKHO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            PhieuXuat = new HashSet<PhieuXuat>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaKH { get; set; }

        [StringLength(100)]
        
        [Required(ErrorMessage ="Tên khách hàng không được để trống")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Tên Khách Hàng không được chứa số")]
        public string TenKH { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage ="Email không được để trống")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Định dạng email không hợp lệ")]
        public string Email { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage ="Số điện thoại không được để trống")]
        [RegularExpression(@"^[0-9]{10,11}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuXuat> PhieuXuat { get; set; }
    }
}
=======
﻿namespace QLKHO.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            PhieuXuat = new HashSet<PhieuXuat>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaKH { get; set; }

        [StringLength(100)]
        
        [Required(ErrorMessage ="Tên khách hàng không được để trống")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Tên Khách Hàng không được chứa số")]
        public string TenKH { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage ="Email không được để trống")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Định dạng email không hợp lệ")]
        public string Email { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage ="Số điện thoại không được để trống")]
        [RegularExpression(@"^[0-9]{10,11}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string SoDienThoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuXuat> PhieuXuat { get; set; }
    }
}
>>>>>>> e63bec154b0ad6905191a88f87d4449a3f379879:QLKHO/QLKHO/Models/KhachHang.cs
