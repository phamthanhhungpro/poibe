using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.BaseModel;

namespace Poi.Id.Logic.Interfaces
{
    public interface ITenantService
    {
        Task<PagingResponse<Tenant>> GetTenant(PagingRequest request);

        Task<Tenant> GetTenantById(Guid id);

        Task<CudResponseDto> CreateTenant(CreateTenantRequest tenant);
        Task<CudResponseDto> UpdateTenant(Guid id, CreateTenantRequest tenant);
        Task<CudResponseDto> DeleteTenant(Guid id);

        Task<IList<Tenant>> GetTenantByInfo(TenantInfo request);

    }
}
