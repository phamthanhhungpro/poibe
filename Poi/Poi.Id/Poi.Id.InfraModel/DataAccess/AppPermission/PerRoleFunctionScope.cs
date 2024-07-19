using Poi.Shared.Model.BaseModel;

namespace Poi.Id.InfraModel.DataAccess.AppPermission
{
    public class PerRoleFunctionScope : BaseEntity
    {
        public Guid PerRoleId { get; set; }
        public PerRole Role { get; set; }

        public Guid PerFunctionId { get; set; }
        public PerFunction Function { get; set; }

        public Guid? PerScopeId { get; set; }
        public PerScope Scope { get; set; }
    }
}
