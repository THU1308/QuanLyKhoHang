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
    public class ChiTietPhieuXuatsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: ChiTietPhieuXuats
        public ActionResult Index()
        {
            var chiTietPhieuXuat = db.ChiTietPhieuXuat.Include(c => c.PhieuXuat).Include(c => c.SanPham);
            return View(chiTietPhieuXuat.ToList());
        }

        // GET: ChiTietPhieuXuats/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuXuat chiTietPhieuXuat = db.ChiTietPhieuXuat.Find(id);
            if (chiTietPhieuXuat == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuXuat);
        }

        // GET: ChiTietPhieuXuats/Create
        public ActionResult Create()
        {
            ViewBag.MaPhieuXuat = new SelectList(db.PhieuXuat, "MaPhieuXuat", "MaPhieuXuat");
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP");
            return View();
        }

        // POST: ChiTietPhieuXuats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaPhieuXuat,DonGia,SoLuong")] ChiTietPhieuXuat chiTietPhieuXuat)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietPhieuXuat.Add(chiTietPhieuXuat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPhieuXuat = new SelectList(db.PhieuXuat, "MaPhieuXuat", "MaPhieuXuat", chiTietPhieuXuat.MaPhieuXuat);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chiTietPhieuXuat.MaSP);
            return View(chiTietPhieuXuat);
        }

        // GET: ChiTietPhieuXuats/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuXuat chiTietPhieuXuat = db.ChiTietPhieuXuat.Find(id);
            if (chiTietPhieuXuat == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPhieuXuat = new SelectList(db.PhieuXuat, "MaPhieuXuat", "MaPhieuXuat", chiTietPhieuXuat.MaPhieuXuat);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chiTietPhieuXuat.MaSP);
            return View(chiTietPhieuXuat);
        }

        // POST: ChiTietPhieuXuats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaPhieuXuat,DonGia,SoLuong")] ChiTietPhieuXuat chiTietPhieuXuat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietPhieuXuat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPhieuXuat = new SelectList(db.PhieuXuat, "MaPhieuXuat", "MaPhieuXuat", chiTietPhieuXuat.MaPhieuXuat);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chiTietPhieuXuat.MaSP);
            return View(chiTietPhieuXuat);
        }

        // GET: ChiTietPhieuXuats/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietPhieuXuat chiTietPhieuXuat = db.ChiTietPhieuXuat.Find(id);
            if (chiTietPhieuXuat == null)
            {
                return HttpNotFound();
            }
            return View(chiTietPhieuXuat);
        }

        // POST: ChiTietPhieuXuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietPhieuXuat chiTietPhieuXuat = db.ChiTietPhieuXuat.Find(id);
            db.ChiTietPhieuXuat.Remove(chiTietPhieuXuat);
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
