using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Interface
{
    public interface IToNhomService
    {
        Task<IEnumerable<PrjToNhom>> GetNoPaging(TenantInfo info);
        Task<PrjToNhom> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(ToNhomRequest ToNhom, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, ToNhomRequest ToNhom, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
    }
}
