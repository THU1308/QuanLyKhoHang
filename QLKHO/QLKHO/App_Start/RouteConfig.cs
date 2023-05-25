using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QLKHO
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Login", id = UrlParameter.Optional }
            );

            // Thêm đường dẫn cho action "ExportExcel"
            routes.MapRoute(
                name: "ExportExcel",
                url: "ThongKe/ExportExcel",
                defaults: new { controller = "ThongKe", action = "ExportExcel" }
            );

            // Logout
            routes.MapRoute(
                name: "Logout",
                url: "Home/Logout",
                defaults: new { controller = "Home", action = "Logout" }
            );

            routes.MapRoute(
            name: "DelSPList",
            url: "PhieuNhaps/DelSPList/{id}",
            defaults: new { controller = "PhieuNhaps", action = "DelSPListP", id = UrlParameter.Optional }
        );

        }
    }
}
