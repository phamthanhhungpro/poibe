using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Interfaces
{
    public interface IPermissionService
    {
        Task<PagingResponse<Permission>> GetPermission(PagingRequest request, TenantInfo info);
        Task<CudResponseDto> CreatePermission(CreatePermissionRequest request);
        Task<CudResponseDto> UpdatePermission(Guid id, UpdatePermissionRequest request);
        Task<CudResponseDto> DeletePermission(Guid id);

    }
}
