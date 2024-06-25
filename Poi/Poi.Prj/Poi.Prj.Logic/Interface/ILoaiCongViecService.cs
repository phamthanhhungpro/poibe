using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Interface
{
    public interface ILoaiCongViecService
    {
        Task<IEnumerable<PrjLoaiCongViec>> GetNoPaging(TenantInfo info, Guid DuanId);
        Task<PrjLoaiCongViec> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(LoaiCongViecRequest request, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, LoaiCongViecRequest request, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
    }
}
