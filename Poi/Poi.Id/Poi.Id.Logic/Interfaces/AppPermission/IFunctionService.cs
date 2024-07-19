using Poi.Id.InfraModel.DataAccess.AppPermission;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests;
using Poi.Id.Logic.Requests.AppPermission;
using Poi.Shared.Model.BaseModel;

namespace Poi.Id.Logic.Interfaces.AppPermission
{
    public interface IFunctionService
    {
        Task<CudResponseDto> CreateAsync(FunctionRequest request, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
        Task<PagingResponse<PerFunction>> GetAllAsync(PagingRequest request, TenantInfo info);
        Task<PerFunction> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, FunctionRequest request, TenantInfo info);

        Task<CudResponseDto> AssignApiToFunctionAsync(AssignEndpointToFunctionRequest request, TenantInfo info);
        Task<CudResponseDto> AssignScopeToFunctionAsync(AssignScopeToFunctionRequest request, TenantInfo info);
        Task<List<FunctionWithGroupDto>> GetFunctionWithGroup(TenantInfo info);
    }
}
