using OfficeOpenXml;
using QLKHO.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLKHO.Controllers
{
    public class ThongKeController : Controller
    {
        private MyDB db = new MyDB();
        // GET: ThongKe
        public ActionResult Index()
        {
            int currentYear = DateTime.Now.Year;
            var phieuXuatList = db.PhieuXuat.Where(p => p.NgayLap.Value.Year == currentYear).ToList();
            decimal[] doanhThuThang = new decimal[12];

            foreach (var phieuXuat in phieuXuatList)
            {
                int thang = phieuXuat.NgayLap.Value.Month;
                doanhThuThang[thang - 1] += phieuXuat.ThanhTien ?? 0;

            }

            ViewBag.DoanhThuThang = doanhThuThang.ToList();

            return View(phieuXuatList);
        }


        // GET: ThongKe/ExportExcel
        public ActionResult ExportExcel()
        {
            int currentYear = DateTime.Now.Year;
            var phieuXuatList = db.PhieuXuat.Where(p => p.NgayLap.Value.Year == currentYear).ToList();
            decimal[] doanhThuThang = new decimal[12];

            foreach (var phieuXuat in phieuXuatList)
            {
                int thang = phieuXuat.NgayLap.Value.Month;
                doanhThuThang[thang - 1] += phieuXuat.ThanhTien ?? 0;
            }

            ViewBag.DoanhThuThang = doanhThuThang.ToList();

            // Xuất file Excel
            byte[] excelBytes = ExportExcelFile(doanhThuThang);

            // Trả về file Excel để tải về
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "DoanhThu.xlsx");
        }

        // Hàm xuất file Excel
        private byte[] ExportExcelFile(decimal[] doanhThuThang)
        {
            // Tạo một package Excel mới
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                // Tạo một worksheet mới trong file Excel
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("DoanhThu");

                // Thiết lập tiêu đề cho các cột
                worksheet.Cells[1, 1].Value = "Tháng";
                worksheet.Cells[1, 2].Value = "Doanh thu";

                // Đổ dữ liệu vào worksheet
                for (int i = 0; i < doanhThuThang.Length; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = i + 1;
                    worksheet.Cells[i + 2, 2].Value = doanhThuThang[i];
                }

                // Thiết lập định dạng cho các cột
                using (var range = worksheet.Cells[1, 1, 1, 2])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                // Thiết lập định dạng cho ô số tiền
                using (var range = worksheet.Cells[2, 2, doanhThuThang.Length + 1, 2])
                {
                    range.Style.Numberformat.Format = "#,##0.00";
                }

                // Lưu file Excel vào MemoryStream
                MemoryStream stream = new MemoryStream();
                excelPackage.SaveAs(stream);

                // Trả về mảng byte của file Excel
                return stream.ToArray();
            }
        }


    }
}