using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Models;

namespace Web_BanHang.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
       
        public PartialViewResult SachMoiPartial()
        {
            var lstsachmoi = db.Saches.Take(4).ToList();
            return PartialView(lstsachmoi);
        }
        // xem chi tiet
        public ViewResult XemChiTiet(int MaSach=0)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.TenChuDe = db.ChuDes.Single(n => n.MaChuDe == sach.MaChuDe).TenChuDe;
            ViewBag.TenNXB = db.NhaXuatBans.Single(n => n.MaNXB == sach.MaNXB).TenNXB;
            return View(sach);
        }
    }
}