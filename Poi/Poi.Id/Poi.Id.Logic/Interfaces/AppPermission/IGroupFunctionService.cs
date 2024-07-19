using Poi.Id.InfraModel.DataAccess.AppPermission;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests.AppPermission;
using Poi.Shared.Model.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Interfaces.AppPermission
{
    public interface IGroupFunctionService
    {
        Task<CudResponseDto> CreateGroupFunctionAsync(FunctionGroupRequest request, TenantInfo info);
        Task<CudResponseDto> UpdateGroupFunctionAsync(Guid id, FunctionGroupRequest request);
        Task<CudResponseDto> DeleteGroupFunctionAsync(Guid id);
        Task<PerGroupFunction> GetGroupFunctionAsync(Guid id);
        Task<List<PerGroupFunction>> GetGroupFunctionsAsync(TenantInfo info);
    }
}
