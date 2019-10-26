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
    }
}