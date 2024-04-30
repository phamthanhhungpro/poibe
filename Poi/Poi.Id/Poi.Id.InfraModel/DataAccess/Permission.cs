using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess
{
    public class Permission : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
