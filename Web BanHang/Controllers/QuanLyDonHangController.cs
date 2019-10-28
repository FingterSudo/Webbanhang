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
    public class QuanLyDonHangController : Controller
    {
        private readonly QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: QuanLyDonHang
        public ActionResult Index(int?Page)
        {
            int pageSize = 10;
            int pageNumber = Page ?? 1;
            IPagedList donhang = db.DonHangs.OrderBy(n => n.NgayDat).ToPagedList(pageNumber, pageSize);
            return View(donhang);
        }
        // xem chi tiet san pham
        public ActionResult ChiTiet(int iMaDonHang)
        {
            DonHang donhang = db.DonHangs.SingleOrDefault(n=>n.MaDonHang== iMaDonHang);
            if (donhang == null)
            {
                Response.StatusCode = 404;
            }
            return View(donhang);
        }
        // xoa san pham
        [HttpGet]
        public ActionResult Xoa(int iMaDonHang)
        {
            DonHang donhang = db.DonHangs.SingleOrDefault(n => n.MaDonHang == iMaDonHang);
            if (donhang == null)
            {
                Response.StatusCode = 404; 
            }
            return View(donhang);
        }
        [HttpPost]
        public ActionResult XoaSanPham(int iMaDonHang)
        {
            DonHang donhang = db.DonHangs.SingleOrDefault(n => n.MaDonHang == iMaDonHang);
            if (donhang == null)
            {
                Response.StatusCode = 404;
            }
            db.DonHangs.Remove(donhang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}