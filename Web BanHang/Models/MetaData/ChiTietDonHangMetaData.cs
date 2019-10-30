using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// using 2 thư viện thiết kế class MetaData
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web_BanHang.Models 
{
    [MetadataType(typeof(ChiTietDonHangMetaData))]
    public partial class ChiTietDonHang 
    {
        internal sealed class ChiTietDonHangMetaData
        {
            [Required]
            [DisplayName("Mã Đơn Hàng")]
            public int MaDonHang { get; set; }
            [Required]
            [DisplayName("Mã Sách")]
            public int MaSach { get; set; }
            [Required]
            [DisplayName("Mã Khách Hàng")]
            public Nullable<int> MaKH { get; set; }
            [Required(ErrorMessageResourceName = "Vui lòng nhập số lượng")]
            [DisplayName("Số Lượng")]          
            public Nullable<int> SoLuong { get; set; }
            [Required]
            [DisplayName("Đơn Giá")]
            public Nullable<decimal> DonGia { get; set; }
        }
    }
}