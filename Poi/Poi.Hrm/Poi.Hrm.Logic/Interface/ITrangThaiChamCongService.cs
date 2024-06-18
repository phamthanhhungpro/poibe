using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Hrm.Logic.Interface
{
    public interface ITrangThaiChamCongService
    {
        Task<List<HrmTrangThaiChamCong>> GetTrangThaiChamCong(TenantInfo tenantInfo);
        Task<HrmTrangThaiChamCong> GetTrangThaiChamCongById(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> CreateTrangThaiChamCong(TenantInfo tenantInfo, TrangThaiChamCongRequest request);

        Task<CudResponseDto> DeleteTrangThaiChamCong(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> UpdateTrangThaiChamCong(Guid id, TenantInfo tenantInfo, TrangThaiChamCongRequest request);
    }
}
