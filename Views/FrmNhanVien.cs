using FrmQLBH.Controllers;
using FrmQLBH.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmQLBH.Views
{
    public partial class FrmNhanVien : Form
    {
        XuLyNhanVien xlnv = new XuLyNhanVien();
        bool themmoi = true;
        public FrmNhanVien()
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

            txtMaNV.Enabled = !value;
            txtTenNV.Enabled = !value;
            cbGT.Enabled = !value;
            txtDiaChi.Enabled = !value;
            txtSDT.Enabled = !value;
            dtpNS.Enabled = !value;
        }
        private void ResetValue()
        {
            txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            cbGT.Checked = false;
            dtpNS.Value = new DateTime(2021, 4, 9);

        }
        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            HienThiDanhSachNhanVien();//Hien thi bang nhan vien
            setButton(true);
        }
        private void HienThiDanhSachNhanVien()
        {
            lsvNhanVien.Items.Clear();
            lsvNhanVien.FullRowSelect = true;
            lsvNhanVien.View = View.Details;
            IEnumerable<NhanVien> dssnv = xlnv.DanhSachNhanVien();
            if (dssnv.Count() > 0)
            {
                foreach (NhanVien nv in dssnv)
                {
                    ListViewItem lvi;
                    lvi = lsvNhanVien.Items.Add(nv.MaNhanVien.ToString());
                    lvi.SubItems.Add(nv.TenNhanVien.ToString());
                    lvi.SubItems.Add(nv.GioiTinh.ToString());
                    lvi.SubItems.Add(nv.DiaChi.ToString());
                    lvi.SubItems.Add(nv.DienThoai.ToString());
                    lvi.SubItems.Add(nv.NgaySinh.ToString());
                }
            }
        }
        private void lsvNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtMaNV.Text = lsvNhanVien.SelectedItems[0].SubItems[0].Text;
            txtTenNV.Text = lsvNhanVien.SelectedItems[0].SubItems[1].Text;
            cbGT.Checked = Boolean.Parse(lsvNhanVien.SelectedItems[0].SubItems[2].Text);
            txtDiaChi.Text = lsvNhanVien.SelectedItems[0].SubItems[3].Text;
            txtSDT.Text = lsvNhanVien.SelectedItems[0].SubItems[4].Text;
            dtpNS.Value = DateTime.Parse(lsvNhanVien.SelectedItems[0].SubItems[0].Text);
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnSkip.Enabled = true;
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            themmoi = true;
            ResetValue();
            setButton(false);
            txtMaNV.Focus();
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (lsvNhanVien.SelectedIndices.Count > 0)
            {
                DialogResult dr = MessageBox.Show("Bạn có muốn xóa ?", "Xóa nhân viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    xlnv.XoaNhanVien(lsvNhanVien.SelectedItems[0].SubItems[0].Text);
                    lsvNhanVien.Items.RemoveAt(lsvNhanVien.SelectedIndices[0]);
                    ResetValue();
                }
            }
            else
                MessageBox.Show("Cần chọn thứ cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (lsvNhanVien.SelectedIndices.Count > 0)
            {
                themmoi = false;
                setButton(false);
                txtMaNV.Enabled = false;
                txtTenNV.Focus();
            }
            else
                MessageBox.Show("Cần chọn thứ cần sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Cần nhập mã chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return;
            }
            if (themmoi)
            {
                if (xlnv.KiemTraTonTai(txtMaNV.Text.Trim()))
                {
                    MessageBox.Show("Mã đã có nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNV.Focus();
                    return;
                }
                xlnv.LuuTruNhanVien(txtMaNV.Text.Trim(), txtTenNV.Text.Trim(), cbGT.Checked, txtDiaChi.Text.Trim(), txtSDT.Text.Trim(), dtpNS.Value);
                MessageBox.Show("Thêm mới thành công", "Thêm chất liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                xlnv.CapNhatNhanVien(txtMaNV.Text.Trim(), txtTenNV.Text.Trim(), cbGT.Checked, txtDiaChi.Text.Trim(), txtSDT.Text.Trim(), dtpNS.Value);
                MessageBox.Show("Cập nhật thành công", "Cập nhật chất liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            HienThiDanhSachNhanVien();
            ResetValue();
            setButton(true);
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
