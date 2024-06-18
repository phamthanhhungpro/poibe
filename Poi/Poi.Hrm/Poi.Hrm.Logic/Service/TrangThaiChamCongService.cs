using Microsoft.EntityFrameworkCore;
using Poi.Hrm.Logic.Interface;
using Poi.Hrm.Logic.Requests;
using Poi.Id.InfraModel.DataAccess;
using Poi.Id.InfraModel.DataAccess.Hrm;
using Poi.Shared.Model.BaseModel;
using Poi.Shared.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poi.Hrm.Logic.Service
{
    public class TrangThaiChamCongService : ITrangThaiChamCongService
    {
        private readonly HrmDbContext _hrmDbContext;
        public TrangThaiChamCongService(HrmDbContext hrmDbContext)
        {
            _hrmDbContext = hrmDbContext;
        }

        public async Task<CudResponseDto> CreateTrangThaiChamCong(TenantInfo tenantInfo, TrangThaiChamCongRequest request)
        {
            var trangThaiChamCong = new HrmTrangThaiChamCong
            {
                TenTrangThai = request.TenTrangThai,
                MaTrangThai = request.MaTrangThai,
                YeuCauGiaiTrinh = request.YeuCauGiaiTrinh,
                TrangThai = request.TrangThai,
                MauSac = request.MauSac,
                Tenant = _hrmDbContext.Tenants.Find(tenantInfo.TenantId)
            };

            await _hrmDbContext.HrmTrangThaiChamCong.AddAsync(trangThaiChamCong);
            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Id = trangThaiChamCong.Id
            };
        }

        public async Task<CudResponseDto> DeleteTrangThaiChamCong(TenantInfo tenantInfo, Guid id)
        {
            var trangThaiChamCong = await _hrmDbContext.HrmTrangThaiChamCong.FindAsync(id);
            if (trangThaiChamCong == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    IsSucceeded = false,
                };
            }

            trangThaiChamCong.IsDeleted = true;
            trangThaiChamCong.DeletedAt = DateTime.UtcNow;

            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Id = trangThaiChamCong.Id
            };
        }

        public async Task<List<HrmTrangThaiChamCong>> GetTrangThaiChamCong(TenantInfo tenantInfo)
        {
            return await _hrmDbContext.HrmTrangThaiChamCong
                .Include(x => x.Tenant)
                .Where(x => x.IsSystem || x.Tenant.Id == tenantInfo.TenantId)
                .ToListAsync();
        }

        public async Task<HrmTrangThaiChamCong> GetTrangThaiChamCongById(TenantInfo tenantInfo, Guid id)
        {
            return await _hrmDbContext.HrmTrangThaiChamCong
                .Include(x => x.Tenant)
                .FirstOrDefaultAsync(x => x.Id == id && x.Tenant.Id == tenantInfo.TenantId);
        }

        public async Task<CudResponseDto> UpdateTrangThaiChamCong(Guid id, TenantInfo tenantInfo, TrangThaiChamCongRequest request)
        {
            var trangThaiChamCong = await _hrmDbContext.HrmTrangThaiChamCong.FindAsync(id);
            if (trangThaiChamCong == null)
            {
                return new CudResponseDto
                {
                    Id = Guid.Empty,
                    IsSucceeded = false,
                };
            }

            trangThaiChamCong.TenTrangThai = request.TenTrangThai;
            trangThaiChamCong.MaTrangThai = request.MaTrangThai;
            trangThaiChamCong.YeuCauGiaiTrinh = request.YeuCauGiaiTrinh;
            trangThaiChamCong.TrangThai = request.TrangThai;
            trangThaiChamCong.MauSac = request.MauSac;

            await _hrmDbContext.SaveChangesAsync();

            return new CudResponseDto
            {
                IsSucceeded = true,
                Id = trangThaiChamCong.Id
            };
        }
    }
}
