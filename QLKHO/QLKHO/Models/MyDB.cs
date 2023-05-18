using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QLKHO.Models
{
    public partial class MyDB : DbContext
    {
        public MyDB()
            : base("name=MyDB")
        {
        }

        public virtual DbSet<ChiTietCungCap> ChiTietCungCap { get; set; }
        public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhap { get; set; }
        public virtual DbSet<ChiTietPhieuXuat> ChiTietPhieuXuat { get; set; }
        public virtual DbSet<DonViTinh> DonViTinh { get; set; }
        public virtual DbSet<KhachHang> KhachHang { get; set; }
        public virtual DbSet<NhaCungCap> NhaCungCap { get; set; }
        public virtual DbSet<PhanQuyen> PhanQuyen { get; set; }
        public virtual DbSet<PhieuNhap> PhieuNhap { get; set; }
        public virtual DbSet<PhieuXuat> PhieuXuat { get; set; }
        public virtual DbSet<Quyen> Quyen { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }
        public virtual DbSet<TaiKhoan> TaiKhoan { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietCungCap>()
                .Property(e => e.GiaNhap)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ChiTietPhieuNhap>()
                .Property(e => e.DonGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ChiTietPhieuXuat>()
                .Property(e => e.DonGia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<DonViTinh>()
                .HasMany(e => e.SanPham)
                .WithOptional(e => e.DonViTinh)
                .WillCascadeOnDelete();

            modelBuilder.Entity<KhachHang>()
                .HasMany(e => e.PhieuXuat)
                .WithOptional(e => e.KhachHang)
                .WillCascadeOnDelete();

            modelBuilder.Entity<NhaCungCap>()
                .HasMany(e => e.PhieuNhap)
                .WithOptional(e => e.NhaCungCap)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PhanQuyen>()
                .HasMany(e => e.TaiKhoan)
                .WithOptional(e => e.PhanQuyen)
                .WillCascadeOnDelete();

            modelBuilder.Entity<PhanQuyen>()
                .HasMany(e => e.Quyen)
                .WithMany(e => e.PhanQuyen)
                .Map(m => m.ToTable("ChiTietPhanQuyen").MapLeftKey("MaPQ").MapRightKey("MaQuyen"));

            modelBuilder.Entity<PhieuNhap>()
                .Property(e => e.ThanhTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PhieuXuat>()
                .Property(e => e.ThanhTien)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SanPham>()
                .Property(e => e.GiaBan)
                .HasPrecision(19, 4);

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.PhieuNhap)
                .WithOptional(e => e.TaiKhoan)
                .WillCascadeOnDelete();

            modelBuilder.Entity<TaiKhoan>()
                .HasMany(e => e.PhieuXuat)
                .WithOptional(e => e.TaiKhoan)
                .WillCascadeOnDelete();
        }
    }
}
