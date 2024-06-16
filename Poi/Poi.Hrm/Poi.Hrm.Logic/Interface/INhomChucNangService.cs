using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Hrm.Logic.Interface
{
    public interface INhomChucNangService
    {
        Task<List<HrmNhomChucNang>> GetNhomChucNang(TenantInfo tenantInfo);
        Task<PagingResponse<HrmNhomChucNang>> GetPagingNhomChucNang(PagingRequest request, TenantInfo info);

        Task<HrmNhomChucNang> GetNhomChucNangById(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> CreateNhomChucNang(TenantInfo tenantInfo, NhomChucNangRequest NhomChucNang);

        Task<CudResponseDto> DeleteNhomChucNang(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> UpdateNhomChucNang(Guid id, TenantInfo tenantInfo, NhomChucNangRequest NhomChucNang);
        Task<CudResponseDto> AssignPermission(AssignNhomChucNangToVaiTroRequest request, TenantInfo tenantInfo);
        Task<CudResponseDto> AssignChucNang(AssignChucNangToNhomChucNangRequest request, TenantInfo tenantInfo);

    }
}
