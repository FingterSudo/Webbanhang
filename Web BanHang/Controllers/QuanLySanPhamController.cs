using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;
namespace Web_BanHang.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        private readonly QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: QuanLySanPham
        public ActionResult Index(int? page)
        {
            int pageSize =9;
            int pageNumber = page ?? 1;
            IPagedList sach = db.Saches.OrderBy(n => n.NgayCapNhap).ToPagedList(pageNumber, pageSize);
            return View(sach);
        }
        // thêm mới sản phẩm
        [HttpGet]
        public ActionResult ThemMoi()
        {
            // đưa dữ liệu vào dropdownlist
            ViewBag.MaChuDe = new SelectList(db.ChuDes, "MaChuDe", "TenChuDe");
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(Sach sach,HttpPostedFileBase fileupload)
        {
            // lưu tên file
            var fileName = Path.GetFileName(fileupload.FileName);
            // lưu đường dẫn của file
            var path = Path.Combine(Server.MapPath("~/HinhAnh"), fileName);
            // kiem tra hinh anh da ton tai
            if (System.IO.File.Exists(path))
            {
                ViewBag.ThongBao = "Hình ảnh đã tồn tại";
            }
            else
            {
                fileupload.SaveAs(path);
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes, "MaChuDe", "TenChuDe");
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");
            return View();
        }
    }
}