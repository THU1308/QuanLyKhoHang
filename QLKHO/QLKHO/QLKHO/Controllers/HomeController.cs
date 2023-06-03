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
            return View();
        }
        public ActionResult LoginPost()
        {
            var account = Request["account"];
            var password = Request["password"];

            // Kiểm tra tính hợp lệ của dữ liệu đăng nhập
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.";
                return View("Login");
            }

            // Tìm kiếm tài khoản với username tương ứng
            TaiKhoan taiKhoan = db.TaiKhoan.FirstOrDefault(t => t.TenTK == account);

            // Kiểm tra mật khẩu
            if (taiKhoan != null && BCrypt.Net.BCrypt.Verify(password, taiKhoan.MatKhau))
            {
                // Mật khẩu đúng, tiến hành đăng nhập
                // Lưu thông tin đăng nhập vào Session hoặc cookie, redirect đến trang chính, ...
                Session["hoten"] = taiKhoan.HoTen;
                Session["quyen"] = taiKhoan.MaPQ;
                Session["tentk"] = taiKhoan.TenTK;
                Session["matk"] = taiKhoan.MaTK;
                return RedirectToAction("Welcome", "Home");
            }
            else
            {
                // Sai tên đăng nhập hoặc mật khẩu
                // Xử lý thông báo lỗi, đưa ra thông báo cho người dùng, ...
                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View("Login");
            }
        }




        public ActionResult Logout()
        {

            Session.Clear();
            Session.Abandon(); // Hủy phiên làm việc hiện tại
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