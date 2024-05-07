using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class App : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }

        public ICollection<Tenant> Tenants { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
