using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Hrm.Logic.Interface
{
    public interface IVaiTroService
    {
        Task<IEnumerable<HrmVaiTro>> GetNoPaging(TenantInfo info);
        Task<HrmVaiTro> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(HrmVaiTro vaiTro, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, HrmVaiTro vaiTro, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
    }
}
