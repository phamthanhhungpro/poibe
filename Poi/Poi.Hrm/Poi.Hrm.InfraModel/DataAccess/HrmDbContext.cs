using Microsoft.EntityFrameworkCore;

namespace Poi.Id.InfraModel.DataAccess
{
    public class HrmDbContext : IdDbContext
    {
        public HrmDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<HrmHoSoNhanSu> HrmHoSoNhanSu { get; set; }
        public DbSet<HrmKhuVucChuyenMon> HrmKhuVucChuyenMon { get; set; }
        public DbSet<HrmPhanLoaiNhanSu> HrmPhanLoaiNhanSu { get; set; }

        public DbSet<HrmViTriCongViec> HrmViTriCongViec { get; set; }
        public DbSet<HrmVaiTro> HrmVaiTro { get; set; }

        public DbSet<HrmThamSoLuong> HrmThamSoLuong { get; set; }
        public DbSet<HrmCongThucLuong> HrmCongThucLuong { get; set; }
        public DbSet<HrmTrangThaiChamCong> HrmTrangThaiChamCong { get; set; }
        public DbSet<HrmCongKhaiBao> HrmCongKhaiBao { get; set; }
        public DbSet<HrmChamCongDiemDanh> HrmChamCongDiemDanh { get; set; }
        public DbSet<HrmDiemDanhHistory> HrmDiemDanhHistory { get; set; }
        public DbSet<HrmGiaiTrinhChamCong> HrmGiaiTrinhChamCong { get; set; }
        public DbSet<HrmNhomChucNang> HrmNhomChucNang { get; set; }
        public DbSet<HrmChucNang> HrmChucNang { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
