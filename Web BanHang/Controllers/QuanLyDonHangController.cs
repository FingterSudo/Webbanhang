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
        public ActionResult ThemMoi()
        {
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");
            return View();
        }
        [HttpPost]
        public ActionResult ThemMoi(FormCollection form)
        {
            DonHang donHang = new DonHang();
            donHang.TenKH = form["txtTenKh"].ToString();
            donHang.DiaChi = form["txtDiaChi"].ToString();
            donHang.DiaChiNhanHang = form["txtDiaChiNh"].ToString();
            donHang.NgayDat = DateTime.Parse(form["txtNgayDat"]);
            donHang.NgayGiao = DateTime.Parse(form["txtNgayGiao"]);
            donHang.EmailKH = form["txtEmail"].ToString();
            donHang.DienThoaiKH = form["txtDienthoaiKh"].ToString();
            donHang.TinhTrangGiaoHang = Convert.ToInt32(form["txtTinhTrang"].ToString());
            donHang.TinhTrangThanhToan = bool.Parse(form["txtTinhTrang"].ToString());
            donHang.TongTien = Convert.ToDecimal(form["txtTongTien"]);
            ChiTietDonHang ctdh = new ChiTietDonHang();
            ctdh.SoLuong = Convert.ToInt32(form["txtSoLuong"].ToString());
            ctdh.DonGia = Convert.ToDecimal (form["txtDonGia"]);
            ctdh.Sach.TenSach = form["txtSach"].ToString();
            ctdh.Sach.MaChuDe = Convert.ToInt32(form["txtMaChuDe"].ToString());
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans, "MaNXB", "TenNXB");
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
                    return DateTime.ParseExact(string.Format("{2}/{1}/{0}",  day, month, year), "dd/MM/yyyy", null);
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