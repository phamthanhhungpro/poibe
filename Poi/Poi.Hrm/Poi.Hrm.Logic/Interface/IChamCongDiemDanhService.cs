﻿using Poi.Hrm.Logic.Dtos;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Hrm.Logic.Interface
{
    public interface IChamCongDiemDanhService
    {
        Task<List<HrmChamCongDiemDanh>> GetChamCongDiemDanh(TenantInfo tenantInfo);
        Task<List<HrmChamCongDiemDanh>> GetChamCongDiemDanhByUserId(TenantInfo tenantInfo, Guid userId, DateTime start, DateTime end);

        Task<CudResponseDto> CreateChamCongDiemDanh(TenantInfo tenantInfo, ChamCongDiemDanhRequest request);

        Task<CudResponseDto> DeleteChamCongDiemDanh(TenantInfo tenantInfo, Guid id);

        Task<CudResponseDto> UpdateChamCongDiemDanh(Guid id, TenantInfo tenantInfo, ChamCongDiemDanhRequest request);
        Task<CudResponseDto> DiemDanhThuCong(TenantInfo tenantInfo, DiemDanhThuCongRequest request);
        Task<List<BangChamCongDto>> BangChamCong(TenantInfo tenantInfo, BangChamCongRequest request);

        Task<HrmChamCongDiemDanh> GetDetailChamCong(TenantInfo tenantInfo, Guid id);
    }
}
