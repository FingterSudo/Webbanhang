using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Models;

namespace Web_BanHang.Controllers
{
    public class NhaXuatBanController : Controller
    {
        // GET: NhaXuatBan
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public PartialViewResult NhaXuatBanPartial()
        {
            return PartialView(db.NhaXuatBans.Take(5).OrderBy(x => x.TenNXB).ToList());
        }
        public ViewResult SachTheoNXB(int MaNXB=0)
        {
            List<Sach> lstSach = db.Saches.Where(n => n.MaNXB == MaNXB).OrderBy(n => n.GiaBan).ToList();
            return View(lstSach);
        }
    }
}

