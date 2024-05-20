﻿using Poi.Id.InfraModel.DataAccess;
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
    public interface IPhongBanService
    {
        Task<CudResponseDto> CreateAsync(PhongBanRequest request, TenantInfo info);
        Task<CudResponseDto> UpdateAsync(Guid id, PhongBanRequest request);
        Task<CudResponseDto> DeleteAsync(Guid id);
        Task<PhongBanBoPhan> GetByIdAsync(Guid id);
        Task<IEnumerable<PhongBanBoPhan>> GetAllAsync(TenantInfo info);
    }
}
