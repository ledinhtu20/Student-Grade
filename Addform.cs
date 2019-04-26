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

namespace _102130052_LeDinhTu_13T1
{
    public partial class Addform : Form
    {
        SinhVienDelegate _themSinhVien;

        public Addform(SinhVienDelegate themSinhVien)
        {
            this._themSinhVien = themSinhVien;
            InitializeComponent();
        }

        private void Addform_Load(object sender, EventArgs e)
        {
            BLLKhoa bllKhoa = new BLLKhoa();
            BLLSinhVien bllSinhVien = new BLLSinhVien();
            cbbKhoa.DataSource = bllKhoa.GetListTenKhoa();
            cbbQueQuan.DataSource = bllSinhVien.GetListQueQuan();
            rbFemale.Checked = true;
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

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string MSSV = txbMssv.Text;
                if (MSSV.Trim().Equals(""))
                    throw new Exception("Không thể để trống MSSV.");
                string Hoten = txbHoTen.Text;
                if (Hoten.Trim().Equals(""))
                    throw new Exception("Không được để trống Họ tên SV.");
                string QueQuan = cbbQueQuan.SelectedValue as String;
                if (QueQuan.Trim().Equals(""))
                    throw new Exception("Không được để trống quê quán.");
                string TenKhoa = (cbbKhoa.DataSource as List<String>)[cbbKhoa.SelectedIndex];
                bool GioiTinh = rbMale.Checked;
                double DiemTB = Helper.ToDouble(txbDiemTB.Text);
                if (DiemTB < 0 || DiemTB > 10)
                    throw new Exception("Điểm trung bình tích lũy không đúng.");
                string HKTT = txbHoKhauThuongTru.Text;
                DateTime NgaySinh = dtNgaySinh.Value;

                _themSinhVien(MSSV, Hoten, NgaySinh, QueQuan, HKTT, DiemTB, TenKhoa, GioiTinh);
                this.Close();
                this.Dispose();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
