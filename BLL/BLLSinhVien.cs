using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using _102130052_LeDinhTu_13T1.DTO;
using _102130052_LeDinhTu_13T1.DAL;

namespace _102130052_LeDinhTu_13T1.BLL
{
    class BLLSinhVien
    {
        DALDataContext _dbQuery;
        DataTable _table = new DataTable();
        BLLKhoa _bllKhoa;

        public BLLSinhVien()
        {
            _dbQuery = DALDataContext.GetInstance();
            _bllKhoa = new BLLKhoa();
            _table.Columns.Add("STT", typeof(int));
            _table.Columns.Add("MSSV", typeof(string));
            _table.Columns.Add("Họ và tên", typeof(string));
            _table.Columns.Add("Ngày sinh", typeof(DateTime));
            _table.Columns.Add("Giới tính", typeof(bool));
            _table.Columns.Add("Điểm TB tích lũy", typeof(double));
            _table.Columns.Add("Khoa", typeof(string));
        }

        public void ThemSinhVien(string MSSV, string Hoten, DateTime NgaySinh, string QueQuan, string HKTT, double DiemTB, string TenKhoa, bool GioiTinh)
        {
            SinhVien entry = new SinhVien
            {
                MSSV = MSSV,
                HoTen =Hoten,
                NgaySinh = NgaySinh,
                QueQuan = QueQuan,
                HoKhauThuongTru = HKTT,
                DiemTBTichLuy = DiemTB,
                GioiTinh = GioiTinh,
                MaKhoa = _bllKhoa.LayMaKhoa(TenKhoa)
            };

            if (TonTaiSinhVien(MSSV)) throw new Exception("Bị Trùng MSSV rồi bạn ơi");
            _dbQuery.InsertSinhVien(entry);
        }

        public void SuaSinhVien(string MSSV, string Hoten, DateTime NgaySinh, string QueQuan, string HKTT, double DiemTB, string TenKhoa, bool GioiTinh)
        {
            var lstSinhVien = _dbQuery.GetListSinhVien().Where<SinhVien>(sv => sv.MSSV.Equals(MSSV));
            if (lstSinhVien.Count() == 0)
                throw new Exception("Sinh viên này không tồn tại");

            var entry = lstSinhVien.First();
            entry.HoTen = Hoten;
            entry.NgaySinh = NgaySinh;
            entry.QueQuan = QueQuan;
            entry.HoKhauThuongTru = HKTT;
            entry.DiemTBTichLuy = DiemTB;
            entry.MaKhoa = _bllKhoa.LayMaKhoa(TenKhoa);
            entry.GioiTinh = GioiTinh;
            _dbQuery.UpdateSinhVien(entry);
        }

        public void XoaSinhVien(string MSSV)
        {
            _dbQuery.DeleteSinhVien(MSSV);
        }

        public void XoaNhieuSinhVien(List<String> MSSVs)
        {
            _dbQuery.DeleteSinhVien(MSSVs);
        }

        private DataTable FillDataTable(List<SinhVien> entrys)
        {
            _table.Rows.Clear();
            int stt = 1;

            foreach (SinhVien sv in entrys)
                _table.Rows.Add(stt++, sv.MSSV, sv.HoTen, new DateTime(sv.NgaySinh.Year, sv.NgaySinh.Month, sv.NgaySinh.Day), sv.GioiTinh, sv.DiemTBTichLuy, sv.Khoa.TenKhoa);

            return _table;
        }

        public bool TonTaiSinhVien(string MSSV)
        {
            return _dbQuery.GetListSinhVien().Where<SinhVien>(sv => sv.MSSV.Equals(MSSV)).Count() != 0;
        }

        public SinhVien TimKiemSinhVien(string MSSV)
        {
            return _dbQuery.GetListSinhVien().Single<SinhVien>(sv => sv.MSSV.Equals(MSSV));
        }

        public DataTable GetListSinhVien()
        {
            return FillDataTable(_dbQuery.GetListSinhVien());
        }

        public List<String> GetListQueQuan()
        {
            return _dbQuery.GetListQueQuan();
        }

        public DataTable TimKiemTheoTen(string hoten)
        {
            var lst = _dbQuery.GetListSinhVien().Where<SinhVien>(sv => sv.HoTen.ToLower().Contains(hoten.ToLower())).ToList<SinhVien>();
            return FillDataTable(lst);
        }

        public DataTable SapXepTheoMSSV()
        {
            var lst = _dbQuery.GetListSinhVien();
            lst.Sort(delegate(SinhVien sv1, SinhVien sv2)
            {
                return sv1.MSSV.CompareTo(sv2.MSSV);
            });

            return FillDataTable(lst);
        }

        public DataTable SapXepTheoHoTen()
        {
            var lst = _dbQuery.GetListSinhVien();
            lst.Sort(delegate (SinhVien sv1, SinhVien sv2)
            {
                return sv1.HoTen.CompareTo(sv2.HoTen);
            });

            return FillDataTable(lst);
        }

        public DataTable SapXepTheoTenKhoa()
        {
            var lst = _dbQuery.GetListSinhVien();
            lst.Sort(delegate (SinhVien sv1, SinhVien sv2)
            {
                return sv1.Khoa.TenKhoa.CompareTo(sv2.Khoa.TenKhoa);
            });

            return FillDataTable(lst);
        }

        public DataTable SapXepTheoDiemTB()
        {
            var lst = _dbQuery.GetListSinhVien();
            lst.Sort(delegate (SinhVien sv1, SinhVien sv2)
            {
                if (sv1.DiemTBTichLuy > sv2.DiemTBTichLuy) return 1;
                else if (sv1.DiemTBTichLuy == sv2.DiemTBTichLuy) return 0;
                return -1;
            });

            return FillDataTable(lst);
        }
    }
}
