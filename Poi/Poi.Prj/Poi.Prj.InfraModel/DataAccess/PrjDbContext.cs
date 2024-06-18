using Microsoft.EntityFrameworkCore;
using Poi.Id.InfraModel.DataAccess;

namespace Poi.Prj.InfraModel.DataAccess
{
    public class PrjDbContext : IdDbContext
    {
        public PrjDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
