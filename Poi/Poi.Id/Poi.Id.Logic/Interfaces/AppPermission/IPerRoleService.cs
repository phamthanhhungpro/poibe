using Poi.Id.InfraModel.DataAccess.AppPermission;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests.AppPermission;
using Poi.Shared.Model.BaseModel;

namespace Poi.Id.Logic.Interfaces.AppPermission
{
    public interface IPerRoleService
    {
        Task<CudResponseDto> CreateRoleAsync(PerRoleRequest request, TenantInfo info);
        Task<CudResponseDto> UpdateRoleAsync(Guid id, PerRoleRequest request);
        Task<CudResponseDto> DeleteRoleAsync(Guid id);
        Task<PerRole> GetRoleAsync(Guid id);
        Task<List<PerRole>> GetRolesAsync(TenantInfo info);

        Task<List<PerRoleFunctionScope>> GetFunctionScopeByRole(Guid roleId, TenantInfo info);
        Task<CudResponseDto> AssignFunctionToRole(AssignFunctionToRoleRequest request, TenantInfo info);

        Task<CudResponseDto> AssignRoleToUser(AssignRoleToUserRequest request, TenantInfo info);
    }
}
