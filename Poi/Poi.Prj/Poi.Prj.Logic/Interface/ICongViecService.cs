using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.Logic.Dtos;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Interface
{
    public interface ICongViecService
    {
        Task<IEnumerable<PrjCongViec>> GetNoPaging(TenantInfo info, Guid DuanId);
        Task<PrjCongViec> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(CongViecRequest request, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, CongViecRequest request, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
        Task<IEnumerable<CongViecGroupByNhomCongViecDto>> GetCongViecGrid(TenantInfo info, Guid DuanId);
        Task<IEnumerable<CongViecKanbanDto>> GetCongViecKanban(TenantInfo info, Guid DuanId);
        Task<CudResponseDto> UpdateKanbanStatus(TenantInfo info, UpdateCongViecKanbanStatusRequest request);
    }
}
