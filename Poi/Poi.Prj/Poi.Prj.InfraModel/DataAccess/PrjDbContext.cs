using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.AppPermission;
using Poi.Id.InfraModel.DataAccess.Prj;

namespace Poi.Prj.InfraModel.DataAccess
{
    public class PrjDbContext : IdDbContext
    {

        public PrjDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PrjLinhVuc> PrjLinhVuc { get; set; }
        public DbSet<PrjToNhom> PrjToNhom { get; set; }
        public DbSet<PrjDuAnNvChuyenMon> PrjDuAnNvChuyenMon { get; set; }
        public DbSet<PrjNhomCongViec> PrjNhomCongViec { get; set; }
        public DbSet<PrjLoaiCongViec> PrjLoaiCongViec { get; set; }
        public DbSet<PrjCongViec> PrjCongViec { get; set; }
        public DbSet<PrjTagCongViec> PrjTagCongViec { get; set; }
        public DbSet<PrjTagComment> PrjTagComment { get; set; }
        public DbSet<PrjComment> PrjComment { get; set; }
        public DbSet<PrjDuAnSetting> PrjDuAnSetting { get; set; }
        public DbSet<PrjKanban> PrjKanban { get; set; }
        public DbSet<PerEndpoint> PerEndpoint { get; set; }
        public DbSet<PerFunction> PerFunction { get; set; }
        public DbSet<PerGroupFunction> PerGroupFunction { get; set; }
        public DbSet<PerRole> PerRole { get; set; }
        public DbSet<PerScope> PerScope { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
