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
    public interface IScopeService
    {
        Task<CudResponseDto> CreateScopeAsync(ScopeRequest request);
        Task<CudResponseDto> UpdateScopeAsync(Guid id, ScopeRequest request);
        Task<CudResponseDto> DeleteScopeAsync(Guid id);
        Task<PerScope> GetScopeAsync(Guid id);
        Task<List<PerScope>> GetScopesAsync(TenantInfo info);

    }
}
