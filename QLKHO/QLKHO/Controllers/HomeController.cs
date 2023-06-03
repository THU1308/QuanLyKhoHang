using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLKHO.Models;
namespace QLKHO.Controllers
{
    public class HomeController : Controller
    {
        private MyDB db = new MyDB();
        public ActionResult Login()
        {
            // Tìm kiếm tài khoản với username tương ứng
            string account = Request["account"];
            string password = Request["password"];
            TaiKhoan taiKhoan = db.TaiKhoan.FirstOrDefault(t => t.TenTK == account);

            // Kiểm tra mật khẩu
            if (taiKhoan != null && BCrypt.Net.BCrypt.Verify(password, taiKhoan.MatKhau))
            {
                // Mật khẩu đúng, tiến hành đăng nhập
                // Lưu thông tin đăng nhập vào Session hoặc cookie, redirect đến trang chính, ...
                Session["hoten"] = taiKhoan.HoTen;
                Session["quyen"] = taiKhoan.MaPQ;
                Session["tentk"] = taiKhoan.TenTK;
                return RedirectToAction("Welcome", "Home");
            }
            else
            {
                // Sai tên đăng nhập hoặc mật khẩu
                // Xử lý thông báo lỗi, đưa ra thông báo cho người dùng, ...

                return View();
            }
        }

        public ActionResult Logout()
        {
            // Xóa dữ liệu phiên làm việc của người dùng
            Session.Clear(); // Xóa toàn bộ dữ liệu trong phiên làm việc
            Session.Abandon(); // Hủy phiên làm việc hiện tại

            // Điều hướng về trang đăng nhập
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Welcome()
        {
            
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}