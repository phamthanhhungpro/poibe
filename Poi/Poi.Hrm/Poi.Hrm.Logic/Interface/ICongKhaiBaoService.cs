using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Hrm.Logic.Interface
{
    public interface ICongKhaiBaoService
    {
        Task<List<HrmCongKhaiBao>> GetCongKhaiBao(TenantInfo tenantInfo);
        Task<HrmCongKhaiBao> GetCongKhaiBaoById(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> CreateCongKhaiBao(TenantInfo tenantInfo, CongKhaiBaoRequest CongKhaiBao);

        Task<CudResponseDto> DeleteCongKhaiBao(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> UpdateCongKhaiBao(Guid id, TenantInfo tenantInfo, CongKhaiBaoRequest CongKhaiBao);
    }
}
