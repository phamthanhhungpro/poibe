using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.BaseModel;

namespace Poi.Id.Logic.Interfaces
{
    public interface ICoQuanDonViService
    {
        Task<CudResponseDto> CreateAsync(CoQuanDonViRequest coQuanDonVi, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, CoQuanDonViRequest coQuanDonVi);
        Task<CudResponseDto> DeleteAsync(Guid id);
        Task<CoQuanDonVi> GetByIdAsync(Guid id);
        Task<IEnumerable<CoQuanDonVi>> GetAllAsync(TenantInfo info);
    }
}
