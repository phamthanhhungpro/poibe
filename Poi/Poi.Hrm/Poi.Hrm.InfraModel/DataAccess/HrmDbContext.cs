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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
