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
            int pageSize = 9;
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
        public ActionResult ThemMoi(Sach sach, HttpPostedFileBase fileupload)
        {
            ViewBag.MaChuDe = new SelectList(db.ChuDes, "MaChuDe", "TenChuDe");
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");
            if (fileupload == null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
            }
            if (ModelState.IsValid)
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
                sach.AnhBia = fileupload.FileName;
                db.Saches.Add(sach);
                db.SaveChanges();
            }
            return View();
        }

        [HttpGet]
        public ActionResult ChinhSua(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaChuDe = new SelectList(db.ChuDes, "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia", sach.MaTacGia);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB", sach.MaNXB);
            return View(sach);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(Sach sach,  FormCollection f, HttpPostedFileBase fileupload)
        {
           
            
            if (fileupload!=null)
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
                sach.AnhBia = fileupload.FileName;
                db.Saches.Add(sach);
                db.SaveChanges();
            }   
            ViewBag.MaChuDe = new SelectList(db.ChuDes, "MaChuDe", "TenChuDe", sach.MaChuDe);
            ViewBag.MaTacGia = new SelectList(db.TacGias, "MaTacGia", "TenTacGia", sach.MaTacGia);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB", sach.MaNXB);
            return RedirectToAction("Index");
        }
        // xóa sản phẩm
        [HttpGet]
        public ActionResult Xoa(int iMaSach)
        {
            Sach sach = db.Saches.Single(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        [HttpPost]
        //[ActionName("Xoa")]
        public ActionResult XoaSach(int MaSach)
        {
            Sach sach = db.Saches.Single(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ChiTiet(int iMaSach )
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if(sach==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }
        public DateTime? FormatDate(string obj)
        {

            try
            {
                if (!string.IsNullOrEmpty(obj))
                {
                    var silit = obj.Split('-');
                    var day = int.Parse(silit[2]);
                    var month = int.Parse(silit[1]);
                    var year = int.Parse(silit[0]);
                    return DateTime.ParseExact(string.Format("{0}/{1}/{2}", month, day, year), "MM/dd/yyyy", null);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return null;
        }
    }
}