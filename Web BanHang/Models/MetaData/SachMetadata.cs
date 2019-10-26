using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// using 2 thư viện thiết kế class MetaData
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Web_BanHang.Models 
{
    [MetadataType(typeof(SachMetadata))]

    public partial class Sach 
    {
        internal sealed class SachMetadata
        {
           
            public int MaSach { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập tên sách này!")]
            [DisplayName("Tên sách(*)")]
            
            public string TenSach { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập mô tả !")]
            [DisplayName("Mô tả(*)")]
            public string MoTa { get; set; }

             
            [DisplayName("Ảnh bìa(*)")]
            public string AnhBia { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập giá bán!")]
            [DisplayName("Giá bán(*)")]
            public Nullable<decimal> GiaBan { get; set; }

            [DataType(DataType.Date)]
            [DisplayName("Ngày cập nhập(*)")]
            [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}",ApplyFormatInEditMode =true)] // định dạng ngày sinh
            [Required(ErrorMessage = "Vui lòng nhập cập nhập")]
            public Nullable<System.DateTime> NgayCapNhap { get; set; }

            [Required(ErrorMessage = "Vui lòng nhập trường này")]
            [DisplayName("Số lượng tồn(*)")]
            public Nullable<int> SoLuongTon { get; set; }
          
            [DisplayName("Mã tác giả(*)")]
            public Nullable<int> MaTacGia { get; set; }
           
            [DisplayName("Mã nhà xuất bản(*)")]
            public Nullable<int> MaNXB { get; set; }
            
            [DisplayName("Mã chủ đề(*)")]
            public Nullable<int> MaChuDe { get; set; }
        }
    }
}