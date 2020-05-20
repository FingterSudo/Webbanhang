using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Text;
using Web_BanHang.Models;
using System.Configuration;

namespace Web_BanHang.Models
{
    public class MailHelper
    {
        public void SendMail(string toEmailAddress, string subject, string content)
        {
            var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
            bool enabledSsl = bool.Parse(ConfigurationManager.AppSettings["EnabledSSL"].ToString());

            string body = content;
            MailMessage message = new MailMessage(new MailAddress(fromEmailAddress, fromEmailDisplayName), new MailAddress(toEmailAddress));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmailAddress, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = enabledSsl;
            client.Port = !string.IsNullOrEmpty(smtpHost) ? Convert.ToInt32(smtpHost) : 0;
            client.Send(message);
        }
        public static void SendEmail1(string toEmail, string fromEmail, string passEmail, string titleEmail, string contentEmail)
        {
            
            MailMessage mail = new MailMessage();
            mail.To.Add(toEmail);
            mail.From = new MailAddress(fromEmail);
            mail.Subject = titleEmail;
            mail.Body = contentEmail;// phần thân của mail ở trên
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = new System.Net.NetworkCredential(fromEmail, passEmail);// tài khoản Gmail của bạn
            smtp.EnableSsl = true;       
            smtp.Send(mail);
        }

        private static ICredentialsByHost NetworkCredential(string fromEmail, string passEmail)
        {
            throw new NotImplementedException();
        }

        public static string MailRegister(string emailCustomer, string customerName,string moblie, string codeConfirm)
        {
            StringBuilder body = new StringBuilder();
            body.Append("<p>Cảm ơn quý khách đã đăng ký tài khoản tại BookStore!</p>");
            body.Append("<table>");
            body.Append("<tr><td colspan='2'><h3>Thông tin khách hàng: </h3></td></tr>");
            body.Append("<tr><th  style='text-align:left;color:#e51a1a;'>Email đăng nhập:</th><td>" + emailCustomer + "</td></tr>");
            body.Append("<tr><th style='text-align:left;'>Họ và tên:</th><td>" + customerName + "</td></tr>");
            body.Append("<tr><th style='text-align:left;'>Điện thoại: </th><td>" + moblie + "</td></tr>");
            body.Append("<tr><th style='text-align:left;color:#e51a1a;'>Mã kích hoạt tài khoản:</th><td>" + codeConfirm + "</td></tr>");
            body.Append("</table>");
            string content = body.ToString();
            return content;
        }
        public static string MailOrder(DonHang ct,KhachHang kh,List<GioHang> giohang  )
        {
            StringBuilder body = new StringBuilder();           
            body.Append("<p>Thông tin chi tiết đơn hàng số: <b style='color:red;'>" + ct.MaDonHang + "</p>");
            body.Append("<table>");
            body.Append("<tr><td colspan='2'><h3>Thông tin người mua:</h3></td></tr>");
            body.Append("<tr><th style='text-align:left;'>Email:</th><td>" + ct.EmailKH + "</td></tr>");
            body.Append("<tr><th style='text-align:left;'>Họ và tên:</th><td>" + ct.TenKH + "</td></tr>");
            body.Append("<tr><th style='text-align:left;'>Số điện thoại:</th><td>" + ct.DienThoaiKH + "</td></tr>");
            body.Append("<tr><th style='text-align:left;'>Địa chỉ:</th><td>" + ct.DiaChi + "</td></tr>");
            body.Append("</table>");
            body.Append("<table>");
            body.Append("<tr><td colspan='2'><h3>Thông tin người nhận</h3></td></tr>");
            body.Append("<tr><th style='text-align:left;'>Email:</th><td>" + ct.EmailKH + "</td></tr>");
            body.Append("<tr><th style='text-align:left;'>Họ và tên:</th><td>" + ct.TenKH + "</td></tr>");
            body.Append("<tr><th style='text-align:left;'>Số điện thoại:</th><td>" + ct.DienThoaiKH + "</td></tr>");
            body.Append("<tr><th style='text-align:left;'>Địa chỉ:</th><td>" + ct.DiaChiNhanHang + "</td></tr>");
            body.Append("</table>"); 
            body.Append("<table>");
            body.Append("<tr><td colspan='2'><h3>Chi tiết đơn hàng</h3></td></tr>");
            body.Append("<tr><th>Tên sản phẩm</th><th>Số lượng</th><th>Đơn giá</th></tr>");
            double totalPrices = 0;
            foreach (var item in giohang)
            {
                body.Append("<tr><td>" + item.sTenSach + "</td><td>" + item.iSoLuong + "</td><td>" + String.Format("{0:0,0}", Convert.ToDecimal(item.dDonGia)) + " VNĐ</td></tr>");
                totalPrices += (item.dDonGia) * item.iSoLuong ; 
            }
            body.Append("<tr><td colspan='2'><b>Tổng tiền</b></td><td>" + String.Format("{0:0,0}", totalPrices) + " VNĐ</td></tr>");
            body.Append("</table>");
            string content = body.ToString();
            return content;
        }
        //public static string MailLienHeAdmin()
    }
}