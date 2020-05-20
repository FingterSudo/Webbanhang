using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_BanHang.Models
{
    public class GioHang
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();

        public int iMaSach { get; set; }
        public int iMaNXB { get; set; }
        public string sTenSach { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }     
        public string eEmail { get; set; }
        public double dThanhTien
        {
            get { return (dDonGia * iSoLuong); }
        }     
        // ham tao gio hang 
        public GioHang(int Masach)
        {
            iMaSach = Masach;
            Sach sach = db.Saches.Single(n => n.MaSach == iMaSach);
            sAnhBia = sach.AnhBia;
            sTenSach = sach.TenSach;
            dDonGia = double.Parse(sach.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}