using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_BanHang.Models;

namespace Web_BanHang.Controllers
{
    public class GioHangController : Controller
    {
        private readonly QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: GioHang
        // gio hang
        #region Giỏ hàng 
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("GioHangRong", "GioHang");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        public ActionResult GioHangRong()
        {
            return View();
        }
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang =Session["GioHang"]  as   List<GioHang> ;
            if (lstGioHang == null)
            {
                //neu gio hang chua ton tai thi tien hanh khoi tao list gio hang {session Gio Hang}
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return  lstGioHang;
        }
        public ActionResult ThemGiohang (int iMaSach, string strURL)
        {// kiem tra xem sach co null khong neu n
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lay ra Sesion gio hang             
            List<GioHang> lstGioHang = LayGioHang();             
             GioHang sanpham = lstGioHang.Find(n => n.iMaSach == iMaSach);
            // kiem tra san pham co null khong neu null thi tao 1  san pham moi         
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSach );
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            // neu khong null thi so luong tang len 1
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }     
        public ActionResult CapNhapGiohang(int iMaSP, FormCollection formsoluong )
        {
            // kiem tra ma san pham
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            // neu get sai ma sp thi tro ve loi 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lấy giỏ hàng tại session ["Gio Hang"]
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
            if (sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(formsoluong["txtSoLuong"].ToString());

            }
            return RedirectToAction("GioHang");
        }
        // xoa gio hang
        public ActionResult XoaGioHang(int iMaSP)
        {
            // kiem tra ma san phan
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            // neu get sai ma san pham thi tra ve loi 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lay gio hang trong session
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);          
            // kiem tra ma san phan neu ton tai se cho sua so luong
            if (sach != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == sanpham.iMaSach);                
            }
            if (lstGioHang.Count == 0)
            {
                Session["Giohang"] = null;
                return RedirectToAction("GioHangRong", "GioHang");
            }            
            return RedirectToAction("GioHang");
        }       
        // tinh tong so luong va tong tien
        // tinh tong so luong
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstgiohang = Session["GioHang"] as List<GioHang>;
            if (lstgiohang != null)
            {
                iTongSoLuong = lstgiohang.Sum(n => n.iSoLuong);
            }            
            return iTongSoLuong;
        }
        // tinh tong tien
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> lstgiohang = Session["GioHang"] as List<GioHang>;
            if (lstgiohang != null)
            {
                iTongTien = lstgiohang.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }
        // xay dung 1 partialView để hiển thị số lượng hàng trên giỏ hàng
        public ActionResult GioHangPartial()
        {
            if (TongSoLuong() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        // xay dung view de chinh sua gio hang
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstgiohang = LayGioHang();
            return View(lstgiohang) ;
        }
        #endregion
        #region Đặt Hàng   
        [HttpGet]
        public ActionResult DatHang()
        {
            var gh = Session["GioHang"];

            return View(gh);
             
        }
        [HttpPost]
        public ActionResult DatHang (string name, string mobile, string address,string email    )
        {
            // Đăng nhập đặt hàng
            // kiem tra dang nhap
            KhachHang KH = (KhachHang)Session["TaiKhoan"];
            if (KH != null  )
            {
                if (Session["GioHang"] == null)
                {
                    return RedirectToAction("GioHangRong", "GioHang");
                }               
                List<GioHang> giohang1 = LayGioHang();
                KhachHang kh2 = (KhachHang)Session["TaiKhoan"];
                DonHang ddh1 = new DonHang();
                ddh1.MaKH = kh2.MaKH;
                ddh1.NgayDat = DateTime.Now;
                db.DonHangs.Add(ddh1);
                db.SaveChanges();
                try
                {
                    ////var id = new DatHang().Insert(ddh);
                    ////var ctdh = new Models.DonHangChiTiet();
                    foreach (var item in giohang1)
                    {
                        ChiTietDonHang dhct = new ChiTietDonHang();
                        dhct.MaSach = item.iMaSach;
                        dhct.MaDonHang = ddh1.MaDonHang;
                        dhct.MaKH = ddh1.MaKH;
                        dhct.DonGia = (decimal)item.dDonGia;
                        dhct.TenKH = kh2.Hoten;
                        dhct.DiaChi = kh2.DiaChi;
                        //dhct.GioiTinh = ddh.KhachHang.;
                        dhct.Sdt = kh2.DienThoai;
                        dhct.Email = kh2.Email;
                        db.ChiTietDonHangs.Add(dhct);
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Home");
                }
                db.SaveChanges();
                if (Session["GioHang"] != null)
                {
                    Session["GioHang"] = null;
                    return RedirectToAction("ThanhToan", "GioHang");
                }
                return RedirectToAction("Index", "Home");

            }
            else
            {
                if (Session["GioHang"] == null)
                {
                    return RedirectToAction("GioHangRong", "GioHang");
                }

                // them don hang

                List<GioHang> giohang = LayGioHang();
                KhachHang kh = new KhachHang();
                kh.Hoten = name;
                kh.DiaChi = address;
                kh.DienThoai = mobile;
                kh.Email = email;
                db.KhachHangs.Add(kh);
                db.SaveChanges();
                DonHang ddh = new DonHang();
                ddh.MaKH = kh.MaKH;
                ddh.NgayDat = DateTime.Now;
                db.DonHangs.Add(ddh);
                db.SaveChanges();
                try
                {
                    ////var id = new DatHang().Insert(ddh);
                    ////var ctdh = new Models.DonHangChiTiet();
                    foreach (var item in giohang)
                    {
                        ChiTietDonHang dhct = new ChiTietDonHang();
                        dhct.MaSach = item.iMaSach;
                        dhct.MaDonHang = ddh.MaDonHang;
                        dhct.DonGia = (decimal)item.dDonGia;
                        dhct.TenKH = ddh.KhachHang.Hoten;
                        dhct.DiaChi = ddh.KhachHang.DiaChi;
                        //dhct.GioiTinh = ddh.KhachHang.;
                        dhct.Sdt = ddh.KhachHang.DienThoai;
                        dhct.Email = ddh.KhachHang.Email;
                        db.ChiTietDonHangs.Add(dhct);
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Home");
                }
                db.SaveChanges();
                if (Session["GioHang"] != null)
                {
                    return RedirectToAction("ThanhToan", "GioHang");
                }
                return RedirectToAction("Index", "Home");
            }
            // truong hop dang nhap an danh
            // kiem tra gio hang

        }
        #endregion

        public ActionResult Success()
        {
            Session["GioHang"] = null;
            return View();
        }
        // tao ham thanh toan
        [HttpGet]
        public ActionResult ThanhToan()
        {
            List<GioHang> gh = new List<GioHang>();
            return View(gh);
        }
        //[HttpPost]
        //public ActionResult ThanhToan()
        //{
        //    ChiTietDonHang dh = new ChiTietDonHang();


        //}
    }
}