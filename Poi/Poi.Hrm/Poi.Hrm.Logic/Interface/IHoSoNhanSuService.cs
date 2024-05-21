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
    public interface IHoSoNhanSuService
    {
        Task<List<HoSoNhanSu>> GetHoSo(TenantInfo tenantInfo);
        Task<HoSoNhanSu> GetHoSoById(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> CreateHoSo(TenantInfo tenantInfo, CreateHoSoNhanSuRequest hoSoNhanSu);

        Task DeleteHoSo(TenantInfo tenantInfo, Guid id);
    }
}
