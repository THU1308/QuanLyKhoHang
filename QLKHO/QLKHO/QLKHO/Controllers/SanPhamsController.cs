<<<<<<< HEAD:QLKHO/QLKHO/QLKHO/Controllers/SanPhamsController.cs
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLKHO.Models;

namespace QuanLyKhoHang.Controllers
{
    public class SanPhamsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPham = db.SanPham.Include(s => s.DonViTinh);
            return View(sanPham.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaDVT = new SelectList(db.DonViTinh, "MaDVT", "TenDVT");

            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaDVT,TenSP,GiaBan,SoLuongCon,Anh")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["anh"];
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = Server.MapPath("~/Images/" + fileName);
                    string path = "/Images/" + fileName;
                    file.SaveAs(filePath);
                    sanPham.Anh = path;
                }
                db.SanPham.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaDVT = new SelectList(db.DonViTinh, "MaDVT", "TenDVT", sanPham.MaDVT);
            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaDVT,TenSP,GiaBan,SoLuongCon,Anh")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                var x = Request.Files["anh"];
                string linkCu = Request["anh2"];

                if (x != null && x.ContentLength > 0)
                {
                    string filePath = Server.MapPath("~/Images/" + x.FileName);
                    string path = "/Images/" + x.FileName;
                    x.SaveAs(filePath);
                    sanPham.Anh = path;
                }
                else
                {

                }

                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDVT = new SelectList(db.DonViTinh, "MaDVT", "TenDVT", sanPham.MaDVT);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPham.Find(id);
            db.SanPham.Remove(sanPham);
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
=======
﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLKHO.Models;

namespace QuanLyKhoHang.Controllers
{
    public class SanPhamsController : Controller
    {
        private MyDB db = new MyDB();

        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPham = db.SanPham.Include(s => s.DonViTinh);
            return View(sanPham.ToList());
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaDVT = new SelectList(db.DonViTinh, "MaDVT", "TenDVT");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSP,MaDVT,TenSP,GiaBan,SoLuongCon,Anh")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                var file = Request.Files["anh"];
                if (file != null && file.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(file.FileName);
                    string filePath = Server.MapPath("~/Images/" + fileName);
                    string path = "/Images/" + fileName;
                    file.SaveAs(filePath);
                    sanPham.Anh = path;
                }
                db.SanPham.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }

            ViewBag.MaDVT = new SelectList(db.DonViTinh, "MaDVT", "TenDVT", sanPham.MaDVT);
            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSP,MaDVT,TenSP,GiaBan,SoLuongCon,Anh")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                var x = Request.Files["anh"];
                string linkCu = Request["anh2"];

                if (x != null && x.ContentLength > 0)
                {
                    string filePath = Server.MapPath("~/Images/" + x.FileName);
                    string path = "/Images/" + x.FileName;
                    x.SaveAs(filePath);
                    sanPham.Anh = path;
                }
                else
                {

                }

                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDVT = new SelectList(db.DonViTinh, "MaDVT", "TenDVT", sanPham.MaDVT);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPham.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPham.Find(id);
            db.SanPham.Remove(sanPham);
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
>>>>>>> e63bec154b0ad6905191a88f87d4449a3f379879:QLKHO/QLKHO/Controllers/SanPhamsController.cs
