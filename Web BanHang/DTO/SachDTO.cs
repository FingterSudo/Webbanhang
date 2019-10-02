using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web_BanHang.Models;

namespace Web_BanHang.DTO
{
    public class SachDTO : Sach
    {
        public string TenChuDe { get; set; }
        public string TenNXB { get; set; }
        public string TenTacGia { get; set; }
    }
}