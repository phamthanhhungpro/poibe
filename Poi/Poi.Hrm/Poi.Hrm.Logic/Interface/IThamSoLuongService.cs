using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Hrm.Logic.Interface
{
    public interface IThamSoLuongService
    {
        Task<List<HrmThamSoLuong>> GetThamSoLuong(TenantInfo tenantInfo);
        Task<HrmThamSoLuong> GetThamSoLuongById(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> CreateThamSoLuong(TenantInfo tenantInfo, ThamSoLuongRequest request);

        Task<CudResponseDto> DeleteThamSoLuong(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> UpdateThamSoLuong(Guid id, TenantInfo tenantInfo, ThamSoLuongRequest request);

    }
}
