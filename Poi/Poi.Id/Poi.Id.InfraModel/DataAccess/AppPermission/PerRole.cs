using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.AppPermission
{
    public class PerRole : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<PerRoleFunctionScope> PerRoleFunctionScope { get; set; }
        public Tenant Tenant { get; set; }
        public Guid TenantId { get; set; }
        public string AppCode { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
