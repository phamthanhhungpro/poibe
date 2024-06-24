using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Interface
{
    public interface INhomCongViecService
    {
        Task<IEnumerable<PrjNhomCongViec>> GetNoPaging(TenantInfo info, Guid DuanId);
        Task<PrjNhomCongViec> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(NhomCongViecRequest request, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, NhomCongViecRequest request, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
    }
}
