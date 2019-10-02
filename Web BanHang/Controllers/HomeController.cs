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
    public class HomeController : Controller
    {
        // GET: Home
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index(int ? page)
        {
            //tao bien so san pham tren trang
            int pageSize = 9;
            int pageNumber = page ?? 1;
            IPagedList indexpt = db.Saches.OrderBy(n => n.NgayCapNhap).ToPagedList(pageNumber, pageSize);

            return View(indexpt);
        }
    }
}