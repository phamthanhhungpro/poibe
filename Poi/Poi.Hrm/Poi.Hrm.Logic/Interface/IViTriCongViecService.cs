using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Hrm.Logic.Interface
{
    public interface IViTriCongViecService
    {
        Task<IEnumerable<HrmViTriCongViec>> GetNoPaging(TenantInfo info);
        Task<HrmViTriCongViec> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(HrmViTriCongViec viTriCongViec, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, HrmViTriCongViec viTriCongViec, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
    }
}
