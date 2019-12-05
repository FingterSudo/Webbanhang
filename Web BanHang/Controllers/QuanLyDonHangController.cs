using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Models;
using PagedList;
using PagedList.Mvc;
using Web_BanHang.DTO;

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
        // xem chi tiet san pham
        public ActionResult ChiTiet(int iMaDonHang)
        {
             DonHang donhang = db.DonHangs.SingleOrDefault(n=>n.MaDonHang== iMaDonHang);
            if (donhang == null)
            {
                Response.StatusCode = 404;
            }
            return View(donhang);
        }
        public PartialViewResult ChiTietHoaDon(int iMaDonHang)
        {
            ChiTietDonHang ctdh = db.ChiTietDonHangs.SingleOrDefault(n => n.MaDonHang == iMaDonHang);
            if (ctdh == null)
            {
                Response.StatusCode = 404;
            } 
            return PartialView("_ChiTietHoaDon", ctdh);
        }
        // xoa san pham
        [HttpGet]
        public ActionResult Xoa(int iMaDonHang)
        {
            ChiTietDonHang ctdh = db.ChiTietDonHangs.FirstOrDefault(n => n.MaDonHang == iMaDonHang);
            DonHang donhang = db.DonHangs.SingleOrDefault(n => n.MaDonHang == iMaDonHang);
            
            if (donhang == null)
            {
                Response.StatusCode = 404; 
            }
            return View(donhang);
        }
        [HttpPost]
        public ActionResult XoaDonHang(int iMaDonHang )
        {
            
            var chitietdonhang = (from Chitiet in  db.ChiTietDonHangs.Where(n => n.MaDonHang == iMaDonHang) select Chitiet).ToList();
             
            if(chitietdonhang == null)
            {
                Response.StatusCode = 404;
            }
            db.ChiTietDonHangs.RemoveRange(chitietdonhang);
            db.SaveChanges();
            DonHang donhang = db.DonHangs.SingleOrDefault(n => n.MaDonHang == iMaDonHang);
            if (donhang == null)
            {
                Response.StatusCode = 404;
            }
          
            db.DonHangs.Remove(donhang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ThemMoiFinal( string search)
        {
            QuanLyBanSachEntities db = new QuanLyBanSachEntities();
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");
             ViewBag.TenSach = GetBookValue(search).ToString();
            //ViewBag.GiaBan = GetPrice(Convert.ToString(giaban)).ToString();
            //List<SelectListItem> 
            List<SelectListItem> statusdelivery = new List<SelectListItem>()
             {
                 new SelectListItem(){Text="chưa giao hàng", Value="1"},
                 new SelectListItem(){Text="đang giao hàng", Value="2"},
                 new SelectListItem(){Text="đã giao hàng", Value="3"}
             };
            ViewBag.GiaoHang = statusdelivery;
            DonHang dh = new DonHang();
            if (dh.TinhTrangGiaoHang == 3)
            {
                return RedirectToAction("Thanhtoan");
            }
            else
            {
                dh.TinhTrangThanhToan = false;
            }
            return View();
        }
        //[HttpPost]
        //public ActionResult ThemMoi(FormCollection form)
        //{
            
        
        //    DonHang donHang = new DonHang();
        //    var statusDonHang =form["TinhTrang"].ToString();
        //    donHang.TenKH = form["txtTenKh"].ToString();
        //    donHang.DiaChi = form["txtDiaChi"].ToString();
        //    donHang.DiaChiNhanHang = form["txtDiaChiNh"].ToString();
        //    donHang.NgayGiao = DateTime.Parse(form["txtNgayGiao"].ToString());
        //    donHang.EmailKH = form["txtEmail"].ToString();
        //    //donHang.TinhTrangGiaoHang = Convert.ToInt32(form["txtGiaohang"]);
            
        //    donHang.TongTien = Convert.ToDecimal(form["txtTongTien"]);
        //    ChiTietDonHang ctdh = new ChiTietDonHang();
        //    ctdh.SoLuong = Convert.ToInt32(form["txtSoLuong"].ToString());
        //    ctdh.DonGia = Convert.ToDecimal (form["txtDonGia"]);
        //    ctdh.Sach.TenSach = form["txtSach"].ToString();
        //    ctdh.MaSach = Convert.ToInt32(form["txtMaSach"].ToString());
        //    ctdh.MaNXB = Convert.ToInt32(form["txtMaMXB"].ToString());
        //    ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");
        //    db.DonHangs.Add(donHang);
        //    db.ChiTietDonHangs.Add(ctdh);
        //    db.SaveChanges();
        //    return View(); 
        //}
        //[HttpPost]
        //public ActionResult ThemMoi2(FormCollection form)
        //{
        //    DonHang donHang = new DonHang();
        //    var statusDonHang = form["TinhTrang"].ToString();
        //    donHang.TenKH = form["txtTenKh"].ToString();
        //    donHang.DiaChi = form["txtDiaChi"].ToString();
        //    donHang.DiaChiNhanHang = form["txtDiaChiNh"].ToString();
        //    donHang.NgayGiao = DateTime.Parse(form["txtNgayGiao"].ToString());
        //    donHang.EmailKH = form["txtEmail"].ToString();
        //    //donHang.TinhTrangGiaoHang = Convert.ToInt32(form["txtGiaohang"]);

        //    donHang.TongTien = Convert.ToDecimal(form["txtTongTien"]);
        //    ChiTietDonHang ctdh = new ChiTietDonHang();
        //    ctdh.SoLuong = Convert.ToInt32(form["txtSoLuong"].ToString());
        //    ctdh.DonGia = Convert.ToDecimal(form["txtDonGia"]);
        //    ctdh.Sach.TenSach = form["txtSach"].ToString();
        //    ctdh.MaSach = Convert.ToInt32(form["txtMaSach"].ToString());
        //    ctdh.MaNXB = Convert.ToInt32(form["txtMaMXB"].ToString());
        //    ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");
        //    db.DonHangs.Add(donHang);
        //    db.ChiTietDonHangs.Add(ctdh);
        //    db.SaveChanges();
        //    return View();
        //}
        [HttpPost]
        public ActionResult ThemMoiFinal(FormCollection form)
        {
            DonHang donHang = new DonHang();
            var statusDonHang = form["TinhTrang"].ToString();
            donHang.TenKH = form["name"].ToString();
            donHang.DiaChi = form["adress"].ToString();
            donHang.DiaChiNhanHang = form["txtDiaChiNh"].ToString();
            donHang.NgayGiao = DateTime.Parse(form["txtNgayGiao"].ToString());
            donHang.EmailKH = form["email"].ToString();
            donHang.DienThoaiKH = form["phone"].ToString();
            //donHang.TinhTrangGiaoHang = Convert.ToInt32(form["txtGiaohang"]);

            donHang.TongTien = Convert.ToDecimal(form["txtTongTien"]);
            ChiTietDonHang ctdh = new ChiTietDonHang();
            ctdh.SoLuong = Convert.ToInt32(form["txtSoLuong"].ToString());
            ctdh.DonGia = Convert.ToDecimal(form["txtDonGia"]);
            ctdh.Sach.TenSach = form["txtSach"].ToString();
            ctdh.MaSach = Convert.ToInt32(form["txtMaSach"].ToString());
            ctdh.MaNXB = Convert.ToInt32(form["txtMaMXB"].ToString());
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");
        
            db.DonHangs.Add(donHang);
            db.ChiTietDonHangs.Add(ctdh);  
            db.SaveChanges();
            return View();
            
        }
        //[HttpPost]
        //public ActionResult hihi()
        //{
        //    return View();
        //}
        public JsonResult GetBookValue(string search)
        {
            QuanLyBanSachEntities db = new QuanLyBanSachEntities();
            List<SachDTO> allbook = db.Saches.Where(n => n.TenSach .Contains(search)).Select(n => new SachDTO
            {
                TenSach = n.TenSach
            }).ToList();
            return new JsonResult { Data = allbook, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
        public JsonResult  GetPrice (string search)
        {
            QuanLyBanSachEntities db = new QuanLyBanSachEntities();
            var giaban = db.Saches.Where(n => n.TenSach.Contains(search)).Select(x => x.GiaBan).FirstOrDefault();
           
            return Json(giaban, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetBookId (string search)
        {
            QuanLyBanSachEntities db = new QuanLyBanSachEntities();
            var BookId = db.Saches.Where(n => n.TenSach.Contains(search)).Select(x => x.MaSach).FirstOrDefault();
            return  Json(BookId, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetPublishersId(string search)
        {
            QuanLyBanSachEntities db = new QuanLyBanSachEntities();
            var PublishersId  = db.Saches.Where(n => n.TenSach.Contains(search)).Select(x => x.MaNXB).FirstOrDefault();
            return Json(PublishersId, JsonRequestBehavior.AllowGet);

        }
        public JsonResult GetAuthor(string search)
        {
            QuanLyBanSachEntities db = new QuanLyBanSachEntities();
            var Author = db.Saches.Where(n => n.TenSach.Contains(search)).Select(x => x.TacGia.TenTacGia).FirstOrDefault();
            return Json(Author, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Thanhtoan()
        {
            return View();
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
                    return DateTime.ParseExact(string.Format("{2}/{1}/{0}", day, month, year), "dd/MM/yyyy", null);
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