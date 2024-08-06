using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.Logic.Dtos;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Interface
{
    public interface ICongViecService
    {
        Task<IEnumerable<PrjCongViec>> GetNoPaging(TenantInfo info, Guid DuanId);
        Task<PrjCongViec> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(CongViecRequest request, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, UpdateCongViecRequest request, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
        Task<PagingResponse<CongViecGroupByNhomCongViecDto>> GetCongViecGrid(TenantInfo info, GetCongViecGridRequest request);
        Task<IEnumerable<CongViecKanbanDto>> GetCongViecKanban(TenantInfo info, Guid DuanId);
        Task<CudResponseDto> UpdateKanbanStatus(TenantInfo info, UpdateCongViecKanbanStatusRequest request);
        Task<CudResponseDto> GiaHanCongViec(TenantInfo info, GiaHanCongViecRequest request);
        Task<IEnumerable<CongViecGroupByNhomCongViecDto>> GetCongViecGridByTrangThai(TenantInfo info, Guid DuanId, string TrangThai);
        Task<CudResponseDto> ApproveGiaHanCongViec(TenantInfo info, ApproveGiaHanCongViec request);
        Task<CudResponseDto> ApproveTrangThaiCongViec(TenantInfo info, ApproveTrangThaiCongViec request);
        Task<CudResponseDto> ApproveDeXuatCongViec(TenantInfo info, ApproveDeXuatCongViec request);
        Task<CudResponseDto> RejectDeXuatCongViec(TenantInfo info, RejectCongViecRequest request);
        Task<CudResponseDto> RejectGiaHanCongViec(TenantInfo info, RejectCongViecRequest request);
        Task<CudResponseDto> RejectTrangThaiCongViec(TenantInfo info, RejectCongViecRequest request);

        Task<IEnumerable<CongViecHoatDongDto>> GetCongViecHoatDong(TenantInfo info, Guid CongViecId);

        Task<PagingResponse<CongViecGridDto>> GetQuanLyCongViec(TenantInfo info, GetQuanLyCongViecRequest request);

    }
}
