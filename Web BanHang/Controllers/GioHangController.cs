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
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
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
            // neu khong null thi so luong tang len 1
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSach);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //public ActionResult CapNhapGiohang(int iMaSP,string sanPham )
        //{
        //    // kiem tra ma san pham
        //    Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
        //    // neu get sai ma sp thi tro ve loi 404
        //    if(sach== null)
        //    {
        //        Response.StatusCode = 404;
        //        return null;
        //    }
        //    // lấy giỏ hàng tại session ["Gio Hang"]
        //    List<GioHang> lstGioHang = LayGioHang();
        //    GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);
        //    if (sanpham != null)
        //    {
        //        sanpham.iSoLuong = int.Parse(sanPham.ToString());

        //    }
        //    return View("GioHang");
        //}
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
                return RedirectToAction("Index", "Home");
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

    }
}