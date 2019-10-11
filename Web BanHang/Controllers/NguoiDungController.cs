using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Models;
namespace Web_BanHang.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DangKy(KhachHang kh)
        { 
            if (ModelState.IsValid)
            {
                // chèn dữ liệu vào khách hàng
                db.KhachHangs.Add(kh);
                // luu dữ liệu khách hàng
                db.SaveChanges();
            }     
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f.Get("txtMatKhau").ToString();
            KhachHang kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if (kh!=null)
            {
                ViewBag.ThongBao = "Chúc mừng bạn đăng nhập thành công";
                Session["TaiKhoan"] = kh;
                return RedirectToAction("Index","Home");
            }
            ViewBag.ThongBao = "Tài khoản bạn đăng nhập không đúng ";
            return View();
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            TempData["messenger"] = "Đã đăng xuất";
            return RedirectToAction("Index","Home");
        }
    }
}