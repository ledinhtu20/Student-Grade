namespace _102130052_LeDinhTu_13T1.DTO
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class QLSVContext : DbContext
    {
        // Your context has been configured to use a 'QLSVContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // '_102130002_PhanThanhAn_13T1.DTO.QLSVContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'QLSVContext' 
        // connection string in the application configuration file.
        public QLSVContext()
            : base("QLSVExample")
        {
            Database.SetInitializer<QLSVContext>(new TaoDatabase());
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Khoa> Khoas { get; set; }
        public virtual DbSet<SinhVien> SinhViens { get; set; }
        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }

    public class TaoDatabase : CreateDatabaseIfNotExists<QLSVContext>
    {
        protected override void Seed(QLSVContext context)
        {
            context.Khoas.Add(new Khoa
            {
                MaKhoa = "CNTT",
                TenKhoa = "Công Nghệ Thông Tin"
            });
            context.Khoas.Add(new Khoa
            {
                MaKhoa = "MT",
                TenKhoa = "Môi Trường"
            });
            context.Khoas.Add(new Khoa
            {
                MaKhoa = "CK",
                TenKhoa = "Cơ Khí"
            });
            context.Khoas.Add(new Khoa
            {
                MaKhoa = "CNTP",
                TenKhoa = "Công Nghệ Thực Phẩm"
            });

            context.SinhViens.Add(new SinhVien
            {
                MSSV = "SV01",
                HoTen = "Nguyen Van A",
                NgaySinh = new DateTime(1995, 12, 10),
                QueQuan = "Da Nang",
                HoKhauThuongTru = "Lien Chieu, Da Nang",
                GioiTinh = true,
                DiemTBTichLuy = 8.0d,
                MaKhoa = "CNTT"
            });

            context.SinhViens.Add(new SinhVien
            {
                MSSV = "SV02",
                HoTen = "Nguyen Van B",
                NgaySinh = new DateTime(1996, 10, 10),
                QueQuan = "Quảng Ngãi",
                HoKhauThuongTru = "Hoa Minh, Da Nang",
                GioiTinh = true,
                DiemTBTichLuy = 8.0d,
                MaKhoa = "CNTT"
            });

            context.SinhViens.Add(new SinhVien
            {
                MSSV = "SV03",
                HoTen = "Nguyen Van C",
                NgaySinh = new DateTime(1996, 10, 10),
                QueQuan = "Quảng Nam",
                HoKhauThuongTru = "Hoa Minh, Da Nang",
                GioiTinh = true,
                DiemTBTichLuy = 8.0d,
                MaKhoa = "CNTT"
            });

            context.SinhViens.Add(new SinhVien
            {
                MSSV = "SV04",
                HoTen = "Nguyen Van D",
                NgaySinh = new DateTime(1996, 10, 10),
                QueQuan = "Huế",
                HoKhauThuongTru = "Hoa Minh, Da Nang",
                GioiTinh = true,
                DiemTBTichLuy = 8.0d,
                MaKhoa = "CNTT"
            });

            context.SaveChanges();
        }
    }
}