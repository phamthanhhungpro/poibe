using Poi.Id.InfraModel.DataAccess;
using Poi.Id.Logic.Dtos;
using Poi.Id.Logic.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Id.Logic.Interfaces
{
    public interface IAppService
    {
        Task<PagingResponse<App>> GetApp(PagingRequest request);

        Task<App> GetAppById(Guid id);

        Task<CudResponseDto> CreateApp(AppRequest app);
        Task<CudResponseDto> UpdateApp(Guid id, AppRequest app);
        Task<CudResponseDto> DeleteApp(Guid id);
    }
}
