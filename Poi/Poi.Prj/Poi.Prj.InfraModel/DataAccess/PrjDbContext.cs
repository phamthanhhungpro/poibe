using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
