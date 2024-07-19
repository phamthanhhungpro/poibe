using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.AppPermission
{
    public class PerEndpoint : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public bool IsPublic { get; set; }
        public string AppCode { get; set; }
        public ICollection<PerFunction> Functions { get; set; }
    }
}
