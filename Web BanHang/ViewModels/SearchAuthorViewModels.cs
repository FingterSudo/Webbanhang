using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_BanHang.Models;
using System.ComponentModel;
using Web_BanHang.DTO;
using PagedList;
using System.Web.Mvc;

namespace Web_BanHang.ViewModels
{
    public class SearchAuthorViewModels
    {
        public IPagedList<SachDTO> GroupSach { get; set; }
        public IEnumerable<SelectListItem> GroupNXB { get; set; }
        public IEnumerable<SelectListItem> GroupChuDe { get; set; }
        public IEnumerable<SelectListItem> GroupTacGia { get; set; }
        public IEnumerable<SelectListItem> GroupPageSizes { get; set; }

        public int PageCount { get; set; }
        public int PageNumber { get; set; }

        public int PageSerial { get; set; }
        public string SortProperty { get; set; }
    }
}