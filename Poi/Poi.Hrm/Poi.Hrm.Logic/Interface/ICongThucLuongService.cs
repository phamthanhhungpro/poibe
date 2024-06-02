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
    public interface ICongThucLuongService
    {
        Task<List<HrmCongThucLuong>> GetCongThucLuong(TenantInfo tenantInfo);
        Task<HrmCongThucLuong> GetCongThucLuongById(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> CreateCongThucLuong(TenantInfo tenantInfo, CongThucLuongRequest congThucLuong);

        Task<CudResponseDto> DeleteCongThucLuong(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> UpdateCongThucLuong(Guid id, TenantInfo tenantInfo, CongThucLuongRequest congThucLuong);
    }
}
