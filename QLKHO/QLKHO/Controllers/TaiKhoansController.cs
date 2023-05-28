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
    public class TaiKhoansController : Controller
    {
        private MyDB db = new MyDB();

        // GET: TaiKhoans
        public ActionResult Index()
        {
            var taiKhoan = db.TaiKhoan.Include(t => t.PhanQuyen).Where(t=>t.MaPQ==2);        
            return View(taiKhoan.ToList());
        }
        public ActionResult CheckRoles()
        {
            if (Convert.ToInt32(Session["quyen"]) == 1)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult CheckRoles1()
        {
            if (Convert.ToInt32(Session["quyen"]) == 1)
            {
                return RedirectToAction("Create");
            }
            return RedirectToAction("CheckRoles");
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        public ActionResult ChangePassword1()
        {
            var currentPassword = Request["currentPassword"];
            var newPassword = Request["newPassword"];
            var confirmPassword = Request["confirmPassword"];

            // Kiểm tra các trường dữ liệu
            if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmPassword))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập đầy đủ thông tin";
                return View();
            }
            else if (newPassword != confirmPassword)
            {
                ViewBag.ErrorMessage = "Mật khẩu mới và xác nhận mật khẩu không khớp.";
                return View();
            }

            // Lấy thông tin tài khoản hiện tại
            string username = (string)Session["tentk"];
            TaiKhoan taiKhoan = db.TaiKhoan.FirstOrDefault(t => t.TenTK == username);

            // Kiểm tra mật khẩu hiện tại
            if (!BCrypt.Net.BCrypt.Verify(currentPassword, taiKhoan.MatKhau))
            {
                ViewBag.ErrorMessage = "Mật khẩu hiện tại không đúng.";
                return View();
            }

            // Mật khẩu hợp lệ, mã hóa mật khẩu mới
            taiKhoan.MatKhau = BCrypt.Net.BCrypt.HashPassword(newPassword);
            db.SaveChanges();

            // Đổi mật khẩu thành công, chuyển hướng về trang chủ hoặc hiển thị thông báo thành công
            ViewBag.SuccessMessage = "Đổi mật khẩu thành công.";
            return View();
        }

        // GET: TaiKhoans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoan.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Create
        public ActionResult Create()
        {
            ViewBag.MaPQ = new SelectList(db.PhanQuyen, "MaPQ", "TenPQ");
            return View();
        }

        // POST: TaiKhoans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTK,TenTK,MaPQ,MatKhau,SoDienThoai,Email,HoTen,NgaySinh,Anh")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                // Mã hóa mật khẩu trước khi lưu vào cơ sở dữ liệu
                taiKhoan.MatKhau = BCrypt.Net.BCrypt.HashPassword(taiKhoan.MatKhau);
              
                db.TaiKhoan.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaPQ = new SelectList(db.PhanQuyen, "MaPQ", "TenPQ", taiKhoan.MaPQ);
            return View(taiKhoan);
        }


        // GET: TaiKhoans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoan.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaPQ = new SelectList(db.PhanQuyen, "MaPQ", "TenPQ", taiKhoan.MaPQ);
            return View(taiKhoan);
        }

        // POST: TaiKhoans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTK,TenTK,MaPQ,MatKhau,SoDienThoai,Email,HoTen,NgaySinh,Anh")] TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaPQ = new SelectList(db.PhanQuyen, "MaPQ", "TenPQ", taiKhoan.MaPQ);
            return View(taiKhoan);
        }

        // GET: TaiKhoans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaiKhoan taiKhoan = db.TaiKhoan.Find(id);
            if (taiKhoan == null)
            {
                return HttpNotFound();
            }
            return View(taiKhoan);
        }

        // POST: TaiKhoans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaiKhoan taiKhoan = db.TaiKhoan.Find(id);
            db.TaiKhoan.Remove(taiKhoan);
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
