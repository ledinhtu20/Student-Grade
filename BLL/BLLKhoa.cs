using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _102130052_LeDinhTu_13T1.DTO;
using _102130052_LeDinhTu_13T1.DAL;

namespace _102130052_LeDinhTu_13T1.BLL
{
    public class BLLKhoa
    {
        DALDataContext _dbQuery;

        public BLLKhoa()
        {
            _dbQuery = DALDataContext.GetInstance();
        }

        public List<String> GetListTenKhoa()
        {
            return _dbQuery.GetListKhoa().Select<Khoa, String>(kh => kh.TenKhoa).ToList<String>();
        }

        public string LayMaKhoa(string value)
        {
            return _dbQuery.GetListKhoa().Single<Khoa>(kh => kh.TenKhoa.Equals(value)).MaKhoa;
        }
    }
}
