using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Interface
{
    public interface ITagCongViecService
    {
        Task<IEnumerable<PrjTagCongViec>> GetNoPaging(TenantInfo info, Guid DuanId);
        Task<PrjTagCongViec> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(TagCongViecRequest request, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, TagCongViecRequest request, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
    }
}
