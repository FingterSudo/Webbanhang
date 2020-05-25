using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// add 2 thu vien thiet ke meta data
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Web_BanHang.Models 
{
    [MetadataType(typeof(KhachHangMetadata))]
    public partial class KhachHang
    {
        internal sealed class KhachHangMetadata
        {
            
            [DisplayName("Mã khách hàng")]
             
            public int MaKH { get; set; }
            
            [DisplayName("Họ tên")]
            [Required(ErrorMessage ="Nhập họ tên")]
            public string Hoten { get; set; }
           
            [DisplayName("Tài khoản")]
            [Required(ErrorMessage ="Nhập tên tài khoản")]
            public string TaiKhoan { get; set; }
    
            [DisplayName("Mật khẩu")]
            [Required(ErrorMessage ="Nhập mật khẩu")]
            [StringLength(100,ErrorMessage ="Mật khẩu \"{0}\" must have {2} character", MinimumLength =8)]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$",
                ErrorMessage ="Mật khẩu phải chứa ít nhất 8 ký tự, 1 chữ viết hoa, 1 chữ viết thường," +
                "1 số và 1 ký tự đặc biệt")]
            [DataType(DataType.Password)]
            public string MatKhau { get; set; }

            [DisplayName("Email")]
            [Required(ErrorMessage ="Nhập Email")]
            public string Email { get; set; }
      
            [DisplayName("Địa chỉ")]
            [Required(ErrorMessage ="Nhập Địa chỉ")]
            public string DiaChi { get; set; }
             
            [DisplayName("Điện thoại")]
            [Required(ErrorMessage ="Nhập Điện thoại")]
            public string DienThoai { get; set; }
       
            [DisplayName("Giới tính")]
            [Required (ErrorMessage ="Nhập giới tính")]
            public string GioiTinh { get; set; }
        
            [DisplayName("Ngày sinh")]
            [Required(ErrorMessage ="Nhập ngày sinh ")]
            public Nullable<System.DateTime> NgaySinh { get; set; }

            [DisplayName("Xác nhận mật khẩu")]
            [Required(ErrorMessage ="Vui lòng nhập lại mật khẩu")]
            [Compare("Password",ErrorMessage ="Mật khẩu nhập lại không trùng khớp, vui lòng nhập lại!")]
            [DataType(DataType.Password)]
            public string ComfirmPassword { get; set; }

        }
    }
}