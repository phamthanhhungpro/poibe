using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.AppPermission
{
    public class PerFunction : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public ICollection<PerScope> Scopes { get; set; }
        public ICollection<PerEndpoint> Endpoints { get; set; }
        public ICollection<PerRoleFunctionScope> PerRoleFunctionScope { get; set; }
        public PerGroupFunction GroupFunction { get; set; }
        public Guid GroupFunctionId { get; set; }
        public string AppCode { get; set; }
        public Guid? MainEndPointId { get; set; }
        public PerEndpoint MainEndPoint { get; set; }

    }
}
