using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Models;
namespace Web_BanHang.Controllers
{
    public class TimKiemAdminController : Controller
    {
        private readonly QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: TimKiemAdmin
       
        public ActionResult _searchBook(FormCollection form)
        {
            String searchString = form["Search"]  ;
            var searchQuery = from book in db.Saches select book;
            if (!String.IsNullOrEmpty(searchString))
            {
                searchQuery = searchQuery.Where(s => s.TenSach.Contains(searchString));
            }
            return PartialView(searchQuery);
        }
        public ActionResult searchValueBook (FormCollection form)
        {
           String searchString = form["searchString"] ;
            var searchQuery = from book in db.Saches select book;
            if (!String.IsNullOrEmpty(searchString))
            {
                searchQuery = searchQuery.Where(s => s.TenSach.Contains(searchString));
            }
            return View(searchQuery);        
        }
        // json searching
        public JsonResult searchBook(string searchBookValue)
        {
          var   bookList = db.Saches.Where(x => x.TenSach.StartsWith(searchBookValue) || searchBookValue == null).ToList();
            return Json(bookList, JsonRequestBehavior.AllowGet);
        }
    }
}