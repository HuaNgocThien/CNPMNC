using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrmQLBH.Models;

namespace FrmQLBH.Controllers
{
    class XuLyChatLieu
    {
        private QLBanHangDataContext db;
        public XuLyChatLieu()
        {
            db = new QLBanHangDataContext();
        }

        public IEnumerable<ChatLieu> DanhSachChatLieu()
        {
            IList<ChatLieu> dscl = new List<ChatLieu>();
            var query = db.ChatLieus.ToList();
            foreach (var cl in query)
            {
                dscl.Add(new ChatLieu()
                {
                    MaChatLieu = cl.MaChatLieu,
                    TenChatLieu = cl.TenChatLieu
                });
            }
            return dscl;
        }
        public bool KiemTraTonTai (string p)
        {
            var dsscl = db.ChatLieus.Where(m => m.MaChatLieu == p).ToList();
            if (dsscl.Count > 0)
            {
                return true;
            }
            return false;
        }
        public void LuuTruChatLieu(string ma, string ten)
        {
            var chatLieuData = new ChatLieu()
            {
                MaChatLieu = ma,
                TenChatLieu = ten
            };
            db.ChatLieus.InsertOnSubmit(chatLieuData);
            db.SubmitChanges();
        }
        public void CapNhatChatLieu(string ma, string ten)
        {
            ChatLieu cl = db.ChatLieus.Where(m => m.MaChatLieu == ma).SingleOrDefault();
            cl.TenChatLieu = ten;
            db.SubmitChanges();
        }
        public void XoaChatLieu(string ma)
        {
            ChatLieu cl = db.ChatLieus.Where(m => m.MaChatLieu == ma).SingleOrDefault();
            db.ChatLieus.DeleteOnSubmit(cl);
            db.SubmitChanges();
        }

    }
}
