using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;
using Web_BanHang.Models;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using Web_BanHang.DTO;
using Web_BanHang.ViewModels;



namespace Web_BanHang.Controllers
{
    public class TimKiemController : Controller
    {
        private readonly QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public class HttpParamActionAttribute : ActionNameSelectorAttribute
        {
            public override bool IsValidName(ControllerContext controllerContext, string actionName, MethodInfo methodInfo)
            {
                if (actionName.Equals(methodInfo.Name, StringComparison.InvariantCultureIgnoreCase))
                    return true;

                var request = controllerContext.RequestContext.HttpContext.Request;
                return request[methodInfo.Name] != null;
            }
        }

        // GET: TimKiem
        public ActionResult Index(int? size, int? page, string sortProperty, string sortOrder, FormCollection form)
        {

            String timkiem = form["searchString"];
           
            

            // tao danh sach hien thi o dropview
            var groupChuDe = (from c in db.ChuDes select c).ToList();

            var groupNXB = (from xuatban in db.NhaXuatBans select xuatban).ToList();

            var groupTacGia = (from tg in db.TacGias select tg).ToList();

            // tao cau truy van giua 2 bang chu de va sach
            var sach1 = (from book in db.Saches
                         join chude in db.ChuDes on book.MaChuDe equals chude.MaChuDe
                         join nxb in db.NhaXuatBans on book.MaNXB equals nxb.MaNXB
                         join tg in db.TacGias on book.MaTacGia equals tg.MaTacGia
                         select new SachDTO()
                         {
                             TenSach = book.TenSach,
                             MaSach = book.MaSach,
                             GiaBan = book.GiaBan,
                             MoTa = book.MoTa,
                             AnhBia = book.AnhBia,
                             MaChuDe = chude.MaChuDe,
                             TenChuDe = chude.TenChuDe,
                             TenNXB = nxb.TenNXB,
                             MaNXB = nxb.MaNXB,
                             MaTacGia = tg.MaTacGia,
                             TenTacGia = tg.TenTacGia

                         });
            if (!String.IsNullOrEmpty(timkiem))
            {
                sach1 = sach1.Where(n => n.TenSach.Contains(timkiem));

            }
            if (!String.IsNullOrEmpty(form["chudeID"]))
            {
                int cd = int.Parse(form["chudeID"]);
                sach1 = sach1.Where(n => n.MaChuDe == cd);
            }
            if (!String.IsNullOrEmpty(form["maNXB"]))
            {
                int nhaxb = int.Parse(form["maNXB"]);
                sach1 = sach1.Where(n => n.MaNXB == nhaxb);
            }
            if (!String.IsNullOrEmpty(form["maTacGia"]))
            {
                int Matacgia = int.Parse(form["maTacGia"]);
                sach1 = sach1.Where(n => n.MaTacGia == Matacgia);
            }
            //if (  cd > 0)
                //{
            //    //int cd = int.Parse(cd);
            //    sach1 = sach1.Where(n => n.MaChuDe == cd);
            //}

            //if (  nhaxb > 0)
            //{
            //    //int maNXB = int.Parse(form["maNXB"]);
            //    sach1 = sach1.Where(n => n.MaNXB == nhaxb);
            //}

            //if (Matacgia > 0)
            //{
            //    //int maTacGia = int.Parse(form["maTacGia"]);
            //    sach1 = sach1.Where(n => n.MaTacGia == nhaxb);
            //}
            /***
             * sach1 = sach1.Where(x=>x.TENCHUDE);
             * 
             * 
             * 
             *  sach1 = sach1.Where(x=>x.NXB);
             */
            if (sortProperty == "TenChuDe")
            {
                sach1 = sach1.OrderBy(x => x.TenChuDe);
                if (sortOrder == "desc")
                {
                    sach1 = sach1.OrderByDescending(x => x.TenChuDe);
                }
            }
            else if (sortProperty == "TenSach")
            {
                sach1 = sach1.OrderBy(x => x.TenSach);
                if (sortOrder == "desc")
                {
                    sach1 = sach1.OrderByDescending(x => x.TenSach);

                }
            }
            else if (sortProperty == "GiaBan")
            {
                sach1 = sach1.OrderBy(x => x.GiaBan);
                if (sortOrder == "desc")
                {
                    sach1 = sach1.OrderByDescending(x => x.GiaBan);
                }
            }
            else if (sortProperty == "TenTacGia")
            {
                sach1 = sach1.OrderBy(x => x.TenTacGia);
                if (sortOrder == "desc")
                {
                    sach1 = sach1.OrderByDescending(x => x.TenTacGia);
                }
            }
            else
            {
                sach1 = sach1.OrderBy(x => x.MaSach);
            }

            #region code thừa để dùng sau
            ////2.1 Thiet lap so trang dang chon List<SelectListItem>
            //foreach (var item in items)
            //{
            //    if (item.Value == size.ToString()) item.Selected = true;
            //}
            //ViewBag.size = items;
            //ViewBag.currentSize = size;

            //if (!String.IsNullOrEmpty(searchString)) // kiem tra xem tu tim kiem co null khong
            //{
            //    sach1 = sach1.Where(x => x.TenSach.Contains(searchString)); //lay toan bo ten sach trong tu tim kiem
            //}
            //// tim kiem theo chu de id
            //if (chudeID > 0)
            //{
            //    sach1 = sach1.Where(n => n.MaChuDe == chudeID);
            //}
            //// tim theo nxb id
            //if (maNXB > 0)
            //{
            //    sach1 = sach1.Where(n => n.MaNXB == maNXB);
            //}

            //// chuyen doi ket qua tu var sang danh sach list
            //List<Sach> listsach = new List<Sach>();

            //int pageSize = (size ?? 5);

            //ViewBag.pageSize = pageSize;

            ////  Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            //// nếu page = null thì lấy giá trị 1 cho biến pageNumber. --- dammio.com
            //int pageNumber = (page ?? 1);

            //// Lấy tổng số record chia cho kích thước để biết bao nhiêu trang
            //int checkTotal = (int)(sach1.Count() / pageSize) + 1;
            //// Nếu trang vượt qua tổng số trang thì thiết lập là 1 hoặc tổng số trang
            //if (pageNumber > checkTotal) pageNumber = checkTotal;



            ////  Trả về các Link được phân trang theo kích thước và số trang.
            //var result = sach1.OrderByDescending(x => x.TenSach).ToPagedList(pageNumber, pageSize);


            // de tinh hien thi so luong san pban tren page  thì dùng công thức 
            // so luong = (pageIndex -1)*pageSize -1 + count ++
            #endregion
            int pageSize = (size ?? 5);
            int pageNumber = (page ?? 1);

            //// Lấy tổng số record chia cho kích thước để biết bao nhiêu trang
            int checkTotal = (int)(sach1.Count() / pageSize) + 1;
            //// Nếu trang vượt qua tổng số trang thì thiết lập là 1 hoặc tổng số trang
            if (pageNumber > checkTotal) pageNumber = checkTotal;

            var viewModel = new SearchAuthorViewModels()
            {
                SortOrder = string.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder,
                SortProperty = sortProperty,
                GroupSach = sach1.ToPagedList(pageNumber, pageSize),
                GroupNXB = groupNXB.Select(x => new SelectListItem()
                {
                    Value = x.MaNXB.ToString(),
                    Text = x.TenNXB
                }).ToList(),
                GroupChuDe = groupChuDe.Select(x => new SelectListItem()
                {
                    Value = x.MaChuDe.ToString(),
                    Text = x.TenChuDe
                }).ToList(),
                GroupTacGia = groupTacGia.Select(x => new SelectListItem()
                {
                    Value = x.MaTacGia.ToString(),
                    Text = x.TenTacGia
                }).ToList(),
                PageCount = checkTotal,
                PageNumber = pageNumber,
                PageSerial = (pageNumber - 1) * pageSize + 1,

                GroupPageSizes = new List<SelectListItem>(){
                    new SelectListItem { Text = "5", Value = "5", Selected = ("5" == size.ToString()) },
                    new SelectListItem { Text = "10", Value = "10", Selected = ("10" == size.ToString()) },
                    new SelectListItem { Text = "20", Value = "20", Selected = ("10" == size.ToString()) },
                    new SelectListItem { Text = "25", Value = "25", Selected = ("25" == size.ToString()) },
                    new SelectListItem { Text = "50", Value = "50", Selected = ("50" == size.ToString()) },
                    new SelectListItem { Text = "100", Value = "100", Selected = ("100" == size.ToString()) },
                    new SelectListItem { Text = "200", Value = "200", Selected = ("200" == size.ToString()) }
                }
            };
            return View(viewModel);
        }
    }
}