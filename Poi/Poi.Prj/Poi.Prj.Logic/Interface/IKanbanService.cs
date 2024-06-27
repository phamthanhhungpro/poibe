using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Interface
{
    public interface IKanbanService
    {
        Task<IEnumerable<PrjKanban>> GetNoPaging(TenantInfo info, Guid DuanId);
        Task<PrjKanban> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(KanbanRequest request, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, KanbanRequest request, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
    }
}
