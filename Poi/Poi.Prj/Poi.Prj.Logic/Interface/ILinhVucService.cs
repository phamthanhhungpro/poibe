using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Interface
{
    public interface ILinhVucService
    {
        Task<IEnumerable<PrjLinhVuc>> GetNoPaging(TenantInfo info);
        Task<PrjLinhVuc> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(LinhVucRequest LinhVuc, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, LinhVucRequest LinhVuc, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
    }
}
