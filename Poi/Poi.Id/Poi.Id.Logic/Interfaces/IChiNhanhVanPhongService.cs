using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.BaseModel;

namespace Poi.Id.Logic.Interfaces
{
    public interface IChiNhanhVanPhongService
    {
        Task<CudResponseDto> CreateAsync(ChiNhanhVanPhongRequest request, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, ChiNhanhVanPhongRequest request);
        Task<CudResponseDto> DeleteAsync(Guid id);
        Task<ChiNhanhVanPhong> GetByIdAsync(Guid id);
        Task<IEnumerable<ChiNhanhVanPhong>> GetAllAsync(TenantInfo info);
    }
}
