using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.Logic.Dtos;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Interface
{
    public interface ICommentService
    {
        Task<CudResponseDto> CreatePrjCommentAsync(CongViecCommentRequest request, TenantInfo info);
        Task<CongViecCommentDto> GetCommentByIdCongViec(Guid congViecId);
    }
}
