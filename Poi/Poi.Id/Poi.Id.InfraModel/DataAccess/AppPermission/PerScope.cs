using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.AppPermission
{
    public class PerScope : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public ICollection<PerFunction> Functions { get; set; }
        public string AppCode { get; set; }
    }
}
