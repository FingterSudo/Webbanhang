using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Models;
using PagedList;
using PagedList.Mvc;


namespace Web_BanHang.Controllers
{
    public class QuanLyNguoiDungController : Controller
    {
        // GET: QuanLyNguoiDung
        private readonly QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int? Page)
        {
            int pageSize = 5;
            int pageNumber = Page ?? 1;
            IPagedList taikhoan = db.TaiKhoans.OrderBy(n => n.HoTen).ToPagedList(pageNumber, pageSize);
            return View(taikhoan);
        }
        public ActionResult DangKy()
        {
            return View();
        } 
        [HttpPost]
        public ActionResult DangKy(TaiKhoan tk)
        {
            if (ModelState.IsValid)
            {
                // chen du lieu vao tai khoan
                db.TaiKhoans.Add(tk);
                // lưu dữ liệu khách hàng
                db.SaveChanges();
            }
            return RedirectToAction("DangKyThanhCong");
        }
        public ActionResult DangKyThanhCong()
        {
            return View();
        }
    }
}