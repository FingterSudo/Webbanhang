using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_BanHang.Models
{
    public class DatHang
    {
        QuanLyBanSachEntities db = null;
        public DatHang()
        {
            db = new QuanLyBanSachEntities();
        }
        public long Insert(DonHang dathang)
        {
            db.DonHangs.Add(dathang);
            db.SaveChanges();
            return dathang.MaDonHang;
        }
    }
}