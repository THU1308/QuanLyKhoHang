﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLKHO.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.html.simpleparser;

namespace QuanLyKhoHang.Controllers
{
    public class PhieuNhapsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: PhieuNhaps
        public ActionResult Index()
        {
            var phieuNhap = db.PhieuNhap.Include(p => p.NhaCungCap).Include(p => p.TaiKhoan);
            var x = db.TaiKhoan.Include(p=>p.PhanQuyen);
            return View(phieuNhap.ToList());
        }
        
        public ActionResult ChiTietPhieu(int? id)
        {
            var chiTietPN = db.ChiTietPhieuNhap
                    .Where(s => s.MaPhieuNhap == id)
                    .Include(s => s.PhieuNhap)
                    .Include(s => s.PhieuNhap.NhaCungCap)
                    .Include(s => s.PhieuNhap.TaiKhoan)
                    .ToList();
            return View(chiTietPN);
        }

        public ActionResult Print(int? id)
        {
            var chiTietPN = db.ChiTietPhieuNhap
                    .Where(s => s.MaPhieuNhap == id)
                    .Include(s => s.PhieuNhap)
                    .Include(s => s.PhieuNhap.NhaCungCap)
                    .Include(s => s.PhieuNhap.TaiKhoan)
                    .ToList();
            return View(chiTietPN);
        }

        public ActionResult ExportToPdf()
        {
            // Lấy mã HTML từ view của bạn
            string html = @"<div class=""container"">
                    <div class=""invoice-header"">
                        <h2 class=""invoice-title"">HÓA ĐƠN</h2>
                    </div>
                    <div class=""invoice-details"">
                        @if (Model.Count() > 0)
                        {
                            var item = Model.ToList()[0];

                            <div class=""col"">
                                <strong>Mã Phiếu:</strong> @item.PhieuNhap.MaPhieuNhap<br>
                                <strong>Mã Nhà Cung Cấp:</strong> @item.PhieuNhap.NhaCungCap.TenNCC<br>
                                <strong>Nhân Viên:</strong> @item.PhieuNhap.TaiKhoan.HoTen<br>
                            </div>
                            <div class=""col"">
                                <strong>Ngày Lập:</strong> @item.PhieuNhap.NgayLap.Value.ToString(""dd-MM-yyyy"")<br>
                                <strong>Tổng Tiền:</strong> @item.PhieuNhap.ThanhTien<br>
                            </div>
                        }
                    </div>
                    <table class=""table table-bordered"">
                        <thead>
                            <tr>
                                <th>Mã Sản Phẩm</th>
                                <th>Tên Sản Phẩm</th>
                                <th>Đơn Giá</th>
                                <th>Số Lượng</th>
                                <th>Thành Tiền</th>
                            </tr>
                        </thead>
                        <tbody>
                            @foreach (var item in Model)
                            {
                                decimal? thanhTien = item.DonGia * item.SoLuong;
                                <tr>
                                    <td>@Html.DisplayFor(modelItem => item.MaSP)</td>
                                    <td>@Html.DisplayFor(modelItem => item.SanPham.TenSP)</td>
                                    <td>@Html.DisplayFor(modelItem => item.DonGia)</td>
                                    <td>@Html.DisplayFor(modelItem => item.SoLuong)</td>
                                    <td>@(thanhTien.HasValue ? thanhTien.Value : 0m)</td>
                                </tr>
                            }
                        </tbody>
                    </table>
                    <div class=""text-right"">
                        <a href=""~/PhieuNhaps/Index"" class=""btn btn-primary"">Quay Lại</a>
                    </div>
                </div>";

            // Khởi tạo tài liệu PDF mới
            Document document = new Document();

            // Khởi tạo PdfWriter để ghi dữ liệu vào tài liệu
            string fileName = "yourfile.pdf";
            string filePath = Server.MapPath("~/Files/" + fileName);
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));

            // Mở tài liệu để bắt đầu ghi
            document.Open();

            // Thiết lập các thuộc tính và phong cách cho tài liệu
            document.SetPageSize(PageSize.A4);
            document.SetMargins(30, 30, 30, 30);
            document.AddCreationDate();

            // Chuyển đổi mã HTML thành các phần tử PDF và thêm chúng vào tài liệu
            StringReader sr = new StringReader(html);
            HTMLWorker htmlparser = new HTMLWorker(document);
            htmlparser.Parse(sr);

            // Kết thúc việc ghi và đóng tài liệu
            document.Close();

            return View("Print");
        }

        // GET: PhieuNhaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhap phieuNhap = db.PhieuNhap.Find(id);
            if (phieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhap);
        }

        public static List<ChiTietPhieuNhap> listctpn = new List<ChiTietPhieuNhap>();
        public static List<SanPham> listsp = new List<SanPham>();
        public static List<ChiTietCungCap> listctcc = new List<ChiTietCungCap>();
        // GET: PhieuNhaps/Create
        public ActionResult Create1()
        {
            var sp = db.SanPham.ToList();
            if (sp != null && sp.Any())
            {
                ViewBag.SanPham = sp;
            }
            else
            {
                ViewBag.SanPham = new List<SanPham>();
            }

            var dvt = db.DonViTinh.ToList();
            if (dvt != null && dvt.Any())
            {
                ViewBag.DonViTinh = dvt;
            }
            else
            {
                ViewBag.DonViTinh = new List<DonViTinh>();
            }

            int idncc, idnhanvien, idsp;
            if (int.TryParse(Request["MaNCC"], out idncc) &&
                int.TryParse(Request["MaTK"], out idnhanvien) &&
                int.TryParse(Request["TenSanPham"], out idsp))
            {

                NhaCungCap ncc = db.NhaCungCap.Find(idncc);
                TaiKhoan tk = db.TaiKhoan.Find(idnhanvien);
                /*ViewBag.idncc = idncc;
                ViewBag.tenncc = ncc != null ? ncc.TenNCC : "";
                ViewBag.idnv = idnhanvien;
                ViewBag.tennv = tk != null ? tk.HoTen : "";
                ViewBag.ngaynhap = Request["ngaynhap"];*/

                Session["mapn"] = Request["mapn"];
                Session["idncc"] = idncc;
                Session["tenncc"] = ncc != null ? ncc.TenNCC : "";
                Session["idnv"] = idnhanvien;
                Session["tennv"] = tk != null ? tk.HoTen : "";
                Session["ngaynhap"] = Request["ngaynhap"];

                SanPham x = db.SanPham.Find(idsp);
                if (x != null)
                {
                    x.GiaBan = int.TryParse(Request["dongia"], out int dongia) ? dongia : 0;
                    x.SoLuongCon = int.TryParse(Request["soluong"], out int soluong) ? soluong : 0;
                    listsp.Add(x);

                    ChiTietPhieuNhap ctpn = new ChiTietPhieuNhap();
                    if (int.TryParse(Request["mapn"], out int mapn))
                    {
                        /*ViewBag.mapn = mapn;*/
                        ctpn.MaPhieuNhap = mapn;
                        ctpn.MaSP = x.MaSP;
                        ctpn.DonGia = x.GiaBan;
                        ctpn.SoLuong = x.SoLuongCon;
                        listctpn.Add(ctpn);


                        /*ChiTietCungCap chiTietCungCap = new ChiTietCungCap();
                        Random random = new Random();
                        int id = random.Next(1000);
                        chiTietCungCap.id = id;
                        chiTietCungCap.MaNCC = idncc;
                        chiTietCungCap.MaSP = idsp;
                        chiTietCungCap.GiaNhap = x.GiaBan;
                        listctcc.Add(chiTietCungCap);*/
                    }
                }
            }
            return View(listsp);
        }



        public ActionResult DelSPList(int? id)
        {
            var itemToRemove = listsp.FirstOrDefault(item => item.MaSP == id);
            if (itemToRemove != null)
            {
                listsp.Remove(itemToRemove);
            }

            var itemsToRemove = listctpn.FirstOrDefault(item => item.MaSP == id);
            if (itemsToRemove != null)
            {
                listctpn.Remove(itemsToRemove);
            }

            return RedirectToAction("Create1");
        }

        public ActionResult DelSPList2(int? id)
        {
            var itemToRemove = listsp.FirstOrDefault(item => item.MaSP == id);
            if (itemToRemove != null)
            {
                listsp.Remove(itemToRemove);
            }

            var itemsToRemove = listctpn.FirstOrDefault(item => item.MaSP == id);
            if (itemsToRemove != null)
            {
                listctpn.Remove(itemsToRemove);
            }
            return RedirectToAction("Create2");
        }


        public ActionResult Create2()
        {
            var sp = db.SanPham.ToList();
            if (sp != null && sp.Any())
            {
                ViewBag.SanPham = sp;
            }
            else
            {
                ViewBag.SanPham = new List<SanPham>();
            }

            var dvt = db.DonViTinh.ToList();
            if (dvt != null && dvt.Any())
            {
                ViewBag.DonViTinh = dvt;
            }
            else
            {
                ViewBag.DonViTinh = new List<DonViTinh>();
            }

            /*var ncc = Request["MaNCC"];
            var ngaynhap = Request["ngaynhap"];
            var nhanvien = Request["MaTK"];*/
           /* ViewBag.ncc = ncc;
            ViewBag.nv = nhanvien;
            ViewBag.ngaynhap = ngaynhap;*/

            /*int idncc;
            if (int.TryParse(Request["idncc"], out idncc))
            {
                ViewBag.idncc = idncc;
            }

            int idnhanvien;
            if (int.TryParse(Request["idtk"], out idnhanvien))
            {
                ViewBag.idnv = idnhanvien;
            }*/

            int idsp;
            if (int.TryParse(Request["TenSanPham"], out idsp))
            {
                SanPham x = db.SanPham.Find(idsp);
                if (x != null)
                {
                    int giaBan;
                    if (int.TryParse(Request["dongia"], out giaBan))
                    {
                        x.GiaBan = giaBan;
                    }

                    int soLuongCon;
                    if (int.TryParse(Request["soluong"], out soLuongCon))
                    {
                        x.SoLuongCon = soLuongCon;
                    }

                    listsp.Add(x);

                    ChiTietPhieuNhap ctpn = new ChiTietPhieuNhap();
                    int maPhieuNhap;
                    if (int.TryParse(Request["mapn"], out maPhieuNhap))
                    {
                        /*ViewBag.mapn = maPhieuNhap;*/
                        ctpn.MaPhieuNhap = maPhieuNhap;
                        ctpn.MaSP = x.MaSP;
                        ctpn.DonGia = x.GiaBan;
                        ctpn.SoLuong = x.SoLuongCon;
                        listctpn.Add(ctpn);

                        /*ChiTietCungCap chiTietCungCap = new ChiTietCungCap();
                        Random random = new Random();
                        int id = random.Next(1000);
                        chiTietCungCap.id = id;
                        chiTietCungCap.MaNCC = idncc;
                        chiTietCungCap.MaSP = idsp;
                        chiTietCungCap.GiaNhap = x.GiaBan;
                        listctcc.Add(chiTietCungCap);*/
                    }                   
                }
            }

            return View(listsp);

        }
        public ActionResult Add()
        {
            var mapn = Request["mapn"];
            var idncc = Request["idncc"];
            var idtk = Request["idtk"];
            var ngaynhap = Request["ngaynhap"];
            decimal thanhtien = 0;
            foreach (var item in listsp)
            {
                thanhtien = ((decimal)(thanhtien + (item.SoLuongCon * item.GiaBan)));
            }
            PhieuNhap pn = new PhieuNhap();
            pn.MaPhieuNhap = int.Parse(mapn);
            pn.MaNCC = int.Parse(idncc);
            pn.MaTK = int.Parse(idtk);
            pn.NgayLap = DateTime.Parse(ngaynhap);
            pn.ThanhTien = thanhtien;
            db.PhieuNhap.Add(pn);
            foreach (var item in listctpn)
            {
                db.ChiTietPhieuNhap.Add(item);
            }
            foreach(var item in listsp)
            {
                SanPham sanPham = db.SanPham.Find(item.MaSP);
                if (sanPham != null)
                {
                    sanPham.SoLuongCon = sanPham.SoLuongCon + item.SoLuongCon;
                    db.SaveChanges();
                }
            }
            /*foreach(var item in listctcc)
            {
                db.ChiTietCungCap.Add(item);
            }*/
            db.SaveChanges();
            listsp.Clear();
            listctpn.Clear();
            return RedirectToAction("Index", "PhieuNhaps");
        }

        public ActionResult BackToList()
        {
            listsp.Clear();
            listctpn.Clear();
            listctcc.Clear();
            return RedirectToAction("Index", "PhieuNhaps");
        }

        public ActionResult Create()
        {
            Random random = new Random();
            int mapn = random.Next(1000);
            ViewBag.mapn = mapn;
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC");
            ViewBag.MaTK = new SelectList(db.TaiKhoan, "MaTK", "HoTen");
            var sp = db.SanPham.ToList();
            if (sp != null && sp.Any())
            {
                ViewBag.SanPham = sp;
            }
            else
            {
                ViewBag.SanPham = new List<SanPham>();
            }

            var dvt = db.DonViTinh.ToList();
            if (dvt != null && dvt.Any())
            {
                ViewBag.DonViTinh = dvt;
            }
            else
            {
                ViewBag.DonViTinh = new List<DonViTinh>();
            }

            return View();
        }

        // POST: PhieuNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhieuNhap,MaNCC,MaTK,NgayLap,ThanhTien")] PhieuNhap phieuNhap)
        {
            if (ModelState.IsValid)
            {
                db.PhieuNhap.Add(phieuNhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", phieuNhap.MaNCC);
            ViewBag.MaTK = new SelectList(db.TaiKhoan, "MaTK", "HoTen", phieuNhap.MaTK);

            return View(phieuNhap);
        }

        // GET: PhieuNhaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhap phieuNhap = db.PhieuNhap.Find(id);
            if (phieuNhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", phieuNhap.MaNCC);
            ViewBag.MaTK = new SelectList(db.TaiKhoan, "MaTK", "TenTK", phieuNhap.MaTK);
            return View(phieuNhap);
        }

        // POST: PhieuNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhieuNhap,MaNCC,MaTK,NgayLap,ThanhTien")] PhieuNhap phieuNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuNhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", phieuNhap.MaNCC);
            ViewBag.MaTK = new SelectList(db.TaiKhoan, "MaTK", "TenTK", phieuNhap.MaTK);
            return View(phieuNhap);
        }

        // GET: PhieuNhaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuNhap phieuNhap = db.PhieuNhap.Find(id);
            if (phieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(phieuNhap);
        }

        // POST: PhieuNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuNhap phieuNhap = db.PhieuNhap.Find(id);
            db.PhieuNhap.Remove(phieuNhap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
