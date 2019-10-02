using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Web_BanHang.Models
{
    public class ListSachViewModel
    {
        public List<SelectListItem> GroupTacGia { get; set; }
        public List<SelectListItem> GroupNXB { get; set; }
        public IPagedList<Sach> GroupSach { get; set; }
    }
}