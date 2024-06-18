using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Hrm.Logic.Interface
{
    public interface IGiaiTrinhChamCongService
    {
        Task<List<HrmGiaiTrinhChamCong>> GetGiaiTrinhChamCong(TenantInfo tenantInfo);
        Task<HrmGiaiTrinhChamCong> GetGiaiTrinhChamCongById(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> CreateGiaiTrinhChamCong(TenantInfo tenantInfo, GiaiTrinhChamCongRequest request);

        Task<CudResponseDto> DeleteGiaiTrinhChamCong(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> UpdateGiaiTrinhChamCong(Guid id, TenantInfo tenantInfo, GiaiTrinhChamCongRequest request);
        Task<List<HrmGiaiTrinhChamCong>> GetGiaiTrinhChamCongByUserId(TenantInfo tenantInfo, Guid userId);

        Task<CudResponseDto> XacNhanGiaiTrinh(TenantInfo tenantInfo, XacNhanGiaiTrinhRequest request);

    }
}
