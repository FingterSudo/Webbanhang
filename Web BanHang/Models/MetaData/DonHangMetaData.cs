using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// using 2 thư viện thiết kế class MetaData
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web_BanHang.Models 
{
    [MetadataType(typeof(DonHangMetaData))]

    public partial class DonHang
    {
        internal sealed class DonHangMetaData
        {
            [DisplayName("Mã Đơn Hàng")]
            public int MaDonHang { get; set; }

            [DisplayName("Mã Khách Hàng")]
            public Nullable<int> MaKH { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập tên khách hàng này!")]
            [DisplayName("Tên Khách Hàng(*)")]
            public string TenKH { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập địa chỉ khác hàng này!")]
            [DisplayName("Địa Chỉ(*)")]
            public string DiaChi { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập địa chỉ nhận hàng")]
            [DisplayName("Địa Chỉ(*)")]
            public string DiaChiNhanHang { get; set; }
             
            [DisplayName("Email Khách Hàng")]
            [Required(ErrorMessage ="Vui lòng nhập Email khách hàng")]
            public string EmailKH { get; set; }
            
            [DisplayName("Tình trạng giao hàng")]
            public Nullable<int> TinhTrangGiaoHang { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessage = "Vui lòng nhập ngày đặt!")]
            [DisplayName("Ngày Đặt(*)")]
            public Nullable<System.DateTime> NgayDat { get; set; }

            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
            [Required(ErrorMessage = "Vui lòng nhập ngày giao!")]
            [DisplayName("Ngày Giao(*)")]
            public Nullable<System.DateTime> NgayGiao { get; set; }

            [DisplayName("Tình trạng đơn hàng")]
            public Nullable<bool> TinhTrangThanhToan { get; set; }

            
            [DisplayName("Điện Thoại Khách Hàng(*)")]
            public string DienThoaiKH { get; set; }
        }
    }
}