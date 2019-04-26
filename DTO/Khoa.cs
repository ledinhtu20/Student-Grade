using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _102130052_LeDinhTu_13T1.DTO
{
    public class Khoa
    {
        [Key]
        [StringLength(20)]
        public string MaKhoa { get; set; }
        [StringLength(100)]
        public string TenKhoa { get; set; }

        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}
