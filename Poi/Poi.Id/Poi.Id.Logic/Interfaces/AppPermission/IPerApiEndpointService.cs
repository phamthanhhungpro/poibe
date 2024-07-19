using Poi.Id.InfraModel.DataAccess.AppPermission;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests;
using Poi.Id.Logic.Requests.AppPermission;
using Poi.Shared.Model.BaseModel;

namespace Poi.Id.Logic.Interfaces.AppPermission
{
    public interface IPerApiEndpointService
    {
        Task<CudResponseDto> CreateAsync(ApiEndpointRequest request, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, ApiEndpointRequest request, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
        Task<PerEndpoint> GetByIdAsync(Guid id, TenantInfo info);
        Task<PagingResponse<PerEndpoint>> GetAllAsync(PagingRequest request, TenantInfo info);
        Task<IEnumerable<PerEndpoint>> GetNopaging(TenantInfo info);
    }
}
