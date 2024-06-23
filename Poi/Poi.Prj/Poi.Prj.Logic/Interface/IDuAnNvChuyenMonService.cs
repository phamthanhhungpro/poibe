﻿using Poi.Id.InfraModel.DataAccess.Prj;
using Poi.Prj.Logic.Requests;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;

namespace Poi.Prj.Logic.Interface
{
    public interface IDuAnNvChuyenMonService
    {
        Task<IEnumerable<PrjDuAnNvChuyenMon>> GetNoPaging(bool isNvChuyenMon, TenantInfo info);
        Task<PrjDuAnNvChuyenMon> GetByIdAsync(Guid id, TenantInfo info);
        Task<CudResponseDto> AddAsync(DuAnNvChuyenMonRequest LinhVuc, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, DuAnNvChuyenMonRequest LinhVuc, TenantInfo info);
        Task<CudResponseDto> DeleteAsync(Guid id, TenantInfo info);
    }
}