using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.Logic.Dtos;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Interface
{
    public interface IDuAnNvChuyenMonService
    {
        Task<IEnumerable<PrjDuAnNvChuyenMon>> GetNoPaging(bool isNvChuyenMon, TenantInfo info, bool isGetAll);
        Task<PrjDuAnNvChuyenMon> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(DuAnNvChuyenMonRequest LinhVuc, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, DuAnNvChuyenMonRequest LinhVuc, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
        Task<PrjDuAnNvChuyenMon> GetViecCaNhan(TenantInfo info);

        Task<PagingResponse<DuanHoatDongDto>> GetHoatDongDuan(TenantInfo info, GetHoatDongDuAnRequest request);

        Task<TongQuanDuAnDto> GetTongQuanDuAn(TenantInfo info, Guid DuanId);

        Task<List<CongViecHoatDongDto>> GetTopHoatDongDuan(TenantInfo info, Guid DuanId);
    }
}
