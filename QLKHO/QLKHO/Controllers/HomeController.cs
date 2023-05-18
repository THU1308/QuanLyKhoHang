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
            string account = Request["account"];
            string password = Request["password"];
            TaiKhoan tk = db.TaiKhoan.FirstOrDefault(t => t.TenTK == account && t.MatKhau == password);
            if (tk != null)
            {
                // Đặt giá trị biến toàn cục
                Session["hoten"] = tk.HoTen;

                // Lấy giá trị biến toàn cục
                string myVariable = (string)Session["hoten"];

                return RedirectToAction("Welcome", "Home");
            }
            return View();


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