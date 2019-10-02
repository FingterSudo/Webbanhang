using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Models;

namespace Web_BanHang.Controllers
{
    public class ChuDeController : Controller
    {
        // GET: ChuDe
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();

        public ActionResult ChuDePartial()
        {
            return View(db.ChuDes.ToList());
        }
        // xem sách theo chủ đề
        public ViewResult SachTheoChuDe(int MaChuDe=0)
        {
            // kiem tra chu de co ton tai hay khong
            ChuDe cb = db.ChuDes.SingleOrDefault(n => n.MaChuDe == MaChuDe);
            //if (cb==null)
            //{
            //    Response.StatusCode = 404;
            //    return null;
            //}
            
            // truy xuat cac quyen sach danh sach theo chủ đề
            List<Sach> lstsach = db.Saches.Where(n => n.MaChuDe == MaChuDe).OrderBy(n => n.GiaBan).ToList();
            //if (lstsach.Count==0)
            //{
            //    ViewBag.Sach = "Không có sách này thuộc chủ đề này";
            //}
            return View(lstsach);
        }
    }
}