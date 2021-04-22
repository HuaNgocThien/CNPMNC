using FrmQLBH.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmQLBH
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        private void chấtLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FrmChatLieu = new FrmChatLieu();
            FrmChatLieu.Show();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FrmNhanVien = new FrmNhanVien();
            FrmNhanVien.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hàngHóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form FrmHangHoa = new FrmHangHoa();
            FrmHangHoa.Show();
        }
    }
}
