using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.BaseModel;

namespace Poi.Id.Logic.Interfaces
{
    public interface IPermissionService
    {
        Task<PagingResponse<Permission>> GetPermission(PagingRequest request, TenantInfo info);
        Task<IList<Permission>> GetNoPaging();
        Task<CudResponseDto> CreatePermission(CreatePermissionRequest request);
        Task<CudResponseDto> UpdatePermission(Guid id, UpdatePermissionRequest request);
        Task<CudResponseDto> DeletePermission(Guid id);
        Task<CudResponseDto> AssignPermission(AssignPermissionRequest request);

    }
}
