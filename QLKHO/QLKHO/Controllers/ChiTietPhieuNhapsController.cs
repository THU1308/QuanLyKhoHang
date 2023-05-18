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
    public class ChiTietPhieuNhapsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: ChiTietPhieuNhaps
        public ActionResult Index()
        {
            var chiTietPhieuNhap = db.ChiTietPhieuNhap.Include(c => c.PhieuNhap).Include(c => c.SanPham);
            return View(chiTietPhieuNhap.ToList());
        }

        // GET: ChiTietPhieuNhaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuNhap chiTietPhieuNhap = db.ChiTietPhieuNhap.Find(id);
            if (chiTietPhieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuNhap);
        }

        // GET: ChiTietPhieuNhaps/Create
        public ActionResult Create()
        {
            ViewBag.MaPhieuNhap = new SelectList(db.PhieuNhap, "MaPhieuNhap", "MaPhieuNhap");
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP");
            return View();
        }

        // POST: ChiTietPhieuNhaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhieuNhap,MaSP,DonGia,SoLuong")] ChiTietPhieuNhap chiTietPhieuNhap)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietPhieuNhap.Add(chiTietPhieuNhap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPhieuNhap = new SelectList(db.PhieuNhap, "MaPhieuNhap", "MaPhieuNhap", chiTietPhieuNhap.MaPhieuNhap);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chiTietPhieuNhap.MaSP);
            return View(chiTietPhieuNhap);
        }

        // GET: ChiTietPhieuNhaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuNhap chiTietPhieuNhap = db.ChiTietPhieuNhap.Find(id);
            if (chiTietPhieuNhap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPhieuNhap = new SelectList(db.PhieuNhap, "MaPhieuNhap", "MaPhieuNhap", chiTietPhieuNhap.MaPhieuNhap);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chiTietPhieuNhap.MaSP);
            return View(chiTietPhieuNhap);
        }

        // POST: ChiTietPhieuNhaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhieuNhap,MaSP,DonGia,SoLuong")] ChiTietPhieuNhap chiTietPhieuNhap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietPhieuNhap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPhieuNhap = new SelectList(db.PhieuNhap, "MaPhieuNhap", "MaPhieuNhap", chiTietPhieuNhap.MaPhieuNhap);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chiTietPhieuNhap.MaSP);
            return View(chiTietPhieuNhap);
        }

        // GET: ChiTietPhieuNhaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuNhap chiTietPhieuNhap = db.ChiTietPhieuNhap.Find(id);
            if (chiTietPhieuNhap == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuNhap);
        }

        // POST: ChiTietPhieuNhaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietPhieuNhap chiTietPhieuNhap = db.ChiTietPhieuNhap.Find(id);
            db.ChiTietPhieuNhap.Remove(chiTietPhieuNhap);
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
