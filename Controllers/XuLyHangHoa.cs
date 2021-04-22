using FrmQLBH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmQLBH.Controllers
{
    class XuLyHangHoa
    {
        private QLBanHangDataContext db;
        public XuLyHangHoa()
        {
            db = new QLBanHangDataContext();
        }

        public IEnumerable<HangHoa> DanhSachHangHoa()
        {
            IList<HangHoa> dshh = new List<HangHoa>();
            var query = db.HangHoas.ToList();
            foreach (var hh in query)
            {
                dshh.Add(new HangHoa()
                {
                    MaHang = hh.MaHang,
                    TenHang = hh.TenHang,
                    MaChatLieu = hh.MaChatLieu,
                    TonDau = hh.TonDau,
                    DonGiaNhap = hh.DonGiaNhap,
                    Hinh = hh.Hinh,
                    GhiChu = hh.GhiChu
                    
                });
            }
            return dshh;
        }
        public bool KiemTraTonTai (string p)
        {
            var dsshh = db.HangHoas.Where(m => m.MaHang == p).ToList();
            if (dsshh.Count > 0)
            {
                return true;
            }
            return false;
        }
        public void LuuTruHangHoa (string ma, string ten, string macl, int soluong, int donnhap, string hinh, string ghichu)
        {
            var hangHoaData = new HangHoa()
            {
                MaHang = ma,
                TenHang = ten,
                MaChatLieu = macl,
                TonDau = soluong,
                DonGiaNhap = donnhap,
                Hinh = hinh,
                GhiChu =ghichu
            };
            db.HangHoas.InsertOnSubmit(hangHoaData);
            db.SubmitChanges();
        }
        public void CapNhatHangHoa(string ma, string ten, string macl, int soluong, int donnhap, string hinh, string ghichu)
        {
            HangHoa hh = db.HangHoas.Where(m => m.MaHang == ma).SingleOrDefault();
            hh.TenHang = ten;
            hh.TonDau = soluong;
            hh.DonGiaNhap = donnhap;
            hh.Hinh = hinh;
            hh.GhiChu = ghichu;
            db.SubmitChanges();
        }
        public void XoaHangHoa(string ma)
        {
            HangHoa hh = db.HangHoas.Where(m => m.MaHang == ma).SingleOrDefault();
            db.HangHoas.DeleteOnSubmit(hh);
            db.SubmitChanges();
        }
    }
}
