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
        public PartialViewResult ChiTietHoaDon(int iMaDonHang)
        {
            ChiTietDonHang ctdh = db.ChiTietDonHangs.SingleOrDefault(n => n.MaDonHang == iMaDonHang);
            if (ctdh == null)
            {
                Response.StatusCode = 404;
            } 
            return PartialView("_ChiTietHoaDon", ctdh);
        }
        // xoa san pham
        [HttpGet]
        public ActionResult Xoa(int iMaDonHang)
        {
            ChiTietDonHang ctdh = db.ChiTietDonHangs.FirstOrDefault(n => n.MaDonHang == iMaDonHang);
            DonHang donhang = db.DonHangs.SingleOrDefault(n => n.MaDonHang == iMaDonHang);
            
            if (donhang == null)
            {
                Response.StatusCode = 404; 
            }
            return View(donhang);
        }
        [HttpPost]
        public ActionResult XoaDonHang(int iMaDonHang )
        {
            
            var chitietdonhang = (from Chitiet in  db.ChiTietDonHangs.Where(n => n.MaDonHang == iMaDonHang) select Chitiet).ToList();
             
            if(chitietdonhang == null)
            {
                Response.StatusCode = 404;
            }
            db.ChiTietDonHangs.RemoveRange(chitietdonhang);
            db.SaveChanges();
            DonHang donhang = db.DonHangs.SingleOrDefault(n => n.MaDonHang == iMaDonHang);
            if (donhang == null)
            {
                Response.StatusCode = 404;
            }
          
            db.DonHangs.Remove(donhang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ThemMoi()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(DonHang donhang,KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                db.DonHangs.Add(donhang);
                db.KhachHangs.Add(kh);
                db.SaveChanges();
            }
            return View(donhang);
        }
    }
}