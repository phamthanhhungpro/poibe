using Microsoft.EntityFrameworkCore;

namespace Poi.Id.InfraModel.DataAccess
{
    public class HrmDbContext : DbContext
    {
        public HrmDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<HoSoNhanSu> HoSoNhanSus { get; set; }
        public DbSet<KhuVucChuyenMon> KhuVucChuyenMons { get; set; }
        public DbSet<PhanLoaiNhanSu> PhanLoaiNhanSus { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
