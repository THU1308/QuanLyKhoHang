using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLKHO.Models;

namespace QLKHO.Controllers
{
    public class PhieuXuatsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: PhieuXuats
        public ActionResult Index()
        {
            var phieuXuat = db.PhieuXuat.Include(p => p.KhachHang).Include(p => p.TaiKhoan);
            return View(phieuXuat.ToList());
        }
        public ActionResult ChiTietPhieu(int? id)
        {
            var chiTietPX = db.ChiTietPhieuXuat
                    .Where(s => s.MaPhieuXuat == id)
                    .Include(s => s.PhieuXuat)
                    .Include(s => s.PhieuXuat.KhachHang)
                    .Include(s => s.PhieuXuat.TaiKhoan)
                    .ToList();
            return View(chiTietPX);
        }
        public ActionResult Print(int? id)
        {
            var chiTietPX = db.ChiTietPhieuXuat
                    .Where(s => s.MaPhieuXuat == id)
                    .Include(s => s.PhieuXuat)
                    .Include(s => s.PhieuXuat.KhachHang)
                    .Include(s => s.PhieuXuat.TaiKhoan)
                    .ToList();
            return View(chiTietPX);
        }
        // GET: PhieuXuats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuXuat phieuXuat = db.PhieuXuat.Find(id);
            if (phieuXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuXuat);
        }
        public static List<PhieuXuat> listpx = new List<PhieuXuat>();
        public static List<ChiTietPhieuXuat> listctpx = new List<ChiTietPhieuXuat>();
        public static List<SanPham> listsp = new List<SanPham>();
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
            int idkh, idnhanvien, idsp;
            if (int.TryParse(Request["MaKH"], out idkh) &&
                int.TryParse(Request["MaTK"], out idnhanvien) &&
                int.TryParse(Request["TenSanPham"], out idsp))
            {
                KhachHang kh = db.KhachHang.Find(idkh);
                TaiKhoan tk = db.TaiKhoan.Find(idnhanvien);
                /* ViewBag.idkh = idkh;
                 ViewBag.tenkh = kh != null ? kh.TenKH : "";
                 ViewBag.idnv = idnhanvien;
                 ViewBag.tennv = tk != null ? tk.HoTen : "";
                 ViewBag.ngayxuat = Request["ngayxuat"];*/

                Session["mapx"] = Request["mapx"];
                Session["idkh"] = idkh;
                Session["tenkh"] = kh != null ? kh.TenKH : "";
                Session["idnv"] = idnhanvien;
                Session["tennv"] = tk != null ? tk.HoTen : "";
                Session["ngayxuat"] = Request["ngayxuat"];
                SanPham x = db.SanPham.Find(idsp);
                if (x != null)
                {
                    x.GiaBan = int.TryParse(Request["dongia"], out int dongia) ? dongia : 0;                 
                    if(x.SoLuongCon> int.Parse(Request["soluong"]))
                    {
                        x.SoLuongCon = int.TryParse(Request["soluong"], out int soluong) ? soluong : 0;
                        listsp.Add(x);
                        Session["listsp"] = listsp;
                    }
                    else
                    {
                        ViewBag.AlertMessage = "Số lượng trong kho thiếu!";
                        return View(listsp);
                    }
                    
                    ChiTietPhieuXuat ctpx = new ChiTietPhieuXuat();
                    if (int.TryParse(Request["mapx"], out int mapx))
                    {
                        
                        ctpx.MaPhieuXuat = mapx;
                        ctpx.MaSP = x.MaSP;
                        ctpx.DonGia = x.GiaBan;
                        ctpx.SoLuong = x.SoLuongCon;
                        listctpx.Add(ctpx);
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

            var itemsToRemove = listctpx.FirstOrDefault(item => item.MaSP == id);
            if (itemsToRemove != null)
            {
                listctpx.Remove(itemsToRemove);
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

            var itemsToRemove = listctpx.FirstOrDefault(item => item.MaSP == id);
            if (itemsToRemove != null)
            {
                listctpx.Remove(itemsToRemove);
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
            /*ViewBag.mapx = Request["mapx"];
            var kh = Request["MaKH"];
            var ngayxuat = Request["ngayxuat"];
            var nhanvien = Request["MaTK"];
            ViewBag.tenkh = kh;
            ViewBag.tennv = nhanvien;
            ViewBag.ngayxuat = ngayxuat;*/

            /*int idkh;
            if (int.TryParse(Request["idkh"], out idkh))
            {
                ViewBag.idkh = idkh;
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

                    int soluong;
                    if (int.TryParse(Request["soluong"], out soluong))
                    {
                        if (x.SoLuongCon > soluong)
                        {
                            x.SoLuongCon = soluong;
                            listsp.Add(x);
                        }
                        else
                        {
                            ViewBag.AlertMessage2 = "Số lượng trong kho thiếu!";
                            return View(listsp);
                        }
                    }
                    ChiTietPhieuXuat ctpx = new ChiTietPhieuXuat();
                    int maPhieuXuat;
                    if (int.TryParse(Request["mapx"], out maPhieuXuat))
                    {
                        ctpx.MaPhieuXuat = maPhieuXuat;
                        ctpx.MaSP = x.MaSP;
                        ctpx.DonGia = x.GiaBan;
                        ctpx.SoLuong = x.SoLuongCon;
                        listctpx.Add(ctpx);
                    }
                }
            }
            return View(listsp);
        }
            

        public ActionResult Add()
        {
            var mapn = Request["mapx"];
            var idkh = Request["idkh"];
            var idtk = Request["idtk"];
            var ngayxuat = Request["ngayxuat"];
            decimal thanhtien = 0;
            foreach (var item in listsp)
            {
                thanhtien = ((decimal)(thanhtien + (item.SoLuongCon * item.GiaBan)));
            }
            PhieuXuat px = new PhieuXuat();
            px.MaPhieuXuat = int.Parse(mapn);
            px.MaKH = int.Parse(idkh);
            px.MaTK = int.Parse(idtk);
            px.NgayLap = DateTime.Parse(ngayxuat);
            px.ThanhTien = thanhtien;
            db.PhieuXuat.Add(px);
            foreach (var item in listctpx)
            {
                db.ChiTietPhieuXuat.Add(item);
            }
            foreach (var item in listsp)
            {
                SanPham sanPham = db.SanPham.Find(item.MaSP);
                if (sanPham != null)
                {
                    sanPham.SoLuongCon = sanPham.SoLuongCon - item.SoLuongCon;
                    db.SaveChanges();
                }
            }
         
            db.SaveChanges();
            listsp.Clear();
            listctpx.Clear();
            return RedirectToAction("Index", "PhieuXuats");
        }
        public ActionResult BackToList()
        {
          
            return RedirectToAction("Index", "PhieuXuats");
        }
        // GET: PhieuXuats/Create
        public ActionResult Create()
        {
            Random random = new Random();
            int mapx = random.Next(1000);
            ViewBag.mapx = mapx;
            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "TenKH");
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

        // POST: PhieuXuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhieuXuat,MaKH,MaTK,NgayLap,ThanhTien")] PhieuXuat phieuXuat)
        {
            if (ModelState.IsValid)
            {
                db.PhieuXuat.Add(phieuXuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "TenKH", phieuXuat.MaKH);
            ViewBag.MaTK = new SelectList(db.TaiKhoan, "MaTK", "TenTK", phieuXuat.MaTK);
            return View(phieuXuat);
        }

        // GET: PhieuXuats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuXuat phieuXuat = db.PhieuXuat.Find(id);
            if (phieuXuat == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "TenKH", phieuXuat.MaKH);
            ViewBag.MaTK = new SelectList(db.TaiKhoan, "MaTK", "TenTK", phieuXuat.MaTK);
            return View(phieuXuat);
        }

        // POST: PhieuXuats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhieuXuat,MaKH,MaTK,NgayLap,ThanhTien")] PhieuXuat phieuXuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuXuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHang, "MaKH", "TenKH", phieuXuat.MaKH);
            ViewBag.MaTK = new SelectList(db.TaiKhoan, "MaTK", "TenTK", phieuXuat.MaTK);
            return View(phieuXuat);
        }

        // GET: PhieuXuats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuXuat phieuXuat = db.PhieuXuat.Find(id);
            if (phieuXuat == null)
            {
                return HttpNotFound();
            }
            return View(phieuXuat);
        }

        // POST: PhieuXuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuXuat phieuXuat = db.PhieuXuat.Find(id);
            db.PhieuXuat.Remove(phieuXuat);
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
