using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Interface
{
    public interface IDuAnSettingService
    {
        Task<IEnumerable<PrjDuAnSetting>> GetNoPaging(TenantInfo info, Guid DuanId);
        Task<PrjDuAnSetting> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(DuAnSettingRequest request, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, DuAnSettingRequest request, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
    }
}
