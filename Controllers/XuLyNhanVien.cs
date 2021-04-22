using FrmQLBH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrmQLBH.Controllers
{
    class XuLyNhanVien
    {
        private QLBanHangDataContext db;
        public XuLyNhanVien()
        {
            db = new QLBanHangDataContext();
        }

        public IEnumerable<NhanVien> DanhSachNhanVien()
        {
            IList<NhanVien> dsnv = new List<NhanVien>();
            var query = db.NhanViens.ToList();
            foreach (var nv in query)
            {
                dsnv.Add(new NhanVien()
                {
                    MaNhanVien = nv.MaNhanVien,
                    TenNhanVien = nv.TenNhanVien,
                    GioiTinh = nv.GioiTinh,
                    DiaChi = nv.DiaChi,
                    DienThoai = nv.DienThoai,
                    NgaySinh = nv.NgaySinh
                });
            }
            return dsnv;
        }
        public bool KiemTraTonTai (string p)
        {
            var dssnv = db.NhanViens.Where(m => m.MaNhanVien == p).ToList();
            if (dssnv.Count > 0)
            {
                return true;
            }
            return false;
        }
        public void LuuTruNhanVien(string ma, string ten, Boolean gt, string dc, string sdt, DateTime ns)
        {
            var nhanVienData = new NhanVien()
            {
                MaNhanVien = ma,
                TenNhanVien = ten,
                GioiTinh = gt,
                DiaChi = dc,
                DienThoai = sdt,
                NgaySinh = ns
            };
            db.NhanViens.InsertOnSubmit(nhanVienData);
            db.SubmitChanges();
        }
        public void CapNhatNhanVien(string ma, string ten, Boolean gt, string dc, string sdt, DateTime ns)
        {
            NhanVien nv = db.NhanViens.Where(m => m.MaNhanVien == ma).SingleOrDefault();
            nv.TenNhanVien = ten;
            nv.GioiTinh = gt;
            nv.DiaChi = dc;
            nv.DienThoai = sdt;
            nv.NgaySinh = ns;
            db.SubmitChanges();
        }
        public void XoaNhanVien(string ma)
        {
            NhanVien nv = db.NhanViens.Where(m => m.MaNhanVien == ma).SingleOrDefault();
            db.NhanViens.DeleteOnSubmit(nv);
            db.SubmitChanges();
        }
    }
}
