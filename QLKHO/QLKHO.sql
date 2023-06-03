﻿
create database QUANLYKHOO


create table Quyen(
	MaQuyen int primary key,
	TenQuyen nvarchar(100)
)

create table PhanQuyen(
	MaPQ int primary key,
	TenPQ nvarchar(100)
)

create table ChiTietPhanQuyen(
	MaPQ int,
	MaQuyen int,
	constraint PK_ChiTietPQ primary key(MaPQ,MaQuyen),
	constraint FK_ChiTietPQ_PQ foreign key(MaPQ) references PhanQuyen(MaPQ) ON DELETE CASCADE ON UPDATE CASCADE ,
	constraint FK_ChiTietPQ_Quyen foreign key(MaQuyen) references Quyen(MaQuyen) ON DELETE CASCADE ON UPDATE CASCADE
)

create table TaiKhoan(
	MaTK int primary key,
	TenTK nvarchar(100),
	MaPQ int ,
	constraint FK_TaiKhoan_PQ foreign key(MaPQ) references PhanQuyen(MaPQ) ON DELETE CASCADE ON UPDATE CASCADE,
	MatKhau nvarchar(100),
	SoDienThoai nvarchar(100),
	Email nvarchar(100),
	HoTen nvarchar(100),
	NgaySinh date,
	Anh nvarchar(100)

)

create table NhaCungCap(
	MaNCC int primary key,
	TenNCC nvarchar(100),
	Email nvarchar(100),
	Fax nvarchar(100),
	SoDienThoai nvarchar(100)
)

create table KhachHang(
	MaKH int primary key,
	TenKH nvarchar(100),
	Email nvarchar(100),
	SoDienThoai nvarchar(100)
)

create table DonViTinh(
	MaDVT int primary key,
	TenDVT nvarchar(100)
)

create table SanPham(
	MaSP int primary key,
	MaDVT int ,
	TenSP nvarchar(100),
	GiaBan money,
	SoLuongCon int,
	Anh nvarchar(100),
	constraint FK_SanPham_DVT foreign key(MaDVT) references DonViTinh(MaDVT) ON DELETE CASCADE ON UPDATE CASCADE
)

create table ChiTietCungCap(
	MaNCC int,
	MaSP int,
	GiaNhap money,
	constraint PK_ChiTietCungCap primary key(MaNCC,MaSP),
	constraint FK_ChiTietCC_NCC foreign key(MaNCC) references NhaCungCap(MaNCC) ON DELETE CASCADE ON UPDATE CASCADE,
	constraint FK_ChiTietCC_SP foreign key(MaSP) references SanPham(MaSP) ON DELETE CASCADE ON UPDATE CASCADE
)

create table PhieuXuat(
	MaPhieuXuat int primary key,
	MaKH int,
	MaTK int,
	NgayLap date,
	ThanhTien money,
	constraint FK_PhieuXuat_KH foreign key(MaKH) references KhachHang(MaKH) ON DELETE CASCADE ON UPDATE CASCADE,
	constraint FK_PhieuXuat_TK foreign key(MaTK) references TaiKhoan(MaTK) ON DELETE CASCADE ON UPDATE CASCADE
)

create table ChiTietPhieuXuat(
	MaSP int ,
	MaPhieuXuat int ,
	DonGia money,
	SoLuong int,
	constraint PK_ChiTietPX primary key(MaSP,MaPhieuXuat),
	constraint FK_ChiTietPhieuXuat_SP foreign key(MaSP) references SanPham(MaSP) ON DELETE CASCADE ON UPDATE CASCADE,
	constraint FK_ChiTietPhieuXuat_PhieuXuat foreign key(MaPhieuXuat) references PhieuXuat(MaPhieuXuat) ON DELETE CASCADE ON UPDATE CASCADE
)

create table PhieuNhap(
	MaPhieuNhap int primary key,
	MaNCC int,
	MaTK int ,
	NgayLap date,
	ThanhTien money,
	constraint FK_PhieuNhap_NCC foreign key(MaNCC) references NhaCungCap(MaNCC) ON DELETE CASCADE ON UPDATE CASCADE,
	constraint FK_PhieuNhap_TK foreign key(MaTK) references TaiKhoan(MaTK) ON DELETE CASCADE ON UPDATE CASCADE
)

create table ChiTietPhieuNhap(
	MaPhieuNhap int ,
	MaSP int,
	DonGia money,
	SoLuong int,
	constraint PK_ChiTietPN primary key(MaPhieuNhap,MaSP),
	constraint FK_ChiTietPhieuNhap_SP foreign key(MaSP) references SanPham(MaSP) ON DELETE CASCADE ON UPDATE CASCADE,
	constraint FK_ChiTietPhieuNhap_PhieuNhap foreign key(MaPhieuNhap) references PhieuNhap(MaPhieuNhap) ON DELETE CASCADE ON UPDATE CASCADE
)
go
insert into NhaCungCap values(1,N'Nguyễn Văn Thư','ssvu97@gmail.com','123456','0385861308')

insert into DonViTinh values(1,N'Chiếc')
insert into DonViTinh values(2,N'Cái')
insert into DonViTinh values(3,N'Miếng')


insert into SanPham values(1,1,N'Tôn',500000,20,N'Không có')
insert into SanPham values(2,1,N'Tôn2',500000,20,N'Không có')

insert into ChiTietCungCap values(1,1,500000)

insert into Quyen values(1,'Admin')
insert into Quyen values(2,'Staff')


insert into PhanQuyen values(1,'PQ Admin')
insert into PhanQuyen values(2,'PQ Staff')

insert into ChiTietPhanQuyen values(1,1)
insert into ChiTietPhanQuyen values(2,2)

insert into TaiKhoan values(6,'admin',1,'$2a$11$8y5ns9.MeF.6ZPa28vN46e6.dgzMiDWShGygFgKgwRa.W5lGDxRy2',0385861308,'ssvu97@gmail.com',N'Nguyễn Văn Thư','08/13/2002','#')

insert into PhieuNhap values(1,1,1,'2023/04/29',1000000)
insert into PhieuNhap values(3,1,1,'2023/04/29',1000000)
insert into ChiTietPhieuNhap values(1,1,500000,1)
insert into ChiTietPhieuNhap values(1,2,500000,1)
select * from PhieuNhap
select * from ChiTietPhieuNhap
select * from SanPham
select * from ChiTietCungCap
select * from KhachHang
delete from PhieuNhap
delete from TaiKhoan

