using Microsoft.EntityFrameworkCore;

namespace Poi.Id.InfraModel.DataAccess
{
    public class HrmDbContext : IdDbContext
    {
        public HrmDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<HoSoNhanSu> HoSoNhanSu { get; set; }
        public DbSet<KhuVucChuyenMon> KhuVucChuyenMon { get; set; }
        public DbSet<PhanLoaiNhanSu> PhanLoaiNhanSu { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
