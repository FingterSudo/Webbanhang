using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_BanHang.Models
{
    public class DonHangChiTiet
    {
        QuanLyBanSachEntities db = null;
        public DonHangChiTiet()
        {
            db = new QuanLyBanSachEntities();
        }
        public bool Insert (ChiTietDonHang Dhct)
        {
            try
            {
                db.ChiTietDonHangs.Add(Dhct);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}