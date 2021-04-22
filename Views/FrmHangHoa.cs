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
    public partial class FrmHangHoa : Form
    {
        XuLyHangHoa xlhh = new XuLyHangHoa();
        bool themmoi = true;
        public FrmHangHoa()
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
            btnOpen.Enabled = !value;

            txtMaHang.Enabled = !value;
            txtTenHang.Enabled = !value;
            cbMChatLieu.Enabled = !value;
            txtSoLuong.Enabled = !value;
            txtGiaNhap.Enabled = !value;
            txtAnh.Enabled = !value;
            txtGhiChu.Enabled = !value;
        }
        private void ResetValue()
        {
            txtMaHang.Text = "";
            txtTenHang.Text = "";
            cbMChatLieu.Text = "";
            txtSoLuong.Text = "";
            txtGiaNhap.Text = "";
            txtAnh.Text = "";
            txtGhiChu.Text = "";
        }

        private void FrmHangHoa_Load(object sender, EventArgs e)
        {
            HienThiDanhSachHangHoa();//Hien thi bang nhan vien
            setButton(true);
        }
        private void HienThiDanhSachHangHoa()
        {
            lsvHangHoa.Items.Clear();
            lsvHangHoa.FullRowSelect = true;
            lsvHangHoa.View = View.Details;
            IEnumerable<HangHoa> dsshh = xlhh.DanhSachHangHoa();
            if (dsshh.Count() > 0)
            {
                foreach (HangHoa hh in dsshh)
                {
                    ListViewItem lvi;
                    lvi = lsvHangHoa.Items.Add(hh.MaHang.ToString());
                    lvi.SubItems.Add(hh.TenHang.ToString());
                    lvi.SubItems.Add(hh.MaChatLieu.ToString());
                    lvi.SubItems.Add(hh.TonDau.ToString());
                    lvi.SubItems.Add(hh.DonGiaNhap.ToString());
                    lvi.SubItems.Add(hh.Hinh.ToString());
                    lvi.SubItems.Add(hh.GhiChu.ToString());
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            themmoi = true;
            ResetValue();
            setButton(false);
            txtMaHang.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lsvHangHoa.SelectedIndices.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa ?", "Xóa hàng hóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    xlhh.XoaHangHoa(lsvHangHoa.SelectedItems[0].SubItems[0].Text);
                    lsvHangHoa.Items.RemoveAt(lsvHangHoa.SelectedIndices[0]);
                    ResetValue();
                }
            }
            else
                MessageBox.Show("Cần chọn thứ cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lsvHangHoa.SelectedIndices.Count > 0)
            {
                themmoi = false;
                setButton(false);
                txtMaHang.Enabled = false;
                txtTenHang.Focus();
            }
            else
                MessageBox.Show("Cần chọn thứ cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMaHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Cần nhập mã hàng hóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHang.Focus();
                return;
            }
            if (themmoi)
            {
                if (xlhh.KiemTraTonTai(txtMaHang.Text.Trim()))
                {
                    MessageBox.Show("Mã đã có nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaHang.Focus();
                    return;
                }
                xlhh.LuuTruHangHoa(txtMaHang.Text.Trim(), txtTenHang.Text.Trim(),cbMChatLieu.Text.Trim(),int.Parse(txtSoLuong.Text.Trim()), int.Parse(txtGiaNhap.Text.Trim()),txtAnh.Text.Trim(),txtGhiChu.Text.Trim());
                MessageBox.Show("Thêm mới thành công", "Thêm hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                xlhh.CapNhatHangHoa(txtMaHang.Text.Trim(), txtTenHang.Text.Trim(), cbMChatLieu.Text.Trim(), int.Parse(txtSoLuong.Text.Trim()), int.Parse(txtGiaNhap.Text.Trim()), txtAnh.Text.Trim(), txtGhiChu.Text.Trim());
                MessageBox.Show("Cập nhật thành công", "Cập nhật hàng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            HienThiDanhSachHangHoa();
            ResetValue();
            setButton(true);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon.....", "Coming soon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnSystem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Coming Soon.....", "Coming Soon", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void picAnh_Click(object sender, EventArgs e)
        {

        }
    }
}
