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
    public class ChiTietCungCapsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: ChiTietCungCaps
        public ActionResult Index()
        {
            var chiTietCungCap = db.ChiTietCungCap.Include(c => c.NhaCungCap).Include(c => c.SanPham);
            return View(chiTietCungCap.ToList());
        }

        // GET: ChiTietCungCaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietCungCap chiTietCungCap = db.ChiTietCungCap.Find(id);
            if (chiTietCungCap == null)
            {
                return HttpNotFound();
            }
            return View(chiTietCungCap);
        }

        // GET: ChiTietCungCaps/Create
        public ActionResult Create()
        {
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC");
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP");
            return View();
        }

        // POST: ChiTietCungCaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNCC,MaSP,GiaNhap")] ChiTietCungCap chiTietCungCap)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietCungCap.Add(chiTietCungCap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", chiTietCungCap.MaNCC);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chiTietCungCap.MaSP);
            return View(chiTietCungCap);
        }

        // GET: ChiTietCungCaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietCungCap chiTietCungCap = db.ChiTietCungCap.Find(id);
            if (chiTietCungCap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", chiTietCungCap.MaNCC);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chiTietCungCap.MaSP);
            return View(chiTietCungCap);
        }

        // POST: ChiTietCungCaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNCC,MaSP,GiaNhap")] ChiTietCungCap chiTietCungCap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietCungCap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNCC = new SelectList(db.NhaCungCap, "MaNCC", "TenNCC", chiTietCungCap.MaNCC);
            ViewBag.MaSP = new SelectList(db.SanPham, "MaSP", "TenSP", chiTietCungCap.MaSP);
            return View(chiTietCungCap);
        }

        // GET: ChiTietCungCaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietCungCap chiTietCungCap = db.ChiTietCungCap.Find(id);
            if (chiTietCungCap == null)
            {
                return HttpNotFound();
            }
            return View(chiTietCungCap);
        }

        // POST: ChiTietCungCaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietCungCap chiTietCungCap = db.ChiTietCungCap.Find(id);
            db.ChiTietCungCap.Remove(chiTietCungCap);
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
