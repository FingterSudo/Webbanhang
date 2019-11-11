using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Controllers;
using Web_BanHang.Models;
using Web_BanHang.DTO;


namespace Web_BanHang.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetBookValue(string search)
        {
            QuanLyBanSachEntities db = new QuanLyBanSachEntities();
            List<SachDTO> allbook = db.Saches.Where(n => n.TenSach.Contains(search)).Select(n => new SachDTO
            {
                TenSach = n.TenSach
            }).ToList();
            return new JsonResult { Data = allbook, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}