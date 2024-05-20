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
    public interface IAppService
    {
        Task<PagingResponse<App>> GetApp(PagingRequest request, TenantInfo info);

        Task<App> GetAppById(Guid id, TenantInfo info);
        Task<IList<App>> GetAppNoPaging();
        Task<IList<App>> GetAppByUser(Guid userId);

        Task<CudResponseDto> CreateApp(AppRequest app);
        Task<CudResponseDto> UpdateApp(Guid id, AppRequest app);
        Task<CudResponseDto> DeleteApp(Guid id);
        Task<CudResponseDto> UpdateUserApp(Guid id, UpdateUserAppRequest app, TenantInfo info);

    }
}
