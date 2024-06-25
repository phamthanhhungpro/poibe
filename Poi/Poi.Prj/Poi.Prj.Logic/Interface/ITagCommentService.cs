using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Interface
{
    public interface ITagCommentService
    {
        Task<IEnumerable<PrjTagComment>> GetNoPaging(TenantInfo info, Guid DuanId);
        Task<PrjTagComment> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(TagCommentRequest request, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, TagCommentRequest request, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
    }
}
