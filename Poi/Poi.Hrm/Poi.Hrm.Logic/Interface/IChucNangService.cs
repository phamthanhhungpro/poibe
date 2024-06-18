using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Hrm.Logic.Interface
{
    public interface IChucNangService
    {
        Task<List<HrmChucNang>> GetChucNang(TenantInfo tenantInfo);
        Task<PagingResponse<HrmChucNang>> GetPagingChucNang(PagingRequest request, TenantInfo info);

        Task<HrmChucNang> GetChucNangById(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> CreateChucNang(TenantInfo tenantInfo, ChucNangRequest ChucNang);

        Task<CudResponseDto> DeleteChucNang(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> UpdateChucNang(Guid id, TenantInfo tenantInfo, ChucNangRequest ChucNang);

    }
}
