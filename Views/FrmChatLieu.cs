using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FrmQLBH.Models;
using FrmQLBH.Controllers;

namespace FrmQLBH.Views
{
    public partial class FrmChatLieu : Form
    {
        XuLyChatLieu xlcl = new XuLyChatLieu();
        bool themmoi = true;

        public FrmChatLieu()
        {
            InitializeComponent();
        }
        public void setButton(bool value)
        {
            btnAdd.Enabled = value;
            btnDelete.Enabled = value;
            btnUpdate.Enabled = value;
            btnSave.Enabled = !value;
            btnSkip.Enabled = !value;
            btnClose.Enabled = value;

            txtMChatLieu.Enabled = !value;
            txtTChatLieu.Enabled = !value;
        }
        private void ResetValue()
        {
            txtMChatLieu.Text = "";
            txtTChatLieu.Text = "";
        }

        private void FrmChatLieu_Load(object sender, EventArgs e)
        {
            HienThiDanhSachChatLieu();//Hien thi bang chat lieu
            setButton(true);
        }
        private void HienThiDanhSachChatLieu()
        {
            lsvChatLieu.Items.Clear();
            lsvChatLieu.FullRowSelect = true;
            lsvChatLieu.View = View.Details;
            IEnumerable<ChatLieu> dsscl = xlcl.DanhSachChatLieu();
            if (dsscl.Count() > 0)
            {
                foreach (ChatLieu cl in dsscl)
                {
                    ListViewItem lvi;
                    lvi = lsvChatLieu.Items.Add(cl.MaChatLieu.ToString());
                    lvi.SubItems.Add(cl.TenChatLieu.ToString());
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            themmoi = true;
            ResetValue();
            setButton(false);
            txtMChatLieu.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsvChatLieu.SelectedIndices.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa ?","Xóa chất liệu",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(dr == DialogResult.Yes)
                {
                    xlcl.XoaChatLieu(lsvChatLieu.SelectedItems[0].SubItems[0].Text);
                    lsvChatLieu.Items.RemoveAt(lsvChatLieu.SelectedIndices[0]);
                    ResetValue();
                }
            }
            else
                MessageBox.Show("Cần chọn thứ cần xóa","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lsvChatLieu.SelectedIndices.Count > 0)
            {
                themmoi = false;
                setButton(false);
                txtMChatLieu.Enabled = false;
                txtTChatLieu.Focus();
            }
            else
                MessageBox.Show("Cần chọn thứ cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMChatLieu.Text.Trim().Length == 0)
            {
                MessageBox.Show("Cần nhập mã chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMChatLieu.Focus();
                return;
            }
            if (themmoi)
            {
                if(xlcl.KiemTraTonTai(txtMChatLieu.Text.Trim()))
                {
                    MessageBox.Show("Mã đã có nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMChatLieu.Focus();
                    return;
                }
                xlcl.LuuTruChatLieu(txtMChatLieu.Text.Trim(), txtTChatLieu.Text.Trim());
                MessageBox.Show("Thêm mới thành công", "Thêm chất liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                xlcl.CapNhatChatLieu(txtMChatLieu.Text.Trim(), txtTChatLieu.Text.Trim());
                MessageBox.Show("Cập nhật thành công", "Cập nhật chất liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            HienThiDanhSachChatLieu();
            ResetValue();
            setButton(true);
        }
        private void lsvChatLieu_SelectedIndexChanged (object sender, EventArgs e)
        {
            if (lsvChatLieu.SelectedIndices.Count>0)
            {
                txtMChatLieu.Text = lsvChatLieu.SelectedItems[0].SubItems[0].Text;
                txtTChatLieu.Text = lsvChatLieu.SelectedItems[0].SubItems[0].Text;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnSkip.Enabled = true;

            }
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            setButton(true);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
