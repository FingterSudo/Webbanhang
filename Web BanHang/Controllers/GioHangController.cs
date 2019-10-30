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
            // Kiểm tra Session giỏ hàng có null hay không
            if (Session["GioHang"] == null)
            {
                // Nếu giỏ hàng null thì RedirectToAction về giỏ hàng 
                //  method RedirectToAction("Action","Controller");
                return RedirectToAction("GioHangRong", "GioHang");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        // tạo phương thức giỏi hàng rỗng 
        public ActionResult GioHangRong()
        {
            // trả về Views
            return View();
        }
        // tạo một phương thức Lấy Giỏ hàng kế thừa từ list<GioHang>
        public List<GioHang> LayGioHang()
        {
            // tạo một đối tượng lstGioHang sau đó gán cho Session["GioHang"
            List<GioHang> lstGioHang =Session["GioHang"]  as   List<GioHang> ;
            if (lstGioHang == null)
            {
                //neu gio hang chua ton tai thi tien hanh khoi tao list gio hang {session Gio Hang}
                lstGioHang = new List<GioHang>();
                // gán session["GioHang"] bằng lstGioHang
                Session["GioHang"] = lstGioHang;
            }
            // trả về đối tượng lstGioHang
            return  lstGioHang;
        }
        // tạo phương thức Thêm Giỏ hàng truyền 2 parameter là iMaSact và strURL
        public ActionResult ThemGiohang (int iMaSach, string strURL)
        {//  tạo một đối tượng sach từ lớp Sach  trả về đối tượng  trùng với 
            //parameter truyền vào iMaSach nếu không có trả về mặc định 
            //Phương thức SingleOrDefault         
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            // kiểm tra xem đối tượng sách có null không
            if (sach == null)
            {
                // nesu sách null thì server trả về một phương thức
                // StatusCode là 404
                //Response ở đây là kết quả server trả về cho client
                Response.StatusCode = 404;
                return null;
            }
            // Sau khi gán session cho listGiohang thì lấy session[GioHang] trong lstGiohang             
            List<GioHang> lstGioHang = LayGioHang();  
            // tạo  đối tượng sanpham từ GioHang  và lấy ra từ listGioHang
            // tìm kiếm và trả về đối tượng theo parameter truyền vào là iMaSach trong viewmodel GioHang
             GioHang sanpham = lstGioHang.Find(n => n.iMaSach == iMaSach);
            // kiểm tra sanpham có null không      
            if (sanpham == null)
            {
                // nếu null thì tạo một giỏ mới theo parameter iMaSach
                sanpham = new GioHang(iMaSach );
                // sử dụng hàm Add để thêm sản phẩm vào lstGioHang
                lstGioHang.Add(sanpham);
                // return trả về strURL
                return Redirect(strURL);
            }
            // neu khong null thi so luong tang len 1
            else
            {
                // nếu sản phẩm không null thì iSoLuong tăng lên 1
                sanpham.iSoLuong++;
                // return trả về strURL
                return Redirect(strURL);
            }
        }     
        // tạo phương thức cập nhập giỏ hàng truyền parameter iMSanPham và dùng formcollection 
        public ActionResult CapNhapGiohang(int iMaSP, FormCollection formsoluong )
        {
            // tạo một đối tượng sach từ lớp Sach trả về đối tượng trùng khớp
            // với parameter truyền vào iMaSach nếu không có trả về mặc định
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            // kiểm tra sách có null không
            if (sach == null)
            {
                // nếu null thì server trả về lỗi 404
                Response.StatusCode = 404;
                // trả về null
                return null;
            }
            // tạo lstGIohang để lấy session[GioHang] từ giỏ hàng
            List<GioHang> lstGioHang = LayGioHang();
            // tạo một  đối tượng sanpham từ lớp Giohang trả về đối tượng được tìm thấy theo
            // parameter truyền vào iMaSach trùng với iMaSP nếu không có trả về mặc định
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP); 
            // kiểm tra xem sanpham có  khác null không
            if (sanpham != null)
            {
                // nếu sản phẩm khác null thì lấy iSoLuong từ txt trong formconllection
                // phải ép kiểu về dạng int vì iSoLuong kiểu int
                sanpham.iSoLuong = int.Parse(formsoluong["txtSoLuong"].ToString());

            }
            // trả về phương thức Giohang
            return RedirectToAction("GioHang");
        }
        // tạo một phương thức XoaGioHang truyền parameter iMSP
        public ActionResult XoaGioHang(int iMaSP)
        {
            //tạo một đối tượng từ sach từ lớp Sach trả về 
            // đối tượng tìm kiếm trùng khớp với parameter iMaSP nếu không trả về mặc định
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSP);
            //kiểm tra sach có null không
            if (sach == null)
            {
                // nếu mà sách null thì trả về dữ liệu server lỗi 404
                Response.StatusCode = 404;
                // return null
                return null;
            }
            // tạo một đối tượng lstGioHang để lấy giỏ hàng từ session["GioHang"]
            List<GioHang> lstGioHang = LayGioHang();
            // tạo một đối tượng sản phẩm từ lớp giỏ hàng 
            // trả về theo đối tượng trùng khớp với parameter iMaSach 
            // hoặc trả về default
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSP);          
            // kiểm tra sách không null
            if (sach != null)
            {
                // nếu sách không null dùng phương thức RemoveAll hủy sách theo iMaSach
                lstGioHang.RemoveAll(n => n.iMaSach == sanpham.iMaSach);                
            }
            // nếu lstGioHang rỗng 
            if (lstGioHang.Count == 0)
            {
                // thì gán Session["GioHang"] bằng null
                Session["Giohang"] = null;
                // trả về phương thức giỏ hàng rỗng trong con troller GioHang
                return RedirectToAction("GioHangRong", "GioHang");
            }            
            return RedirectToAction("GioHang");
        }       
       // tạo một hàm tính tổng số lượng
        private int TongSoLuong()
        {
            // khai báo biến và khởi tạo biến bằng 0
            int iTongSoLuong = 0;
            // tạo và gián đối tượng lstgiohang từ Session["GioHang"]
            List<GioHang> lstgiohang = Session["GioHang"] as List<GioHang>;
            // kiểm trả xem lstgiohang khác null
            if (lstgiohang != null)
            {
                // iTongSoLuong bằng tính tổng số iSoLuong trong lstGiohang
                iTongSoLuong = lstgiohang.Sum(n => n.iSoLuong);
            }     
            // trả về iTongSoLuong
            return iTongSoLuong;
        }
        // tạo hàm tính tổng tiền
        private double TongTien()
        {
            // khai báo biến và khởi tạo biến bằng 0
            double iTongTien = 0;
            // tạo một đối tượng lstgiohang lấy từ sesson["GioHang"]
            List<GioHang> lstgiohang = Session["GioHang"] as List<GioHang>;
            // kiểm tra lstGioHang khác null
            if (lstgiohang != null)
            {
                // iTongTien bằng tính tổng số lượng đơn giá  sản phẩm trong lstGioHang
                iTongTien = lstgiohang.Sum(n => n.dThanhTien);
            }
            return iTongTien;
        }
        //xây dựng 1 partial view để trả về trên giỏ hàng
        public ActionResult GioHangPartial()
        {
            // kiểm tra hàm TongSoLuong bằng không
            if (TongSoLuong() == 0)
            {
                // nếu bằng == 0 thì return về ParitalView
                return PartialView();
            }
            // nếu không
            // tạo một ViewBag TongSoLuong = TongSoLuong
            ViewBag.TongSoLuong = TongSoLuong();
            // tạo một ViewBag TongTien = TongTien
            ViewBag.TongTien = TongTien();
            // trả về Partial View()
            return PartialView();
        }
        // xây dựng phương thức SuaGioHang
        public ActionResult SuaGioHang()
        {
            // kiểm tra Session["GioHang"] có null hay không
            if (Session["GioHang"] == null)
            {
                // nếu null thì trả về phương thức Index của controller Home
                return RedirectToAction("Index", "Home");
            }
            // nếu không tạo một đối tượng lstgiohang lấy từ sessiong["GioHang"]
            List<GioHang> lstgiohang = LayGioHang();
            // trả về lstgohang
            return View(lstgiohang) ;
        }
        #endregion
        #region Đặt Hàng   
        // gọi phương thức get
        [HttpGet]
        // tạo phương thức DatHang
        public ActionResult DatHang()
        {
           // khởi tạo biến gh Session["GioHang"]
            var gh = Session["GioHang"];
            // trả về gh
            return View(gh);     
        }
        // gọi phương thức post
        [HttpPost]
        // tạo phương thức DatHang truyền vào parameter name,mobile, address, email
        // gender
        public ActionResult DatHang (string name, string mobile, string address,string email,string gender)
        {
            // Đăng nhập đặt hàng
            //tạo một đối tượng từ Session["TaiKhoan"]
            KhachHang KH = (KhachHang)Session["TaiKhoan"];
            // nếu KH có rỗng hay không
            if (KH != null  )
            {
                // nếu KH không rỗng thì kiểm tra Session["GioHang"]
                if (Session["GioHang"] == null)
                {
                    // nếu Session["GioHang'] rỗng 
                    // trả về phương thức GioHangRong trong controller GioHang
                    return RedirectToAction("GioHangRong", "GioHang");
                }        
                // khởi tạo đối tượng giohang1 để lấy Session["GioHang"]
                List<GioHang> giohang1 = LayGioHang();
                //tao tong tien
                var tongtien = giohang1.Sum(n => n.dThanhTien);
                // tạo một đối tượng kh2 từ session["TaiKhoan"]
                KhachHang kh2 = (KhachHang)Session["TaiKhoan"];
                // tạo một đội tượng ddh1 từ DonHang
                DonHang ddh1 = new DonHang();
                // gán các biến từ kh2 và ddh1
                ddh1.MaKH = kh2.MaKH;
                ddh1.TenKH = kh2.Hoten;
                ddh1.DiaChi = kh2.DiaChi;
                ddh1.DiaChiNhanHang = address;
                ddh1.EmailKH = email;
                ddh1.DienThoaiKH = mobile;
                ddh1.NgayDat = DateTime.Now;
                ddh1.TongTien = Convert.ToDecimal(tongtien);
                // thêm  đối tượng ddh1 vào DonHangs
                db.DonHangs.Add(ddh1);
                //lưu  đối tượng ddh1 vào DonHangs
                db.SaveChanges();
                // khởi tạo chuỗi content từ lớp MailHelper gọi phương thức MailOrder 
                // và truyền parameter vào MailOrder 
                string content = MailHelper.MailOrder(ddh1, kh2, giohang1);
                // khởi tạo chuỗi mail từ Email của đối tượng kh2
                string mail = kh2.Email;
                // dùng hàm try  catch bắt lỗi
                try
                {
                    ////var id = new DatHang().Insert(ddh);
                    ////var ctdh = new Models.DonHangChiTiet();
                    //dùng hàm foreach để lưu đối tượng từ giohang sang chititietdonhang
                    foreach (var item in giohang1)
                    {
                        // tạo chi tiet don hang
                        ChiTietDonHang dhct = new ChiTietDonHang();
                        // nhập các đối tượng 
                        dhct.MaSach = item.iMaSach;
                        dhct.MaDonHang = ddh1.MaDonHang;
                        // thieu so luong dong hang
                        dhct.SoLuong = item.iSoLuong;
                        dhct.MaKH = ddh1.MaKH;
                        dhct.DonGia = (decimal)item.dDonGia;
                        // thêm chi tiết đơn hàng vào db
                        db.ChiTietDonHangs.Add(dhct);
                    }
                }
                catch (Exception ex)
                {
                    // return về action index của controller Home
                    return RedirectToAction("Index", "Home");
                }
                // lưu vào db
                db.SaveChanges();
                // truyền tham số vào phương thức Sendmail
                MailHelper.SendEmail1(mail, "dangnhatchi@gmail.com", "1996@Bach", "Đơn Hàng", content);
                // kiểm tra giỏ hàng có khác null không
                if (Session["GioHang"] != null)
                {
                    // nếu khác null nếu khác thì gán giỏ hàng null
                    Session["GioHang"] = null;
                    // trả về action ThanhToan của controller GioHang
                    return RedirectToAction("ThanhToan", "GioHang");
                }
                // nếu null thì trả về action Index của home
                return RedirectToAction("Index", "Home");

            }
            // nếu khác hàng không null thì chuyển sang ẩn danh các cmt la lá như trên
            else
            {
                if (Session["GioHang"] == null)
                {
                    return RedirectToAction("GioHangRong", "GioHang");
                }
                // them don hang
                List<GioHang> giohang = LayGioHang();
                // tinh toang tien
                var tongtien = giohang.Sum(n => n.dThanhTien);
                //them don vi khach hang 
                KhachHang kh = new KhachHang();
                kh.Hoten = name;
                kh.DiaChi = address;
                kh.DienThoai = mobile;
                kh.Email = email;
                kh.GioiTinh = gender;
                db.KhachHangs.Add(kh);  
                db.SaveChanges();
                DonHang ddh = new DonHang();
                ddh.NgayDat = DateTime.Now;
                ddh.MaKH = kh.MaKH;
                ddh.TenKH = kh.Hoten;
                ddh.DiaChi = kh.DiaChi;
                ddh.DiaChiNhanHang = address;
                ddh.EmailKH = email;
                ddh.DienThoaiKH = mobile;
                ddh.NgayDat = DateTime.Now;
                ddh.TongTien = Convert.ToDecimal(tongtien);
                db.DonHangs.Add(ddh);
                db.SaveChanges();
                string content = MailHelper.MailOrder(ddh, kh, giohang);
                string mail = email;
                try
                {
                    foreach (var item in giohang)
                    {
                        ChiTietDonHang dhct = new ChiTietDonHang();
                        dhct.MaSach = item.iMaSach;
                        dhct.MaDonHang = ddh.MaDonHang;
                        dhct.DonGia = (decimal)item.dDonGia;
                        dhct.SoLuong = item.iSoLuong;
                        db.ChiTietDonHangs.Add(dhct);  
                    }
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Index", "Home");
                }
                db.SaveChanges();
               
                MailHelper.SendEmail1(mail, "dangnhatchi@gmail.com", "1996@Bach", "Đơn Hàng", content);
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
        // tạo một phương thức Success
        public ActionResult Success()
        {
            // gán session["GioHang"]
            Session["GioHang"] = null;
            // trả về View 
            return View();
        }
        // tao ham thanh toan
        // gọi phương thức Get
        [HttpGet]
        // tạo phương thức ThanhToan
        public ActionResult ThanhToan()
        {
            // tạo đối tượng gh
            List<GioHang> gh = new List<GioHang>();
            // trả về View
            return View(gh);
        }
        //[HttpPost]
        //public ActionResult ThanhToan()
        //{
        //    ChiTietDonHang dh = new ChiTietDonHang();
        //}       
    }
}