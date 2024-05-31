using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Hrm.Logic.Interface
{
    public interface IHoSoNhanSuService
    {
        Task<List<HrmHoSoNhanSu>> GetHoSo(TenantInfo tenantInfo);
        Task<HrmHoSoNhanSu> GetHoSoById(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> CreateHoSo(TenantInfo tenantInfo, CreateHoSoNhanSuRequest hoSoNhanSu);

        Task<CudResponseDto> DeleteHoSo(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> UpdateHoSo(Guid id, TenantInfo tenantInfo, CreateHoSoNhanSuRequest hoSoNhanSu);

    }
}
