using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Models;
using PagedList;
using PagedList.Mvc;
using Web_BanHang.DTO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace Web_BanHang.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        private readonly QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: QuanLyDonHang
        public ActionResult Index(int? Page)
        {
            int pageSize = 10;
            int pageNumber = Page ?? 1;

            IPagedList donhang = db.DonHangs.OrderBy(n => n.NgayDat).ToPagedList(pageNumber, pageSize);
            IPagedList sach = db.Saches.OrderBy(n => n.GiaBan).ToPagedList(pageNumber, pageSize);
            return View(donhang);
        }
        // xem chi tiet san pham
        public ActionResult ChiTiet(int iMaDonHang)
        {
            DonHang donhang = db.DonHangs.SingleOrDefault(n => n.MaDonHang == iMaDonHang);
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
        public ActionResult XoaDonHang(int iMaDonHang)
        {

            var chitietdonhang = (from Chitiet in db.ChiTietDonHangs.Where(n => n.MaDonHang == iMaDonHang) select Chitiet).ToList();

            if (chitietdonhang == null)
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
        public ActionResult ThemMoiFinal(string search)
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
        //[HttpPost]
        //public ActionResult ThemMoiFinal(FormCollection form)
        //{
        //    DonHang donHang = new DonHang();
            
        //    donHang.TenKH = form["txtTenKH"].ToString();
        //    donHang.DiaChi = form["txtDiaChiGiao"].ToString();
        //    donHang.DiaChiNhanHang = form["txtDiaChiNH"].ToString();
        //    donHang.NgayGiao = DateTime.Parse(form["txtNgayGiao"].ToString());
        //    donHang.EmailKH = form["txtEmailKH"].ToString();
        //    donHang.DienThoaiKH = form["txtPhoneKH"].ToString();
        //    donHang.TinhTrangGiaoHang = null;

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
        
        public JsonResult GetBookValue(string search)
        {
            QuanLyBanSachEntities db = new QuanLyBanSachEntities();
            List<SachDTO> allbook = db.Saches.Where(n => n.TenSach.Contains(search)).Select(n => new SachDTO
            {
                MaSach = n.MaSach,
                TenSach = n.TenSach,
                GiaBan = n.GiaBan,
                SoLuongTon = n.SoLuongTon,
                MaNXB = n.MaNXB,
             }).ToList();
            return new JsonResult { Data = allbook, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetPrice(string search)
        {
            QuanLyBanSachEntities db = new QuanLyBanSachEntities();
            var giaban = db.Saches.Where(n => n.TenSach.Contains(search)).Select(x => x.GiaBan).FirstOrDefault();

            return Json(giaban, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBookId(string search)
        {
            QuanLyBanSachEntities db = new QuanLyBanSachEntities();
            var BookId = db.Saches.Where(n => n.TenSach.Contains(search)).Select(x => x.MaSach).FirstOrDefault();
            return Json(BookId, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetPublishersId(string search)
        {
            QuanLyBanSachEntities db = new QuanLyBanSachEntities();
            var PublishersId = db.Saches.Where(n => n.TenSach.Contains(search)).Select(x => x.MaNXB).FirstOrDefault();
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

        // ajax save data
        //[HttpPost]
        // public JsonResult SaveData(string strOrder, object strOrderDetail, string strOrderCustomer)
        // {
        //     JavaScriptSerializer serializer = new JavaScriptSerializer();

        //     object orderDetail = serializer.DeserializeObject(strOrderDetail);
        //     DonHang donhang = serializer.Deserialize<DonHang>(strOrder);
        //     KhachHang kh = serializer.Deserialize<KhachHang>(strOrderCustomer);

        //     //bool status = false;
        //     //string message = string.Empty;
        //     //add new order

        //     dynamic jsonObj = JsonConvert.DeserializeObject(orderDetail);
        //     try
        //     {
        //         db.KhachHangs.Add(kh);
        //         db.SaveChanges();
        //         donhang.NgayDat = DateTime.Now;
        //         db.DonHangs.Add(donhang);
        //         db.SaveChanges();
        //         foreach (var item in lstGioHang)
        //         {
        //             ChiTietDonHang ctdh = new ChiTietDonHang();
        //             ctdh.MaDonHang = donhang.MaDonHang;
        //             ctdh.MaKH = kh.MaKH;
        //             ctdh.MaSach = item.iMaSach;
        //             ctdh.MaNXB = item.iMaNXB;
        //             ctdh.DonGia = Convert.ToDecimal(item.dDonGia);
        //             ctdh.SoLuong = item.iSoLuong;
        //             db.ChiTietDonHangs.Add(ctdh);
        //             db.SaveChanges();
        //         }
        //     }
        //     catch (System.Data.Entity.Validation.DbEntityValidationException ex)
        //     {

        //         foreach (var eve in ex.EntityValidationErrors)
        //         {
        //             Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //             foreach (var ve in eve.ValidationErrors)
        //             {
        //                 Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //         ve.PropertyName, ve.ErrorMessage);
        //             }
        //         }
        //         throw;
        //     }
        //     return Json("true", JsonRequestBehavior.AllowGet);
        // }

        [HttpPost]
        public JsonResult SaveData(DonHang donhang, KhachHang khachhang, List<ChiTietDonHang> ctdh)
        {
            try
            {
                db.KhachHangs.Add(khachhang);
                db.SaveChanges();
                donhang.NgayDat = DateTime.Now;
                donhang.MaKH = khachhang.MaKH;
                db.DonHangs.Add(donhang);
                db.SaveChanges();
                foreach (var item in ctdh)
                {
                    ChiTietDonHang chitietdh = new ChiTietDonHang();
                    chitietdh.DonGia = item.DonGia;
                    chitietdh.MaKH = khachhang.MaKH;
                    chitietdh.MaNXB = item.MaNXB;
                    chitietdh.MaSach = item.MaSach;
                    chitietdh.SoLuong = item.SoLuong;
                    db.ChiTietDonHangs.Add(chitietdh);
                    db.SaveChanges();
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                   eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("-Property: \"{0}\", Error: \"{1}\"",
                ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return Json("true", JsonRequestBehavior.AllowGet);
        }

    }
}