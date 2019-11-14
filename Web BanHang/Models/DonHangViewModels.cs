using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Web_BanHang.Models
{
    public class DonHangViewModels: DonHang
    {
        public IEnumerable<SelectListItem>  GroupStatusDelivery { get; set; }   
    }
}