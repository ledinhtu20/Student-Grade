using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _102130052_LeDinhTu_13T1.DTO;

namespace _102130052_LeDinhTu_13T1.DAL
{
    public class DALDataContext
    {
        private static DALDataContext _instance = null;
        private static QLSVContext _dbContext = null;

        private DALDataContext()
        {
            _dbContext = new QLSVContext();
        }

        public static DALDataContext GetInstance()
        {
            if (_instance == null)
                _instance = new DALDataContext();

            return _instance;
        }

        public List<Khoa> GetListKhoa()
        {
            return _dbContext.Khoas.ToList<Khoa>();
        }

        public List<String> GetListQueQuan()
        {
            return _dbContext.SinhViens.Select<SinhVien, String>(sv => sv.QueQuan).Distinct<String>().ToList<String>();
        }

        public List<SinhVien> GetListSinhVien()
        {
            return _dbContext.SinhViens.ToList<SinhVien>();
        }

        public void UpdateSinhVien(SinhVien entry)
        {
            SinhVien s = _dbContext.SinhViens.Single<SinhVien>(sv => sv.MSSV == entry.MSSV);
            s = entry;
            _dbContext.SaveChanges();
        }

        public void InsertSinhVien(SinhVien entry)
        {
            _dbContext.SinhViens.Add(entry);
            _dbContext.SaveChanges();
        }

        public void DeleteSinhVien(string MSSV)
        {
            SinhVien s = _dbContext.SinhViens.Single<SinhVien>(sv => sv.MSSV == MSSV);
            _dbContext.SinhViens.Remove(s);
            _dbContext.SaveChanges();
        }

        public void DeleteSinhVien(List<String> MSSVs)
        {
            foreach(var mssv in MSSVs)
            {
                SinhVien s = _dbContext.SinhViens.Single<SinhVien>(sv => sv.MSSV == mssv);
                _dbContext.SinhViens.Remove(s);
            }

            _dbContext.SaveChanges();
        }

        public List<SinhVien> GetListSinhVienFromHoTen(string HoTen)
        {
            return _dbContext.SinhViens.Where<SinhVien>(sv => sv.HoTen.Contains(HoTen)).ToList<SinhVien>();
        }
    }
}
