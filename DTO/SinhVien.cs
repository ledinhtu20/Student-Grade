using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _102130052_LeDinhTu_13T1.DTO
{
    public class SinhVien
    {
        [Key]
        [StringLength(20)]
        public string MSSV { get; set; }
        [StringLength(100)]
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string HoKhauThuongTru { get; set; }
        public bool GioiTinh { get; set; }
        public double DiemTBTichLuy { get; set; }
        public string QueQuan { get; set; }
        [StringLength(20)]
        public string MaKhoa { get; set; }

        [ForeignKey("MaKhoa")]
        public virtual Khoa Khoa { get; set; }
    }
}
