using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _102130052_LeDinhTu_13T1.BLL;
using _102130052_LeDinhTu_13T1.DTO;

namespace _102130052_LeDinhTu_13T1
{
    public delegate void SinhVienDelegate(string MSSV, string Hoten, DateTime NgaySinh, string QueQuan, string HKTT, double DiemTB, string TenKhoa, bool GioiTinh);

    public partial class Mainform : Form
    {
        BLLKhoa _bllKhoa;
        BLLSinhVien _bllSinhVien;

        public Mainform()
        {
            InitializeComponent();
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            _bllSinhVien = new BLLSinhVien();
            _bllKhoa = new BLLKhoa();
            cbbKhoa.DataSource = _bllKhoa.GetListTenKhoa();
            cbbQueQuan.DataSource = _bllSinhVien.GetListQueQuan();
            //dgvDanhSach.DataSource = _bllSinhVien.GetListSinhVien();
            rbMale.Checked = true;

            cbbSort.DataSource = new List<string>
            {
                "Mã sinh viên",
                "Họ và tên",
                "Tên khoa",
                "Điểm trung bình tích lũy"
            };
        }

        private void FormRefresh()
        {
            cbbKhoa.DataSource = _bllKhoa.GetListTenKhoa();
            cbbQueQuan.DataSource = _bllSinhVien.GetListQueQuan();
            dgvDanhSach.DataSource = _bllSinhVien.GetListSinhVien();
        }

        private void txbDiemTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            dgvDanhSach.DataSource = _bllSinhVien.GetListSinhVien();
        }

        private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= dgvDanhSach.Rows.Count) return;

            var row = dgvDanhSach.Rows[e.RowIndex];
            string MSSV = row.Cells[1].Value.ToString();

            SinhVien sv = _bllSinhVien.TimKiemSinhVien(MSSV);
            txbHoTen.Text = sv.HoTen;
            txbMssv.Text = sv.MSSV;
            txbHoKhauThuongTru.Text = sv.HoKhauThuongTru;
            txbDiemTB.Text = sv.DiemTBTichLuy.ToString();
            dtNgaySinh.Value = sv.NgaySinh;
            cbbKhoa.SelectedIndex = Helper.ComboBoxIndex(cbbKhoa.DataSource as List<String>, sv.Khoa.TenKhoa);
            cbbQueQuan.SelectedIndex = Helper.ComboBoxIndex(cbbQueQuan.DataSource as List<String>, sv.QueQuan);
            if ((bool)sv.GioiTinh) rbMale.Checked = true;
            else rbFemale.Checked = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new Addform(ThemSinhVien).ShowDialog();
        }

        public void ThemSinhVien(string MSSV, string Hoten, DateTime NgaySinh, string QueQuan, string HKTT, double DiemTB, string TenKhoa, bool GioiTinh)
        {
            _bllSinhVien.ThemSinhVien(MSSV, Hoten, NgaySinh, QueQuan, HKTT, DiemTB, TenKhoa, GioiTinh);
            FormRefresh();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string MSSV = txbMssv.Text;
                if (MSSV.Trim().Equals(""))
                    throw new Exception("Sao không chọn SV nào");
                string Hoten = txbHoTen.Text;
                if (Hoten.Trim().Equals(""))
                    throw new Exception("Không được để trống họ tên.");
                string QueQuan = cbbQueQuan.SelectedValue as String;
                if (QueQuan.Trim().Equals(""))
                    throw new Exception("Không được để trống Quê quán.");
                string TenKhoa = (cbbKhoa.DataSource as List<String>)[cbbKhoa.SelectedIndex];
                bool GioiTinh = rbMale.Checked;
                double DiemTB = Helper.ToDouble(txbDiemTB.Text);
                if (DiemTB < 0 || DiemTB > 10)
                    throw new Exception("Điểm TB tích lũy không đúng.");
                string HKTT = txbHoKhauThuongTru.Text;
                DateTime NgaySinh = dtNgaySinh.Value;

                _bllSinhVien.SuaSinhVien(MSSV, Hoten, NgaySinh, QueQuan, HKTT, DiemTB, TenKhoa, GioiTinh);
                FormRefresh();
            }
            catch(Exception exc)
            {
                ShowError(exc.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int count = dgvDanhSach.SelectedRows.Count;
                if (count == 0)
                {
                    if (dgvDanhSach.RowCount == 0) throw new Exception("Không có sinh viên nào được chọn.");
                    if (dgvDanhSach.CurrentRow.Index >= 0 && dgvDanhSach.CurrentRow.Index < dgvDanhSach.RowCount)
                        count = 1;
                }

                if (count == 1)
                {
                    if(Confirm("Xóa Sinh viên hả bạn", "Xác nhận muốn xóa SV bạn muốn xóa"))
                    {
                        string MSSV = dgvDanhSach.Rows[dgvDanhSach.CurrentRow.Index].Cells[1].Value.ToString();

                        _bllSinhVien.XoaSinhVien(MSSV);
                    }
                }
                else
                {
                    if(Confirm("Xóa Sinh viên hả bạn", "Xác nhận muốn xóa SV bạn muốn xóa"))
                    {
                        List<string> MSSVs = new List<string>();

                        foreach (DataGridViewRow row in dgvDanhSach.SelectedRows)
                            MSSVs.Add(row.Cells[1].Value.ToString());

                        _bllSinhVien.XoaNhieuSinhVien(MSSVs);
                    }
                }

                FormRefresh();
            }
            catch (Exception exc)
            {
                ShowError(exc.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string hoten = txbSearch.Text;

                if (hoten.Trim().Equals(""))
                    throw new Exception("Không được để trống nội dung tìm kiếm");
                dgvDanhSach.DataSource = _bllSinhVien.TimKiemTheoTen(hoten);
            }
            catch (Exception exc)
            {
                ShowError(exc.Message);
            }
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            try
            {
                int idx = cbbSort.SelectedIndex;

                switch(idx)
                {
                    case 0:
                        dgvDanhSach.DataSource = _bllSinhVien.SapXepTheoMSSV();
                        break;
                    case 2:
                        dgvDanhSach.DataSource = _bllSinhVien.SapXepTheoTenKhoa();
                        break;
                    case 1:
                        dgvDanhSach.DataSource = _bllSinhVien.SapXepTheoHoTen();
                        break;
                    case 3:
                        dgvDanhSach.DataSource = _bllSinhVien.SapXepTheoDiemTB();
                        break;
                }
            }
            catch (Exception exc)
            {
                ShowError(exc.Message);
            }
        }

        public static bool Confirm(string title, string content)
        {
            return (MessageBox.Show(content, title,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes);
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Lỗi sml!");
        }

        private void cbbQueQuan_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbMale_CheckedChanged(object sender, EventArgs e)
        {
            rbFemale.Checked = false;
        }

        /*private void rbFemale_CheckedChanged(object sender, EventArgs e)
        {
            rbMale.Checked = false;
        }

        private void rbMale_CheckedChanged_1(object sender, EventArgs e)
        {

        }*/
    }
}
